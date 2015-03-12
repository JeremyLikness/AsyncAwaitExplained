using System;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace _07bAsyncAwaitWithContext
{
    public class Controller
    {
        public string SynchronousActionMethod()
        {
            return "C#er : IMage";
        }

        public async Task<string> AsynchronousActionMethod()
        {
            var client = new HttpClient();
            return await client.GetStringAsync("http://csharperimage.jeremylikness.com/");
        }
    }


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private const string WebSite = "http://ivision.com/our-services/technology-services/application-development/";

        public MainWindow()
        {
            Debug.WriteLine("Main Window Started in Thread {0}", Thread.CurrentThread.ManagedThreadId);
            InitializeComponent();
            ThreadText.Text = Thread.CurrentThread.ManagedThreadId.ToString(CultureInfo.InvariantCulture);            
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ((Button) sender).IsEnabled = false;
            Debug.WriteLine("Button Clicked in Thread {0}", Thread.CurrentThread.ManagedThreadId);
            try
            {
                Debug.WriteLine("Going to read a website from thread {0}.", Thread.CurrentThread.ManagedThreadId);
                var client = new HttpClient();

                var site = await client.GetStringAsync(WebSite);
                ((Button) sender).IsEnabled = true;
                ThreadText.Text = Thread.CurrentThread.ManagedThreadId.ToString(CultureInfo.InvariantCulture);
                Debug.WriteLine("Read {0} bytes from thread {1}.", site.Length, Thread.CurrentThread.ManagedThreadId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("I can't take it, this happened: {0}!", ex.Message);
            }
        }
    }
}
