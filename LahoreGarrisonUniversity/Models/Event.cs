using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace LahoreGarrisonUniversity.Models
{

    [Bind(Exclude = "Id, CreatedAt")]
    public partial class Event
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title Required!")]
        [DataType(DataType.Text)]
        [DisplayName("Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description Required!")]
        [DataType(DataType.Text)]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Attandess Required!")]
        [DataType(DataType.Text)]
        [DisplayName("For Whome")]
        public string ForWhome { get; set; }

        [Required(ErrorMessage = "Location Required!")]
        [DataType(DataType.Text)]
        [DisplayName("Location")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Duration Required!")]
        [DisplayName("Duration")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "Image Required!")]
        [DataType(DataType.Text)]
        [DisplayName("Media Url")]
        public string MediaUrl { get; set; }

        [Required(ErrorMessage = "Start Date Required!")]
        [DisplayName("Start Date")]
        [DataType(DataType.DateTime)]
        public Nullable<DateTime> StartDate { get; set; }

        [DataType(DataType.DateTime)]
        public Nullable<DateTime> CreatedAt { get; set; }

        [DisplayName("User Name")]
        public string UserName { get; set; }
    }
}
