using LifeNotes.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace LifeNotes.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/statistics")]
    public class StatisticsController : ControllerBase
    {
        private readonly LifeNotesContext _context;

        public StatisticsController(LifeNotesContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        [HttpGet]
        public IActionResult GetStatistics([FromQuery] long userId, [FromQuery] string criteria, [FromQuery] int numberOfDays)
        {
            List<Coordinates> statistics = new List<Coordinates>();

            int notesCount = _context.Notes.ToList().Count;
            int elemsToSkip = notesCount > numberOfDays ? notesCount - numberOfDays : 0;

            var arrayOfNotes = _context.Notes.Where(u => u.UserId == userId).Skip(elemsToSkip).ToList();
            criteria = criteria == "general" ? "generall" : criteria;
            criteria = criteria.Replace(criteria[0], Char.ToUpper(criteria[0]));
            foreach (var item in arrayOfNotes)
            {
                var value = item.GetType().GetProperty(criteria).GetValue(item);
                int _Y = Convert.ToInt32(value);
                statistics.Add(
                    new Coordinates()
                    {
                        X = item.DateId.ToString(),
                        Y = _Y
                    });
            }
            return Ok(statistics);


        }
        
    }
    public struct Coordinates
    { 
        public string X { get; set; } //DateId
        public int Y { get; set; } //Value
    }
}
