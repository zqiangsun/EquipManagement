using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EquipManagement.Models
{
    public class ApplicationRecord
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; }
        public string Applicant { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string IsApprove { get; set; }
        public string Conclusion { get; set; }        
    }
}