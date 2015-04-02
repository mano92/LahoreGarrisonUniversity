using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LahoreGarrisonUniversity.Models
{
    public class FrontEndCourse
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Flag { get; set; }
        public string Semester { get; set; }
        public string Duration { get; set; }
        public int CreditHourTheory { get; set; }
        public int CreditHourPractical { get; set; }
        public string Level { get; set; }
        public string StudentLevel { get; set; }
        public string Description { get; set; }
        public string CourseStructure { get; set; }
        public string ImageUrl { get; set; }
        public string RecommendedBooks { get; set; }
        public string PrerequsiteCourse { get; set; }
    }
}