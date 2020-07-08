using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LifeNotes.Models
{
    public class NoteDTO
    {
        public long Id { get; set; }
        public string OnlyDate { get; set; }
        public string DayOfWeek { get; set; }
        public string Comment { get; set; }
        public byte[] ImageData { get; set; }
        public byte WeatherCriteriaRating { get; set; }
        public byte MoodCriteriaRating { get; set; }
        public byte ProductivityCriteriaRating { get; set; }
        public byte GenerallCriteriaRating { get; set; }
    }
}
