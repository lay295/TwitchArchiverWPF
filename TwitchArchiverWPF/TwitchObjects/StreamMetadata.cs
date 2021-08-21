using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TwitchArchiverWPF.TwitchObjects
{
    public class StreamMetadata
    {
        public DateTime StreamTime { get; set; }
        public string? StreamerName { get; set; }
        public string? StreamerId { get; set; }
        public string? Thumbnail { get; set; }
        public string? Title { get; set; }
        public List<string> Games { get; set; } = new List<string>();
        public int Length { get; set; }
        public string? LiveStreamPath { get; set; }
        public string? LiveChatPath { get; set; }
        public string? VodStreamPath { get; set; }
        public string? VodChatPath { get; set; }
        [JsonIgnore]
        public string? MetadataPath { get; set; }
        [JsonIgnore]
        public BitmapImage? ThumbnailImage { 
            get
            {
                if (String.IsNullOrWhiteSpace(Thumbnail))
                    return null;
                else
                {
                    BitmapImage bi = new BitmapImage();

                    bi.BeginInit();
                    bi.StreamSource = new MemoryStream(System.Convert.FromBase64String(Thumbnail));
                    bi.EndInit();

                    return bi;
                }
            } 
        }
        [JsonIgnore]
        public TimeSpan LengthTimeSpan
        {
            get
            {
                return TimeSpan.FromSeconds(Length);
            }
        }
    }
}
