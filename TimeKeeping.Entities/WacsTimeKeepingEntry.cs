using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeltyAutomation.TimeKeeping.Entities
{
    [Table("WacsTimeKeepingEntries")]
    public class WacsTimeKeepingEntry
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public DateTimeOffset Started { get; set; }
        public DateTimeOffset Ended { get; set; }
    }
}
