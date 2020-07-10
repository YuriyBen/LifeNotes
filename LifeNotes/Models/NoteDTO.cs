using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LifeNotes.Models
{
    public class NoteDTO
    {
//        public long Id { get; set; }
        public string DateId { get; set; } //yyyMMdd
        public string Comment { get; set; }
        public byte[] Image { get; set; }
        public byte Weather { get; set; }
        public byte Mood { get; set; }
        public byte Productivity { get; set; }
        public byte General { get; set; }
        public long UserId { get; set; }
        public int Next { get; set; }
        public int Previous { get; set; }
    }
}
