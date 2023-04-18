// Cynthia Tristán Álvarez
// Paula Sierra Luque

using AdventureGame;
using Listas;
using System.Xml.Linq;
using static AdventureGame.Map;

namespace AdventureGame
{
    public class Map
    {
        public struct Item
        { // información de cada ítem
            public string name, description; // como aparecen en el archivo CrowtherItems
            public int initialRoom; // índice de la habitación donde está al principio del juego
        }

        Room[] rooms; // array de habitaciones indexadas con la numeración de CrowtherRooms
        int nRooms; // número de habitaciones = índice a la primera posición libre en rooms
        public Item[] items; // array de items en el juego indexados por orden de aparición en el archivo
        int nItems; // número de ítems = índice la primera posición libre en items
        int maxRoutes; // número máximo de rutas por habitación

        public Map(int maxRooms = 100, int maxRts = 10, int maxItems = 20)
        {
            rooms = new Room[maxRooms];
            nRooms = 0;
            maxRoutes = maxRts;
            items = new Item[maxItems];
            nItems = 0;
        }

        public void AddItemMap(string name, string description, int iniRoom)
        // anade item a array de items con datos dados, anade item al mapa pero no lo coloca
        //en la habitacion. la habitacion puede no estar creada (se usara SetItemsRoom)
        {
            if (nItems < items.Length) // si hay espacio
            {
                Item item = new() // crea un item nuevo
                {
                    name = name,
                    description = description,
                    initialRoom = iniRoom
                };

                items[nItems] = item; // lo asigna
                nItems++; // lo añade
            }
            else // si no hay espacio
                Console.WriteLine("ERROR: no se puede añadir nueva ruta.");
        }

        private int GetItemIndex(string name)
        { // busca el ítem name en el array de ítems y devuelve
          // su posición en dicho array; -1 si no existe tal ítem.
          //            no se como hacerlo usando BuscaDato() ???
            int index = 0;
            while (index < items.Length && items[index].name != name) // solucionado error de salida de array
                index++;

            if (index == items.Length) // si ha acabado el array significa que NO lo ha encontrado
                return -1;
            else // si ha acabado antes de llegar al final significa que SÍ lo ha encontrado
                return index;
        }

        public void AddRoom(int nRoom, string name, string description)
        {
            if (nRoom < rooms.Length) // si nRoom cabe
            {
                Room newRoom = new(name, description, maxRoutes);
                rooms[nRoom] = newRoom;
                nRooms++;
            }
            else // si no
            {
                Console.WriteLine("No se puede añadir la habitación.");
            }
        }

        public void AddRouteRoom(int nRoom, string dir, int destRoom, string condItem)
        {
            int condicion = -1; // si no tiene condItem, se queda en 0 (no se si tiene que ser 0???)
            if (condItem != null || condItem != "") // si tiene condItem
                condicion = GetItemIndex(condItem); // se lo asigna a la variable
            rooms[nRoom].AddRoute(dir, destRoom, condicion); // añade la ruta
        }

        public void AddItemRoom(int nRoom, int itemId)
        {
            rooms[nRoom].AddItem(itemId);
        }

        public string GetInfoRoom(int nRoom)
        {
            return rooms[nRoom].GetInfo();
        }

        public string GetItemsRoom(int nRoom)
        {
            return rooms[nRoom].GetStringItems();
        }

        #region 5. Lectura y almacenamiento de datos
        public void SetItemsRooms()
        {   // Recorre el array de items del mapa, añadiendo cada uno a su habitación de inicio.
            int i = 0;
            // for (i = 0; i < items.Length; i++) // ESTO ESTA MAL porque recorre espacios vacíos
            while (i < items.Length && items[i].name != null) // hasta que se acabe el array o encuentre un elto vacío
            {
                AddItemRoom(items[i].initialRoom, GetItemIndex(items[i].name)); // añade cada item a su habitación inicial
                i++;
            }
        }
        public void WriteMap()
        {   // Escribe en pantalla toda la información del mapa.
            for (int i = 0; i < nRooms; i++)
            {                                               
                Console.Write(GetInfoRoom(i));              // HABITACIONES DEL MAPA

                Console.WriteLine("Direcciones:");          // DIRECCIONES DE LA HABITACIÓN
                for (int j = 0; j < rooms[i].nRoutes; j++)  
                    Console.WriteLine("\t" + rooms[i].routes[j].direction 
                                    + "\t" + rooms[i].routes[j].destRoom
                                    + "\t" + rooms[i].routes[j].conditionalItem);

                if (rooms[i].GetArrayItems().Length > 0) // si la habitación tiene items
                    Console.WriteLine(GetItemsRoom(i));     // ÍTEMS DE LA HABITACIÓN
                Console.WriteLine();
            }
        }
        #endregion

        #region 6. Acciones del Jugador
        
        // public bool TakeItemRoom(int nRoom, string itemName, List inventory)
        // {

        // }
        // public bool DropItemRoom(int nRoom, string itemName, List inventory)
        // {

        // }
        // public ListaEnlazada Move(int nRoom, string dir, ListaEnlazada inventory)
        // {

        // }
        // public string GetItemsInfo(ListaEnlazada inventory)
        // {

        // }
        #endregion
    }
}
