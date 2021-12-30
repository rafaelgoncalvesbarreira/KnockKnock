using RestApiTester.Models;
using System.Net.Http;
using System.Text.Json;

public class ApiTester
{
    private readonly RequestConfig config;

    public ApiTester(RequestConfig config)
    {
        this.config = config;
    }

    public async Task Execute(){
        var client = new HttpClient();
        // var request = await CreteRequest();
        var request = new HttpRequestBuilder(this.config.Url)
            .WithRequestType(this.config.Type)
            .WithData(this.config.Data, this.config.Headers.ContentType)
            .Build();

        var response = await client.SendAsync(request);
        Console.WriteLine($"HTTP Code: {response.StatusCode}");

        var responseString = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseString);
    }

    private HttpMethod FromRequestType(){
        switch (this.config.Type){
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