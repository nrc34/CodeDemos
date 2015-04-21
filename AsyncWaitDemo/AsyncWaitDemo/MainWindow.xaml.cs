using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Diagnostics;

namespace AsyncWaitDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static SolidColorBrush resultBrush = Brushes.Green;

        static Stopwatch stopWatch;

        public MainWindow()
        {
            InitializeComponent();

            ComponentDispatcher.ThreadIdle += 
                new System.EventHandler(ComponentDispatcher_ThreadIdle);

            stopWatch = new Stopwatch();
            stopWatch.Start();

            

        }


        
        private void ComponentDispatcher_ThreadIdle(object sender, EventArgs e)
        {
            demoGrid.Dispatcher.Invoke(
                        System.Windows.Threading.DispatcherPriority.Normal, 
                        new Action(myAction));

            gridResult.Dispatcher.Invoke(
                        DispatcherPriority.Normal, 
                        new Action(showEndResult));

            labelFPS.Dispatcher.Invoke(
                        DispatcherPriority.Normal, 
                        new Action(showFPS));

            Thread.Sleep(17);
        }

        private void showFPS()
        {
            stopWatch.Stop();

            labelFPS.Content = 
                stopWatch.ElapsedMilliseconds.ToString() +
                " : " + ((float)1000 / stopWatch.ElapsedMilliseconds);

            stopWatch.Restart();
        }

        private void showEndResult()
        {
            gridResult.Background = resultBrush;
        }

        private void myAction()
        {
            Random r = new Random();

            string randomColor = "#FF" + 
                                 r.Next(0, 255).ToString("X2") + 
                                 r.Next(0, 255).ToString("X2") + 
                                 r.Next(0, 255).ToString("X2");
            try
            {
                demoGrid.Background = 
                    (SolidColorBrush)(
                        new BrushConverter().ConvertFrom(randomColor));
            }
            catch (Exception e)
            {

                //Console.WriteLine(e.StackTrace);
            }
            
        }

        private void buttonAsyncWork_Click(object sender, RoutedEventArgs e)
        {
            resultBrush = Brushes.Green;

            DoStuffAsync(); // Async

            //MyStuff(); // Not Async

            MessageBox.Show("Wait this green square will be yellow!");
        }

        static async void DoStuffAsync()
        {

            int t = await Task.Run(() => MyStuff());

        }

        private static int MyStuff()
        {

            // heavy stuff!

            // Compute total count of digits in strings.
            int size = 0;
            for (int z = 0; z < 100; z++)
            {
                for (int i = 0; i < 1000000; i++)
                {


                    string value = i.ToString();
                    if (value == null)
                    {
                        return 0;
                    }
                    size += value.Length;
                }
            }

            resultBrush = Brushes.Gold;
            return size;
        }
    }
}
