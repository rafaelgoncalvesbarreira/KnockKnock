using System.Text.Json;
using RestApiTester.Models;
using System.Net.Http;

public class HttpRequestBuilder
{
    private const string APPLICATION_JSON = "application/json";
    private const string FORM_DATA = "application/x-www-url-formencoded";

    private HttpContent? content;
    private string url;
    private HttpMethod httpMethod;


    public HttpRequestBuilder(string url)
    {
        this.url = url;
        this.content = null;
        this.httpMethod = HttpMethod.Get;
    }

    public HttpRequestBuilder WithData(Dictionary<string, object> data, string contentType)
    {
        if (contentType == APPLICATION_JSON)
        {
            var json = JsonSerializer.Serialize(data).ToString();
            content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        }
        // else if(header.StartsWith(FORM_DATA))
        else
        {
            content = new FormUrlEncodedContent(data
                .Select(x => new KeyValuePair<string, string>(x.Key, x.Value.ToString() ?? "")
            ));
        }

        return this;
    }

    public HttpRequestBuilder WithRequestType(RequestType method)
    {
        switch (method)
        {
            case RequestType.POST:
                this.httpMethod = HttpMethod.Post;
                break;
            case RequestType.PUT:
                this.httpMethod = HttpMethod.Put;
                break;
            case RequestType.DELETE:
                this.httpMethod = HttpMethod.Delete;
                break;
            case RequestType.GET:
            default:
                this.httpMethod = HttpMethod.Get;
                break;
        }

        return this;
    }
    public HttpRequestMessage Build()
    {
        var request = new HttpRequestMessage();
        request.Method = this.httpMethod;
        if (this.httpMethod == HttpMethod.Get)
        {
            if (this.content != null)
            {

                var queryString = this.content.ReadAsStringAsync().GetAwaiter().GetResult();
                var builder = new UriBuilder(this.url);
                builder.Query = queryString;
                request.RequestUri = new Uri(builder.ToString());
            }
        }
        else
        {

            request.RequestUri = new Uri(this.url);
            request.Content = this.content;
        }

        return request;
    }
}