// Cynthia Tristán Álvarez
// Paula Sierra Luque

using System.Text.RegularExpressions;
using Listas;

namespace AdventureGame
{
    internal class Program
    {
        const string ITEMS_FILE = @"CrowtherItems.txt",
                     ROOMS_FILE = @"CrowtherRooms.txt",
                  COMMANDS_FILE = @"CommandsFile.txt";

        static void Main()
        {
            Map map                 = new();                            // MAPEADO
            ListaEnlazada inventory = new();
            //AutocompletadoComandos autocompletado = new(); //wip
            int playerRoom          = 1;

            ReadInventory(ITEMS_FILE, map); 
            ReadRooms(ROOMS_FILE, map);

            map.SetItemsRooms();
            //map.WriteMap();

            Console.Write("Welcome to Adventure!! "); Instructions();   // INTRODUCCIÓN
            string[] commands = ReadCommands();                         // HISTORIAL DE COMANDOS DE ARCHIVO
            Console.WriteLine("\n" + map.GetInfoRoom(playerRoom) + "\n");      // INFORMACIÓN INICIAL

            string historial = string.Empty; // historial de comandos de esta sesión
            if (commands != null && commands.Length > 0) 
                // comprueba que existe el archivo y tiene por lo menos una línea
            {
                Console.WriteLine("Command History:\n");
                for (int i = 0; i < commands.Length; i++) // para cada comando
                {
                    Console.WriteLine("> " + commands[i]); // lo muestra
                    ProcessCommand(map, commands[i], ref playerRoom, inventory); // y lo ejecuta
                }
            }

            while (playerRoom > 0)                                      // BUCLE PRINCIPAL
            {
                Console.Write("> ");
                string input = Console.ReadLine()!;
                ProcessCommand(map, input, ref playerRoom, inventory);
                if (input.Trim().ToUpper() != "END") 
                    historial += "," + input;

                // EXTENSION
                //ConsoleKeyInfo key = Console.ReadKey(true);
                //if(key.Key == ConsoleKey.Tab) //si se pulsa tabulador
                {
                    //se buscan comandos para autocompletar
                    //autocompletado.BuscarComandos(Console.ReadLine()!);
                }
            }

            if (SaveCommands(historial))                                                      // GUARDADO DE HISTORIAL
                Console.WriteLine("The game has been saved succesfully.");
            else Console.WriteLine("The game has ended without saving.");
        }

        #region Métodos Read
        static void ReadInventory(string file, Map map)
        //lee archivo file y escribe informacion en pantalla
        {
            StreamReader sr = null!;
            try
            {
                sr = new(file);
                while (!sr.EndOfStream)
                {
                    string name, desc, iniRoom;
                    name = sr.ReadLine()!;
                    desc = sr.ReadLine()!;
                    iniRoom = sr.ReadLine()!;
                    sr.ReadLine();
                    map.AddItemMap(name, desc, int.Parse(iniRoom)); // duda con el enunciado resuelta
                }
            }
            catch (FileNotFoundException fnfe) { Console.WriteLine($"ERROR DE ARCHIVO: No se ha encontrado archivo de inventario.\n{fnfe.Message}"); }
            catch (IOException ioe) { Console.WriteLine($"ERROR DE I/O: {ioe.Message}"); }
            catch (Exception e) { Console.WriteLine($"ERROR: {e.Message}"); Environment.Exit(0); }
            finally { sr?.Close(); }
        }

        static void ReadRooms(string file, Map map)
        //lee archivo file, coge el numero de cada habitacion e invoca ReadRoom()
        {
            StreamReader f = null!;
            try
            {
                f = new(file);
                while (!f.EndOfStream)
                { // mientras siga el documento
                    int n = int.Parse(f.ReadLine()!); // número de la habitación (- 1 porque empieza en el 1)
                    ReadRoom(ref f, n, map);          // lee esta habitación
                }
            }
            catch (FileNotFoundException fnfe) { Console.WriteLine($"ERROR DE ARCHIVO: No se ha encontrado archivo de habitaciones.\n{fnfe.Message}"); }
            catch (IOException ioe) { Console.WriteLine($"ERROR DE I/O: {ioe.Message}"); }
            catch (Exception e) { Console.WriteLine($"ERROR: {e.Message}"); Environment.Exit(0); }
            finally { f?.Close(); }
        }

