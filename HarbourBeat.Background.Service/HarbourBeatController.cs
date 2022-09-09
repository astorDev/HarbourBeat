using HarbourBeat.Protocol;

public class HarbourBeatController
{
    readonly Client client;

    public HarbourBeatController(Client client)
    {
        this.client = client;
    }
    
    [RunsEvery("00:00:30")]
    public async Task<string[]> EnsureAllDashboardsAreCreated()
    {
        var dashboards = await client.GetDashboards();
        foreach (var pending in dashboards.Pending) await this.client.PutDashboard(pending);

        return dashboards.Pending;
    }
}