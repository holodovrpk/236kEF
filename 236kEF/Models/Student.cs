using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _236kEF.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        [MaxLength(70)]
        public string FIO { get; set; }
        public int? YearB {  get; set; }
        [MaxLength(20)]
        public string? Phone { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
