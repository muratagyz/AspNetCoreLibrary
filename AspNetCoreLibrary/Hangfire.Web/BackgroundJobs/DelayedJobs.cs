using System.Drawing;

namespace Hangfire.Web.BackgroundJobs;

public class DelayedJobs
{
    public static string AddWaterMarkJob(string filename, string watermarkText)
    {
        return Hangfire.BackgroundJob.Schedule(() => ApplyWatermark(filename, watermarkText), TimeSpan.FromSeconds(30));
    }

    public static void ApplyWatermark(string filename, string watermarkText)
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pictures", filename);
        using (var bitmap = Bitmap.FromFile(path))
        {
            using (Bitmap temp = new(bitmap.Width, bitmap.Height))
            {
                using (Graphics grp = Graphics.FromImage(temp))
                {
                    grp.DrawImage(bitmap, 0, 0);
                    var font = new Font(FontFamily.GenericSansSerif, 25, FontStyle.Bold);
                    var color = Color.FromArgb(255, 0, 0);
                    var bursh = new SolidBrush(color);
                    var point = new Point(20, bitmap.Height - 50);
                    grp.DrawString(watermarkText, font, bursh, point);
                    temp.Save(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pictures/watermarks",
                    filename));
                }
            }
        }
    }
}