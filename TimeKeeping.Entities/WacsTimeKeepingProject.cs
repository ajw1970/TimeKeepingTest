using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeltyAutomation.TimeKeeping.Entities
{
    [Table("WacsTimeKeepingProjects")]
    public class WacsTimeKeepingProject
    {
        [Key]
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string SaleNo { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public decimal OpeningHours { get; set; }
        public decimal NpdHours { get; set; }

        public string FormattedDescription {
            get {
                if (String.IsNullOrEmpty(SaleNo))
                {
                    return Description;
                }
                return SaleNo.TrimEnd() + ": " + Description.TrimEnd();
            }
        }
    }
}
