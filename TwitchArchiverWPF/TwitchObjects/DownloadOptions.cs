namespace TwitchArchiverWPF
{
    public class DownloadOptions
    {
        public bool DownloadLiveStream { get; set; } = false;
        public bool DownloadLiveChat { get; set; } = false;
        public bool DownloadVodStream { get; set; } = false;
        public bool DownloadVodChat { get; set; } = false;
    }
}