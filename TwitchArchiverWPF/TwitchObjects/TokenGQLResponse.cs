using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchArchiverWPF.TwitchObjects
{
    public class StreamPlaybackAccessToken
    {
        public string? value { get; set; }
        public string? signature { get; set; }
        public string? __typename { get; set; }
    }

    public class VideoPlaybackAccessToken
    {
        public string? value { get; set; }
        public string? signature { get; set; }
        public string? __typename { get; set; }
    }

    public class Data
    {
        public StreamPlaybackAccessToken? streamPlaybackAccessToken { get; set; }
        public VideoPlaybackAccessToken? videoPlaybackAccessToken { get; set; }
    }

    public class TokenGQLResponse
    {
        public Data? data { get; set; }
        public Extensions? extensions { get; set; }
    }
}