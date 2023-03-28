// Cynthia Tristán Álvarez
// Paula Sierra Luque

using System.Text.RegularExpressions;

namespace AdventureGame
{
    internal class Program
    {
        const string ITEMS_FILE = @"CrowtherItems.txt";
        const string ROOMS_FILE = @"CrowtherRooms.txt";

        static void Main()
        {
            ReadInventory(ITEMS_FILE);
            ReadRooms(ROOMS_FILE);
        }

        #region métodos
        static void ReadInventory(string file)
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

        static void ReadRooms(string file)
        {
            StreamReader f = new (file);
            while (!f.EndOfStream)
            {
                int n = int.Parse(f.ReadLine()!);
                ReadRoom(ref f, n);
            }
            f.Close();
        }

        static void ReadRoom(ref StreamReader f, int n)
        {
            Console.WriteLine("Room: " + n + "   "
                + "Name: "  + f.ReadLine() + "   "
                + "Descr: " + f.ReadLine());
            f.ReadLine(); // ------
            string nl = f.ReadLine()!;
            while (!f.EndOfStream && !string.IsNullOrWhiteSpace(nl)) // hasta que haya línea en blanco
            {
                nl = Regex.Replace(nl, @"\s+", "/"); // Reemplaza todos los espacios con un solo '/'
                string[] bits = nl.Split("/"); // Parte la línea en trozos entre '/'
                nl = "Route from room " + n + " to room " + bits[1] + ", direction " + bits[0] + ". CondItem: ";
                if (bits.Length > 2) { nl += bits[2]; } // Si tiene CondItem se lo añade
                Console.WriteLine(nl); // Lo escribe
                nl = f.ReadLine()!; // Siguiente línea
            }
        }
        #endregion
    }
}