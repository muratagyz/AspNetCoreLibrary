using System.Diagnostics;

namespace Hangfire.Web.BackgroundJobs;

public class ContinuationsJobs
{
    public static void WriteWaterMarkStatus(string id, string fileName)
    {
        Hangfire.BackgroundJob.ContinueJobWith(id,
            () => Debug.WriteLine($"{fileName} : resime watermark eklenmiştir."));
    }
}