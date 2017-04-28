using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPF_DirectoryInfo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GetAllFiles_Click(object sender, RoutedEventArgs e)
        {

            HttpClient client = new HttpClient();
            var data = client.GetAsync("http://localhost:12784/api/webapi")
            // listOfFiles.DataContext = data.Content.ReadAsStringAsync().Result;

           .ContinueWith(response =>
            {
                if (response.Exception != null)
                {
                    MessageBox.Show(response.Exception.Message);
                }
                else
                {
                    System.Threading.Thread.Sleep(2000);
                    HttpResponseMessage message = response.Result;
                    string responseText = message.Content.ReadAsStringAsync().Result;

                    // converts Json into CS //
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    ObservableCollection<string> files = jss.Deserialize<ObservableCollection<string>>(responseText);

                    Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                        (Action)(() => {
                            listOfFiles.DataContext = files;

                            Binding binding = new Binding();
                            listOfFiles.SetBinding(ItemsControl.ItemsSourceProperty, binding);

                            (listOfFiles.ItemsSource as ObservableCollection<string>).RemoveAt(0);
                        }));
                }
            });
        }

        private async void buttonGetByName_Click(object sender, RoutedEventArgs e)
        {
            // petq e argument input anenq, => GetAsync()-i mej uri-n enq grum //
            //Thread.Sleep(5000); 
            string uri = string.Format("http://localhost:12784/api/webapi?name={0}", Uri.EscapeDataString(textBoxFileName.Text));
            HttpClient client = new HttpClient();
            var data = await client.GetAsync(uri);
            textBoxFileContent.Text = data.Content.ReadAsStringAsync().Result;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            string uri = string.Format("http://localhost:12784/api/webapi?name={0}", Uri.EscapeDataString(textBoxFileName.Text));
            HttpClient client = new HttpClient();
            client.DeleteAsync(uri);
        }

        private void selectionEvent(object sender, SelectionChangedEventArgs e)
        {
            textBoxFileName.Text = listOfFiles.SelectedItem.ToString();
        }
    }
}
