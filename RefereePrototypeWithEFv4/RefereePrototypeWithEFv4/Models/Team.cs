using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RefereePrototypeWithEFv4.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<GamePlayer> GamePlayers { get; set; }
    }
}