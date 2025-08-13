using System.Diagnostics;

namespace SearchApplication.ActivityResources;

public class Instrumentation : IDisposable
{
    internal const string ActivitySourceName = "SearchActivity";
    internal const string ActivitySourceVersion = "1.0.0";

    public Instrumentation()
    {
        this.ActivitySource = new ActivitySource(ActivitySourceName, ActivitySourceVersion);
    }

    public ActivitySource ActivitySource { get; }

    public void Dispose()
    {
        this.ActivitySource.Dispose();
    }
}