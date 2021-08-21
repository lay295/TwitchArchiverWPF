using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchArchiverWPF.TwitchObjects
{
    public class Game
    {
        public string? name { get; set; }
    }

    public class Video
    {
        public string? id { get; set; }
        public string? status { get; set; }
        public string? broadcastType { get; set; }
        public DateTime? createdAt { get; set; }
        public Game? game { get; set; }
        public string? title { get; set; }
        public DateTime? publishedAt { get; set; }
        public DateTime? recordedAt { get; set; }
    }

    public class VodData
    {
        public Video? video { get; set; }
    }

    public class VodGQLResponse
    {
        public VodData? data { get; set; }
        public Extensions? extensions { get; set; }
    }
}
