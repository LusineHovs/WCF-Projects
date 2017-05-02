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
using System.Net.Http.Formatting;


namespace WPF_DirectoryInfo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<string> files;
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
                   // System.Threading.Thread.Sleep(2000);
                   HttpResponseMessage message = response.Result;
                   string responseText = message.Content.ReadAsStringAsync().Result;

                   // converts Json into CS //
                   JavaScriptSerializer jss = new JavaScriptSerializer();
                   files = jss.Deserialize<ObservableCollection<string>>(responseText);

                   Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                       (Action)(() =>
                       {
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
            
            if (listOfFiles.SelectedItem != null)
            {
                string uri = string.Format("http://localhost:12784/api/webapi?filename=C:\\Testing\\{0}", Uri.EscapeDataString(textBoxFileName.Text));
                HttpClient client = new HttpClient();
                client.DeleteAsync(uri);
                
                if (files != null)
                {
                    files.RemoveAt(files.IndexOf(listOfFiles.SelectedItem.ToString()));
                    textBoxFileContent.Text = null;
                }
            }

        }

        private void selectionEvent(object sender, SelectionChangedEventArgs e)
        {
            if (listOfFiles.SelectedItem!=null)
            {
                textBoxFileName.Text = listOfFiles.SelectedItem.ToString();
            }

        }

        private void Createnew_Click(object sender, RoutedEventArgs e)
        {
            if (invisibPanel.Visibility == Visibility.Collapsed)
                invisibPanel.Visibility = Visibility.Visible;
            textBoxFileContent.Text = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (NewFile_Name_TextBox != null)
            {
                string uri = string.Format("http://localhost:12784/api/Webapi?name=C:\\Testing\\{0}", Uri.EscapeDataString(NewFile_Name_TextBox.Text));
                HttpClient client = new HttpClient();
                client.PostAsync(uri, textBoxFileContent.Text, new JsonMediaTypeFormatter())
                    .ContinueWith(response =>
                    {
                        if (response.Exception != null)
                        {
                            MessageBox.Show(response.Exception.Message);
                        }
                        else
                        {
                            MessageBox.Show("File successfully created");
                        }
                    });
            }

        }

        private void UpdateFile_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxFileName.Text != null && listOfFiles.SelectedItems != null)
            {
                string uri = string.Format("http://localhost:12784/api/Webapi?name={0}", Uri.EscapeDataString(textBoxFileName.Text));
                HttpClient client = new HttpClient();
                client.PutAsync(uri, textBoxFileContent.Text, new JsonMediaTypeFormatter())
                    .ContinueWith(response =>
                    {
                        if (response.Exception != null)
                        {
                            MessageBox.Show(response.Exception.Message);
                        }
                        else
                        {
                            MessageBox.Show("File successfully updated");
                        }
                    });


            }
        }
    }
}
