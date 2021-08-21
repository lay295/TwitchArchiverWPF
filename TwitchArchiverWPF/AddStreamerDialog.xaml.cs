using HandyControl.Interactivity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
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
using MessageBox = HandyControl.Controls.MessageBox;

namespace TwitchArchiverWPF
{
    /// <summary>
    /// Interaction logic for AddStreamerDialog.xaml
    /// </summary>
    public partial class AddStreamerDialog
    {
        DateTime lastTyped = DateTime.MinValue;
        ObservableCollection<Streamer> StreamerItems;
        bool isValidated = false;
        string avatarUrl = "";
        string streamerName = "";
        public AddStreamerDialog(ObservableCollection<Streamer> streamerItems)
        {
            InitializeComponent();
            StreamerItems = streamerItems;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
#pragma warning disable CS8629 // Nullable value type may be null.
            lastTyped = DateTime.MinValue;

            if (!isValidated)
            {
                ButtonAdd.IsEnabled = false;
                await ValidateUser();
                ButtonAdd.IsEnabled = true;
            }
            if (isValidated)
            {
                DownloadOptions downloadOptions = new DownloadOptions() { DownloadLiveStream = (bool)CheckDownloadLiveStream.IsChecked, DownloadLiveChat = (bool)CheckDownloadLiveChat.IsChecked, DownloadVodStream = (bool)CheckDownloadVodStream.IsChecked, DownloadVodChat = (bool)CheckDownloadVodChat.IsChecked };
                if (!downloadOptions.DownloadLiveChat && !downloadOptions.DownloadLiveStream && !downloadOptions.DownloadVodChat && !downloadOptions.DownloadVodStream)
                {
                    MessageBox.Show("At least 1 download option needs to be selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Streamer newStreamer = new Streamer() { Name = streamerName, Id = StreamerId.Text, AvatarUrl = avatarUrl, StreamCount = 0, DownloadOptions = downloadOptions };
                    if (!StreamerItems.Any(x => x.Id == newStreamer.Id))
                    {
                        StreamerItems.Add(newStreamer);
                        ControlCommands.Close.Execute(this, this);
                    }
                    else
                    {
                        MessageBox.Show("Streamer already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Error validating username, cannot find Twitch user.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
#pragma warning restore CS8629
        }

        private async void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            lastTyped = currentTime;
            isValidated = false;
            await Task.Delay(1000);
            if (lastTyped == currentTime)
            {
                //Assume user is done typing, try to get Twitch User
                await ValidateUser();
            }
        }

        private async Task ValidateUser()
        {
            try
            {
                using WebClient client = new WebClient();
                client.Headers.Add("Client-ID", "kimne78kx3ncx6brgo4mv6wki5h1ko");
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                string gqlResponse = await client.UploadStringTaskAsync("https://gql.twitch.tv/gql", "{\"query\":\"query{user(login:\\\"" + StreamerName.Text + "\\\"){id,login,displayName, profileImageURL(width: 70)}}\",\"variables\":{}}");
                ProfileResponse? profileResponse = JsonConvert.DeserializeObject<ProfileResponse>(gqlResponse);
                if (profileResponse != null && profileResponse.data != null && profileResponse.data.user != null)
                {
                    StreamerId.Text = profileResponse.data.user.id;
                    isValidated = true;
                    if (profileResponse.data.user.profileImageURL != null)
                    {
                        try
                        {
                            avatarUrl = profileResponse.data.user.profileImageURL;
                            BitmapImage image = new BitmapImage(new Uri(avatarUrl));
                            StreamerAvatar.Source = image;
                        }
                        catch { }
                    }
                    if (profileResponse.data.user.displayName != null && profileResponse.data.user.displayName.All(char.IsLetterOrDigit))
                        streamerName = profileResponse.data.user.displayName;
                }
            }
            catch { }
        }

        private void TextBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            isValidated = false;
        }

        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            CheckDownloadLiveStream.IsChecked = Settings.Settings.Default.DownloadLiveStream;
            CheckDownloadLiveChat.IsChecked = Settings.Settings.Default.DownloadLiveChat;
            CheckDownloadVodStream.IsChecked = Settings.Settings.Default.DownloadVodStream;
            CheckDownloadVodChat.IsChecked = Settings.Settings.Default.DownloadVodChat;
        }

        private void CheckDownloadLiveStream_Checked(object sender, RoutedEventArgs e)
        {
            Settings.Settings.Default.DownloadLiveStream = true;
            Settings.Settings.Default.Save();
        }

        private void CheckDownloadLiveStream_Unchecked(object sender, RoutedEventArgs e)
        {
            Settings.Settings.Default.DownloadLiveStream = false;
            Settings.Settings.Default.Save();
        }

        private void CheckDownloadLiveChat_Checked(object sender, RoutedEventArgs e)
        {
            Settings.Settings.Default.DownloadLiveChat = true;
            Settings.Settings.Default.Save();
        }

        private void CheckDownloadLiveChat_Unchecked(object sender, RoutedEventArgs e)
        {
            Settings.Settings.Default.DownloadLiveChat = false;
            Settings.Settings.Default.Save();
        }

        private void CheckDownloadVodStream_Checked(object sender, RoutedEventArgs e)
        {
            Settings.Settings.Default.DownloadVodStream = true;
            Settings.Settings.Default.Save();
        }

        private void CheckDownloadVodStream_Unchecked(object sender, RoutedEventArgs e)
        {
            Settings.Settings.Default.DownloadVodStream = false;
            Settings.Settings.Default.Save();
        }

        private void CheckDownloadVodChat_Checked(object sender, RoutedEventArgs e)
        {
            Settings.Settings.Default.DownloadVodChat = true;
            Settings.Settings.Default.Save();
        }

        private void CheckDownloadVodChat_Unchecked(object sender, RoutedEventArgs e)
        {
            Settings.Settings.Default.DownloadVodChat = false;
            Settings.Settings.Default.Save();
        }
    }
}
