using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Building
    {
        public int Floors; // количество этажей
        public int Area;   // площадь здания 
        public int Occupants; // количество жильцов

        public int AreaPerPerson()
        {
            return Area / Occupants;
        }
    }

    class BuidingDemo
    {
        static void Main() // Main метод 
        {
            Building house = new Building(); // создать объект типа Building // ЭКЗЕПЛЯР house 
            //int areaPP; // площадь на одного человека 

            // происвоить значение полям в объете house 
            house.Occupants = 4;
            house.Area = 2500;
            house.Floors = 2;

            // вычислить площадь на одного человка 
            //areaPP = house.Area / house.Occupants;

            //Console.WriteLine("Дом имеет: \n " + house.Floors + "этаж\n " + house.Occupants + "жильца\n " + house.Area + "кв. футов площади, из них\n "
            //  + areaPP + "приходится на одного человека ");

            Console.WriteLine(house.AreaPerPerson());

            Console.ReadKey();

        }
    }
}
