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

        [DisplayName("Title")]
        public string Title { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("For Whome")]
        public string ForWhome { get; set; }

        [DisplayName("Location")]
        public string Location { get; set; }

        [DisplayName("Duration")]
        public int Duration { get; set; }

        [DisplayName("Media Url")]
        public string MediaUrl { get; set; }

        [DisplayName("Start Date")]
        [DataType(DataType.DateTime)]
        public Nullable<DateTime> StartDate { get; set; }

        [DataType(DataType.DateTime)]
        public Nullable<DateTime> CreatedAt { get; set; }

        [DisplayName("User Name")]
        public string UserName { get; set; }
    }
}
