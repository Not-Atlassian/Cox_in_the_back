
using Microsoft.ML.Transforms.Onnx;
using Microsoft.ML;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using System.Collections.Generic;
using Tokenizers.DotNet;
using Microsoft.ML.Data;
using BERTTokenizers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApi.Services
{
    public class ModelInput
    {
        [VectorType(1, 128)]
        [ColumnName("input_ids")]
        public long[] InputIds { get; set; }

        [VectorType(1, 128)]
        [ColumnName("attention_mask")]
        public long[] AttentionMask { get; set; }

        [VectorType(1, 128)]
        [ColumnName("token_type_ids")]
        public long[] TokenTypeIds { get; set; }
    }

    public class ModelOutput
    {
        [VectorType(1, 6)]
        [ColumnName("output")]
        public float[] Output { get; set; }
    }

    public class StoryPointEstimator
    {
        private readonly MLContext _mlContext;
        private readonly OnnxScoringEstimator estimator;
        public StoryPointEstimator() {
            _mlContext = new MLContext();
            estimator = _mlContext.Transforms.ApplyOnnxModel("./AIModels/storypoint_estimator.onnx");
        }

        public async Task<int> EstimateStoryPoints(string title, string description)
        {
            var tokenizer = new BertUncasedBaseTokenizer();

            var encoded = tokenizer.Encode(128, title + " " + description);

            var bertInput = new ModelInput()
            {
                InputIds = encoded.Select(t => t.InputIds).ToArray(),
                AttentionMask = encoded.Select(t => t.AttentionMask).ToArray(),
                TokenTypeIds = encoded.Select(t => t.TokenTypeIds).ToArray()
            };
            var data = _mlContext.Data.LoadFromEnumerable(new List<ModelInput>());
            var transformedData = estimator.Fit(data);
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(transformedData);
            var prediction = predictionEngine.Predict(bertInput);
            var index = prediction.Output.ToList().IndexOf(prediction.Output.Max());
            return index switch
            {
                0 => 1,
                1 => 2,
                2 => 3,
                3 => 5,
                4 => 8,
                5 => 13,
                _ => 1
            };
        }
    }

}
