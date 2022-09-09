using System.ComponentModel.Design;
using System.Net.Http.Json;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Nist.Responses;

namespace Kibana.Protocol;

public class Client
{
    public HttpClient Http { get; }

    public Client(HttpClient http)
    {
        this.Http = http;
    }

    public Task<SavedObjectCollection> GetDashboards() =>
        this.Http.GetAsync(Uris.Dashboards).Read<SavedObjectCollection>();

    public Task<SavedObjectCollection> GetIndexes() =>
        this.Http.GetAsync(Uris.Indexes).Read<SavedObjectCollection>();

    public Task<DataStreamCollection> GetDataStreams() =>
        this.Http.GetAsync(Uris.DataStreams).Read<DataStreamCollection>();

    public Task<dynamic?> PostDashboard(object request) =>
        this.Http.PostAsJsonAsync(Uris.DashboardImport, request).ReadNullable<dynamic>();

    public Task<dynamic?> DeleteDashboard(string id) =>
        this.Http.DeleteAsync(Uris.SavedDashboard(id)).ReadNullable<dynamic>();

    public Task<dynamic> PostIndex(IndexCandidate candidate) =>
        this.Http.PostAsJsonAsync(Uris.IndexPattern, candidate).Read<dynamic>();

    public Task<dynamic?> DeleteIndex(string id) =>
        this.Http.DeleteAsync(Uris.SavedIndex(id)).ReadNullable<dynamic>();
}

public static class Extensions
{
    public static IHttpClientBuilder AddKibanaClient(this IServiceCollection services, IConfiguration configuration,
        string configurationPath = "KibanaUrl")
    {
        return services.AddHttpClient<Client>(cl =>
        {
            cl.BaseAddress = new(configuration[configurationPath]);
            cl.DefaultRequestHeaders.Add("kbn-xsrf", "true");
        });
    }
}