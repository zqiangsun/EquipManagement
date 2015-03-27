using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EquipManagement.Models
{
    public class Equipment
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "设备名称")]
        public string Name { get; set; }
        [Display(Name = "设备类型")]
        public virtual EquipmentType Type { get; set; }
        public int TypeId { get; set; }
        [Display(Name = "购置日期")]
        public DateTime PurchaseDate { get; set; }
        [Display(Name = "图片")]
        public byte[] Image { get; set; }
        public byte[] Thumb { get; set; }
        [Display(Name = "所有人")]
        public virtual ApplicationUser Owner { get; set; }
        public string OwnerId { get; set; }
        //public int ApplicantId { get; set; }
        //[Display(Name = "使用人")]
        //public virtual ApplicationRecord Applicant { get; set; }
        [Display(Name = "使用记录")]
        public virtual ICollection<ApplicationRecord> Records { get; set; }

    }
}