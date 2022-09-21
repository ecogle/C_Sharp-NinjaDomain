using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NinjaDomain.Classes;
using NinjaDomain.Classes.Enums;
using NinjaDomain.DataModel;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new NullDatabaseInitializer<NinjaContext>());
            //InsertNinja();
            //SimpleNinjaQueries();
            //SimpleClanQuery();
            //QueryAndUpdateNinja();
            //RetrieveDataWithFind();
            //RetreiveDataWithStoredProc();
            //DeleteNinja();
            //DeleteNinjaWithKeyValue();
            //DeleteNinjaViaSproc();
            //InsertNinjaWithEquipment();
            //SimpleNinjaGraphQuery();
            ProjectionQuery();

            Console.ReadLine();
        }

        private static void DeleteNinjaViaSproc()
        {
            var keyVal = 8;
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Database.ExecuteSqlCommand("exec DeleteNinjaViaSproc {0}", keyVal);
            }
            
        }

        private static void DeleteNinjaWithKeyValue()
        {
            var keyVal = 6;
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninja = context.Ninjas.Find(keyVal);
                context.Ninjas.Remove(ninja);
                context.SaveChanges();
            }
        }

        private static void DeleteNinja()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninja = context.Ninjas.FirstOrDefault();
                context.Ninjas.Remove(ninja);
                context.SaveChanges();
            }
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

        private static void SimpleNinjaQueries()
        {
            using (var context = new NinjaContext())
            {
                var ninjas = context.Ninjas.ToList();
            }
        }

        private static void SimpleClanQuery()
        {
            using (var context = new NinjaContext())
            {
                var clans = context.Clans.ToList();
            }
        }

        private static void QueryAndUpdateNinja()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninja = context.Ninjas.FirstOrDefault();
                ninja.ServedInOniwaban = (!ninja.ServedInOniwaban);
                context.SaveChanges();
            }
        }

        private static void QueryAndUpdateNinjaDisconnected()
        {
            Ninja ninja;

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                ninja = context.Ninjas.FirstOrDefault();
            }

            ninja.ServedInOniwaban = (!ninja.ServedInOniwaban);

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Ninjas.Add(ninja);
                context.SaveChanges();
            }
        }

        private static void RetrieveDataWithFind()
        {
            var keyval = 5;
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninja = context.Ninjas.Find(keyval++);
                Console.WriteLine("After Find#1:" + ninja.name);

                var someNinja = context.Ninjas.Find(keyval);
                Console.WriteLine("After Find#2:" + someNinja.name);

                ninja = null;
            }
        }

        private static void RetreiveDataWithStoredProc()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninjas = context.Ninjas.SqlQuery("exec GetOldNinjas");

                foreach (var ninja in ninjas)
                {
                    Console.WriteLine(ninja.name);
                }
            }
        }

        private static void InsertNinjaWithEquipment()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninja = new Ninja
                {
                    name = "Kacy Catanzaro",
                    ServedInOniwaban = false,
                    DateOfBirth = new DateTime(1990, 1, 14),
                    ClanId = 1
                };

                var muscles = new NinjaEquipment
                {
                    Name = "Muscles",
                    Type = EquipmentType.Tool
                };

                var spunk = new NinjaEquipment
                {
                    Name = "Spunk",
                    Type = EquipmentType.Weapon
                };

                context.Ninjas.Add(ninja);
                ninja.EquipmentOwned.Add(muscles);
                ninja.EquipmentOwned.Add(spunk);
                context.SaveChanges();
            }
        }

        public static void SimpleNinjaGraphQuery()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;

                var ninja = context.Ninjas.FirstOrDefault(n => n.name.StartsWith("Kacy"));
                Console.WriteLine("Ninja Retrieved: " + ninja.name);

                Console.WriteLine("Ninja Equip Count: {0}",ninja.EquipmentOwned.Count);
            }
            
        }

        private static void ProjectionQuery()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninjas = context.Ninjas
                    .Select(n => new { n.name, n.DateOfBirth, n.EquipmentOwned })
                    .ToList();
            }
            
        }
    }
}
