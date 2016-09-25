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
using System.Net.Http.Headers;

namespace RefereePrototypeWithEFv4.Controllers
{
    public class SubstitutionsController : ApiController
    {
        private RefereePrototypeWithEFv4Context db = new RefereePrototypeWithEFv4Context();

        // GET: api/Substitutions
        public IQueryable<SubstitutionDTO> GetSubstitutions()
        {
            var substitutions = from g in db.Substitutions
                                select new SubstitutionDTO()
                                {
                                    Id = g.Id,
                                    SubstitutionTime = g.SubstitutionTime,
                                    GamePlayerGoingOffId = g.GamePlayerGoingOffId,

                                    GamePlayerGoingOnId = g.GamePlayerGoingOnId,

                                    TeamName = g.GamePlayer.Team.Name,
                                    GameId = g.GamePlayer.Game.Id
                                };
            return substitutions;
        }

        public async Task<List<SubstitutionDTO>> GetSubstitutionsByGameId(int gameId)
        {
            var substitutions = from g in db.Substitutions
                                select new SubstitutionDTO()
                                {
                                    Id = g.Id,
                                    SubstitutionTime = g.SubstitutionTime,
                                    GamePlayerGoingOffId = g.GamePlayerGoingOffId,

                                    GamePlayerGoingOnId = g.GamePlayerGoingOnId,

                                    TeamName = g.GamePlayer.Team.Name,
                                    GameId = g.GamePlayer.GameId
                                };
            List<SubstitutionDTO> substitutionsByGameId = new List<SubstitutionDTO>();

            foreach (var item in substitutions)
            {
                HttpClient GoingOnClient = new HttpClient();
                HttpResponseMessage GoingOnResponse = await GoingOnClient.GetAsync("http://localhost:64553/api/gameplayers/" + item.GamePlayerGoingOnId);
                if (GoingOnResponse.IsSuccessStatusCode)
                {
                    GamePlayer goingOnPlayer = await GoingOnResponse.Content.ReadAsAsync<GamePlayer>();
                    item.GamePlayerGoingOnFirstName = goingOnPlayer.FirstName;
                    item.GamePlayerGoingOnLastName = goingOnPlayer.LastName;
                    item.GameId = goingOnPlayer.GameId;

                }

                HttpClient GoingOffClient = new HttpClient();
                HttpResponseMessage GoingOffResponse = await GoingOffClient.GetAsync("http://localhost:64553/api/gameplayers/" + item.GamePlayerGoingOffId);
                if (GoingOffResponse.IsSuccessStatusCode)
                {
                    GamePlayer goingOffPlayer = await GoingOffResponse.Content.ReadAsAsync<GamePlayer>();
                    item.GamePlayerGoingOffFirstName = goingOffPlayer.FirstName;
                    item.GamePlayerGoingOffLastName = goingOffPlayer.LastName;

                }
                if (gameId == item.GameId)
                {
                    substitutionsByGameId.Add(item);
                }

            }


            return substitutionsByGameId;
        }

        // GET: api/Substitutions/5
        [ResponseType(typeof(SubstitutionDTO))]
        public async Task<IHttpActionResult> GetSubstitution(int id)
        {

            var substitution = await db.Substitutions.Include(b => b.GamePlayer).Select(b =>
            new SubstitutionDTO()
            {
                Id = b.Id,
                SubstitutionTime = b.SubstitutionTime,
                GamePlayerGoingOffId = b.GamePlayerGoingOffId,

                GamePlayerGoingOnId = b.GamePlayerGoingOnId,

                TeamName = b.GamePlayer.Team.Name,
                GameId = b.GamePlayer.GameId
            }).SingleOrDefaultAsync(b => b.Id == id);

            HttpClient GoingOnClient = new HttpClient();
            HttpResponseMessage GoingOnResponse = await GoingOnClient.GetAsync("http://localhost:64553/api/gameplayers/" + substitution.GamePlayerGoingOnId);
            if (GoingOnResponse.IsSuccessStatusCode)
            {
                GamePlayer goingOnPlayer = await GoingOnResponse.Content.ReadAsAsync<GamePlayer>();
                substitution.GamePlayerGoingOnFirstName = goingOnPlayer.FirstName;
                substitution.GamePlayerGoingOnLastName = goingOnPlayer.LastName;

            }

            HttpClient GoingOffClient = new HttpClient();
            HttpResponseMessage GoingOffResponse = await GoingOffClient.GetAsync("http://localhost:64553/api/gameplayers/" + substitution.GamePlayerGoingOffId);
            if (GoingOffResponse.IsSuccessStatusCode)
            {
                GamePlayer goingOffPlayer = await GoingOffResponse.Content.ReadAsAsync<GamePlayer>();
                substitution.GamePlayerGoingOffFirstName = goingOffPlayer.FirstName;
                substitution.GamePlayerGoingOffLastName = goingOffPlayer.LastName;

            }

            if (substitution == null)
            {
                return NotFound();
            }

            return Ok(substitution);
        }

        // PUT: api/Substitutions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSubstitution(int id, Substitution substitution)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != substitution.Id)
            {
                return BadRequest();
            }

            db.Entry(substitution).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubstitutionExists(id))
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

        // POST: api/Substitutions
        [ResponseType(typeof(Substitution))]
        public async Task<IHttpActionResult> PostSubstitution(Substitution substitution)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Substitutions.Add(substitution);
            await db.SaveChangesAsync();

            // New code:
            // Load author name
            db.Entry(substitution).Reference(x => x.GamePlayer).Load();

            var dto = new SubstitutionDTO()
            {
                Id = substitution.Id,
                SubstitutionTime = substitution.SubstitutionTime,
                GamePlayerGoingOffId = substitution.GamePlayerGoingOffId,

                GamePlayerGoingOnId = substitution.GamePlayerGoingOnId,

                TeamName = substitution.GamePlayer.Team.Name
            };

            return CreatedAtRoute("DefaultApi", new { id = substitution.Id }, substitution);
        }

        // DELETE: api/Substitutions/5
        [ResponseType(typeof(Substitution))]
        public async Task<IHttpActionResult> DeleteSubstitution(int id)
        {
            Substitution substitution = await db.Substitutions.FindAsync(id);
            if (substitution == null)
            {
                return NotFound();
            }

            db.Substitutions.Remove(substitution);
            await db.SaveChangesAsync();

            return Ok(substitution);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubstitutionExists(int id)
        {
            return db.Substitutions.Count(e => e.Id == id) > 0;
        }
    }
}