using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchArchiverWPF.TwitchObjects
{
    public class Node
    {
        public string? id { get; set; }
        public string? title { get; set; }
        public Game? game { get; set; }
        public string? broadcastType { get; set; }
        public string? status { get; set; }
        public DateTime createdAt { get; set; }
    }

    public class Edge
    {
        public Node? node { get; set; }
    }

    public class Videos
    {
        public List<Edge>? edges { get; set; }
    }

    public class ArchiveVideo
    {
        public string? id { get; set; }
        public string? status { get; set; }
    }

    public class Stream
    {
        public ArchiveVideo? archiveVideo { get; set; }
        public string? id { get; set; }
        public string? title { get; set; }
        public string? type { get; set; }
        public int viewersCount { get; set; }
        public DateTime createdAt { get; set; }
        public Game? game { get; set; }
    }

    public class StreamData
    {
        public StreamUser? user { get; set; }
    }

    public class StreamUser
    {
        public string? id { get; set; }
        public string? login { get; set; }
        public string? displayName { get; set; }
        public Videos? videos { get; set; }
        public Stream? stream { get; set; }
        public BroadcastSettings? broadcastSettings { get; set; }
    }
    public class BroadcastSettings
    {
        public string? title { get; set; }
    }
    public class StreamGqlData
    {
        public StreamData? data { get; set; }
        public Extensions? extensions { get; set; }
    }
}
