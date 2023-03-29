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

            public static bool operator ==(Route r1, Route r2) { return false; }
            public static bool operator !=(Route r1, Route r2) { return false; }
        }

        string name, description; // nombre y descripción de la habitación leídos de CrowtherRooms
        Route[] routes; // array de rutas de la habitación
        int nRoutes; // número de rutas = índice a la primera ruta libre
        List<Item> items; // lista de índices de ítems (al array de ítems de Map)

        public Room(string nam, string des, int maxRts)
        {
            name = nam;
            description = des;
            routes = new Route[maxRts];
            items = new List<Item>();
        }

        public Room()
        {
        }

        public void AddRoute(string dir, int desR, int condIt)
        {
            Route ruta = new() // inicaliza nueva ruta
            {
                direction = dir,
                destRoom = desR,
                conditionalItem = condIt
            };

            int i = 0; // para añadirlo en el primer hueco nulo
            while (routes[i] != null) i++;
            routes[i] = ruta;
        }

        public void AddItem(int it)
        {
            items.Add(it);
        }

        public string GetInfo()
        {
            return null;
        }

        public int[] GetArrayItems()
        {
            return null;
        }
    }

}