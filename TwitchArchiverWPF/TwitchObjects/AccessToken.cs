using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchArchiverWPF.TwitchObjects
{
	public class AccessToken
	{
		public string value { get; set; } = "";
		public string signature { get; set; } = "";
		public DateTime expires { get; set; } = DateTime.MinValue;
	}
}
