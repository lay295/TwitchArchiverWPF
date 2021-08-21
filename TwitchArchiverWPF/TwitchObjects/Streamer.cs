using TwitchArchiverWPF.Settings;

namespace TwitchArchiverWPF
{
    public class Streamer
    {
        public string Name { get; set; } = "";
        public string Id { get; set; } = "";
        public string AvatarUrl { get; set; } = "";
        public int StreamCount { get; set; } = 0;
        public DownloadOptions DownloadOptions { get; set; } = new DownloadOptions();
        public bool OverrideRecordingSettings { get; set; } = false;
        public RecordingSettings RecordingSettings { get; set;} = new RecordingSettings();
        public bool OverrideRenderSettings { get; set; } = false;
        public RenderSettings RenderSettings { get; set; } = new RenderSettings();
    }
}
