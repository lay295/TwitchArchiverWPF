using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchArchiverWPF.Settings
{
    public class GlobalSettings
    {
        public RecordingSettings RecordingSettings { get; set; } = new RecordingSettings();
        public RenderSettings RenderSettings { get; set; } = new RenderSettings();
    }
}