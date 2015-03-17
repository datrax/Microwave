using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Microwave
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Controls.Image PowerLever;
        System.Windows.Controls.Image TimeLever;
        string[] mode = { "Grill", "Pizza", "Defrosting" };
        int ChosenMode = -1;
        int Power = 100;
        int Time = 10;
        string displayInformation="";
        bool IsOpen = false;
        BitmapImage[] sources = new BitmapImage[2];
        public MainWindow()
        {
            InitializeComponent();
            Display.Foreground = System.Windows.Media.Brushes.GreenYellow;
            
        }



        private void FormLoaded(object sender, RoutedEventArgs e)
        {

            sources[0] = new BitmapImage(new Uri(Environment.CurrentDirectory + @"\1.jpg"));
            sources[1] = new BitmapImage(new Uri(Environment.CurrentDirectory + @"\2.jpg"));
            ImageBrush myBrush = new ImageBrush();
            System.Windows.Controls.Image image = new System.Windows.Controls.Image();
            image.Source = sources[0];
            myBrush.ImageSource = image.Source;
            menu.Background = myBrush;
            PowerLever = new System.Windows.Controls.Image();
            TimeLever = new System.Windows.Controls.Image();
            PowerLever.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + @"\3.png"));
            PowerLever.Width = 72;
            PowerLever.Height = 72;
            canvas.Children.Add(PowerLever);
            Canvas.SetTop(PowerLever, 377);
            Canvas.SetLeft(PowerLever, 641);

            TimeLever.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + @"\3.png"));
            TimeLever.Width = 50;
            TimeLever.Height = 50;
            canvas.Children.Add(TimeLever);

            Canvas.SetTop(TimeLever, 252);
            Canvas.SetLeft(TimeLever, 649);

        }

        private void LeftClick(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.GetPosition(menu).X < 662 && Mouse.GetPosition(menu).X > 638 && Mouse.GetPosition(menu).Y < 224 && Mouse.GetPosition(menu).Y > 203)
            {
                displayInformation = mode[++ChosenMode % 3];
                Display.Content = displayInformation;
            }
            else
                if (Mouse.GetPosition(menu).X < 721 && Mouse.GetPosition(menu).X > 698 && Mouse.GetPosition(menu).Y < 221 && Mouse.GetPosition(menu).Y > 202)
                {


                    if (IsOpen)
                    {
                        Display.Content = "Close door";                        
                    }
                    else
                    {
                        Display.Content = "Coocking";
                    }
                }
                else
                {
                    ImageBrush myBrush = new ImageBrush();
                    System.Windows.Controls.Image image = new System.Windows.Controls.Image();
                    if (IsOpen)
                    {
                        image.Source = sources[0];
                        Display.Content = displayInformation;
                    }
                    else
                    {
                        image.Source = sources[1];

                    }
                    IsOpen = !IsOpen;
                    myBrush.ImageSource = image.Source;
                    menu.Background = myBrush;
             //            MessageBox.Show(Mouse.GetPosition(menu).Y + " " + Mouse.GetPosition(menu).X);
                }
        }

        private void RotateLever(object sender, MouseWheelEventArgs e)
        {
            ChosenMode = -1;

            if (Mouse.GetPosition(menu).Y > 320)
            {
                int angle;
                if (e.Delta > 0)
                    Power += 100;
                else
                    Power -= 100;
                if (Power > 1200) Power -= 1200;
                if (Power < 100) Power = 1200;
                angle = Convert.ToInt32(Power / 100.0 * 30 - 30);
                RotateTransform rotateTransform1 =
               new RotateTransform(angle);
                rotateTransform1.CenterX = PowerLever.Width / 2;
                rotateTransform1.CenterY = PowerLever.Height / 2;

                PowerLever.RenderTransform = rotateTransform1;
            }
            else
            {
                int angle;
                if (e.Delta > 0)
                    Time += 1;
                else
                    Time -= 1;
                if (Time > 20) Time = 20;
                if (Time < 0) Time = 0;

                angle = Convert.ToInt32(Time * 9 - 90);
                RotateTransform rotateTransform1 =
               new RotateTransform(angle);
                rotateTransform1.CenterX = TimeLever.Width / 2;
                rotateTransform1.CenterY = TimeLever.Height / 2;

                TimeLever.RenderTransform = rotateTransform1;
            }
            displayInformation ="Time " +Time.ToString() + ":00\nPower "+Power.ToString();
            Display.Content = displayInformation;
        }

    }
}
