using System.Diagnostics;
using System.Threading;

namespace _07bAsyncAwaitWithContext
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            Debug.WriteLine("App started in thread {0}", Thread.CurrentThread.ManagedThreadId);
        }
    }
}
