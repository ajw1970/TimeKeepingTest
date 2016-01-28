using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeltyAutomation.M2M.QueryData {
    [Table("WacsTimeKeepingUsers")]
    public class WacsTimeKeepingUser
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; }
        public string DisplayName { get; set; }
        public string Department { get; set; }
        public bool IsDepartmentAdmin { get; set; }
    }
}