        static void ReadRoom(ref StreamReader f, int n, Map map)
        { 
            string name, desc;
            name = f.ReadLine()!;
            desc = f.ReadLine()!;
            f.ReadLine();                                            // Línea separatoria "------"
            map.AddRoom(n, name, desc);                              // Añade la habitacion leida al mapa
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

        static void ProcessCommand(Map map, string input, ref int playerRoom, ListaEnlazada inventory)
        {
            string[] words = input.Trim().ToUpper().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (words?.Length > 0) // si se ha escrito algo
                switch (words[0])
                {
                    case "HELP": // muestra la ayuda del juego
                        Console.WriteLine("COMMANDS: "
                                        + "\n\tHelp: shows available commands."
                                        + "\n\tInventory: shows the contents of your inventory."
                                        + "\n\tLook: shows the information of the current room."
                                        + "\n\tItems: shows the items in the current room."
                                        + "\n\tTake <item>: moves the item in the room to your inventory."
                                        + "\n\tDrop <item>: moves the item in your inventory to the room."
                                        + "\n\t<direction>: moves you towards the specified direction."); 
                        break;

                    case "INVENTORY": // muestra el inventario actual del jugador
                        string inventario = map.GetItemsInfo(inventory);
                        if (inventario.Trim() == string.Empty) // si está vacío
                            Console.WriteLine("There are no items in your inventory.");
                        else Console.WriteLine(inventario);
                        break;

                    case "LOOK": // muestra la información de la habitación actual
                        Console.WriteLine(map.GetInfoRoom(playerRoom));
                        break;

                    case "ITEMS": // muestra los ítems de la habitación actual
                        string items = map.GetItemsRoom(playerRoom);
                        if (items.Trim() == string.Empty) // si está vacío
                            Console.WriteLine("There are no items in your inventory.");
                        else Console.WriteLine(items);
                        break;

                    case "TAKE": // si el item está en habitación actual lo recoge
                                 // y lo añade al inventario del jugador;
                                 // mensaje de error en otro caso
                        if (words.Length > 1) // si hay una 2ª palabra
                            if (map.TakeItemRoom(playerRoom, words[1], inventory))
                                Console.WriteLine(words[1] + " taken.");
                            else Console.WriteLine("There is no such item in the room.");
                        else Console.WriteLine("Please specify an item to take.");
                        break;

                    case "DROP": // si el ítem está en el inventario del jugador,
                                 // lo elimina del inventario y lo deja en la habitación actual;
                                 // mensaje de error en otro caso
                        if (words.Length > 1) // si hay una 2ª palabra
                            if (map.DropItemRoom(playerRoom, words[1], inventory))
                                Console.WriteLine(words[1] + " dropped.");
                            else Console.WriteLine("There is no such item in your inventory.");
                        else Console.WriteLine("Please specify an item to drop.");
                        break;

                    case "END": // sale del programa
                        playerRoom = -1;
                        break;

                    default: // se interpreta como dirección de movimiento,
                             // que se gestionará con el método correspondiente de Map.
                        int[] salas = map.Move(playerRoom, words[0], inventory).ToArray(); // salas del camino realizado 
                        if (salas.Length > 0) // si se ha movido 
                        {
                            for (int i = 0; i < salas.Length && salas[i] > 0; i++) // para cada sala del camino (excepto sala final)
                                Console.WriteLine("\n" + map.GetInfoRoom(salas[i])); // muestra la información 
                            playerRoom = salas[^1];
                        }
                        else Console.WriteLine("Cannot move in that direction."); // lo interpreta como movimiento inválido
                        break;
                }
            Console.WriteLine(); // línea vacía estética de separación
        }

        static void Instructions()
        {
            Console.WriteLine("Would you like instructions?");
            Console.Write("\n> ");
            if (Console.ReadLine()!.Trim().ToUpper() == "YES") 
                Console.WriteLine("\nSomewhere nearby is Colossal Cave, where others have found fortunes " +
                    "in treasure and gold, though it is rumored that some who enter are never seen again. " +
                    " Magic is said to work in the cave.  I will be your eyes and hands.  Direct me with " +
                    "commands of 1 or 2 words.  Should you get stuck, type \"HELP\" for some general commands.\n");
        }
    
        static string[] ReadCommands()
        { // lea comandos de un archivo dado (uno por línea, por ejemplo) y los ejecute en secuencia.
            StreamReader sr = null!;
            string[] comandos = null!;
            try
            {
                sr = new StreamReader(COMMANDS_FILE);
                comandos = new string[File.ReadAllLines(COMMANDS_FILE).Length];
                int i = 0;
                while (!sr.EndOfStream)
                {
                    comandos[i] += sr.ReadLine();
                    i++;
                }
            }
            catch (FileNotFoundException fnfe) { Console.WriteLine("There is no command save file."); }
            catch (Exception e) { Console.WriteLine($"Error: " + e.Message); }
            finally { sr?.Close(); }
            return comandos;
        }

        static bool SaveCommands(string commands)
        {
            bool retorno = false;
            StreamWriter sw = null!;
            try
            {
                sw = new StreamWriter(COMMANDS_FILE, true);
                string[] words = commands.Trim().ToUpper().Split(",", StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < words.Length; i++)
                    sw.WriteLine(words[i]);
                retorno = true;
            }
            catch (Exception e) { Console.WriteLine($"Error: " + e.Message); retorno = false; }
            finally { sw?.Close(); }
            return retorno;
        }
    }
}