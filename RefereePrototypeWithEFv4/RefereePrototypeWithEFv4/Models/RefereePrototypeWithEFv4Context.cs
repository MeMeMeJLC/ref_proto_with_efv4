using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RefereePrototypeWithEFv4.Models
{
    public class RefereePrototypeWithEFv4Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public RefereePrototypeWithEFv4Context() : base("name=RefereePrototypeWithEFv4Context")
        {
        }

        public System.Data.Entity.DbSet<RefereePrototypeWithEFv4.Models.Game> Games { get; set; }

        public System.Data.Entity.DbSet<RefereePrototypeWithEFv4.Models.Team> Teams { get; set; }

        public System.Data.Entity.DbSet<RefereePrototypeWithEFv4.Models.GamePlayer> GamePlayers { get; set; }

        public System.Data.Entity.DbSet<RefereePrototypeWithEFv4.Models.Goal> Goals { get; set; }

        public System.Data.Entity.DbSet<RefereePrototypeWithEFv4.Models.GameIncident> GameIncidents { get; set; }

        public System.Data.Entity.DbSet<RefereePrototypeWithEFv4.Models.Substitution> Substitutions { get; set; }
    }
}
