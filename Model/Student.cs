using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentTeendanceBackend.Model
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Roll { get; set; }
        public string Phone { get; set; }
        public string Session { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
    }
}
