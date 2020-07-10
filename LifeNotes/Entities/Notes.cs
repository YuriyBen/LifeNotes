using System;
using System.Collections.Generic;

namespace LifeNotes.Entities
{
    public partial class Notes
    {
        public long Id { get; set; }
        public int DateId { get; set; }
        public string Comment { get; set; }
        public byte[] ImageData { get; set; }
        public byte Weather { get; set; }
        public byte Mood { get; set; }
        public byte Productivity { get; set; }
        public byte Generall { get; set; }
        public long UserId { get; set; }

        public virtual Users User { get; set; }
    }
}
