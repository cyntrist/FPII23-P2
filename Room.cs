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

        public string name, description; // nombre y descripción de la habitación leídos de CrowtherRooms
        public Route[] routes; // array de rutas de la habitación
        public int nRoutes; // número de rutas = índice a la primera ruta libre
        ListaEnlazada items; // lista de índices de ítems (al array de ítems de Map)

        public Room(string nam, string des, int maxRts) //constructora Room
        {
            name = nam;
            description = des;
            routes = new Route[maxRts];
            nRoutes = 0; 
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
                routes[nRoutes] = ruta; // o routes[nRoutes++] = ruta; ?
                nRoutes++;
            }
            else // si no hay espacio
                Console.WriteLine("ERROR: no se puede añadir nueva ruta.");
        }

        public void AddItem(int it)
        {
            items.InsertaFinal(it);
        }

        public string GetInfo()
        {
            return "Habitación: "   + name 
               + "\nDescripción: "  + description 
               + "\n";
        }

        public int[] GetArrayItems()
        {
            return items.ToArray();
        }

        public string GetStringItems() // MÉTODO NUEVO: ILEGAL??
        {
            return items.ToString();
        }

        #region 6. Acciones del Jugador
        // public int Move(string dir, List inventory)
        // {
            
        // }
        // public bool ForcedMove()
        // {
            
        // }
        public bool RemoveItem(int it)
        {
            if (items.BuscaDato(it)) // si el elemento está en la lista
            {  
                items.EliminaElto(it); // lo eliminamos
                return true;           //y devuelve true
            } 
            else return false;   // si no esta devuelve false
        }
        #endregion
    }
}