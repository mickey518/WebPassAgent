using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

namespace WebPassAgent
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PassPage : Page
    {
        public PassListPage passListPage;
        public PassPage()
        {
            this.InitializeComponent();
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var pass = new WebPass();
            pass.Id = Guid.NewGuid();
            pass.Host = boxWebHost.Text;
            pass.Uri = boxWebHost.Text;
            pass.Username = boxUsername.Text;
            pass.Password = boxPassword.Password;

            //Task.Run(async () => await WebPassManager.Add(pass));
            await WebPassManager.Add(pass);
            MainPage.ContentFrame.Navigate(typeof(PassListPage));
        }
    }
}
