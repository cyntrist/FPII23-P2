// Cynthia Tristán Álvarez
// Paula Sierra Luque

using System.Text.RegularExpressions;
using Crowther.Maps;
using static Crowther.Maps.Map;
using Crowther.Rooms;
using static Crowther.Rooms.Room;

namespace AdventureGame
{
    internal class Program
    {
        const string ITEMS_FILE = @"CrowtherItems.txt",
                     ROOMS_FILE = @"CrowtherRooms.txt";

        static void Main()
        {
            ReadInventory(ITEMS_FILE);
            ReadRooms(ROOMS_FILE);
        }

        #region métodos
        static private void ReadInventory(string file)
        {
            StreamReader sr = new StreamReader(file);
            while (!sr.EndOfStream)
            {
                Console.WriteLine("Item name: " + sr.ReadLine() + "   " 
                                + "Descr: " + sr.ReadLine() + "   " 
                                + "InitRoom: " + sr.ReadLine());
                sr.ReadLine();
            }
            sr.Close();
        }

        static private void ReadRooms(string file)
        {
            StreamReader f = new (file);
            while (!f.EndOfStream)
            { // mientras siga el documento
                int n = int.Parse(f.ReadLine()!); // número de la habitación
                ReadRoom(ref f, n); // lee esta habitación
            }
            f.Close();
        }

        static private void ReadRoom(ref StreamReader f, int n)
        { // PROBLEMA: no funciona respecto a n de verdad jeje :^) solo lee de arriba a abajo
            Console.WriteLine("Room: " + n + "   "
                + "Name: "  + f.ReadLine() + "   "
                + "Descr: " + f.ReadLine());
            f.ReadLine();                                            // Línea separatoria "------"
            string nl = f.ReadLine()!;                               // Lee la siguiente línea
            while (!string.IsNullOrWhiteSpace(nl))                   // Hasta que haya línea en blanco
            {
                nl = Regex.Replace(nl, @"\s+", "/");                 // Reemplaza todos los espacios con un solo '/'
                string[] bits = nl.Split("/");                       // Parte la línea en trozos entre '/'
                nl = "Route from room " + n + " to room " + bits[1]
                    + ", direction " + bits[0] 
                    + ". CondItem: ";
                if (bits.Length > 2) { nl += bits[2]; }              // Si tiene CondItem se lo añade
                Console.WriteLine(nl);                               // Lo escribe
                nl = f.ReadLine()!;                                  // Siguiente línea
            }                                                        // Si la siguiente línea es blanca, acaba el método
        }

        /// <summary>
        /// ////////////////////
        /// </summary>
        static private void ReadInventory(string file, Map map)
        {

        }

        static private void ReadRooms(string file, Map map)
        {

        }

        static private void ReadRoom(StreamReader f, int nRoom, Map map)
        {

        }
        #endregion
    }
}