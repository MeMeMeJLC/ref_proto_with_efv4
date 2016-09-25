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
using System.Collections;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace RefereePrototypeWithEFv4.Controllers
{
    public class GoalsController : ApiController
    {
        private RefereePrototypeWithEFv4Context db = new RefereePrototypeWithEFv4Context();

        // GET: api/Goals
        public IQueryable<GoalDTO> GetGoals()
        {
            var goals = from g in db.Goals
                        select new GoalDTO()
                        {
                            Id = g.Id,
                            IsOwnGoal = g.IsOwnGoal,
                            TimeScored = g.TimeScored,
                            GamePlayerFirstName = g.GamePlayer.FirstName,
                            GamePlayerLastName = g.GamePlayer.LastName,
                            TeamName = g.GamePlayer.Team.Name
                        };

            return goals;
        }

        public Scores GetGoalsByTeamInGame(int gameId)
        {

            string teamOneName = null;
            string teamTwoName = null;
            int teamOneGoals = 0;
            int teamTwoGoals = 0;

            var goals = from g in db.Goals
                        select new GoalDTO()
                        {
                            Id = g.Id,
                            IsOwnGoal = g.IsOwnGoal,
                            TimeScored = g.TimeScored,
                            GamePlayerFirstName = g.GamePlayer.FirstName,
                            GamePlayerLastName = g.GamePlayer.LastName,
                            TeamName = g.GamePlayer.Team.Name,
                            GameId = g.GamePlayer.GameId
                        };

            foreach (var item in goals)
            {
                if (gameId == item.GameId)
                {
                    if (teamOneName == null)
                    {
                        teamOneName = item.TeamName;
                    }
                    else if (teamTwoName == null)
                    {
                        teamTwoName = item.TeamName;
                    }

                    if (teamOneName == item.TeamName && item.IsOwnGoal == false)
                    {
                        teamOneGoals += 1;
                    }
                    else if (teamOneName == item.TeamName && item.IsOwnGoal == true)
                    {
                        teamTwoGoals += 1;
                    }
                    else if (teamTwoName == item.TeamName && item.IsOwnGoal == false)
                    {
                        teamTwoGoals += 1;
                    }
                    else if (teamTwoName == item.TeamName && item.IsOwnGoal == true)
                    {
                        teamOneGoals += 1;
                    }
                }

            }

            var scores = new Scores
            {
                TeamOneName = teamOneName,
                TeamOneGoals = teamOneGoals,
                TeamTwoName = teamTwoName,
                TeamTwoGoals = teamTwoGoals
            };

            return scores;

        }
    

        public class Scores{
            public string TeamOneName;
            public int TeamOneGoals;
            public string TeamTwoName;
            public int TeamTwoGoals;
        }


        // GET: api/Goals/5
        [ResponseType(typeof(GoalDTO))]
        public async Task<IHttpActionResult> GetGoal(int id)
        {
            var goal = await db.Goals.Include(b => b.GamePlayer).Select(b =>
             new GoalDTO()
             {
                 Id = b.Id,
                 IsOwnGoal = b.IsOwnGoal,
                 TimeScored = b.TimeScored,
                 GamePlayerFirstName = b.GamePlayer.FirstName,
                 GamePlayerLastName = b.GamePlayer.LastName,
                 TeamName = b.GamePlayer.Team.Name
             }).SingleOrDefaultAsync(b => b.Id == id);
            if (goal == null)
            {
                return NotFound();
            }

            return Ok(goal);
        }

        // PUT: api/Goals/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGoal(int id, Goal goal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != goal.Id)
            {
                return BadRequest();
            }

            db.Entry(goal).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoalExists(id))
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

        // POST: api/Goals
        [ResponseType(typeof(Goal))]
        public async Task<IHttpActionResult> PostGoal(Goal goal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Goals.Add(goal);
            await db.SaveChangesAsync();

            // New code:
            // Load author name
            db.Entry(goal).Reference(x => x.GamePlayer).Load();

            var dto = new GoalDTO()
            {
                Id = goal.Id,
                IsOwnGoal = goal.IsOwnGoal,
                TimeScored = goal.TimeScored,
                GamePlayerFirstName = goal.GamePlayer.FirstName,
                GamePlayerLastName = goal.GamePlayer.LastName,
                TeamName = goal.GamePlayer.Team.Name
            };

            return CreatedAtRoute("DefaultApi", new { id = goal.Id }, goal);
        }

        // DELETE: api/Goals/5
        [ResponseType(typeof(Goal))]
        public async Task<IHttpActionResult> DeleteGoal(int id)
        {
            Goal goal = await db.Goals.FindAsync(id);
            if (goal == null)
            {
                return NotFound();
            }

            db.Goals.Remove(goal);
            await db.SaveChangesAsync();

            return Ok(goal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GoalExists(int id)
        {
            return db.Goals.Count(e => e.Id == id) > 0;
        }
    }
}