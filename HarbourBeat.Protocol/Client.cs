using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HarbourBeat.Protocol;

public class Client
{
    public HttpClient HttpClient { get; }

    public Client(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }

    public Task<About> GetAbout() => this.HttpClient.GetAsync(Uris.About).Read<About>();

    public Task<Dashboards> GetDashboards() => this.HttpClient.GetAsync(Uris.Dashboards).Read<Dashboards>();

    public Task<Dashboard> PutDashboard(string id) => this.HttpClient.PutAsync(Uris.Dashboard(id), null).Read<Dashboard>();
}

public static class ServiceRegistrationExtensions
{
    public static void AddHarbourBeatClient(this IServiceCollection services, string urlPath = "HarbourBeatUrl")
    {
        services.AddHttpClient<Client>((sp, cl) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            cl.BaseAddress = new Uri(configuration[urlPath]);
        });
    } 
}