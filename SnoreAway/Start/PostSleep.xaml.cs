using SnoreAway.Helper;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SnoreAway.Start
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PostSleep : Page
    {
        public PostSleep()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DatabaseHelperClass Db_Helper = new DatabaseHelperClass();//Creating object for DatabaseHelperClass.cs from ViewModel/DatabaseHelperClass.cs    
            var sleep = Db_Helper.ReadPostSleep(App.SessionId);

            TglFresh.IsOn = sleep.SleepWell;
            TglOnTime.IsOn = sleep.OnTime;
            txtTimes.Text = sleep.WakeNumber.ToString();

            Frame rootFrame = Window.Current.Content as Frame;
            btnBack.IsEnabled = rootFrame.CanGoBack;


        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {

            Models.PostSleep postSleep = new Models.PostSleep();
            postSleep.SessionId = App.SessionId;
            postSleep.ProfileId = App.UserId;
            postSleep.SleepWell = TglFresh.IsOn;
            postSleep.WakeNumber = Convert.ToInt16(txtTimes.Text);
            postSleep.OnTime = TglOnTime.IsOn;

            DatabaseHelperClass Db_Helper = new DatabaseHelperClass();//Creating object for DatabaseHelperClass.cs from ViewModel/DatabaseHelperClass.cs    
            var sleep = Db_Helper.ReadPostSleep(App.SessionId);

            if(sleep != null)
              {
                Db_Helper.UpdatePostSleep(postSleep);
            }
            else
            {

                Db_Helper.InsertPostSleep(postSleep);

            }


            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(History.Details));

        }


        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();

            }
        }
    }
}
