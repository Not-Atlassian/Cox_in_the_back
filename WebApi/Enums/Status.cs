using System.ComponentModel;

namespace WebApi.Enums
{
    public enum Status
    {
        [Description("open")]
        Open,
        [Description("in progress")]
        InProgress,
        [Description("completed")]
        Completed
    }
}