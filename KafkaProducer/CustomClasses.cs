using System.Runtime.Serialization;
using System.Text;
using Confluent.Kafka;
using Newtonsoft.Json;

[DataContract]
[Serializable]
public class WebApiResponse<T, U>
{
    /// <summary>
    /// Request result.
    /// </summary>
    [DataMember]
    [JsonProperty(NullValueHandling = NullValueHandling.Include)]
    public virtual T Response { get; set; }

    public virtual U Response2 { get; set; }

    /// <summary>
    /// Errors list.
    /// </summary>
    [DataMember]
    public WebApiError Error { get; set; }

    /// <summary>
    /// Success.
    /// </summary>
    [DataMember]
    public bool Success { get; set; }
}

public class WebApiResponse<T>
{
    /// <summary>
    /// Request result.
    /// </summary>
    [DataMember]
    [JsonProperty(NullValueHandling = NullValueHandling.Include)]
    public virtual T Response { get; set; }

    /// <summary>
    /// Errors list.
    /// </summary>
    [DataMember]
    public WebApiError Error { get; set; }

    /// <summary>
    /// Success.
    /// </summary>
    [DataMember]
    public bool Success { get; set; }
}

[DataContract]
public class WebApiError
{
    /// <summary>
    /// Exception message.
    /// </summary>
    [DataMember]
    public string Message { get; set; }

    /// <summary>
    /// Exception type.
    /// </summary>
    [DataMember]
    public string Type { get; set; }

    /// <summary>
    /// Inner errors.
    /// </summary>
    [DataMember]
    public WebApiError Inner { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="WebApiError"/> class.
    /// </summary>
    /// <param name="exception">Exception.</param>
    public WebApiError(Exception exception)
    {
        if (exception != null)
        {
            this.Type = exception.GetType().Name;
            this.Message = exception.Message;
        }
    }
}

public class MyJsonSerializer : ISerializer<WebApiError>
{
    public byte[] Serialize(WebApiError data, SerializationContext context) =>
        Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(data)).ToArray();
}