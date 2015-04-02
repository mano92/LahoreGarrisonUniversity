using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LahoreGarrisonUniversity.Models
{
    public class Course
    {
        public Course()
        {
            this.CourseTeacher=new Collection<Teacher>();
        }
        public int CourseId { get; set; }
        [Required(ErrorMessage = "There must be a Course Code\nSample: CSE-***")]
        [Remote("CheckCodeUnique", "Courses", ErrorMessage = "Name must be Unique")]
        public String Code { get; set; }
        [Required(ErrorMessage = "There must be a Course Name")]
        [Remote("CheckNameUnique","Courses",ErrorMessage = "Name must be Unique")]
        public String Name { get; set; }
        [Required(ErrorMessage = "Required Field Must Be Filled")]
        public float Credit { get; set; }
        public String Description{ get; set; }
        [Required(ErrorMessage = "There must be a semester")]
        public int SemesterId { get; set; }
        public virtual Semester CourseSemester { get; set; }
         [Required(ErrorMessage = "There must be a Department")]
        public int DepartmentId { get; set; }
        public virtual Department CourseDepartment{ get; set; }
        public virtual String AssignTo { get; set; }
        public virtual ICollection<Teacher> CourseTeacher{ get; set; }
        public virtual ICollection<RoomAllocation> RoomAllocationList  { get; set; }
    }
}