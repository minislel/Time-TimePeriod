using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
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
        public byte HH;
        public byte MM;
        public byte SS;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Add_New_Clock(object sender, RoutedEventArgs e)
        {
           
           Time clock = new Time();
            HH = 32;
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {

        }
    }
}