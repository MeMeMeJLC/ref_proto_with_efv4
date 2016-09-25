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
    public class GamePlayersController : ApiController
    {
        private RefereePrototypeWithEFv4Context db = new RefereePrototypeWithEFv4Context();

        // GET: api/GamePlayers
        public IQueryable<GamePlayerDTO> GetGamePlayers()
        {
            var gamePlayers = from g in db.GamePlayers
                              select new GamePlayerDTO()
                              {
                                  Id = g.Id,
                                  FirstName = g.FirstName,
                                  LastName = g.LastName,

                                  IsCaptain = g.IsCaptain,
                                  IsStartingSubstitute = g.IsStartingSubstitute,
                                  TeamId = g.Team.Id,
                                  TeamName = g.Team.Name,
                                  GameId = g.GameId
                              };

            return gamePlayers;
        }

        public  List<GamePlayerDTO> GetGamePlayersByGameId(int gameId)
        {
            var gamePlayers = from g in db.GamePlayers
                              select new GamePlayerDTO()
                              {
                                  Id = g.Id,
                                  FirstName = g.FirstName,
                                  LastName = g.LastName,

                                  IsCaptain = g.IsCaptain,
                                  IsStartingSubstitute = g.IsStartingSubstitute,
                                  TeamId = g.Team.Id,
                                  TeamName = g.Team.Name,
                                  GameId = g.GameId
                              };

            List<GamePlayerDTO> gamePlayersByGameId = new List<GamePlayerDTO>();

            foreach (var item in gamePlayers)
            {
                if (gameId == item.GameId)
                {
                    gamePlayersByGameId.Add(item);
                }
            }

                        return gamePlayersByGameId;
        }

        public List<GamePlayerDTO> GetGamePlayersByTeamId(int teamId)
        {
            var gamePlayers = from g in db.GamePlayers
                              select new GamePlayerDTO()
                              {
                                  Id = g.Id,
                                  FirstName = g.FirstName,
                                  LastName = g.LastName,

                                  IsCaptain = g.IsCaptain,
                                  IsStartingSubstitute = g.IsStartingSubstitute,
                                  TeamId = g.Team.Id,
                                  TeamName = g.Team.Name,
                                  GameId = g.GameId
                              };

            List<GamePlayerDTO> gamePlayersByTeamId = new List<GamePlayerDTO>();

            foreach (var item in gamePlayers)
            {
                if (teamId == item.TeamId)
                {
                    gamePlayersByTeamId.Add(item);
                }
            }

            return gamePlayersByTeamId;
        }

        // GET: api/GamePlayers/5
        [ResponseType(typeof(GamePlayerDTO))]
        public async Task<IHttpActionResult> GetGamePlayer(int id)
        {
            // GamePlayer gamePlayer = await db.GamePlayers.FindAsync(id);
            var gamePlayer = await db.GamePlayers.Include(b => b.Team).Select(b =>
             new GamePlayerDTO()
             {
                 Id = b.Id,
                 FirstName = b.FirstName,
                 LastName = b.LastName,
                 IsCaptain = b.IsCaptain,
                 IsStartingSubstitute = b.IsStartingSubstitute,
                 TeamId = b.Team.Id,
                 TeamName = b.Team.Name,
                 GameId = b.GameId
             }).SingleOrDefaultAsync(b => b.Id == id);
            if (gamePlayer == null)
            {
                return NotFound();
            }

            return Ok(gamePlayer);
        }

        // PUT: api/GamePlayers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGamePlayer(int id, GamePlayer gamePlayer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gamePlayer.Id)
            {
                return BadRequest();
            }

            db.Entry(gamePlayer).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GamePlayerExists(id))
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

        // POST: api/GamePlayers
        [ResponseType(typeof(GamePlayer))]
        public async Task<IHttpActionResult> PostGamePlayer(GamePlayer gamePlayer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GamePlayers.Add(gamePlayer);
            await db.SaveChangesAsync();

            // New code:
            // Load author name
            db.Entry(gamePlayer).Reference(x => x.Team).Load();

            var dto = new GamePlayerDTO()
            {
                Id = gamePlayer.Id,
                FirstName = gamePlayer.FirstName,
                LastName = gamePlayer.LastName,
                IsCaptain = gamePlayer.IsCaptain,
                IsStartingSubstitute = gamePlayer.IsStartingSubstitute,
                GameId = gamePlayer.GameId,
                TeamName = gamePlayer.Team.Name
            };

            return CreatedAtRoute("DefaultApi", new { id = gamePlayer.Id }, gamePlayer);
        }

        // DELETE: api/GamePlayers/5
        [ResponseType(typeof(GamePlayer))]
        public async Task<IHttpActionResult> DeleteGamePlayer(int id)
        {
            GamePlayer gamePlayer = await db.GamePlayers.FindAsync(id);
            if (gamePlayer == null)
            {
                return NotFound();
            }

            db.GamePlayers.Remove(gamePlayer);
            await db.SaveChangesAsync();

            return Ok(gamePlayer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GamePlayerExists(int id)
        {
            return db.GamePlayers.Count(e => e.Id == id) > 0;
        }
    }
}