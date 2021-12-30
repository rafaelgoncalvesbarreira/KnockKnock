using RestApiTester.Models;
using System.Net;
using System.Net.Http;
using System.Text.Json;

public class ApiTester
{
    private readonly RequestConfig config;

    public ApiTester(RequestConfig config)
    {
        this.config = config;
    }

    public async Task Execute()
    {
        var client = new HttpClient
        {
            DefaultRequestVersion = HttpVersion.Version30,
            DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrHigher
        };
        // var request = await CreteRequest();
        var request = new HttpRequestBuilder(this.config.Url)
            .WithRequestType(this.config.Type)
            .WithQuery(this.config.Query)
            .WithData(this.config.Data, this.config.Headers.ContentType)
            .Build();

        try
        {

            var response = await client.SendAsync(request);
            Console.WriteLine($"HTTP Code: {response.StatusCode}");

            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private HttpMethod FromRequestType()
    {
        switch (this.config.Type)
        {
            case RequestType.POST:
                return HttpMethod.Post;
            case RequestType.PUT:
                return HttpMethod.Put;
            case RequestType.DELETE:
                return HttpMethod.Delete;
            case RequestType.GET:
            default:
                return HttpMethod.Get;
        }
    }
}