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
        }

        #region Métodos Read
        static private void ReadInventory(string file, Map map)
        {
            StreamReader sr = new(file);
            while (!sr.EndOfStream)
            {
                string name, desc, iniRoom;
                name = sr.ReadLine();
                desc = sr.ReadLine();
                iniRoom = sr.ReadLine();
                Console.WriteLine("Item name: " + name + "   " 
                                + "Descr: " + desc + "   " 
                                + "InitRoom: " + iniRoom);
                sr.ReadLine();
                map.AddItem(name, desc, int.Parse(iniRoom));
                // map.AddItemRoom(int.Parse(iniRoom), ???)
                // en el enunciado ponía AddItemRoom pero no entiendo por qué??
                // porque además no podemos usar getitemindex() porque es privado
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
            string name, desc;
            name = f.ReadLine()!;
            desc = f.ReadLine()!;
            Console.WriteLine("Room: "  + n     + "   "
                            + "Name: "  + name  + "   "
                            + "Descr: " + desc);
            f.ReadLine();                                            // Línea separatoria "------"

            map.AddRoom(n, name, desc); // deberiamos llamar description en el readline anterior ? hecho, y name también

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

                map.AddRouteRoom(n, bits[0], int.Parse(bits[1]), conditionalItem); // Esto ya debería ir bien? idk

                newline = f.ReadLine()!;                                  // Siguiente línea
            }                                                        // Si la siguiente línea es blanca, acaba el método
        }
        #endregion
    }
}