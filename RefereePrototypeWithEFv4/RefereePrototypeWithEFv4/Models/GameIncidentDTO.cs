using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RefereePrototypeWithEFv4.Models
{
    public class GameIncidentDTO
    {
        public int Id { get; set; }
        public string IncidentType { get; set; }
        public TimeSpan IncidentTime { get; set; }
        public int GameId { get; set; }
        public int GamePlayerId { get; set; }
        public string GamePlayerFirstName { get; set; }
        public string GamePlayerLastName { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
    }
}