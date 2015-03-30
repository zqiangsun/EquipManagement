using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EquipManagement.Models
{
    public class ApplicationRecord
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public virtual Equipment Equipment { get; set; }
        [Display(Name="申请人")]
        [Required]
        public string Applicant { get; set; }
        [Display(Name="电子邮箱")]
        [EmailAddress]
        [Required]
        public string Contact { get; set; }
        [Display(Name="班级")]
        [Required]
        public string StudentInfo { get; set; }
        [Display(Name="申请日期")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime ApplicationDate { get; set; }
        [Display(Name="归还日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime ReturnDate { get; set; }
        [Display(Name="审批通过")]
        public bool IsApprove { get; set; }
    
    }
}