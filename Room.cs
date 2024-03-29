﻿// Cynthia Tristán Álvarez
// Paula Sierra Luque

using Listas;

namespace AdventureGame
{
    internal class Room
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
        ListaEnlazada items; // lista de índices de ítems (al array de ítems de Map)

        public Room(string nam, string des, int maxRts) //constructora Room
        {
            name = nam;
            description = des;
            routes = new Route[maxRts];
            nRoutes = 0;
            items = new ListaEnlazada();
        }

        #region ADICIÓN
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
        #endregion

        #region INFORMACIÓN
        public string GetInfo()
        {
            return name + "\n" + description;
        }

        public string GetRouteInfo(int index) // método nuevo, devuelve la información de la ruta indexada de la habitación
        {
            return "\t" + routes[index].direction
                 + "\t" + routes[index].destRoom
                 + "\t" + routes[index].conditionalItem;
        }

        public int GetRouteNumber() // método nuevo, sólo es un getter de nRoutes para no cambiarle la accesibilidad 
        {
            return nRoutes;
        }

        public int[] GetArrayItems()
        {
            return items.ToArray();
        }
        #endregion

        #region 6. Acciones del Jugador
        public int Move(string dir, ListaEnlazada inventory)
        // devuelve la habitación de destino en la dirección dir, si es posible el movimiento.
        // Para ello busca la primera ruta en esa dirección que no requiera ítem condicional,
        // o bien, requiera un ítem presente en la lista inventory. Si existe tal ruta
        // devuelve la habitación de destino correspondiente; en otro caso devuelve -1.
        {
            int room = -1; // Por si no se encuentra room

            for (int i = 0; i < nRoutes; i++)
            {
                if (routes[i].direction == dir) //si dir es igual a una de las posibles rutas
                {
                    if (routes[i].conditionalItem == -1) //si no hay condItem
                        room = routes[i].destRoom; //vamos a la room destino
                    else if (inventory.BuscaDato(routes[i].conditionalItem)) // si sí lo hay y lo tiene en el inv
                        room = routes[i].destRoom; //vamos a la room destino
                }
            }
            return room;
        }

        public bool ForcedMove() //comprueba si la habitacion tiene al menos una ruta
        { // (por definición, si una es forzada, todas deben serlo).
            bool retorno = false;
            int i = 0;
            while (i < nRoutes && routes[i].direction != "FORCED") // hasta que encuentre una ruta forzada o acabe
                i++; // lo sigue recorriendo
            if (i < nRoutes) // si cuando acaba es menor o igual que el tamaño del array, lo ha encontrado
                retorno = true; // devuelve true
            return retorno; // si no, false
        }

        public bool RemoveItem(int it)
        { // elimina el ítem de índice it de la lista de ítems de la habitación, si existe.
          // En ese caso devuelve true, en otro caso false.
            bool retorno = false;
            if (items.BuscaDato(it)) // si el elemento está en la lista
            {
                items.EliminaElto(it); // lo eliminamos
                retorno = true;        //y devuelve true
            }
            return retorno;   // si no esta devuelve false
        }
        #endregion
    }
}