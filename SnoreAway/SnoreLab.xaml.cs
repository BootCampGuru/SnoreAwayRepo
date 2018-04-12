using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SnoreAway
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SnoreLab : Page
    {

        private MediaCapture CaptureMedia;

        private IRandomAccessStream AudioStream;
        private AudioEncodingQuality SelectedQuality;
        private FileSavePicker FileSave;

        private DispatcherTimer DishTimer;
        public SnoreLab()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)

        {

            await InitMediaCapture();

        }

        private async Task InitMediaCapture()

        {

            CaptureMedia = new MediaCapture();

            var captureInitSettings = new MediaCaptureInitializationSettings();

            captureInitSettings.StreamingCaptureMode = StreamingCaptureMode.Audio;

            await CaptureMedia.InitializeAsync(captureInitSettings);

           // CaptureMedia.Failed += MediaCaptureOnFailed;

           // CaptureMedia.RecordLimitationExceeded += MediaCaptureOnRecordLimitationExceeded;

        }

        private async void RecordBtn_Click(object sender, RoutedEventArgs e)
        {
            
            MediaEncodingProfile encodingProfile = null;
            var audioQualities = Enum.GetValues(typeof(AudioEncodingQuality)).Cast<AudioEncodingQuality>();
            SelectedQuality = (AudioEncodingQuality)audioQualities.First();
            AudioStream = new InMemoryRandomAccessStream();
            encodingProfile = Windows.Media.MediaProperties.MediaEncodingProfile.CreateMp3(SelectedQuality);
            await CaptureMedia.StartRecordToStreamAsync(encodingProfile, AudioStream);

            //UpdateRecordingControls(RecordingMode.Recording);

           // DishTimer.Start();
        }

        private void StopBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
