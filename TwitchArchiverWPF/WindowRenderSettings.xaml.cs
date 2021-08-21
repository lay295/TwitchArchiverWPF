using HandyControl.Controls;
using HandyControl.Tools;
using SkiaSharp;
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
using Window = System.Windows.Window;

namespace TwitchArchiverWPF
{
    /// <summary>
    /// Interaction logic for WindowRenderSettings.xaml
    /// </summary>
    public partial class WindowRenderSettings : Window
    {
        public SKFontManager fontManager = SKFontManager.CreateDefault();
        RenderSettings renderSettings = new RenderSettings();
        public WindowRenderSettings(RenderSettings? RenderSettings)
        {
            InitializeComponent();
            if (RenderSettings != null)
                renderSettings = RenderSettings;

            List<string> fonts = new List<string>(fontManager.FontFamilies);
            fonts.Sort();
            foreach (var font in fonts)
            {
                ComboFonts.Items.Add(font);
            }
            Codec h264Codec = new Codec() { Name = "H264", InputArgs = "-framerate {fps} -f rawvideo -analyzeduration {max_int} -probesize {max_int} -pix_fmt bgra -video_size {width}x{height} -i -", OutputArgs = "-c:v libx264 -preset veryfast -crf 18 -pix_fmt yuv420p \"{save_path}\"" };
            Codec h265Codec = new Codec() { Name = "H265", InputArgs = "-framerate {fps} -f rawvideo -analyzeduration {max_int} -probesize {max_int} -pix_fmt bgra -video_size {width}x{height} -i -", OutputArgs = "-c:v libx265 -preset veryfast -crf 18 -pix_fmt yuv420p \"{save_path}\"" };
            Codec vp8Codec = new Codec() { Name = "VP8", InputArgs = "-framerate {fps} -f rawvideo -analyzeduration {max_int} -probesize {max_int} -pix_fmt bgra -video_size {width}x{height} -i -", OutputArgs = "-c:v libvpx -crf 18 -b:v 2M -pix_fmt yuva420p -auto-alt-ref 0 \"{save_path}\"" };
            Codec vp9Codec = new Codec() { Name = "VP9", InputArgs = "-framerate {fps} -f rawvideo -analyzeduration {max_int} -probesize {max_int} -pix_fmt bgra -video_size {width}x{height} -i -", OutputArgs = "-c:v libvpx-vp9 -crf 18 -b:v 2M -pix_fmt yuva420p \"{save_path}\"" };
            Codec rleCodec = new Codec() { Name = "RLE", InputArgs = "-framerate {fps} -f rawvideo -analyzeduration {max_int} -probesize {max_int} -pix_fmt bgra -video_size {width}x{height} -i -", OutputArgs = "-c:v qtrle -pix_fmt argb \"{save_path}\"" };
            Codec proresCodec = new Codec() { Name = "ProRes", InputArgs = "-framerate {fps} -f rawvideo -analyzeduration {max_int} -probesize {max_int} -pix_fmt bgra -video_size {width}x{height} -i -", OutputArgs = "-c:v prores_ks -qscale:v 62 -pix_fmt argb \"{save_path}\"" };
            VideoContainer mp4Container = new VideoContainer() { Name = "MP4", SupportedCodecs = new List<Codec>() { h264Codec, h265Codec } };
            VideoContainer movContainer = new VideoContainer() { Name = "MOV", SupportedCodecs = new List<Codec>() { h264Codec, h265Codec, rleCodec, proresCodec } };
            VideoContainer webmContainer = new VideoContainer() { Name = "WEBM", SupportedCodecs = new List<Codec>() { vp8Codec, vp9Codec } };
            VideoContainer mkvContainer = new VideoContainer() { Name = "MKV", SupportedCodecs = new List<Codec>() { h264Codec, h265Codec, vp8Codec, vp9Codec } };
            ComboFormats.Items.Add(mp4Container);
            ComboFormats.Items.Add(movContainer);
            ComboFormats.Items.Add(webmContainer);
            ComboFormats.Items.Add(mkvContainer);

            if (renderSettings != null)
            {
                if (ComboFonts.Items.Contains(renderSettings.Font))
                    ComboFonts.SelectedItem = renderSettings.Font;
                TextFontSize.Text = renderSettings.FontSize.ToString();
                TextFontColor.Text = renderSettings.FontColor;
                TextBackgroundColor.Text = renderSettings.BackgroundColor;
                RectangleFontColor.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(TextFontColor.Text));
                RectangleBackgroundColor.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(TextBackgroundColor.Text));
                CheckOutline.IsChecked = renderSettings.Outline;
                CheckTimestamp.IsChecked = renderSettings.Timestamp;
                CheckFFZ.IsChecked = renderSettings.FFZEmotes;
                CheckBTTV.IsChecked = renderSettings.BTTVEmotes;
                CheckSTV.IsChecked = renderSettings.STVEmotes;
                TextHeight.Text = renderSettings.Height.ToString();
                TextWidth.Text = renderSettings.Width.ToString();
                TextUpdateTime.Text = renderSettings.UpdateTime.ToString();
                TextFramerate.Text = renderSettings.Framerate.ToString();
                CheckSubMessages.IsChecked = renderSettings.SubMessages;
                CheckGenerateMask.IsChecked = renderSettings.GenerateMask;
                for (int i = 0; i < ComboFormats.Items.Count; i++)
                {
                    if (((VideoContainer)ComboFormats.Items[i]).Name == renderSettings.VideoContainer)
                        ComboFormats.SelectedIndex = i;
                }
                for (int i = 0; i < ComboCodecs.Items.Count; i++)
                {
                    if (((Codec)ComboCodecs.Items[i]).Name == renderSettings.Codec)
                        ComboCodecs.SelectedIndex = i;
                }
            }
        }

        private void ButtonFontColor_Click(object sender, RoutedEventArgs e)
        {
            var picker = SingleOpenHelper.CreateControl<ColorPicker>();
            var window = new PopupWindow
            {
                PopupElement = picker
            };
            picker.Confirmed += delegate {
                Color color = picker.SelectedBrush.Color;
                string colorText = "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
                TextFontColor.Text = colorText;
                RectangleFontColor.Fill = new SolidColorBrush(color);
                window.Close(); 
            };
            picker.Canceled += delegate { window.Close(); };
            window.Show(ButtonFontColor, false);
        }

        private void ButtonBackgroundColor_Click(object sender, RoutedEventArgs e)
        {
            var picker = SingleOpenHelper.CreateControl<ColorPicker>();
            var window = new PopupWindow
            {
                PopupElement = picker
            };
            picker.Confirmed += delegate {
                Color color = picker.SelectedBrush.Color;
                string colorText = "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
                TextBackgroundColor.Text = colorText;
                RectangleBackgroundColor.Fill = new SolidColorBrush(color);
                window.Close();
            };
            picker.Canceled += delegate { window.Close(); };
            window.Show(ButtonFontColor, false);
        }

        private void ComboFormats_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VideoContainer currentContainer = (VideoContainer)ComboFormats.SelectedItem;
            ComboCodecs.Items.Clear();
            if (currentContainer.SupportedCodecs != null)
            {
                foreach (Codec codec in currentContainer.SupportedCodecs)
                {
                    ComboCodecs.Items.Add(codec);
                    if (codec.Name == renderSettings.Codec)
                        ComboCodecs.SelectedItem = codec;
                }
            }

            if (ComboCodecs.SelectedItem == null)
                ComboCodecs.SelectedIndex = 0;

            renderSettings.VideoContainer = currentContainer.Name;
        }

        private void ComboFonts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            renderSettings.Font = (string)ComboFonts.SelectedItem;
        }

        private void TextFontSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                renderSettings.FontSize = int.Parse(TextFontSize.Text);
            }
            catch { }
        }

        private void TextFontColor_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                ColorConverter.ConvertFromString(TextFontColor.Text);
                renderSettings.FontColor = TextFontColor.Text;
            }
            catch { }
        }

        private void TextBackgroundColor_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                ColorConverter.ConvertFromString(TextBackgroundColor.Text);
                renderSettings.BackgroundColor = TextBackgroundColor.Text;
            }
            catch { }
        }

        private void CheckOutline_Checked(object sender, RoutedEventArgs e)
        {
            renderSettings.Outline = true;
        }

        private void CheckOutline_Unchecked(object sender, RoutedEventArgs e)
        {
            renderSettings.Outline = false;
        }

        private void CheckTimestamp_Checked(object sender, RoutedEventArgs e)
        {
            renderSettings.Timestamp = true;
        }

        private void CheckTimestamp_Unchecked(object sender, RoutedEventArgs e)
        {
            renderSettings.Timestamp = false;
        }

        private void CheckFFZ_Checked(object sender, RoutedEventArgs e)
        {
            renderSettings.FFZEmotes = true;
        }

        private void CheckFFZ_Unchecked(object sender, RoutedEventArgs e)
        {
            renderSettings.FFZEmotes = false;
        }

        private void CheckBTTV_Checked(object sender, RoutedEventArgs e)
        {
            renderSettings.BTTVEmotes = true;
        }

        private void CheckBTTV_Unchecked(object sender, RoutedEventArgs e)
        {
            renderSettings.BTTVEmotes = false;
        }

        private void CheckSTV_Checked(object sender, RoutedEventArgs e)
        {
            renderSettings.STVEmotes = true;
        }

        private void CheckSTV_Unchecked(object sender, RoutedEventArgs e)
        {
            renderSettings.STVEmotes = false;
        }

        private void TextHeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                renderSettings.Height = int.Parse(TextHeight.Text);
            }
            catch { }
        }

        private void TextWidth_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                renderSettings.Width = int.Parse(TextWidth.Text);
            }
            catch { }
        }

        private void TextUpdateTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                renderSettings.UpdateTime = double.Parse(TextUpdateTime.Text);
            }
            catch { }
        }

        private void TextFramerate_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                renderSettings.Framerate = int.Parse(TextFramerate.Text);
            }
            catch { }
        }

        private void CheckSubMessages_Checked(object sender, RoutedEventArgs e)
        {
            renderSettings.SubMessages = true;
        }

        private void CheckSubMessages_Unchecked(object sender, RoutedEventArgs e)
        {
            renderSettings.SubMessages = false;
        }

        private void CheckGenerateMask_Checked(object sender, RoutedEventArgs e)
        {
            renderSettings.GenerateMask = true;
        }

        private void CheckGenerateMask_Unchecked(object sender, RoutedEventArgs e)
        {
            renderSettings.GenerateMask = false;
        }

        private void ComboCodecs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Codec currentCodec = (Codec)ComboCodecs.SelectedItem;
            if (currentCodec != null)
            {
                renderSettings.Codec = currentCodec.Name;
                renderSettings.InputArgs = currentCodec.InputArgs;
                renderSettings.OutputArgs = currentCodec.OutputArgs;
            }
        }
    }
    public class VideoContainer
    {
        public string Name = "";
        public List<Codec>? SupportedCodecs;

        public override string ToString()
        {
            return Name == null ? "" : Name;
        }
    }

    public class Codec
    {
        public string Name = "";
        public string InputArgs = "";
        public string OutputArgs = "";

        public override string ToString()
        {
            return Name == null ? "" : Name;
        }
    }
}
