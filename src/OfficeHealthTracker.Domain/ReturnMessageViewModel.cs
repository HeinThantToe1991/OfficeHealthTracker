using System.Text.Json.Serialization;

namespace OfficeHealthTracker.Domain
{
    public class ReturnMessageViewModel<T>
    {

        [JsonPropertyName("success")]
        public virtual bool Success { get; set; }

        [JsonPropertyName("messageStatus")]
        public  virtual MessageStatus MessageStatus { get; set; }
        [JsonPropertyName("message")]
        public virtual string? Message { get; set; }
        [JsonPropertyName("generateNo")]
        public virtual string? GenerateNo {get;set;}
        [JsonPropertyName("returnValue")]
        public virtual string? ReturnValue { get; set; }
        [JsonPropertyName("data")]
        public virtual T Data { get; set; }
    }

    public enum MessageStatus
    {
        Info = 0,
        Error = 1,
        Success = 2,
        Warning = 3
    }
}
