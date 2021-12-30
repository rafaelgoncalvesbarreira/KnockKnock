using System.Text.Json;
using System.Text.Json.Serialization;
using RestApiTester.Models;

class ConfigParser
{
    public ConfigParser()
    {
    }

    public async Task<RequestConfig> ParseConfig(string path)
    {
        var emptyConfig = new RequestConfig("", RequestType.GET, new Dictionary<string, object>(), new Dictionary<string, string>(), new Header("",""));

        try
        {
            var content = await File.ReadAllTextAsync(path);
            var options = new JsonSerializerOptions{
                Converters= {
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                }
            };
            var config = JsonSerializer.Deserialize<RequestConfig>(content, options);

            if(config != null){
                return config;
            }
            return emptyConfig;
        }
        catch (Exception)
        {
            return emptyConfig;
        }
    }
}