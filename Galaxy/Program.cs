using System;
using System.Collections.Generic;

namespace GalaxyApp
{
    class Program
    {
        /// <summary>Главный метод приложения.</summary>
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Galaxy News!");
            IterateThroughList();
            Console.ReadKey();
        }

        /// <summary>Перебирает список галактик и выводит информацию.</summary>
        private static void IterateThroughList()
        {
            var theGalaxies = new List<Galaxy>
            {
                new Galaxy() { Name="Tadpole", MegaLightYears=400, GalaxyType=new GType('S')},
                new Galaxy() { Name="Pinwheel", MegaLightYears=25, GalaxyType=new GType('S')},
                new Galaxy() { Name="Cartwheel", MegaLightYears=500, GalaxyType=new GType('L')},
                new Galaxy() { Name="Small Magellanic Cloud", MegaLightYears=.2, GalaxyType=new GType('I')},
                new Galaxy() { Name="Andromeda", MegaLightYears=3, GalaxyType=new GType('S')},
                new Galaxy() { Name="Maffei 1", MegaLightYears=11, GalaxyType=new GType('E')}
            };

            foreach (Galaxy theGalaxy in theGalaxies)
            {
                // ИСПРАВЛЕНО: добавлен .MyGType
                Console.WriteLine(theGalaxy.Name + "  " + theGalaxy.MegaLightYears + ",  " + theGalaxy.GalaxyType.MyGType);
            }
        }
    }

    /// <summary>Представляет галактику.</summary>
    public class Galaxy
    {
        public string Name { get; set; }
        public double MegaLightYears { get; set; }
        /// <summary>Тип галактики. ИСПРАВЛЕНО: object изменён на GType.</summary>
        public GType GalaxyType { get; set; }
    }

    /// <summary>Классификация типа галактики.</summary>
    public class GType
    {
        /// <summary>Конструктор. Определяет тип по символу.</summary>
        /// <param name="type">S, E, I, L</param>
        public GType(char type)
        {
            switch (type)
            {
                case 'S': MyGType = "Spiral"; break;
                case 'E': MyGType = "Elliptical"; break;
                case 'I': MyGType = "Irregular"; break;  // ИСПРАВЛЕНО: 'l' на 'I'
                case 'L': MyGType = "Lenticular"; break;
                default: MyGType = "Unknown"; break;
            }
        }
        public object MyGType { get; set; }
    }
}