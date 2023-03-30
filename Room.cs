// Cynthia Tristán Álvarez
// Paula Sierra Luque

using AdventureGame;
using Listas;

namespace AdventureGame
{
    public class Room
    {
        public struct Route
        { // tipo para las rutas
            public string direction;
            public int destRoom, // habitación destino de la ruta
            conditionalItem; // índice del ítem condicional (al array de ítems de Map)

            //public static bool operator ==(Route r1, Route r2) { return false; }
            //public static bool operator !=(Route r1, Route r2) { return false; }
        }

        string name, description; // nombre y descripción de la habitación leídos de CrowtherRooms
        Route[] routes; // array de rutas de la habitación
        int nRoutes; // número de rutas = índice a la primera ruta libre
        ListaEnlazada items; // lista de índices de ítems (al array de ítems de Map)

        public Room(string nam, string des, int maxRts)
        {
            name = nam;
            description = des;
            routes = new Route[maxRts];
            items = new ListaEnlazada();
        }

        public void AddRoute(string dir, int desR, int condIt)
        {
            if (nRoutes < routes.Length) // si hay espacio
            {
                Route ruta = new() // inicaliza nueva ruta
                {
                    direction = dir,
                    destRoom = desR,
                    conditionalItem = condIt
                };

                routes[nRoutes] = ruta; 
                nRoutes++;
                //routes[nRoutes++] = ruta;
            }
            else // si no hay espacio
            {
                Console.WriteLine("ERROR: no se puede añadir nueva ruta.");
            }
        }

        public void AddItem(int it)
        {
            items.InsertaFinal(it);
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