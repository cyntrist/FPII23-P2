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
            Map map                 = new();
            ListaEnlazada inventory = new();
            int playerRoom          = 3;
            ReadInventory(ITEMS_FILE, map); 
            ReadRooms(ROOMS_FILE, map);
            map.SetItemsRooms();
            //map.WriteMap();

            Console.WriteLine("ADVENTURE");
            Console.WriteLine(map.GetInfoRoom(playerRoom) + "\n");
            while (true) // bucle ppal.
            {
                Console.Write("> ");
                string input = Console.ReadLine()!;
                ProcessCommand(map, input, playerRoom, inventory);
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
                int n = int.Parse(f.ReadLine()!); // número de la habitación (- 1 porque empieza en el 1)
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

        static void ProcessCommand(Map map, string input, int playerRoom, ListaEnlazada inventory)
        {
            string[] words = input.Trim().ToUpper().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (words.Length > 0) 
                switch (words[0])
                {
                    case "HELP": // muestra la ayuda del juego
                        Console.WriteLine("COMMANDS: "
                                        + "\n\tInventory: shows the content of your inventory."
                                        + "\n\tLook: shows the information of the current room."
                                        + "\n\tItems: shows the items in the current room."
                                        + "\n\tTake <item>: moves the item in the room to your inventory."
                                        + "\n\tDrop <item>: moves the item in your inventory to the current room."
                                        + "\n\t<direction>: moves you to the specified direction."); 
                        break;
                    case "INVENTORY": // muestra el inventario actual del jugador
                        Console.WriteLine("Inventory " + inventory.ToString());
                        break;
                    case "LOOK": // muestra la información de la habitación actual
                        Console.WriteLine(map.GetInfoRoom(playerRoom));
                        break;
                    case "ITEMS": // muestra los ítems de la habitación actual
                        Console.WriteLine("Room " + map.GetItemsRoom(playerRoom));
                        break;
                    case "TAKE": // si el item está en habitación actual lo recoge
                                 // y lo añade al inventario del jugador;
                                 // mensaje de error en otro caso
                        if (words[1] != null) // si hay una 2ª palabra
                            map.TakeItemRoom(playerRoom, words[1], inventory); 
                        break;
                    case "DROP": // si el ítem está en el inventario del jugador,
                                 // lo elimina del inventario y lo deja en la habitación actual;
                                 // mensaje de error en otro caso
                        /////////////////// se vienen cositas
                        break;
                    default: // se interpreta como dirección de movimiento,
                             // que se gestionará con el método correspondiente de Map.
                        map.Move(playerRoom, words[0], inventory); /////////////// hay que acabar move
                        break;
                }
            Console.WriteLine(); // línea vacía estética de separación
        }
        #endregion
    }
}