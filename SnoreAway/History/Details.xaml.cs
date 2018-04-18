using SnoreAway.Helper;
using SnoreAway.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SnoreAway.History
{
    /// <summary>se
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Details : Page
    {
        public Details()
        {
            InitializeComponent();
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();

            }
        }
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(MainPage));
        }

        private async void HyperAccount_Click(object sender, RoutedEventArgs e)
        {
            //Open recording
            DatabaseHelperClass Db_Helper = new DatabaseHelperClass();
            var session = Db_Helper.ReadSession(App.SessionId);
            if (session != null)
            {
                Windows.Storage.StorageFolder storageFolder =
Windows.Storage.ApplicationData.Current.LocalFolder;

                Windows.Storage.StorageFile mediaFile =
                    await storageFolder.GetFileAsync(session.FileLocation);

                mediaElement1.Source = new Uri(mediaFile.Path);
                mediaElement1.Play();
            }

        }

        private async void DisplayWaveForm()
        {
            List<LineItem> recordslist = new List<LineItem>();
           
            //Open recording
            DatabaseHelperClass Db_Helper = new DatabaseHelperClass();
            var session = Db_Helper.ReadSession(App.SessionId);
            if (session != null)
            {
                Windows.Storage.StorageFolder storageFolder =
Windows.Storage.ApplicationData.Current.LocalFolder;

                Windows.Storage.StorageFile mediaFile =
                    await storageFolder.GetFileAsync(session.FileLocation);

                var path = mediaFile.Path;

                NAudio.Wave.WaveChannel32 wave = new NAudio.Wave.WaveChannel32(new NAudio.Wave.MediaFoundationReader(@"Sample\test.wav"));

                byte[] buffer = new byte[16384];
                int read = 0;
                List<DataPoint> dataPoints = new List<DataPoint>();
        
                List<float> records = new List<float>();
                while(wave.Position < wave.Length)
                {
                    read = wave.Read(buffer, 0, 16384);
                    for (int i =0; i < read /4; i++)
                    {
                        // dataPoints.Add(new DataPoint() { BitConverter.ToSingle(buffer, i * 4) }); 
                        var convertor = BitConverter.ToSingle(buffer, i * 4);
                        records.Add(convertor);

                        recordslist.Add(new LineItem()
                        {
                            Name = i.ToString() ,
                            Amount = convertor
                        });


                    }
                }
                List<LineItem> newWave = new List<LineItem>();

                for (int i=0; i< 600 ; i++)
                {
                   
                    newWave.Add(recordslist[i]);
                }

                 (lineChart.Series[0] as LineSeries).ItemsSource = newWave;

            }


     
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {



             DisplayWaveForm();

            DatabaseHelperClass Db_Helper = new DatabaseHelperClass();//Creating object for DatabaseHelperClass.cs from ViewModel/DatabaseHelperClass.cs    
            var preSleepSession = Db_Helper.ReadPreSleep(App.SessionId);
            var postSleepSession = Db_Helper.ReadPostSleep(App.SessionId);
            var session = Db_Helper.ReadSession(App.SessionId);

        
           //Open Wave File
           //Read it into memory

            if(session != null)
            {
                StartTime.Text = session.StartTime != null ? session.StartTime : "";
                EndTime.Text = session.EndTime != null ? session.EndTime : "";
                Duration.Text = session.Duration != null ? session.Duration : "";
            }


            if (preSleepSession != null)
            {

                Coffee.Text = preSleepSession.CoffeeFlag ? "Individual drank Coffee" : "";
                Smoke.Text = preSleepSession.SmokeFlag ? "Individual Smoked" : "";
                Drink.Text = preSleepSession.DrinkFlag ? "Individual Drank" : "";
                Medicine.Text = preSleepSession.Pain ? "Individual felt Pain or was Sick" : "";
                LargeMeal.Text = preSleepSession.HeavyMeal ? "Individual had a large meal" : "";
                DinnerTime.Text = preSleepSession.DinnerTime.ToString();



            }

            if(postSleepSession != null)
            {
                SleptWell.Text = postSleepSession.SleepWell ? "Individual slept well" : "Individual didn't sleep well"; 
                FeltFresh.Text = postSleepSession.OnTime ? "Individual felt fresh after waking up" : "Individual didn't feel fresh after waking up";
                WokeUp.Text = "Individual woke up " + postSleepSession.WakeNumber + " times";

            }

        }
    }
}
