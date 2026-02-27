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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace _236kEF
{
    /// <summary>
    /// Логика взаимодействия для StudentsPage.xaml
    /// </summary>
    public partial class StudentsPage : Page
    {
        CollegeContext db = new CollegeContext();
        ObservableCollection<Student> students = new ObservableCollection<Student>();


        public StudentsPage()
        {
            InitializeComponent();

            db.Students.Include(s => s.Group).Load();
            students = db.Students.Local.ToObservableCollection();

            StudentTable.ItemsSource = students;
            ListGroups.ItemsSource = db.Groups.ToList();

            // загрузка групп
            var dbGroups = db.Groups
                .Select(g => new GroupFilterItem { Id = g.GroupId, Name = g.Name })
                .ToList();

            // добавляем "Все" в начало
            dbGroups.Insert(0, new GroupFilterItem { Id = 0, Name = "Все" });

            FilterGroups.ItemsSource = dbGroups;


           
        }

        private void StudentTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StudentTable.SelectedItem is Student s)
            {
                DataContext = s;

            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Student s = new Student();
            AddStudentWindow w = new AddStudentWindow();
            w.DataContext = s;
            w.ListGroups.ItemsSource = db.Groups.ToList();

            w.ShowDialog();

            if (w.DialogResult == true)
            {
                students.Add(s);
                db.SaveChanges();
            }
        }

        private void SortAsc_Click(object sender, RoutedEventArgs e)
        {
            var sorted = students
                .OrderBy(s => s.FIO)
                .ToList();

            students = new ObservableCollection<Student>(sorted);
            StudentTable.ItemsSource=students;
        }

        private void SortDesc_Click(object sender, RoutedEventArgs e)
        {
            var sorted = students
                .OrderByDescending(s => s.FIO)
                .ToList();

            students = new ObservableCollection<Student>(sorted);
            StudentTable.ItemsSource = students;
        }

        private void Is18_Click(object sender, RoutedEventArgs e)
        {
            int year = DateTime.Now.Date.Year;

            var filtered = students
                .Where(s => (s.YearB + 18) < year ).ToList();

            students = new ObservableCollection<Student>(filtered);
            StudentTable.ItemsSource = students;
        }

        private void AllYears_Click(object sender, RoutedEventArgs e)
        {
            students = new ObservableCollection<Student>(
                db.Students.Local.ToList());
            StudentTable.ItemsSource = students;
        }

        private void FilterGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // students — ваша ObservableCollection, привязанная к DataGrid
            var query = db.Students.Local.ToList();

            int id_group = Convert.ToInt32(FilterGroups.SelectedValue);

            if (id_group != 0) // если не "Все"
                query = query.Where(s => s.GroupId == id_group).ToList();

            students = new ObservableCollection<Student>(query);
            StudentTable.ItemsSource = students;

        }

        private void SearchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            var result = db.Students.Local.ToList();

            if (SearchText.Text != "")
            {
                result = result
                  .Where(s => s.FIO.ToLower().Contains(SearchText.Text.ToLower())
                || s.Phone.Contains(SearchText.Text)).ToList();
            }


            students = new ObservableCollection<Student>(result);
            StudentTable.ItemsSource = students;

        }
    }
}
