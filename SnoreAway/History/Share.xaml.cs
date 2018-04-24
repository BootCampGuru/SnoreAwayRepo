using SnoreAway.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Syndication;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SnoreAway.History
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Share : Page
    {
        public Share()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DatabaseHelperClass Db_Helper = new DatabaseHelperClass();//Creating object for DatabaseHelperClass.cs from ViewModel/DatabaseHelperClass.cs    
            
            var preSleepSession = Db_Helper.ReadAllSessions(App.UserId).Where(s => s.Id == App.SessionId).ToList();
            lvDataBinding.ItemsSource = preSleepSession;
        }
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(MainPage));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();

            }


        }

        //Upload information either encrypted/decrypted with the private key of the venodor or provider

        private async void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            Windows.Web.Syndication.SyndicationClient client = new SyndicationClient();
            DatabaseHelperClass Db_Helper = new DatabaseHelperClass();
            var session = Db_Helper.ReadSession(App.SessionId);
  
            // Upload the meta data associated with the sleep session. Use public key of the provider to encrypt the data before sending it over 
            // Upload the file to the S3 server and provide the server location
            try
            {

                var values = new Dictionary<string, string>
                    {
                 { "session", session.Id.ToString() },
                 { "sessioninfo", session.FileLocation }
                    };


                using (var sleepClient = new HttpClient())
                {
                    sleepClient.BaseAddress = new Uri("http://127.0.0.1:3000/");
                    var content = new FormUrlEncodedContent(values
                    );
                    var result = await sleepClient.PostAsync("/PostSession", content);
                    string resultContent = await result.Content.ReadAsStringAsync();

                    MessageDialog messageDialog = new MessageDialog("Data has been shared with chosen providers");//Text should not be empty    
                    await messageDialog.ShowAsync();

                }


            }
            catch(Exception ex)
            { 
            }

        }
    }
}
