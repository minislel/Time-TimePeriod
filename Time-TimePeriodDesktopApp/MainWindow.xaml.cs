using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimePeriodLibrary;
namespace Time_TimePeriodDesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window 
    {
        public string TimeDisplayed
        {
            get { return (string)GetValue(TimeDisplayedProperty); }
            set { SetValue(TimeDisplayedProperty, value); }
        }
        public static readonly DependencyProperty TimeDisplayedProperty =
        DependencyProperty.Register("MyProperty2", typeof(string), typeof(Window), new UIPropertyMetadata(string.Empty));
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Add_New_Clock(object sender, RoutedEventArgs e)
        {
            StackPanel timeList = this.FindName("timeList") as StackPanel;

            
            Time clock = new Time(21, 1, 1);
            TimeDisplayed = clock.ToString();
            Button button = new Button();
            button.Height = 30;
            button.Content = TimeDisplayed;
            //button.Click = "GetClock()";
            timeList.Children.Add(button);





        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {

        }
    }
}