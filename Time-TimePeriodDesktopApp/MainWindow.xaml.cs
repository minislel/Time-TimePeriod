using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public partial class MainWindow : Window
    {
        DispatcherTimer dispatcherTimerSW = new System.Windows.Threading.DispatcherTimer(priority: DispatcherPriority.Send);
        DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer(priority:DispatcherPriority.Send);
        public ObservableCollection<Time> Clocks { get; set; } = new ObservableCollection<Time>();
        public ObservableCollection<TimePeriod> Timers { get; set; } = new ObservableCollection<TimePeriod>();
        
        public bool CurrentClockSet = false;
        public bool Addition;
        public TimePeriod SW;
        public bool SWRunning;
        
        private Time _currentClock;
        public Time CurrentClock
        {
            get { return Clocks.FirstOrDefault(c => c.Id == CurrentClockID); }
            set
            {
                    _currentClock = value;
            }
        }
        public int CurrentClockID { get; set; }

        private TimePeriod _currentStopWatch;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Start();
            dispatcherTimerSW.Tick += new EventHandler(dispatcherTimerSW_Tick);
            dispatcherTimerSW.Interval = TimeSpan.FromMilliseconds(1);
            dispatcherTimerSW.Start();
            SW = new TimePeriod(0);

        }
        private void Add_New_Stopwatch(object sender, RoutedEventArgs e)
        { 
            
        
        }
        private void Add_New_Clock(object sender, RoutedEventArgs e)
        {
            Clock.Visibility = Visibility.Visible;
            TimeDisplayed.Visibility = Visibility.Visible;
            hourHand.Visibility = Visibility.Visible;
            minuteHand.Visibility = Visibility.Visible;
            secondHand.Visibility = Visibility.Visible;
            addTP.Visibility = Visibility.Visible;
            subtractTP.Visibility = Visibility.Visible;
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
            CurrentClockID = clock.Id;
            CurrentClockSet = true;
        }
        private void DisplayClock(object sender, RoutedEventArgs e)
        {
            Button senderButton = sender as Button;
            if (senderButton.Tag is int clockId)
            {
                CurrentClock = Clocks.FirstOrDefault(c => c.Id == clockId);
                CurrentClockID = clockId;
            }
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            const int constantInterval = 1000;
            var now = DateTime.Now;
            var nowMilliseconds = (int)now.TimeOfDay.TotalMilliseconds;
            var timerInterval = constantInterval - nowMilliseconds % constantInterval + 5;
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(timerInterval);
            for (int i = 0; i < Clocks.Count; i++)
            {
                Clocks[i] = Clocks[i] + new TimePeriod(1);
            }

            if(CurrentClockSet)
            {
                TimeDisplayed.Content = Clocks.FirstOrDefault(c=> c.Id == CurrentClockID);
                hourHand.RenderTransform = new RotateTransform(-90+((CurrentClock.Hours)*30) + (CurrentClock.Minutes / 2.0));
                minuteHand.RenderTransform = new RotateTransform(-90 + (CurrentClock.Minutes)*6 + (CurrentClock.Seconds / 10.0));
                secondHand.RenderTransform = new RotateTransform(-90 + (CurrentClock.Seconds*6));
            }
        }
        private void dispatcherTimerSW_Tick(object sender, EventArgs e) 
        {
            if (SWRunning)
            {
                SW.MSTick();

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
            Time newclock;
            if (Addition) 
            {  newclock = Clocks.FirstOrDefault(c => c.Id == CurrentClockID) + new TimePeriod(HHByte, MMByte, SSByte); }
            else { newclock = Clocks.FirstOrDefault(c => c.Id == CurrentClockID) - new TimePeriod(HHByte, MMByte, SSByte); }
            newclock.Id = CurrentClockID;
            Clocks.Remove(Clocks.FirstOrDefault(c => c.Id == CurrentClockID));
            Clocks.Add(newclock);
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

        private void SWReset_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SWStart_Click(object sender, RoutedEventArgs e)
        {
            
        }


        private void DisplayTimer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addNewTimer_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}