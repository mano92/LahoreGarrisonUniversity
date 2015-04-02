
using System;

namespace LahoreGarrisonUniversity.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department ADepartment { get; set; }
        public int StudentId { get; set; }
        public virtual Student AStudent { get; set; }

        public DateTime DateTime { get; set; }
        public String Result{ get; set; }



    }
}