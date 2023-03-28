// Cynthia Tristán Álvarez
// Paula Sierra Luque

using Crowther.Maps;
using static Crowther.Maps.Map;

namespace Crowther.Rooms
{
    public class Room
    {
        public struct Route
        { // tipo para las rutas
            public string direction;
            public int destRoom, // habitación destino de la ruta
            conditionalItem; // índice del ítem condicional (al array de ítems de Map)
        }

        string name, description; // nombre y descripción de la habitación leídos de CrowtherRooms
        Route[] routes; // array de rutas de la habitación
        int nRoutes; // número de rutas = índice a la primera ruta libre
        List<Item> items; // lista de índices de ítems (al array de ítems de Map)

        public Room(string nam, string des, int maxRts)
        {

        }

        public Room()
        {

        }

        static public void AddRoute(string dir, int desR, int condIt)
        {

        }

        static public void AddItem(int it)
        {

        }

        static public string GetInfo()
        {
            return null;
        }

        static public int[] GetArrayItems()
        {
            return null;
        }
    }

}