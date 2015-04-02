using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LahoreGarrisonUniversity.Models
{
    public class Department
    {
        public int DepartmentId{ get; set; }
        [Required(ErrorMessage = "You must given an valid Department Code")]
        [Display(Name = "Department Code")]
        [Remote("CheckCode","Department",ErrorMessage = "This code Already Exist")]
      
        public String Code { get; set; }
         [Required(ErrorMessage = "Name field must be filled")]
         [Display(Name = "Department name")]
       [Remote("CheckName", "Department", ErrorMessage = "This code Already Exist")]
        public String Name { get; set; }



       

    }
}