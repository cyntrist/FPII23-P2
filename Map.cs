// Cynthia Tristán Álvarez
// Paula Sierra Luque

using AdventureGame;
using Listas;

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

        public void AddItem(string name, string description, int iniRoom)
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
            {
                Console.WriteLine("ERROR: no se puede añadir nueva ruta.");
            }
        }

        private int GetItemIndex(string name)
        {
            int index = 0;
            while (items[index].name != name) //me da error al compilar
                index++;

            if (index == items.Length) // si ha acabado el array significa que NO lo ha encontrado
                return -1;
            else // si ha acabado antes de llegar al final significa que SÍ lo ha encontrado
                return index;
        }

        public void AddRoom(int nRoom, string name, string description)
        {
            if (nRoom >= rooms.Length) //si nRoom no cabe
            {
                Console.WriteLine("No se puede añadir la habitación.");
            }
            else // añadimos room
            {
                Room newRoom = new(name, description, maxRoutes);
                rooms[nRoom] = newRoom;
                nRooms++;
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
            return rooms[nRoom].GetArrayItems().ToString();
        }

        public void SetItemsRooms()
        {
            
        }

        public void WriteMap()
        {
            for (int n = 0; n < nRooms; n++)
            {
                //Console.WriteLine("Room " + n + ": " + rooms[n].name);    // nombre de cada room
                //Console.WriteLine(rooms[n].description);                  //descripcion de cada room
                Console.WriteLine("Directions: " );                         //direccion ?¿?¿ no se como implementarlo
                Console.WriteLine("Items: " + rooms[n].GetArrayItems());    //items de cada habitacion
                Console.WriteLine();                                        //linea en blanco (estetica)
            }
        }

        #region 6. Acciones del Jugador
        // public bool TakeItemRoom(int nRoom, string itemName, List inventory)
        // {
            
        // }
        // public bool DropItemRoom(int nRoom, string itemName, List inventory)
        // {

        // }

        // public List Move(int nRoom, string dir, List inventory)
        // {

        // }

        // public string GetItemsInfo(List inventory)
        // {

        // }
        #endregion
    }
}
