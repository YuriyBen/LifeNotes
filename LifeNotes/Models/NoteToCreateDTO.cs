using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LifeNotes.Models
{
    public class NoteToCreateDTO
    {
        public DateTime DateInfo { get; set; } = DateTime.Now;
        public string Comment { get; set; }
        public byte[] ImageData { get; set; }

        [Range(1,5)]
        public byte WeatherCriteriaRating { get; set; }
        [Range(1, 5)]
        public byte MoodCriteriaRating { get; set; }
        [Range(1, 5)]
        public byte ProductivityCriteriaRating { get; set; }
        [Range(1, 5)]
        public byte GenerallCriteriaRating { get; set; }
    }
}
