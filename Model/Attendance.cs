using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentTeendanceBackend.Model
{
    public class Attendance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string AdminId { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime  { get; set; }
        public DateTime EndTime { get; set; }
    }
}
