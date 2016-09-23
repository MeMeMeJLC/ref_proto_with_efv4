using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RefereePrototypeWithEFv4.Models
{
    public class GameIncident
    {
        public int Id { get; set; }
        public string IncidentType { get; set; }
        public TimeSpan IncidentTime { get; set; }


        public int GamePlayerId { get; set; }
        public GamePlayer GamePlayer { get; set; }
    }
}