using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeetupAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeetupAPI.Models;

namespace MeetupAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/meetup")]
    public class MeetupController : BaseController
    {
        private readonly MeetupAPIContext _context;

        public MeetupController(MeetupAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetMeetup()
        {
            var meetups = await _context.Meetup.OrderBy(c=>c.Data).ToListAsync();
            return Response(meetups);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMeetup([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return Response();
            }

            var meetup = await _context.Meetup.SingleOrDefaultAsync(m => m.Id == id);

            if (meetup == null)
            {
                ModelState.AddModelError(string.Empty, "Meetup não encontrado.");
                return Response();
            }

            return Response(meetup);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeetup([FromRoute] int id, [FromBody] Meetup meetup)
        {
            if (!ModelState.IsValid)
            {
                return Response();
            }

            if (id != meetup.Id)
            {
                ModelState.AddModelError(string.Empty, "O ID do Meetup não é valido");
                return Response();
            }

            if (meetup.Data < DateTime.Now)
            {
                ModelState.AddModelError(string.Empty, "A data do Meetup deve ser no futuro");
                return Response();
            }

            _context.Entry(meetup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeetupExists(id))
                {
                    ModelState.AddModelError(string.Empty, "Meetup não encontrado.");
                    return Response();
                }
                else
                {
                    throw;
                }
            }

            return Response();
        }

        [HttpPost]
        public async Task<IActionResult> PostMeetup([FromBody] Meetup meetup)
        {
            if (!ModelState.IsValid)
            {
                return Response();
            }

            if (meetup.Data < DateTime.Now)
            {
                ModelState.AddModelError(string.Empty, "A data do Meetup deve ser no futuro");
                return Response();
            }

            _context.Meetup.Add(meetup);
            await _context.SaveChangesAsync();

            return Response(_context.Meetup.Find(meetup.Id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeetup([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return Response();
            }

            var meetup = await _context.Meetup.SingleOrDefaultAsync(m => m.Id == id);
            if (meetup == null)
            {
                ModelState.AddModelError(string.Empty, "Meetup não encontrado.");
                return Response();
            }

            _context.Meetup.Remove(meetup);
            await _context.SaveChangesAsync();

            return Response(meetup);
        }

        private bool MeetupExists(int id)
        {
            return _context.Meetup.Any(e => e.Id == id);
        }
    }
}