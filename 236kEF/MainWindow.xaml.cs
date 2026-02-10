using _236kEF.Models;
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

namespace _236kEF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CollegeContext db = new CollegeContext();
        public MainWindow()
        {
            InitializeComponent();

            
        }

        private void Group_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new GroupsPage());
        }

        private void Student_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new StudentsPage());
        }
    }
}