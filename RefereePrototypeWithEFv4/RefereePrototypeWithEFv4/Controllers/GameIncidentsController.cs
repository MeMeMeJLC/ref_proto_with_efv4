using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RefereePrototypeWithEFv4.Models;

namespace RefereePrototypeWithEFv4.Controllers
{
    public class GameIncidentsController : ApiController
    {
        private RefereePrototypeWithEFv4Context db = new RefereePrototypeWithEFv4Context();

        // GET: api/GameIncidents
        public IQueryable<GameIncidentDTO> GetGameIncidents()
        {
            var gameIncidents = from g in db.GameIncidents
                              select new GameIncidentDTO()
                              {
                                  Id = g.Id,
                                  IncidentType = g.IncidentType,
                                  IncidentTime = g.IncidentTime,
                                  GamePlayerFirstName = g.GamePlayer.FirstName,
                                  GamePlayerLastName = g.GamePlayer.LastName
                              };
            return gameIncidents;
        }

        // GET: api/GameIncidents/5
        [ResponseType(typeof(GameIncidentDTO))]
        public async Task<IHttpActionResult> GetGameIncident(int id)
        {
            var gameIncident = await db.GameIncidents.Include(b => b.GamePlayer).Select(b =>
            new GameIncidentDTO()
            {
                 Id = b.Id,
                 IncidentType = b.IncidentType,
                 IncidentTime = b.IncidentTime,
                 GamePlayerFirstName = b.GamePlayer.FirstName,
                 GamePlayerLastName = b.GamePlayer.LastName
             }).SingleOrDefaultAsync(b => b.Id == id);

            if (gameIncident == null)
            {
                return NotFound();
            }

            return Ok(gameIncident);
        }

        // PUT: api/GameIncidents/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGameIncident(int id, GameIncident gameIncident)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gameIncident.Id)
            {
                return BadRequest();
            }

            db.Entry(gameIncident).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameIncidentExists(id))
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

        // POST: api/GameIncidents
        [ResponseType(typeof(GameIncident))]
        public async Task<IHttpActionResult> PostGameIncident(GameIncident gameIncident)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GameIncidents.Add(gameIncident);
            await db.SaveChangesAsync();

            // New code:
            // Load author name
            db.Entry(gameIncident).Reference(x => x.GamePlayer).Load();

            var dto = new GameIncidentDTO()
            {
                Id = gameIncident.Id,
                IncidentType = gameIncident.IncidentType,
                IncidentTime = gameIncident.IncidentTime,
                GamePlayerFirstName = gameIncident.GamePlayer.FirstName,
                GamePlayerLastName = gameIncident.GamePlayer.LastName,
                TeamName = gameIncident.GamePlayer.Team.Name
            };

            return CreatedAtRoute("DefaultApi", new { id = gameIncident.Id }, gameIncident);
        }

        // DELETE: api/GameIncidents/5
        [ResponseType(typeof(GameIncident))]
        public async Task<IHttpActionResult> DeleteGameIncident(int id)
        {
            GameIncident gameIncident = await db.GameIncidents.FindAsync(id);
            if (gameIncident == null)
            {
                return NotFound();
            }

            db.GameIncidents.Remove(gameIncident);
            await db.SaveChangesAsync();

            return Ok(gameIncident);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GameIncidentExists(int id)
        {
            return db.GameIncidents.Count(e => e.Id == id) > 0;
        }
    }
}