using HandyControl.Controls;
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
using TwitchArchiverWPF.Settings;
using MessageBox = HandyControl.Controls.MessageBox;

namespace TwitchArchiverWPF
{
    /// <summary>
    /// Interaction logic for PageStreamers.xaml
    /// </summary>
    public partial class PageStreamers : Page
    {
        static public ObservableCollection<Streamer> GridItems { get; set; } = new ObservableCollection<Streamer>();
        public PageStreamers()
        {
            InitializeComponent();

            try
            {
                List<Streamer>? streamerList = JsonConvert.DeserializeObject<List<Streamer>>(Settings.Settings.Default.Streamers);
                if (streamerList != null)
                    GridItems = new ObservableCollection<Streamer>(streamerList);
            }
            catch { }

            this.DataContext = this;
            GridItems.CollectionChanged += GridItems_CollectionChanged;
        }

        private void GridItems_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SaveSettings();
        }

        private void SaveSettings()
        {
            string streamerConfig = JsonConvert.SerializeObject(GridItems);
            Settings.Settings.Default.Streamers = streamerConfig;
            Settings.Settings.Default.Save();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Dialog.Show(new AddStreamerDialog(GridItems));
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            WindowStreamerSettings streamerSettings = new WindowStreamerSettings((Streamer)StreamerGrid.SelectedItem);
            streamerSettings.ShowDialog();
            SaveSettings();
        }

        private void FolderButton_Click(object sender, RoutedEventArgs e)
        {
            Streamer selectedStreamer = (Streamer)StreamerGrid.SelectedItem;
            if (!String.IsNullOrWhiteSpace(Settings.Settings.Default.SaveFolder))
            {
                if (Directory.Exists(Settings.Settings.Default.SaveFolder))
                {
                    List<string> directoryList = new List<string>(Directory.GetDirectories(Settings.Settings.Default.SaveFolder));
                    for (int i = 0; i < directoryList.Count; i++)
                    {
                        try
                        {
                            string folderName = Path.GetFileName(directoryList[i]);
                            if (folderName.Contains("_"))
                            {
                                string Id = folderName.Split('_').Last();
                                if (Id.Trim() == selectedStreamer.Id)
                                {
                                    //Found streamer folder
                                    Process.Start("explorer.exe", directoryList[i]);
                                    return;
                                }
                            }
                        }
                        catch { }
                    }
                }
                
                string newFolder = Path.Combine(Settings.Settings.Default.SaveFolder, $"{selectedStreamer.Name}_{selectedStreamer.Id}");
                if (!Directory.Exists(newFolder))
                    Directory.CreateDirectory(newFolder);
                Process.Start("explorer.exe", newFolder);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Streamer selectedStreamer = (Streamer)StreamerGrid.SelectedItem;
            MessageBoxResult result = MessageBox.Show("Are you sure you want to remove " + selectedStreamer.Name + " from the list?\nThis will not remove previously downloaded VODs", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                GridItems.Remove(selectedStreamer);
            }
        }
    }
}
