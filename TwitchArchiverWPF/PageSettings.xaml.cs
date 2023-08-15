using HandyControl.Tools.Extension;
using IWshRuntimeLibrary;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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
using File = System.IO.File;

namespace TwitchArchiverWPF
{
    /// <summary>
    /// Interaction logic for PageSettings.xaml
    /// </summary>
    public partial class PageSettings : Page
    {
        GlobalSettings globalSettings;
        public PageSettings()
        {
            InitializeComponent();
            if (String.IsNullOrWhiteSpace(Settings.Settings.Default.SaveFolder))
            {
                Settings.Settings.Default.SaveFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), "Twitch");
                if (!Directory.Exists(Settings.Settings.Default.SaveFolder))
                    Directory.CreateDirectory(Settings.Settings.Default.SaveFolder);
                if (String.IsNullOrWhiteSpace(Settings.Settings.Default.TempFolder))
                {
                    Settings.Settings.Default.TempFolder = Path.Combine(Settings.Settings.Default.SaveFolder, ".tmp");
                    if (!Directory.Exists(Settings.Settings.Default.TempFolder))
                        Directory.CreateDirectory(Settings.Settings.Default.TempFolder);
                }

                Settings.Settings.Default.Save();
            }

            TextDownloadFolder.Text = Settings.Settings.Default.SaveFolder;
            TextTempFolder.Text = Settings.Settings.Default.TempFolder;

#pragma warning disable CS8601 // Possible null reference assignment.
            globalSettings = JsonConvert.DeserializeObject<GlobalSettings>(Settings.Settings.Default.GlobalSettings);
#pragma warning restore CS8601 // Possible null reference assignment.
            if (globalSettings == null)
                globalSettings = new GlobalSettings();

            CheckTTV.IsChecked = globalSettings.RecordingSettings.EnableTtv;
            CheckOauth.IsChecked = globalSettings.RecordingSettings.EnableLiveOauth;
            NumLive.Value = globalSettings.RecordingSettings.LiveCheck;
            CheckOauthVod.IsChecked = globalSettings.RecordingSettings.EnableVodOauth;
            CheckRender.IsChecked = globalSettings.RenderSettings.RenderChat;
            if (globalSettings.RenderSettings.RenderPrefrence == RenderPrefrence.Live)
                RadioLive.IsChecked = true;
            else if (globalSettings.RenderSettings.RenderPrefrence == RenderPrefrence.VOD)
                RadioVod.IsChecked = true;
            else
                RadioBoth.IsChecked = true;
            TextOauthVod.Text = globalSettings.RecordingSettings.VodOauth;
            TextOauth.Text = globalSettings.RecordingSettings.LiveOauth;

            QualityPreference.SelectedItem = (QualityPreference.FindName(globalSettings.RecordingSettings.QualityPreference) as ComboBoxItem);


