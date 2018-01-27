using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AESwitcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int screenWidth = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
        private int screenHeight = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;

        public MainWindow()
        {
            var device = runSwitcher();
            InitializeComponent();
            initNotification(device);
        }

        private void initNotification(string device)
        {
            lblEndpoint.Content = device;
            this.Width = GetTextWidth(device);
            this.Left = screenWidth - this.Width - 16;
            this.Top = screenHeight - this.Height;
            InitializeAnimation();
        }

        private string runSwitcher()
        {
            var result = "Unable to run EndpointController.";

            try
            {
                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "EndPointController.exe",
                        Arguments = "-c",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };

                proc.Start();
                while (!proc.StandardOutput.EndOfStream)
                {
                    result = proc.StandardOutput.ReadLine();
                }
            }
            catch (Exception ex)
            {
                result = $"Error: ${ex.Message}";
            }

            return result;
        }

        private void InitializeAnimation()
        {
            var slideInAnimation = new DoubleAnimation(screenWidth, this.Left, new Duration(TimeSpan.FromMilliseconds(200)));
            Storyboard.SetTargetProperty(slideInAnimation, new PropertyPath("Left"));

            var slideOutAnimation = new DoubleAnimationUsingKeyFrames();
            slideOutAnimation.KeyFrames.Add(new SplineDoubleKeyFrame(this.Left, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(1800))));
            slideOutAnimation.KeyFrames.Add(new SplineDoubleKeyFrame(screenWidth, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(2000))));
            Storyboard.SetTargetProperty(slideOutAnimation, new PropertyPath("Left"));

            var windowStoryboard = new Storyboard();
            windowStoryboard.Completed += WindowStoryboard_Completed;
            windowStoryboard.Children.Add(slideInAnimation);
            windowStoryboard.Children.Add(slideOutAnimation);

            var windowEventTrigger = new EventTrigger();
            var windowBeginStoryboard = new BeginStoryboard();
            windowBeginStoryboard.Storyboard = windowStoryboard;
            this.BeginStoryboard(windowStoryboard);
        }

        private void WindowStoryboard_Completed(object sender, EventArgs e)
        {
            this.Close();
        }

        private double GetTextWidth(string candidate)
        {
            var formattedText = new FormattedText(
                candidate,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface(this.lblEndpoint.FontFamily, this.lblEndpoint.FontStyle, this.lblEndpoint.FontWeight, this.lblEndpoint.FontStretch),
                this.lblEndpoint.FontSize,
                Brushes.White,
                new NumberSubstitution(),
                TextFormattingMode.Ideal);

            return formattedText.Width;
        }
    }
}
