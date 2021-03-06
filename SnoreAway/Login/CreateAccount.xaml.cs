﻿using SnoreAway.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SnoreAway.Login
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateAccount : Page
    {
        public CreateAccount()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            btnBack.IsEnabled = rootFrame.CanGoBack;
        }


        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();

            }
        }

        private async void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            DatabaseHelperClass Db_Helper = new DatabaseHelperClass();//Creating object for DatabaseHelperClass.cs from ViewModel/DatabaseHelperClass.cs    

            //Look up user, if exists notify user

              if(Db_Helper.HasAccontCreated(txtUserName.Text))
            {
                MessageDialog messageDialog = new MessageDialog("Account exists! Please log in, click on the back button");//Text should not be empty    
                await messageDialog.ShowAsync();
                return;
            }

            if (txtUserName.Text != "" & txtPassword.Password != "")
            {

                using (SHA256 shaHash = SHA256.Create())
                {

                    var password = Hashing.GetSha256Hash(shaHash, txtPassword.Password);
                    var account = Db_Helper.InsertAccount(new Models.Account(txtUserName.Text,  password));
                    App.UserId = account.Id;
                    Frame.Navigate(typeof(MainPage));//after adding new user redirect to profile page   

                }
                   
            }
            else
            {
                MessageDialog messageDialog = new MessageDialog("Please fill both fields");//Text should not be empty    
                await messageDialog.ShowAsync();
            }
        }
    }
}
