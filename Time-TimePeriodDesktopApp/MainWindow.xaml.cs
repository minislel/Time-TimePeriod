using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using TimePeriodLibrary;
namespace Time_TimePeriodDesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window 
    {

        DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer(priority:DispatcherPriority.Send);
        public List<Time> Clocks = new List<Time>();
        public Time CurrentClock { get; set; }
        public bool CurrentClockSet = false;
        public bool Addition;

        public MainWindow()
        {
            InitializeComponent();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Start();
        }

        private void Add_New_Clock(object sender, RoutedEventArgs e)
        {
            
            Welcome.Visibility = Visibility.Hidden;
            Ellipse clockDisplay = this.FindName("Clock") as Ellipse;
            clockDisplay.Visibility = Visibility.Visible;
            Popup1.IsOpen = true;
            

        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void Cancel_Popup_Click(object sender, RoutedEventArgs e)
        {
            
            Popup1.IsOpen = false;

            hh.Text= string.Empty;
            mm.Text= string.Empty;
            ss.Text= string.Empty;
        }
        private void newClock(byte HH, byte MM, byte SS)
        {
            
            

            Time clock = new Time(HH, MM, SS);
            Clocks.Add(clock);
            CurrentClock = clock;
            TimeDisplayed.Content = clock.ToString();
            Button button = new Button();
            button.Height = 30;
            button.DataContext = clock;
            Binding binding = new Binding();
            binding.Path = new PropertyPath("");
            binding.StringFormat = "{}{0}";
            button.SetBinding(Button.ContentProperty, binding);
            button.Click += new RoutedEventHandler(DisplayClock);
            timeList.Children.Add(button);
            CurrentClockSet = true;
        }
        private void DisplayClock(object sender, RoutedEventArgs e)
        {
            
            Button senderButton = sender as Button;

            if (senderButton.DataContext is Time time) 
            {
               CurrentClock = time;
            }
            TimeDisplayed.Content = CurrentClock.ToString();

        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            const int constantInterval = 1000;
            var now = DateTime.Now;
            var nowMilliseconds = (int)now.TimeOfDay.TotalMilliseconds;
            var timerInterval = constantInterval - nowMilliseconds % constantInterval + 5;
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(timerInterval);
            int dd = Clocks.Count;
            for (int i = 0; i < Clocks.Count; i++)
            {
                Clocks[i] = Clocks[i] + new TimePeriod(1);
            }
            
            foreach (Button button in timeList.Children)
            {
                if (button.DataContext is Time time)
                {
                    time = time + new TimePeriod(1);
                    button.DataContext = time;
                    
                }
            }
            if(CurrentClockSet == true)
            {
                CurrentClock = CurrentClock + new TimePeriod(1);
                TimeDisplayed.Content = CurrentClock.ToString();
                hourHand.RenderTransform = new RotateTransform(-90+(CurrentClock.NumberOfSecondsFromMidnight / 3600));
                minuteHand.RenderTransform = new RotateTransform(-90 + (CurrentClock.Minutes));
                secondHand.RenderTransform = new RotateTransform(-90 + (CurrentClock.Seconds));


            }

        }
        private void OK_Popup_Click(object sender, RoutedEventArgs e)
        {
            byte HHByte = hh.Text == string.Empty ? (byte)0 : byte.Parse(hh.Text);
            byte MMByte = mm.Text == string.Empty ? (byte)0 : byte.Parse(mm.Text);
            byte SSByte = ss.Text == string.Empty ? (byte)0 : byte.Parse(ss.Text);
            newClock(HHByte, MMByte, SSByte);
            Popup1.IsOpen = false;
            hh.Text = string.Empty;
            mm.Text = string.Empty;
            ss.Text = string.Empty;
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Add_TimePeriod(object sender, RoutedEventArgs e)
        {
            Popup2.IsOpen = true;
            Addition = true;
        }
        private void OK_Popup_TP_Click(object sender, RoutedEventArgs e) 
        {
            
            byte HHByte = hhTP.Text == string.Empty ? (byte)0 : byte.Parse(hhTP.Text);
            byte MMByte = mmTP.Text == string.Empty ? (byte)0 : byte.Parse(mmTP.Text);
            byte SSByte = ssTP.Text == string.Empty ? (byte)0 : byte.Parse(ssTP.Text);
            if (Addition) 
            { CurrentClock = CurrentClock + new TimePeriod(HHByte, MMByte, SSByte); }
            else { CurrentClock = CurrentClock - new TimePeriod(HHByte, MMByte, SSByte); }
            Popup2.IsOpen = false;
            hhTP.Text = string.Empty;
            mmTP.Text = string.Empty;
            ssTP.Text = string.Empty;
        }
        private void Cancel_Popup_TP_Click(object sender, RoutedEventArgs e)
        {

            Popup2.IsOpen = false;

            hh.Text = string.Empty;
            mm.Text = string.Empty;
            ss.Text = string.Empty;
        }

        private void Subtract_TimePeriod(object sender, RoutedEventArgs e)
        {
            Popup2.IsOpen = true;
            Addition = false;
        }
    }
}