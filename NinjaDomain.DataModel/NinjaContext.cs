using NinjaDomain.Classes;
using System.Data.Entity;

namespace NinjaDomain.DataModel
{
    public class NinjaContext :DbContext
    {
        //Explore Repository Pattern
        public DbSet<Ninja> Ninjas { get; set; }
        public DbSet <Clan> Clans { get; set; }
        public DbSet<NinjaEquipment> Equipment { get; set; }
    }
}
