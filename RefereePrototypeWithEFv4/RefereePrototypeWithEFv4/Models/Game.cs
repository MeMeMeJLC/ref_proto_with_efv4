using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RefereePrototypeWithEFv4.Models
{
    public class Game
    {
        public int Id { get; set; }
        public DateTime GameDateTime { get; set; }
        //public ICollection<GamePlayer> GamePlayers { get; set; }
    }
}