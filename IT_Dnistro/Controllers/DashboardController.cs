﻿using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DataBase;
using Microsoft.AspNetCore.Mvc;
using IT_Dnistro.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace IT_Dnistro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")]
    public class DashboardController : Controller
    {
        private readonly DatabaseContext _context;
        IWebHostEnvironment _appEnvironment;
        //private int countParticipant = 0;
        public static int IdTour;

        public DashboardController(DatabaseContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        [HttpGet]
        [Route("tour-id")]
        public ActionResult GetTourId(int idTour)
        {
            IdTour = idTour;
            return RedirectToAction("GetParticipants");
        }

        [HttpGet("participant")]
        public IActionResult GetParticipants()
        {
            var tourType = _context.TourTypes.Find(IdTour);
            if (tourType == null)
            {
                var tourTypeFirst = _context.TourTypes.FirstOrDefault()?.Id; //1
                if (tourTypeFirst != null)
                {
                    IdTour = tourTypeFirst.Value;
                }
            }

            var type = tourType ?? _context.TourTypes.Find(IdTour);
            if (type?.Id != null)//4
            {
                DateTime dateTimes = type.TourDateFrom;
                TimeSpan diff = dateTimes - DateTime.Now;
                ViewBag.Time = diff.Days;
                ViewBag.TourName = type.TourTypeName;
            }

            //foreach (var count in _context.Participants)
            //{
            //    countParticipant++;
            //}
            ViewBag.Count = _context.Participants.Count();


            var items = _context.Participants
                .Where(d => d.TourTypeId == IdTour)
                .Select(x => new ParticipantsViewModel()
            {
                Id = x.Id,
                FullName = x.FullName,
                EMail = x.EMail,
                PhoneNumber = x.PhoneNumber,
                TourName = x.TourType.TourTypeName
            }).ToList();

            return View(items);
        }

        [HttpGet("add")]
        public IActionResult AddParticipant()
        {
            ViewData["TourTypeId"] = new SelectList(_context.TourTypes, "Id", "TourTypeName");
            return View();
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddParticipant([FromForm]ParticipantsViewModel model)
        {
            if (ModelState.IsValid)
            {
                Participant participant = new Participant()
                {
                    Id = model.Id,
                    FullName = model.FullName,
                    EMail = model.EMail,
                    PhoneNumber = model.PhoneNumber,
                    TourTypeId = IdTour
                };
                _context.Add(participant);
                var tourType = await _context.TourTypes.FindAsync(IdTour);
                tourType.Amount++;

                await _context.SaveChangesAsync().ConfigureAwait(true);

                return RedirectToAction(nameof(GetParticipants));
            }
            return View(model);
        }

        [HttpGet("edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["TourTypeId"] = new SelectList(_context.TourTypes, "Id", "TourTypeName");
            if (id == null)
            {
                return NotFound();
            }

            var Participant = await _context.Participants.FindAsync(id);
            if (Participant == null)
            {
                return NotFound();
            }

            return View(Participant);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> Edit([FromForm]int id, [FromForm]Participant participant)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(participant);
                    await _context.SaveChangesAsync().ConfigureAwait(true);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(GetParticipants));
            }
            return View(participant);
        }

        [HttpGet("delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return StatusCode((int)HttpStatusCode.NotAcceptable);
            }

            var participant = await _context.Participants.FindAsync(id).ConfigureAwait(true);
            if (participant == null)
            {
                return NotFound();
            }

            return View(participant);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteConfirmed([FromForm]int id)
        {
            var participant = await _context.Participants.FindAsync(id);
            _context.Participants.Remove(participant);
            var TourType = await _context.TourTypes.FindAsync(participant.TourTypeId);
            if (TourType.Amount != 0)
            {
                TourType.Amount--;
            }
            await _context.SaveChangesAsync().ConfigureAwait(true);
            return RedirectToAction(nameof(GetParticipants));
        }
    }
}