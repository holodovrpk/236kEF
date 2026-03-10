using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _236kEF.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required, MaxLength(150)]
        public string FullName { get; set; }

        [Required, MaxLength(50)]
        public string Login { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        // Роль может быть пустой при регистрации
        [MaxLength(30)]
        public string? Role { get; set; }
    }

}
