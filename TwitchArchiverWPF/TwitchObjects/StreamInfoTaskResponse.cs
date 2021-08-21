using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchArchiverWPF.TwitchObjects
{
    class StreamInfoTaskResponse
    {
        public string? Title { get; set; }
        public List<string> Games { get; set; } = new List<string>();
    }
}
