using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EquipManagement.Models
{
    public class EquipmentType
    {
        [Key]       
        public int Id { get; set; }
        [DisplayName("类型名称")]
        [Required]
        [Index(IsUnique = true)]  
        [StringLength(20)]
        public string Name { get; set; }
    }
}