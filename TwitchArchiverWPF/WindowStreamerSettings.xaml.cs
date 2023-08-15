using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using TwitchArchiverWPF.Settings;

namespace TwitchArchiverWPF
{
    /// <summary>
    /// Interaction logic for WindowStreamerSettings.xaml
    /// </summary>
    public partial class WindowStreamerSettings : Window
    {
        Streamer currentStreamer;
        public WindowStreamerSettings(Streamer streamer)
        {
            InitializeComponent();
            currentStreamer = streamer;

            CheckDownloadLiveStream.IsChecked = currentStreamer.DownloadOptions.DownloadLiveStream;
            CheckDownloadLiveChat.IsChecked = currentStreamer.DownloadOptions.DownloadLiveChat;
            CheckDownloadVodStream.IsChecked = currentStreamer.DownloadOptions.DownloadVodStream;
            CheckDownloadVodChat.IsChecked = currentStreamer.DownloadOptions.DownloadVodChat;

            CheckOverrideRecording.IsChecked = currentStreamer.OverrideRecordingSettings;
            CheckTTV.IsChecked = currentStreamer.RecordingSettings.EnableTtv;
            CheckOauth.IsChecked = currentStreamer.RecordingSettings.EnableLiveOauth;
            TextOauth.Text = currentStreamer.RecordingSettings.LiveOauth;
            NumLive.Value = currentStreamer.RecordingSettings.LiveCheck;
            CheckOauthVod.IsChecked = currentStreamer.RecordingSettings.EnableVodOauth;
            TextOauthVod.Text = currentStreamer.RecordingSettings.VodOauth;
            QualityPreference.SelectedItem = (QualityPreference.FindName(currentStreamer.RecordingSettings.QualityPreference) as ComboBoxItem);

            CheckOverridePost.IsChecked = currentStreamer.OverrideRenderSettings;
            CheckRender.IsChecked = currentStreamer.RenderSettings.RenderChat;
            if (currentStreamer.RenderSettings.RenderPrefrence == RenderPrefrence.Live)
                RadioLive.IsChecked = true;
            else if (currentStreamer.RenderSettings.RenderPrefrence == RenderPrefrence.VOD)
                RadioVod.IsChecked = true;
            else if (currentStreamer.RenderSettings.RenderPrefrence == RenderPrefrence.Both)
                RadioBoth.IsChecked = true;
        }

        private void CheckOverrideRecording_Checked(object sender, RoutedEventArgs e)
        {
            CheckTTV.IsEnabled = true;
            CheckOauth.IsEnabled = true;
            if (CheckOauth.IsChecked == true)
                TextOauth.IsEnabled = true;
            NumLive.IsEnabled = true;
            CheckOauthVod.IsEnabled = true;
            if (CheckOauthVod.IsChecked == true)
                TextOauthVod.IsEnabled = true;
            QualityPreference.IsEnabled = true;
            currentStreamer.OverrideRecordingSettings = true;
        }

        private void CheckOverrideRecording_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckTTV.IsEnabled = false;
            CheckOauth.IsEnabled = false;
            TextOauth.IsEnabled = false;
            NumLive.IsEnabled = false;
            CheckOauthVod.IsEnabled = false;
            TextOauthVod.IsEnabled = false;
            QualityPreference.IsEnabled = false;
            currentStreamer.OverrideRecordingSettings = false;
        }

        private void CheckTTV_Checked(object sender, RoutedEventArgs e)
        {
            currentStreamer.RecordingSettings.EnableTtv = true;
        }

        private void CheckTTV_Unchecked(object sender, RoutedEventArgs e)
        {
            currentStreamer.RecordingSettings.EnableTtv = false;
        }

        private void CheckOauth_Checked(object sender, RoutedEventArgs e)
        {
            TextOauth.IsEnabled = true;
            currentStreamer.RecordingSettings.EnableLiveOauth = true;
        }

        private void CheckOauth_Unchecked(object sender, RoutedEventArgs e)
        {
            TextOauth.IsEnabled = false;
            currentStreamer.RecordingSettings.EnableLiveOauth = false;
        }

        private void TextOauth_LostFocus(object sender, RoutedEventArgs e)
        {
            currentStreamer.RecordingSettings.LiveOauth = TextOauth.Text;
        }

