using _236kEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _236kEF
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        public static string Hash(string input)
        {
            using var sha = SHA256.Create();
            return Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(input)));
        }


        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text.Trim(); // берём логин из поля и убираем пробелы
            string password = txtPass.Text;  // берём введённый пароль

            // проверяем, что поля не пустые
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите логин и пароль.");
                return;
            }

            CollegeContext db = new CollegeContext(); // подключаемся к базе
                                                    // ищем пользователя по логину
            var user = db.Users.FirstOrDefault(u => u.Login == login );

            if (user == null) // если такого логина нет
            {
                MessageBox.Show("Пользователь не найден.");
                return;
            }

            if (Hash(password) != user.PasswordHash) // если пароль не совпадает
            {
                MessageBox.Show("Неверный пароль.");
                return;
            }

            CurrentUser.Set(user); // сохраняем данные вошедшего пользователя

            MainWindow main = new MainWindow(); // открываем главное окно
            main.Show();
            this.Close(); // закрываем окно авторизации


        }
    }
}
