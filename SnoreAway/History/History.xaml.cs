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

namespace SnoreAway.History
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class History : Page
    {
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(MainPage));
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DatabaseHelperClass Db_Helper = new DatabaseHelperClass();//Creating object for DatabaseHelperClass.cs from ViewModel/DatabaseHelperClass.cs    
            var preSleepSession = Db_Helper.ReadAllSessions(App.UserId);

            lvDataBinding.ItemsSource = preSleepSession;
        }
            public History()
        {
            this.InitializeComponent();
        }

        private void lnkEdit_Click(object sender, RoutedEventArgs e)
        {
            var value = sender as HyperlinkButton;
            App.SessionId = Convert.ToInt16(value.CommandParameter.ToString());
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(Start.PreSleep));
        }

        private void lnkDelete_Click(object sender, RoutedEventArgs e)
        {
            var value = sender as HyperlinkButton;
            DatabaseHelperClass Db_Helper = new DatabaseHelperClass();
            Db_Helper.DeleteSession(Convert.ToInt16(value.CommandParameter.ToString()));
      
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(History));
        }

        private void lnkShare_Click(object sender, RoutedEventArgs e)
        {
            var value = sender as HyperlinkButton;
            App.SessionId = Convert.ToInt16(value.CommandParameter.ToString());
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(Share));
        }

        private void lvDataBinding_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var test = sender;
        }

        private void lnkDetails_Click(object sender, RoutedEventArgs e)
        {
            var value = sender as HyperlinkButton;

            App.SessionId = Convert.ToInt16(value.CommandParameter.ToString());

            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(Details));
        }

        private void lvDataBinding_ItemClick(object sender, ItemClickEventArgs e)
        {
            var itemClicked = e.ClickedItem;

        }
    }
}
