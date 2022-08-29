using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NinjaDomain.Classes;
using NinjaDomain.DataModel;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new NullDatabaseInitializer<NinjaContext>());
            InsertNinja();

            Console.ReadLine();
        }
        private static void InsertNinja()
        {
            var ninja = new Ninja
            {
                name = "Leonardo",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(1984, 4, 15),
                ClanId = 1
            };

            var ninja2 = new Ninja
            {
                name = "Rafael",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(1984, 4, 16),
                ClanId = 1
            };

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Ninjas.AddRange(new List<Ninja> {ninja,ninja2});
                context.SaveChanges();
            }
            
        }
    }
}
