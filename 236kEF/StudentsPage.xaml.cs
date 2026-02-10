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
    }
}
