using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _236kEF.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(70)]
        public string? Curator { get; set; }
        public int? Course {  get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
