using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchArchiverWPF
{
    public enum RenderPrefrence
    {
        Live,
        VOD,
        Both
    }
    public class RenderSettings
    {
        public bool RenderChat { get; set; } = false;
        public RenderPrefrence RenderPrefrence { get; set; } = RenderPrefrence.VOD;
        public string Font { get; set; } = "Arial";
        public int FontSize { get; set; } = 24;
        public string FontColor { get; set; } = "#FFFFFF";
        public string BackgroundColor { get; set; } = "#111111";
        public bool Outline { get; set; } = false;
        public bool Timestamp { get; set; } = false;
        public bool FFZEmotes { get; set; } = true;
        public bool BTTVEmotes { get; set; } = true;
        public bool STVEmotes { get; set; } = true;
        public int Height { get; set; } = 1200;
        public int Width { get; set; } = 600;
        public double UpdateTime { get; set; } = 0.5;
        public int Framerate { get; set; } = 30;
        public string InputArgs { get; set; } = "-framerate {fps} -f rawvideo -analyzeduration {max_int} -probesize {max_int} -pix_fmt bgra -video_size {width}x{height} -i -";
        public string OutputArgs { get; set; } = "-c:v libx264 -preset veryfast -crf 18 -pix_fmt yuv420p \"{save_path}\"";
        public string VideoContainer { get; set; } = "MP4";
        public string Codec { get; set; } = "H264";
        public bool SubMessages { get; set; } = true;
        public bool GenerateMask { get; set; } = false;
    }
}
