// Cynthia Tristán Álvarez
// Paula Sierra Luque

using System.Text.RegularExpressions;
using AdventureGame;
using Listas;

namespace AdventureGame
{
    internal class Program
    {
        const string ITEMS_FILE = @"CrowtherItems.txt",
                     ROOMS_FILE = @"CrowtherRooms.txt";

        static void Main()
        {
            //tienen que tomar MAP pero no se muy bien como 
            //ReadInventory(ITEMS_FILE, ?¿?¿); 
            //ReadRooms(ROOMS_FILE, ?¿?¿);
        }

        #region métodos
        static private void ReadInventory(string file, Map map)
        {
            StreamReader sr = new(file);
            while (!sr.EndOfStream)
            {
                Console.WriteLine("Item name: " + sr.ReadLine() + "   " 
                                + "Descr: " + sr.ReadLine() + "   " 
                                + "InitRoom: " + sr.ReadLine());
                sr.ReadLine();
                //map.AddItemRoom(nroom, itemId); // añade el item itemId a la habitacion nRoom
            }
            sr.Close();
        }

        static private void ReadRooms(string file, Map map)
        {
            StreamReader f = new (file);
            while (!f.EndOfStream)
            { // mientras siga el documento
                int n = int.Parse(f.ReadLine()!); // número de la habitación
                ReadRoom(ref f, n, map); // lee esta habitación
            }
            f.Close();
        }

        static private void ReadRoom(ref StreamReader f, int n, Map map)
        { //Creo que n ya se lee correctamente
            Console.WriteLine("Room: " + n + "   "
                + "Name: "  + f.ReadLine() + "   "
                + "Descr: " + f.ReadLine());
            f.ReadLine();                                            // Línea separatoria "------"

            //map.AddRoom(n, f, description); // deberiamos llamar description en el readline anterior ?

            string newline = f.ReadLine()!;                               // Lee la siguiente línea
            while (!string.IsNullOrWhiteSpace(newline))                   // Hasta que haya línea en blanco
            {
                newline = Regex.Replace(newline, @"\s+", "/");            // Reemplaza todos los espacios con un solo '/'
                string[] bits = newline.Split("/");                       // Parte la línea en trozos entre '/'

                string conditionalItem = ""; 
                if (bits.Length > 2)                                      // Si tiene CondItem se lo añade
                    conditionalItem = bits[2];
                
                newline = "Route from room " + n + " to room " + bits[1]
                    + ", direction " + bits[0] 
                    + ". CondItem: " + conditionalItem;              
                Console.WriteLine(newline);                               // Lo escribe

                //map.AddRouteRoom(n, bits[1], bits[0], conditionalItem); //ayuda

                newline = f.ReadLine()!;                                  // Siguiente línea
            }                                                        // Si la siguiente línea es blanca, acaba el método
        }
        #endregion
    }
}