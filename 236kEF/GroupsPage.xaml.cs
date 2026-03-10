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
            GroupItem.ItemsSource = groups;

            if (CurrentUser.Role == "Пользователь")
            {
                bAdd.Visibility = Visibility.Collapsed;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }

        private void Plitka_Click(object sender, RoutedEventArgs e)
        {
            GroupItem.Visibility = Visibility.Visible;
            GroupTable.Visibility = Visibility.Collapsed;
        }

        private void Table_Click(object sender, RoutedEventArgs e)
        {
            GroupItem.Visibility = Visibility.Collapsed;
            GroupTable.Visibility = Visibility.Visible;
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).DataContext is Group g)
            {

                var r = MessageBox.Show($"Удалить группу {g.Name}?", "Удаление", 
                    MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (r == MessageBoxResult.Yes)
                {
                    groups.Remove(g);
                    db.SaveChanges();
                }
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).DataContext is Group g)
            {
                AddGroupWindow w = new AddGroupWindow();
                w.DataContext = g;
                w.ShowDialog();
                db.SaveChanges();
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Group g = new Group();
            AddGroupWindow w = new AddGroupWindow();
            w.DataContext = g;

            if (w.ShowDialog() == true)
            {
                groups.Add(g);
                db.SaveChanges();
            }
        }
    }
}
