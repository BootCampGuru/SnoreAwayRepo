﻿using SnoreAway.Helper;
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


        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {


            DatabaseHelperClass Db_Helper = new DatabaseHelperClass();//Creating object for DatabaseHelperClass.cs from ViewModel/DatabaseHelperClass.cs    
            var preSleepSession = Db_Helper.ReadPreSleep(App.SessionId);
            var postSleepSession = Db_Helper.ReadPostSleep(App.SessionId);
            var session = Db_Helper.ReadSession(App.SessionId);

        
           //Open Wave File
           //Read it into memory

            if(session != null)
            {
                StartTime.Text = session.StartTime != null ? "Sleep Start Time :" + session.StartTime : "";
                EndTime.Text = session.EndTime != null ? "Sleep End Time :" + session.EndTime : "";
                Duration.Text = session.Duration != null ? "Sleep Duration :" +  session.Duration : "";
            }


            if (preSleepSession != null)
            {

                Coffee.Text = preSleepSession.CoffeeFlag ? "Individual drank Coffee" : "Individual didn't drink Coffee before bed";
                Smoke.Text = preSleepSession.SmokeFlag ? "Individual Smoked" : "Individual didn't smoke before bed";
                Drink.Text = preSleepSession.DrinkFlag ? "Individual Drank" : "Individual didn't drink before bed";
                Medicine.Text = preSleepSession.Pain ? "Individual felt Pain or was Sick" : "Individual didn't feel any pain before bed";
                LargeMeal.Text = preSleepSession.HeavyMeal ? "Individual had a large meal" : "Individual didn't have a large meal";
                DaySleep.Text = preSleepSession.DaySleepFlag != 0 ? "Indivdual slept " + preSleepSession.DaySleepFlag.ToString() + " times" : "";
                DinnerTime.Text = preSleepSession.DinnerTime.ToString();



            }

            if(postSleepSession != null)
            {
                SleptWell.Text = postSleepSession.SleepWell ? "Individual slept well" : "Individual didn't sleep well"; 
                FeltFresh.Text = postSleepSession.OnTime ? "Individual felt fresh after waking up" : "Individual didn't feel fresh after waking up";
                WokeUp.Text = "Individual woke up " + postSleepSession.WakeNumber + " times";

            }

        }

        private void chartLink_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(SessionCharts));
        }
    }
}
