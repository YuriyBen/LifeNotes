using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LifeNotes.Models
{
    public class NoteToCreateDTO
    {
        [Required]
        public string Comment { get; set; }
        public byte[] ImageData { get; set; }

        [Range(1,5)]
        public byte Weather { get; set; }
        [Range(1, 5)]
        public byte Mood { get; set; }
        [Range(1, 5)]
        public byte Productivity { get; set; }
        [Range(1, 5)]
        public byte General { get; set; }
    }
}
