using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RefereePrototypeWithEFv4.Models
{
    public class GoalDTO
    {
        public int Id { get; set; }
        public bool IsOwnGoal { get; set; }
        public TimeSpan TimeScored { get; set; }
        public string GamePlayerFirstName { get; set; }
        public string GamePlayerLastName { get; set; }
        public string TeamName { get; set; }
        public int GameId { get; set; }
 
    }
}