namespace HarbourBeat.Protocol;

public record About(string Description, string Version, string Environment);

public record Dashboards(string[] Existing, string[] Pending);

public record Dashboard(string Id);