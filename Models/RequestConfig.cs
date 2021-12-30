namespace RestApiTester.Models
{   
    public record RequestConfig(string Url, RequestType Type, Dictionary<string, object> Data, Header Headers);
}