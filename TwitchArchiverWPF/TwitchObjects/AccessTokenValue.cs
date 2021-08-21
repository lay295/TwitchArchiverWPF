using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchArchiverWPF.TwitchObjects
{
    public class Authorization
    {
        public bool? forbidden { get; set; }
        public string? reason { get; set; }
    }

    public class Chansub
    {
        public List<object>? restricted_bitrates { get; set; }
        public int? view_until { get; set; }
    }

    public class Private
    {
        public bool? allowed_to_view { get; set; }
    }

    public class AccessTokenValue
    {
        public bool? adblock { get; set; }
        public Authorization? authorization { get; set; }
        public bool? blackout_enabled { get; set; }
        public string? channel { get; set; }
        public int? channel_id { get; set; }
        public Chansub? chansub { get; set; }
        public bool? ci_gb { get; set; }
        public string? geoblock_reason { get; set; }
        public string? device_id { get; set; }
        public long? expires { get; set; }
        public bool? extended_history_allowed { get; set; }
        public string? game { get; set; }
        public bool? hide_ads { get; set; }
        public bool? https_required { get; set; }
        public bool? mature { get; set; }
        public bool? partner { get; set; }
        public string? platform { get; set; }
        public string? player_type { get; set; }
        public Private? @private { get; set; }
        public bool? privileged { get; set; }
        public string? role { get; set; }
        public bool? server_ads { get; set; }
        public bool? show_ads { get; set; }
        public bool? subscriber { get; set; }
        public bool? turbo { get; set; }
        public object? user_id { get; set; }
        public string? user_ip { get; set; }
        public int? version { get; set; }
    }
}
