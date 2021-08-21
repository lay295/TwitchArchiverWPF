using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TwitchArchiverWPF.TwitchObjects;
using Path = System.IO.Path;

namespace TwitchArchiverWPF
{
    /// <summary>
    /// Interaction logic for PageVods.xaml
    /// </summary>
    public partial class PageVods : Page
    {
        static public ObservableCollection<StreamMetadata> VodList { get; set; } = new ObservableCollection<StreamMetadata>();
        public PageVods()
        {
            InitializeComponent();
            this.DataContext = this;
            RefreshVods();
        }

        public static void RefreshVods()
        {
            List<Streamer> streamers = PageStreamers.GridItems.ToList();
            foreach (var streamer in streamers)
            {
                streamer.StreamCount = 0;
            }
            VodList.Clear();
            if (!String.IsNullOrWhiteSpace(Settings.Settings.Default.SaveFolder) && Directory.Exists(Settings.Settings.Default.SaveFolder))
            {
                List<string> metadataFiles = new List<string>();
                List<string> dirList = new List<string>(Directory.GetDirectories(Settings.Settings.Default.SaveFolder));
                foreach (string dir in dirList)
                {
                    try
                    {
                        if (Path.GetFileName(dir) != ".tmp")
                        {
                            metadataFiles.AddRange(Directory.GetFiles(dir, "metadata.json", SearchOption.AllDirectories));
                        }
                    }
                    catch { }
                }

                foreach (var metadata in metadataFiles)
                {
                    StreamMetadata? meta = JsonConvert.DeserializeObject<StreamMetadata>(File.ReadAllText(metadata));
                    if (meta != null)
                    {
                        meta.MetadataPath = metadata;
                        VodList.Add(meta);
                        Streamer? streamer = streamers.FirstOrDefault(x => x.Id == meta.StreamerId);
                        if (streamer != null)
                            streamer.StreamCount++;
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RefreshVods();
        }

        private void FolderButton_Click(object sender, RoutedEventArgs e)
        {
            StreamMetadata selectedVod = (StreamMetadata)VodGrid.SelectedItem;
            string? folder = Path.GetDirectoryName(selectedVod.MetadataPath);
            if (!String.IsNullOrWhiteSpace(folder))
            {
                Process.Start("explorer.exe", folder);
            }
        }
    }
}
