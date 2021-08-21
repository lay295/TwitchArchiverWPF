using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchArchiverWPF.TwitchObjects
{
    public class ProfileResponse
    {
        public ProfileData? data { get; set; }
        public Extensions? extensions { get; set; }
    }

    public class User
    {
        public string? id { get; set; }
        public string? login { get; set; }
        public string? displayName { get; set; }
        public string? profileImageURL { get; set; }
    }

    public class ProfileData
    {
        public User? user { get; set; }
    }
}
