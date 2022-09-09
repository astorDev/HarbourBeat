using System.Text.Json;

using Client = Kibana.Protocol.Client;

namespace HarbourBeat.WebApi.Controllers;

[Route(Uris.Dashboards)]
public class DashboardsController
{
    readonly Client kibana;
    const string Containers = "containers";

    public DashboardsController(Kibana.Protocol.Client kibana)
    {
        this.kibana = kibana;
    }
    
    [HttpGet]
    public async Task<Dashboards> Get()
    {
        var dashboards = await this.kibana.GetDashboards();
        var existing = dashboards.Items.Select(d => d.Id).ToArray();

        var pending = new[] { Containers }.Except(existing);
        return new(existing, pending.ToArray());
    }

    [HttpPut("{id}")]
    public async Task<Dashboard> Put(string id)
    {
        if (id != Containers) throw new UnknownDashboardException();
        var importJson = await File.ReadAllTextAsync("containers.json");
        object importObject = JsonSerializer.Deserialize<dynamic>(importJson)!;
        await KibanaException.Wrap(() => this.kibana.PostDashboard(importObject));
        return new(id);
    }
}