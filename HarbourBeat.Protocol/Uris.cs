namespace HarbourBeat.Protocol;

public class Uris
{
    public const string About = "about";
    public const string Dashboards = "dashboards";

    public static string Dashboard(string id) => $"{Dashboards}/{id}";
}