        private void NumLive_ValueChanged(object sender, HandyControl.Data.FunctionEventArgs<double> e)
        {
            if (this.IsInitialized)
            {
                currentStreamer.RecordingSettings.LiveCheck = (int)NumLive.Value;
            }
        }

        private void CheckOverridePost_Checked(object sender, RoutedEventArgs e)
        {
            CheckRender.IsEnabled = true;
            RadioLive.IsEnabled = true;
            RadioVod.IsEnabled = true;
            RadioBoth.IsEnabled = true;
            ButtonEditRender.IsEnabled = true;
            currentStreamer.OverrideRenderSettings = true;
        }

        private void CheckOverridePost_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckRender.IsEnabled = false;
            RadioLive.IsEnabled = false;
            RadioVod.IsEnabled = false;
            RadioBoth.IsEnabled = false;
            ButtonEditRender.IsEnabled = false;
            currentStreamer.OverrideRenderSettings = false;
        }

        private void CheckRender_Checked(object sender, RoutedEventArgs e)
        {
            currentStreamer.RenderSettings.RenderChat = true;
        }

        private void CheckRender_Unchecked(object sender, RoutedEventArgs e)
        {
            currentStreamer.RenderSettings.RenderChat = false;
        }

        private void RadioLive_Checked(object sender, RoutedEventArgs e)
        {
            if (this.IsInitialized)
                currentStreamer.RenderSettings.RenderPrefrence = RenderPrefrence.Live;
        }

        private void RadioVod_Checked(object sender, RoutedEventArgs e)
        {
            if (this.IsInitialized)
                currentStreamer.RenderSettings.RenderPrefrence = RenderPrefrence.VOD;
        }

        private void RadioBoth_Checked(object sender, RoutedEventArgs e)
        {
            if (this.IsInitialized)
                currentStreamer.RenderSettings.RenderPrefrence = RenderPrefrence.Both;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowRenderSettings renderSettings = new WindowRenderSettings(currentStreamer.RenderSettings);
            renderSettings.ShowDialog();
        }

        private void CheckOauthVod_Checked(object sender, RoutedEventArgs e)
        {
            if (CheckOverrideRecording.IsChecked == true)
            {
                TextOauthVod.IsEnabled = true;
            }
            currentStreamer.RecordingSettings.EnableVodOauth = true;
        }

        private void CheckOauthVod_Unchecked(object sender, RoutedEventArgs e)
        {
            TextOauthVod.IsEnabled = false;
            currentStreamer.RecordingSettings.EnableVodOauth = false;
        }

        private void TextOauthVod_LostFocus(object sender, RoutedEventArgs e)
        {
            currentStreamer.RecordingSettings.VodOauth = TextOauthVod.Text;
        }

        private void CheckDownloadLiveStream_Checked(object sender, RoutedEventArgs e)
        {
            currentStreamer.DownloadOptions.DownloadLiveStream = true;
        }

        private void CheckDownloadLiveChat_Checked(object sender, RoutedEventArgs e)
        {
            currentStreamer.DownloadOptions.DownloadLiveChat = true;
        }

        private void CheckDownloadVodStream_Checked(object sender, RoutedEventArgs e)
        {
            currentStreamer.DownloadOptions.DownloadVodStream = true;
        }

        private void CheckDownloadVodChat_Checked(object sender, RoutedEventArgs e)
        {
            currentStreamer.DownloadOptions.DownloadVodChat = true;
        }

        private void CheckDownloadLiveStream_Unchecked(object sender, RoutedEventArgs e)
        {
            currentStreamer.DownloadOptions.DownloadLiveStream = false;
        }

        private void CheckDownloadLiveChat_Unchecked(object sender, RoutedEventArgs e)
        {
            currentStreamer.DownloadOptions.DownloadLiveChat = false;
        }

        private void CheckDownloadVodStream_Unchecked(object sender, RoutedEventArgs e)
        {
            currentStreamer.DownloadOptions.DownloadVodStream = false;
        }

        private void CheckDownloadVodChat_Unchecked(object sender, RoutedEventArgs e)
        {
            currentStreamer.DownloadOptions.DownloadVodChat = false;
        }

        private void QualityPreference_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentStreamer.RecordingSettings.QualityPreference = ((sender as ComboBox).SelectedItem as ComboBoxItem).Name as string;
        }
    }
}
