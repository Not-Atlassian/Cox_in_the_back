
using BERTTokenizers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.Onnx;
using System.Collections.Generic;
using Tokenizers.DotNet;
using WebApi.Models;

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
        private readonly String ModelPath = "./AIModels/storypoint_estimator.onnx";
        private readonly String GithubRepo = "Not-Atlassian/StoryPointEstimator";
        private readonly String ModelFilename = "storypoint_estimator.onnx";

        public StoryPointEstimator()
        {
            _mlContext = new MLContext();
            EnsureModelExists().Wait();
            estimator = _mlContext.Transforms.ApplyOnnxModel("./AIModels/storypoint_estimator.onnx");
        }


        private async Task EnsureModelExists()
        {
            if (!File.Exists(ModelPath))
            {
                Console.WriteLine("Model file not found locally. Downloading...");
                await DownloadModelFromGithub();
            }

        }

        private async Task DownloadModelFromGithub()
        {
            using var httpClient = new HttpClient();
            var apiUrl = $"https://api.github.com/repos/{GithubRepo}/releases/latest";

            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; StoryPointEstimator)");

            var response = await httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            var releaseData = await response.Content.ReadAsStringAsync();

            var releaseJson = System.Text.Json.JsonDocument.Parse(releaseData);
            var assets = releaseJson.RootElement.GetProperty("assets");

            string? downloadUrl = null;
            foreach (var asset in assets.EnumerateArray())
            {
                if (asset.GetProperty("name").GetString() == ModelFilename)
                {
                    downloadUrl = asset.GetProperty("browser_download_url").GetString();
                    break;
                }
            }

            if (downloadUrl == null)
            {
                throw new FileNotFoundException("Model file not found in the latest GitHub release.");
            }

            Console.WriteLine($"Downloading model from {downloadUrl}...");

            var modelData = await httpClient.GetByteArrayAsync(downloadUrl);

            Directory.CreateDirectory(Path.GetDirectoryName(ModelPath)!);

            await File.WriteAllBytesAsync(ModelPath, modelData);

            Console.WriteLine($"Model downloaded and saved to {ModelPath}");
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
                6 => int.MaxValue,
                _ => 1
            };
        }
    }

}