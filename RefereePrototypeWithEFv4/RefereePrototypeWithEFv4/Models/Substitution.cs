using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RefereePrototypeWithEFv4.Models
{
    public class Substitution
    {
        public int Id { get; set; }
        public TimeSpan SubstitutionTime { get; set; }
        public int GamePlayerGoingOnId { get; set; }
        public int GamePlayerGoingOffId { get; set; }
        public GamePlayer GamePlayer { get; set; }
    }
}