using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WebPassAgent
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PassListPage : Page
    {
        private ObservableCollection<WebPass> WebPasses;
        public PassListPage()
        {
            this.InitializeComponent();
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            async () =>
            {
                // Your UI update code goes here!
                WebPasses = new ObservableCollection<WebPass>(await WebPassManager.GetWebPassesAsync());
                passList.ItemsSource = null;
                passList.ItemsSource = WebPasses;
            }
            ).AsTask();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            MainPage.ContentFrame.Navigate(typeof(PassPage));
        }

        private void PassList_CellEditEnded(object sender, DataGridCellEditEndedEventArgs e)
        {

            Task.Run(async () => await WebPassManager.SaveAsync(WebPasses.ToList()));
        }

        private void MfDel_Click(object sender, RoutedEventArgs e)
        {
            if (passList.SelectedItems != null && passList.SelectedItems.Count > 0)
            {
                foreach (var item in passList.SelectedItems.OfType<WebPass>().ToList())
                {
                    WebPasses.Remove(item);
                    Task.Run(async () => await WebPassManager.Delete(item));
                    passList.ItemsSource = null;
                    passList.ItemsSource = WebPasses;
                }
            }
            else
            {
                
            }
        }
    }
}
