using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

namespace Memory_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

                
        public MainWindow()
        {
            InitializeComponent();
            start();
        }

        DispatcherTimer timer = new DispatcherTimer();
        List<int> tags = new List<int>();

        public void start()
        {
            var rand = new Random();
            int buttonAmount = 8;
            var unRandTags = new List<int>();
            for (int i = 0; i < buttonAmount/2; i++)
            {
                unRandTags.Add(i); unRandTags.Add(i);
            }
            tags = unRandTags.OrderBy(v => rand.Next()).ToList();
            timer.Interval = new TimeSpan(0,0,0,2,0);
            timer.Tick += new EventHandler(timer_tick);
        }

        Button? lastClick;
        Button click;
        bool wasMatch = false;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!timer.IsEnabled) 
            {
                click = sender as Button;

                // all button tags are default set to "-1"
                if (click.Tag.Equals("-1"))
                {
                    click.Tag = tags.Last();
                    tags.RemoveAt(tags.Count - 1);
                }

                if (lastClick == null) //first click
                {
                    click.IsEnabled = false;
                    click.Content = click.Tag.ToString();
                    lastClick = click;
                }
                else
                {
                    if (click.Tag.Equals(lastClick.Tag))
                    {
                        click.IsEnabled = false;
                        click.Content = click.Tag.ToString();
                        lastClick = null;
                    } 
                    else
                    { 
                        timer.IsEnabled = true;
                        click.IsEnabled = false;
                        click.Content = click.Tag.ToString();
                    }
                }
            }

        }

        private void timer_tick(object sender, EventArgs e)
        {
            lastClick.Content = "Button";
            lastClick.IsEnabled = true;
            click.Content = "Button";
            click.IsEnabled = true;
            lastClick = null;

            timer.IsEnabled = false;
        }


    }
}
