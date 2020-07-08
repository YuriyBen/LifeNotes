using System;
using System.Collections.Generic;

namespace LifeNotes.Entities
{
    public partial class Note
    {
        public long Id { get; set; }
        public DateTime DateInfo { get; set; }
        public string Comment { get; set; }
        public byte[] ImageData { get; set; }
        public byte WeatherCriteriaRating { get; set; }
        public byte MoodCriteriaRating { get; set; }
        public byte ProductivityCriteriaRating { get; set; }
        public byte GenerallCriteriaRating { get; set; }
        public long UserId { get; set; }

        public virtual UserInfo User { get; set; }
    }
}
