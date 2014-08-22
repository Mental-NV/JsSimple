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
using PasswordGenerator.Models;

namespace PasswordGenerator.Controllers
{
    public class FeedbacksController : ApiController
    {
        private FeedbackContext db = new FeedbackContext();

        // GET: api/Feedbacks
        public IQueryable<Feedback> GetFeedbackSet()
        {
            return db.FeedbackSet;
        }

        // GET: api/Feedbacks/5
        [ResponseType(typeof(Feedback))]
        public IHttpActionResult GetFeedback(int id)
        {
            Feedback Feedback = db.FeedbackSet.Find(id);
            if (Feedback == null)
            {
                return NotFound();
            }

            return Ok(Feedback);
        }

        // PUT: api/Feedbacks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFeedback(int id, Feedback Feedback)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Feedback.FeedId)
            {
                return BadRequest();
            }

            db.Entry(Feedback).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedbackExists(id))
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

        // POST: api/Feedbacks
        [ResponseType(typeof(Feedback))]
        public IHttpActionResult PostFeedback(Feedback Feedback)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FeedbackSet.Add(Feedback);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Feedback.FeedId }, Feedback);
        }

        // DELETE: api/Feedbacks/5
        [ResponseType(typeof(Feedback))]
        public IHttpActionResult DeleteFeedback(int id)
        {
            Feedback Feedback = db.FeedbackSet.Find(id);
            if (Feedback == null)
            {
                return NotFound();
            }

            db.FeedbackSet.Remove(Feedback);
            db.SaveChanges();

            return Ok(Feedback);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FeedbackExists(int id)
        {
            return db.FeedbackSet.Count(e => e.FeedId == id) > 0;
        }
    }
}