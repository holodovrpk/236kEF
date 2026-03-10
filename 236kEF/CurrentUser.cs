using _236kEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _236kEF
{
    public static class CurrentUser
    {
        public static int UserId { get; set; }
        public static string FullName { get; set; }
        public static string Login { get; set; }
        public static string? Role { get; set; }

        public static void Set(User user)
        {
            UserId = user.UserId;
            FullName = user.FullName;
            Login = user.Login;
            Role = user.Role;
        }
    }

}
