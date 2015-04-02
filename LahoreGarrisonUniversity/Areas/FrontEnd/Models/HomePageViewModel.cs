using System.Collections.Generic;
using LahoreGarrisonUniversity.Models;

namespace LahoreGarrisonUniversity.Areas.FrontEnd.Models
{
    public class HomePageViewModel
    {
        public IEnumerable<Event> Events { get; set; }
        public IEnumerable<Testimonial> Testimonial { get; set; }
        public IEnumerable<News> News { get; set; } 
    }
}