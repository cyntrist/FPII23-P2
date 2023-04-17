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
            Map map = new();
            ReadInventory(ITEMS_FILE, map); 
            ReadRooms(ROOMS_FILE, map);
            map.SetItemsRooms();
            map.WriteMap();
        }

        #region Métodos Read
        static private void ReadInventory(string file, Map map)
        {
            StreamReader sr = new(file);
            while (!sr.EndOfStream)
            {
                string name, desc, iniRoom;
                name    = sr.ReadLine()!;
                desc    = sr.ReadLine()!;
                iniRoom = sr.ReadLine()!;
                sr.ReadLine();
                map.AddItemMap(name, desc, int.Parse(iniRoom)); // duda con el enunciado resuelta
            }
            sr.Close();
        }

        static private void ReadRooms(string file, Map map)
        {
            StreamReader f = new (file);
            while (!f.EndOfStream)
            { // mientras siga el documento
                int n = int.Parse(f.ReadLine()!) - 1; // número de la habitación (- 1 porque empieza en el 1)
                ReadRoom(ref f, n, map);          // lee esta habitación
            }
            f.Close();
        }

        static private void ReadRoom(ref StreamReader f, int n, Map map)
        { // 
            string name, desc;
            name = f.ReadLine()!;
            desc = f.ReadLine()!;
            f.ReadLine();                                            // Línea separatoria "------"
            map.AddRoom(n, name, desc);                              // Añade al mapa la habitación leída
            string newline = f.ReadLine()!;                               // Lee la siguiente línea
            while (!string.IsNullOrWhiteSpace(newline))                   // Hasta que haya línea en blanco
            {
                newline = Regex.Replace(newline, @"\s+", "/");            // Reemplaza todos los espacios con un solo '/'
                string[] bits = newline.Split("/");                       // Parte la línea en trozos entre '/'

                string condItem = ""; 
                if (bits.Length > 2)                                      // Si tiene CondItem 
                    condItem = bits[2];                                   // Se lo añade
                
                map.AddRouteRoom(n, bits[0], int.Parse(bits[1]), condItem); // Añade a la habitación en el mapa la ruta leída
                newline = f.ReadLine()!;                                  // Siguiente línea
            }                                                        // Si la siguiente línea es blanca, acaba el método
        }
        #endregion
    }
}