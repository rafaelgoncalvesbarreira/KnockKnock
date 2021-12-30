namespace KnockKnock.Models
{   
    public record RequestConfig(string Url, RequestType Type, Dictionary<string, object> Data, Dictionary<string, string> Query, Header? Headers);
}