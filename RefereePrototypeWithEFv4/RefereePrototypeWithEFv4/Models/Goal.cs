using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RefereePrototypeWithEFv4.Models
{
    public class Goal
    {
        public int Id { get; set; }
        public bool IsOwnGoal { get; set; }
        public TimeSpan TimeScored { get; set; }

        //foreign key
        public int GamePlayerId { get; set; }
        //navigation property
        public GamePlayer GamePlayer { get; set; }
    }
}