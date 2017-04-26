using System.Net.Http;
using System.Windows;

namespace WpfBaseClientForWebApi
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

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            var data = await client.GetAsync("http://localhost:9851/api/wpf");
            box.Text = data.Content.ReadAsStringAsync().Result;
        }
    }
}
