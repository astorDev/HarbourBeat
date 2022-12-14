[Route(Uris.About)]
public class AboutController
{
    public IHostEnvironment Environment { get; }

    public AboutController(IHostEnvironment environment)
    {
        Environment = environment;
    }

    [HttpGet]
    public About GetAbout()
    {        
        return new(
            Description : "HarbourBeat webapi",
            Version : this.GetType().Assembly.GetName().Version!.ToString(),
            Environment : this.Environment.EnvironmentName
        );
    }
}