            using (RegistryKey? key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                if (key != null)
                {
                    if (key.GetValue("Twitch Archiver") == null)
                    {
                        CheckStartup.IsChecked = false;
                    }
                    else
                    {
                        CheckStartup.IsChecked = true;
                    }
                }
            }
        }

        private void TextDownloadFolder_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newPath = TextDownloadFolder.Text;
            if (Directory.Exists(newPath))
            {
                Settings.Settings.Default.SaveFolder = newPath;
                Settings.Settings.Default.Save();
            }
        }

        private void TextTempFolder_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newPath = TextTempFolder.Text;
            if (Directory.Exists(newPath))
            {
                Settings.Settings.Default.TempFolder = newPath;
                Settings.Settings.Default.Save();
            }
        }

        private void ButtonDownloadFolder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                TextDownloadFolder.Text = dialog.SelectedPath;
            }
        }

        private void ButtonTempFolder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                TextTempFolder.Text = dialog.SelectedPath;
            }
        }

        private void CheckOauth_Checked(object sender, RoutedEventArgs e)
        {
            TextOauth.IsEnabled = true;
            globalSettings.RecordingSettings.EnableLiveOauth = true;
            SaveSettings();
        }

        private void CheckOauth_Unchecked(object sender, RoutedEventArgs e)
        {
            TextOauth.IsEnabled = false;
            globalSettings.RecordingSettings.EnableLiveOauth = false;
            SaveSettings();
        }

        private void CheckTTV_Checked(object sender, RoutedEventArgs e)
        {
            globalSettings.RecordingSettings.EnableTtv = true;
            SaveSettings();
        }

        private void CheckTTV_Unchecked(object sender, RoutedEventArgs e)
        {
            globalSettings.RecordingSettings.EnableTtv = false;
            SaveSettings();
        }

        private void CheckRender_Checked(object sender, RoutedEventArgs e)
        {
            RadioLive.IsEnabled = true;
            RadioVod.IsEnabled = true;
            RadioBoth.IsEnabled = true;
            globalSettings.RenderSettings.RenderChat = true;
            SaveSettings();
        }

        private void CheckRender_Unchecked(object sender, RoutedEventArgs e)
        {
            RadioLive.IsEnabled = false;
            RadioVod.IsEnabled = false;
            RadioBoth.IsEnabled = false;
            globalSettings.RenderSettings.RenderChat = false;
            SaveSettings();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowRenderSettings renderSettings = new WindowRenderSettings(globalSettings.RenderSettings);
            renderSettings.ShowDialog();
            SaveSettings();
        }

        private void CheckOauthVod_Checked(object sender, RoutedEventArgs e)
        {
            TextOauthVod.IsEnabled = true;
            globalSettings.RecordingSettings.EnableVodOauth = true;
            SaveSettings();
        }

        private void CheckOauthVod_Unchecked(object sender, RoutedEventArgs e)
        {
            TextOauthVod.IsEnabled = false;
            globalSettings.RecordingSettings.EnableVodOauth = false;
            SaveSettings();
        }

        private void SaveSettings()
        {
            if (globalSettings != null)
            {
                Settings.Settings.Default.GlobalSettings = JsonConvert.SerializeObject(globalSettings, Formatting.None);
                Settings.Settings.Default.Save();
            }
        }

        private void TextOauth_LostFocus(object sender, RoutedEventArgs e)
        {
            globalSettings.RecordingSettings.LiveOauth = TextOauth.Text;
            SaveSettings();
        }

        private void NumLive_ValueChanged(object sender, HandyControl.Data.FunctionEventArgs<double> e)
        {
            if (this.IsInitialized)
            {
                globalSettings.RecordingSettings.LiveCheck = (int)NumLive.Value;
                SaveSettings();
            }
        }

        private void TextOauthVod_LostFocus(object sender, RoutedEventArgs e)
        {
            globalSettings.RecordingSettings.VodOauth = TextOauthVod.Text;
            SaveSettings();
        }

        private void RadioLive_Checked(object sender, RoutedEventArgs e)
        {
            globalSettings.RenderSettings.RenderPrefrence = RenderPrefrence.Live;
            SaveSettings();
        }

        private void RadioVod_Checked(object sender, RoutedEventArgs e)
        {
            globalSettings.RenderSettings.RenderPrefrence = RenderPrefrence.VOD;
            SaveSettings();
        }

        private void RadioBoth_Checked(object sender, RoutedEventArgs e)
        {
            globalSettings.RenderSettings.RenderPrefrence = RenderPrefrence.Both;
            SaveSettings();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (this.IsInitialized)
            {
                using (RegistryKey? key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    if (key != null)
                    {
                        Process? currentProcess = Process.GetCurrentProcess();
                        if (currentProcess != null && currentProcess.MainModule != null && currentProcess.MainModule.FileName != null)
                            key.SetValue("Twitch Archiver", "\"" + currentProcess.MainModule.FileName + "\"");
                    } 
                }
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.IsInitialized)
            {
                using (RegistryKey? key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    if (key != null)
                    {
                        Process? currentProcess = Process.GetCurrentProcess();
                        if (currentProcess != null && currentProcess.MainModule != null && currentProcess.MainModule.FileName != null)
                            key.DeleteValue("Twitch Archiver", false);
                    }
                }
            }
        }

        private void QualityPreference_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            globalSettings.RecordingSettings.QualityPreference = ((sender as ComboBox).SelectedItem as ComboBoxItem).Name as string;
            SaveSettings();
        }
    }
}
