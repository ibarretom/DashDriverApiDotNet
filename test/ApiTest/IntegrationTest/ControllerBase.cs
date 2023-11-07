using Api;
using Infra.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Shared;
using System.Globalization;
using System.Text;

namespace ApiTest.IntegrationTest;

public class ControllerBase : IClassFixture<WebFactory<Program>>, IDisposable
{
    private readonly WebFactory<Program> _factory;
    private readonly HttpClient _client;

    public ControllerBase(WebFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
        ErrorMessagesResource.Culture = CultureInfo.CurrentCulture;
    }

    public void Dispose()
    {
        using (var scope = _factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<DashDriverContext>();

            foreach (var entity in context.Model.GetEntityTypes())
            {
                var tableName = entity.GetTableName();
                var query = $"DELETE FROM {tableName}";
                context.Database.ExecuteSqlRaw(query);
            }
        }
    }

    protected async Task<HttpResponseMessage> PostRequest(string metodo, object body)
    {
        var jsonString = JsonConvert.SerializeObject(body);
        return await _client.PostAsync(metodo, new StringContent(jsonString, Encoding.UTF8, "application/json"));
    }
}
