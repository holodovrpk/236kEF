using _236kEF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace _236kEF
{
    /// <summary>
    /// Логика взаимодействия для GroupsPage.xaml
    /// </summary>
    public partial class GroupsPage : Page
    {
        CollegeContext db = new CollegeContext();

        ObservableCollection<Group> groups = new ObservableCollection<Group>();
        public GroupsPage()
        {
            InitializeComponent();

            db.Groups.Load();
            groups = db.Groups.Local.ToObservableCollection();

            GroupTable.ItemsSource = groups;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }
    }
}
