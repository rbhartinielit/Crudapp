using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Crudapp.Models
{
    public class Employee
    {
        [Key]
        public int id { get; set; }

        [Required (ErrorMessage ="required")]
        public string ename { get; set; }

        [Required(ErrorMessage = "required")]
        public string fname { get; set; }


        [Required(ErrorMessage = "required")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }


        [Required(ErrorMessage = "required")]
        public string mobile { get; set; }

        [Required(ErrorMessage = "required")]
        public string descr { get; set; }

        public int deptid { get; set; }
        [NotMapped]
        public string Department { get; set; }
    }
}
