using SnoreAway.Helper;
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
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;
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
        private TimeSpan SpanTime;
        private DispatcherTimer DishTimer;
        private AudioEncodingFormat SelectedFormat;
        public SnoreLab()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)

        {

            await InitMediaCapture();
            UpdateRecordingControls(RecordingMode.Initializing);
            InitTimer();

        }
        private void InitTimer()

        {

            DishTimer = new DispatcherTimer();

            DishTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);

            DishTimer.Tick += TimerOnTick;

        }

        private void TimerOnTick(object sender, object o)

        {

            SpanTime = SpanTime.Add(DishTimer.Interval);

            Duration.DataContext = SpanTime;

        }

        private async void StopBtn_Click(object sender, RoutedEventArgs e)

        {

            await CaptureMedia.StopRecordAsync();

            UpdateRecordingControls(RecordingMode.Stopped);

            DishTimer.Stop();

            InitFileSavePicker();

        }
        private async Task InitMediaCapture()

        {

            CaptureMedia = new MediaCapture();

            var captureInitSettings = new MediaCaptureInitializationSettings();

            captureInitSettings.StreamingCaptureMode = StreamingCaptureMode.Audio;

            await CaptureMedia.InitializeAsync(captureInitSettings);

            CaptureMedia.Failed += MediaCaptureOnFailed;

            CaptureMedia.RecordLimitationExceeded += MediaCaptureOnRecordLimitationExceeded;

        }

        private void InitFileSavePicker()

        {
            var audioEncodingFormats = Enum.GetValues(typeof(AudioEncodingFormat)).Cast<AudioEncodingFormat>();

            SelectedFormat = (AudioEncodingFormat)audioEncodingFormats.First();

            FileSave = new FileSavePicker();

            FileSave.FileTypeChoices.Add("Encoding", new List<string>() { SelectedFormat.ToFileExtension() });

            FileSave.SuggestedStartLocation = PickerLocationId.MusicLibrary;

        }

        private void UpdateRecordingControls(RecordingMode recordingMode)

        {

            switch (recordingMode)

            {

                case RecordingMode.Initializing:

                    RecordBtn.IsEnabled = true;

                    StopBtn.IsEnabled = false;

                    SaveBtn.IsEnabled = false;

                    break;

                case RecordingMode.Recording:

                    RecordBtn.IsEnabled = false;

                    StopBtn.IsEnabled = true;

                    SaveBtn.IsEnabled = false;

                    break;

                case RecordingMode.Stopped:

                    RecordBtn.IsEnabled = true;

                    StopBtn.IsEnabled = false;

                    SaveBtn.IsEnabled = true;

                    break;

                default:

                    throw new ArgumentOutOfRangeException("recordingMode");

            }

        }

        private async void RecordBtn_Click(object sender, RoutedEventArgs e)
        {

            MediaEncodingProfile encodingProfile = null;
            var audioQualities = Enum.GetValues(typeof(AudioEncodingQuality)).Cast<AudioEncodingQuality>();
            SelectedQuality = (AudioEncodingQuality)audioQualities.First();
            AudioStream = new InMemoryRandomAccessStream();
            encodingProfile = Windows.Media.MediaProperties.MediaEncodingProfile.CreateMp3(SelectedQuality);
            await CaptureMedia.StartRecordToStreamAsync(encodingProfile, AudioStream);

            UpdateRecordingControls(RecordingMode.Recording);

            DishTimer.Start();
        }

        private async void MediaCaptureOnRecordLimitationExceeded(MediaCapture sender)

        {

            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>

            {

                await sender.StopRecordAsync();

                var warningMessage = new MessageDialog("The media recording has been stopped because you exceeded the maximum recording length.", "Recording Stoppped");

                await warningMessage.ShowAsync();

            });

        }

        private async void MediaCaptureOnFailed(MediaCapture sender, MediaCaptureFailedEventArgs errorEventArgs)

        {

            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>

            {

                var warningMessage = new MessageDialog(String.Format("The media capture failed: {0}", errorEventArgs.Message), "Capture Failed");

                await warningMessage.ShowAsync();

            });

        }

        private async void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            var mediaFile = await FileSave.PickSaveFileAsync();


            if (mediaFile != null)

            {

                using (var dataReader = new DataReader(AudioStream.GetInputStreamAt(0)))

                {

                    await dataReader.LoadAsync((uint)AudioStream.Size);

                    byte[] buffer = new byte[(int)AudioStream.Size];

                    dataReader.ReadBytes(buffer);

                    await FileIO.WriteBytesAsync(mediaFile, buffer);

                    UpdateRecordingControls(RecordingMode.Initializing);

                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
