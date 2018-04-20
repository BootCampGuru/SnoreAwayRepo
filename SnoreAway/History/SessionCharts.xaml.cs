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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SessionCharts : Page
    {

        List<LineItem> lineChartV = new List<LineItem>();
        List<LineItem> pieChartV = new List<LineItem>();
        List<LineItem> barChartV = new List<LineItem>();
        List<LineItem> columnChartV = new List<LineItem>();
        public SessionCharts()
        {
            this.InitializeComponent();
        }


        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {


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

                NAudio.Wave.WaveChannel32 wave = new NAudio.Wave.WaveChannel32(new NAudio.Wave.MediaFoundationReader(@"Sample\Session_5.wav"));

                byte[] buffer = new byte[16384];
                int read = 0;
                List<DataPoint> dataPoints = new List<DataPoint>();

                List<float> records = new List<float>();
                while (wave.Position < wave.Length)
                {
                    read = wave.Read(buffer, 0, 16384);
                    for (int i = 0; i < read / 4; i++)
                    {
                        // dataPoints.Add(new DataPoint() { BitConverter.ToSingle(buffer, i * 4) }); 
                        var convertor = BitConverter.ToSingle(buffer, i * 4);
                        records.Add(convertor);

                        recordslist.Add(new LineItem()
                        {
                            Name = i.ToString(),
                            Amount = convertor
                        });


                    }
                }
                List<LineItem> newWave = new List<LineItem>();

                for (int i = 0; i < 600; i++)
                {

                    newWave.Add(recordslist[i]);
                }

                 (lineChart.Series[0] as LineSeries).ItemsSource = newWave;

            }



        }
    }
}
