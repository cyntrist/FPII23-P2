// Cynthia Tristán Álvarez
// Paula Sierra Luque

using System.Collections.Generic;
using System.Text.RegularExpressions;
using AdventureGame;
using Listas;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventureGame
{
    internal class Program
    {
        const string ITEMS_FILE = @"CrowtherItems.txt",
                     ROOMS_FILE = @"CrowtherRooms.txt";

        static void Main()
        {
            Map map = new();
            ListaEnlazada inventory = new();
            ReadInventory(ITEMS_FILE, map); 
            ReadRooms(ROOMS_FILE, map);
            map.SetItemsRooms();
            map.WriteMap();

            while (true) // bucle ppal.
            {

            }
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

        void ProcessCommand(Map map, string input, int playerRoom, ListaEnlazada inventory)
        {
            string[] words = input.Trim().ToUpper().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            switch (words[0])
            {
                case "HELP": // muestra la ayuda del juego
                    Console.WriteLine("AYUDA: "); // falta ponerla
                    break;
                case "INVENTORY": // muestra el inventario actual del jugador
                    Console.WriteLine(inventory.ToString());
                    break;
                case "LOOK": // muestra la información de la habitación actual
                    Console.WriteLine(map.GetInfoRoom(playerRoom));
                    break;
                case "ITEMS": // muestra los ítems de la habitación actual
                    Console.WriteLine(map.GetItemsRoom(playerRoom));
                    break;
                case "TAKE " + "<item>": // si el item está en habitación actual lo recoge
                                         // y lo añade al inventario del jugador;
                                         // mensaje de error en otro caso
                    // no se muy bien como hacer lo del string comando
                    break;
                case "DROP " + "<item>": // si el ítem está en el inventario del jugador,
                                         // lo elimina del inventario y lo deja en la habitación actual;
                                         // mensaje de error en otro caso
                    // no se muy bien como hacer lo del string comando
                    break;
                default: // se interpreta como dirección de movimiento,
                         // que se gestionará con el método correspondiente de Map.
                    map.Move(playerRoom, words[0], inventory);
                    break;
            }
        }
        #endregion
    }
}