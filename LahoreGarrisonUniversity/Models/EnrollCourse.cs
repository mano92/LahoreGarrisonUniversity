using System;
using System.ComponentModel.DataAnnotations;

namespace LahoreGarrisonUniversity.Models
{
    public class EnrollCourse
    {
        public int EnrollCourseId { set; get; }
        
        [Required(ErrorMessage = "Student can't be empty")]
        public int StudentId { set; get; }
        public virtual Student Student { set; get; }

        [Required(ErrorMessage = "Course can't be empty")]
        public int CourseId { set; get; }
        public virtual Course Course { set; get; }
        [Required(ErrorMessage = "Enroll date can't be empty")]
        [DataType(DataType.Date)]
        public DateTime EnrollDate { set; get; }

        public virtual string GradeName { set; get; }
    }
}