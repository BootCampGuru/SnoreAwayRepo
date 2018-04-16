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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SnoreAway
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(SnoreLab));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(App.UserId != 0)
            {
                Login.Visibility = Visibility.Collapsed;
                txtLogin.Visibility = Visibility.Collapsed;
                txtSession.Visibility = Visibility.Visible;
                btnSleep.Visibility = Visibility.Visible;
                txtLogout.Visibility = Visibility.Visible;
                btnLogout.Visibility = Visibility.Visible;
                txtResults.Visibility = Visibility.Visible;
                btnHistory.Visibility = Visibility.Visible;
                txtProfile.Visibility = Visibility.Visible;
                btnProfile.Visibility = Visibility.Visible;

            }
            else
            {
                Login.Visibility = Visibility.Visible;
                txtLogout.Visibility = Visibility.Visible;
                btnLogout.Visibility = Visibility.Collapsed;
                btnProfile.Visibility = Visibility.Collapsed;
                btnSleep.Visibility = Visibility.Collapsed;
                btnHistory.Visibility = Visibility.Collapsed;
                txtProfile.Visibility = Visibility.Collapsed;
                txtResults.Visibility = Visibility.Collapsed;
                txtLogout.Visibility = Visibility.Collapsed;
                txtSession.Visibility = Visibility.Collapsed;
            }
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(Profile.Profile));
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(History.History));
        }

        private void btnSleep_Click(object sender, RoutedEventArgs e)
        {
            //Reset Session, so user can create a new session
            App.SessionId = 0;
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(Start.PreSleep));
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(Login.Login));
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(Login.Logout));
        }
    }
}
