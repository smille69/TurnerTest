using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AngularJSWebApiEmpty;
using AngularJSWebApiEmpty.Models;

namespace AngularJSWebApiEmpty.Controllers
{
    public class TitlesController : ApiController
    {
        private TitlesDataEntities db = new TitlesDataEntities();

        // GET api/Titles
        public IEnumerable<TitleModel> GetTitles()
        {
            using (var db = new TitlesDataEntities())
            {
                var titles = db.Titles.Select(r => new TitleModel
                {
                    TitleId = r.TitleId,
                    TitleName = r.TitleName,
                    TitleNameSortable = r.TitleNameSortable,
                    TitleTypeId = r.TitleTypeId,
                    ReleaseYear = r.ReleaseYear.Value,
                    ProcessDateTimeUTC = r.ProcessedDateTimeUTC.Value
                }).ToList();
                return titles;
            }
        }

        // GET api/Titles/5
        public TitleModel GetTitle(int id)
        {
            var title = db.Titles.Where(x => x.TitleId == id).Select(r => new TitleModel()
            {
                TitleId = r.TitleId,
                TitleName = r.TitleName,
                TitleNameSortable = r.TitleNameSortable,
                TitleTypeId = r.TitleTypeId,
                ReleaseYear = r.ReleaseYear.Value,
                ProcessDateTimeUTC = r.ProcessedDateTimeUTC.Value,
                GenreName = r.TitleGenres.FirstOrDefault(x => x.TitleId == r.TitleId).Genre.Name
            }).SingleOrDefault();


            return title;
        }

        //GET api/Search/{searchfor}
        [HttpGet]
        public IEnumerable<TitleModel> Search(string searchfor)
        {
            using (var db = new TitlesDataEntities())
            {
                var titles = db.Titles.Where(x => x.TitleName.Contains(searchfor)).Select(r => new TitleModel()
                {
                    TitleId = r.TitleId,
                    TitleName = r.TitleName,
                    TitleNameSortable = r.TitleNameSortable,
                    TitleTypeId = r.TitleTypeId,
                    ReleaseYear = r.ReleaseYear.Value,
                    ProcessDateTimeUTC = r.ProcessedDateTimeUTC.Value
                }).ToList();

                return titles;
            }
        }

        // PUT api/Titles/5
        public IHttpActionResult PutTitle(int id, Title title)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != title.TitleId)
            {
                return BadRequest();
            }

            db.Entry(title).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TitleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Titles
        [ResponseType(typeof(Title))]
        public IHttpActionResult PostTitle(Title title)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Titles.Add(title);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TitleExists(title.TitleId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = title.TitleId }, title);
        }

        // DELETE api/Titles/5
        [ResponseType(typeof(Title))]
        public IHttpActionResult DeleteTitle(int id)
        {
            Title title = db.Titles.Find(id);
            if (title == null)
            {
                return NotFound();
            }

            db.Titles.Remove(title);
            db.SaveChanges();

            return Ok(title);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TitleExists(int id)
        {
            return db.Titles.Count(e => e.TitleId == id) > 0;
        }
    }
}