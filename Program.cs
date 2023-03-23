// Cynthia Tristán Álvarez
// Paula Sierra Luque

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
            StreamReader f = new StreamReader(file);
            while (!f.EndOfStream)
            {
                int n = int.Parse(f.ReadLine());
                ReadRoom(ref f, n);
            }
            f.Close();
        }

        static void ReadRoom(ref StreamReader f, int n)
        {
            while(!f.EndOfStream && f.Peek() != -1)
            Console.WriteLine("Room: " + f.ReadLine() + "   "
                                + "Name: " + f.ReadLine() + "   "
                                + "InitRoom: " + f.ReadLine());
            f.ReadLine();
        }
        #endregion
    }
}