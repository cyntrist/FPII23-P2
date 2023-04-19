// Cynthia Tristán Álvarez
// Paula Sierra Luque

using Listas;

namespace AdventureGame
{
    internal class Map
    {
        public struct Item
        { // información de cada ítem
            public string name, description; // como aparecen en el archivo CrowtherItems
            public int initialRoom; // índice de la habitación donde está al principio del juego
        }

        Room[] rooms; // array de habitaciones indexadas con la numeración de CrowtherRooms
        int nRooms; // número de habitaciones = índice a la primera posición libre en rooms
        Item[] items; // array de items en el juego indexados por orden de aparición en el archivo
        int nItems; // número de ítems = índice la primera posición libre en items
        int maxRoutes; // número máximo de rutas por habitación

        public Map(int maxRooms = 100, int maxRts = 10, int maxItems = 20)
        { // crea los arrays rooms e items de tamaños maxRooms y maxItems, con 0 habitaciones y 0 ítems.
          // Además inicializa el atributo maxRoutes = maxRts.
            rooms = new Room[maxRooms];
            nRooms = 0;
            maxRoutes = maxRts;
            items = new Item[maxItems];
            nItems = 0;
        }

        #region Adición
        public void AddItemMap(string name, string description, int iniRoom)
        // anade item a array de items con datos dados, anade item al mapa pero no lo coloca
        // en la habitacion. la habitacion puede no estar creada (se usara SetItemsRoom)
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

        public void AddRoom(int nRoom, string name, string description)
        { // añade la habitación nRoom al mapa con el nombre y la descripción dados.
            if (nRoom < rooms.Length) // si nRoom cabe
            {
                Room newRoom = new(name, description, maxRoutes);
                rooms[nRoom] = newRoom;
                nRooms++;
            }
            else // si no cabe
                Console.WriteLine("No se puede añadir la habitación.");
        }

        public void AddRouteRoom(int nRoom, string dir, int destRoom, string condItem)
        { // añade a la habitación nRoom una nueva ruta con dirección dir y habitación destino destRoom.
          // El nombre del ítem condicional viene dado como string (cadena vacía si no hay tal ítem);
          // hay que obtener su índice con GetItemIndex para invocar al método AddRoute de la clase Room.
            int condicion = -1; // si no tiene condItem, se queda en 0 (no se si tiene que ser 0???)
            if (condItem != null || condItem != "") // si tiene condItem
                condicion = GetItemIndex(condItem!); // se lo asigna a la variable
            rooms[nRoom].AddRoute(dir, destRoom, condicion); // añade la ruta
        }

        public void AddItemRoom(int nRoom, int itemId)
        { // añade el ítem itemId a la habitación nRoom.
            rooms[nRoom].AddItem(itemId);
        }
        #endregion

        #region Información
        private int GetItemIndex(string name)
        { // busca el ítem name en el array de ítems y devuelve
          // su posición en dicho array; -1 si no existe tal ítem. buscadato()??
            int index = 0;
            while (index < items.Length && items[index].name != name) // solucionado error de salida de array
                index++;

            if (index == items.Length) // si ha acabado el array significa que NO lo ha encontrado
                return -1;
            else // si ha acabado antes de llegar al final significa que SÍ lo ha encontrado
                return index;
        }

        public string GetInfoRoom(int nRoom)
        { // devuelve una cadena de texto con el nombre y la descripción de la habitación nRoom.
            return rooms[nRoom].GetInfo();
        }

        public string GetItemsRoom(int nRoom)
        { // devuelve un string con la información de los ítems de la habitación nRoom.
            string info = string.Empty;
            int[] indexes = rooms[nRoom].GetArrayItems();
            for (int i = 0; i < indexes.Length; i++)
                info += "\n" + items[indexes[i]].name + ": " + items[indexes[i]].description;
            return info;
        }

        public string GetItemsInfo(ListaEnlazada inventory)
        { // devuelve un string con el nombre y la descripción de los ítems de inventory.
            string info = string.Empty;
            int[] indexes = inventory.ToArray();
            for (int i = 0; i < indexes.Length; i++)
                info += "\n" + items[indexes[i]].name + ": " + items[indexes[i]].description;
            return info;
        }
        #endregion

        #region 5. Lectura y almacenamiento de datos
        public void SetItemsRooms()
        {   // Recorre el array de items del mapa, añadiendo cada uno a su habitación de inicio.
            int i = 0;
            while (i < items.Length && items[i].name != null) // hasta que se acabe el array o encuentre un elto vacío
            {
                AddItemRoom(items[i].initialRoom, GetItemIndex(items[i].name)); // añade cada item a su habitación inicial
                i++;
            }
        }
        public void WriteMap()
        {   // Escribe en pantalla toda la información del mapa.
            for (int i = 1; i < nRooms; i++)
            {
                Console.WriteLine(GetInfoRoom(i));              // HABITACIONES DEL MAPA

                Console.WriteLine("Directions:");               // DIRECCIONES DE LA HABITACIÓN
                for (int j = 1; j < rooms[i].GetRouteNumber(); j++)
                    Console.WriteLine(rooms[i].GetRouteInfo(j));

                if (rooms[i].GetArrayItems().Length > 0) // si la habitación tiene items
                    Console.WriteLine("Items: " + GetItemsRoom(i)); // ÍTEMS DE LA HABITACIÓN
                Console.WriteLine();
            }
        }
        #endregion

        #region 6. Acciones del Jugador
        public bool TakeItemRoom(int nRoom, string itemName, ListaEnlazada inventory)
        { // busca el ítem de nombre itemName en la habitación nRoom.
          // Si está lo elimina de dicha habitación, lo añade a inventory y devuelve true;
          // en otro caso devuelve false.
            bool retorno = false;
            int[] roomItems = rooms[nRoom].GetArrayItems(); // índices de los ítems en la habitacion
            int index = GetItemIndex(itemName); // índice general del ítem a buscar

            if (roomItems.Length > 0 && index > -1) // si la habitación tiene objetos y el índice es válido
            {
                int i     = 0;                      // contador
                while (i < roomItems.Length && roomItems[i] != index) // mientras recorra el array && no encuentre el ítem
                    i++;

                if (roomItems[i] == index) // si está el ítem en la habitación
                {
                    rooms[nRoom].RemoveItem(index); // lo elimina de la habitación
                    inventory.InsertaFinal(index); // lo añade al inventario
                    retorno = true;
                }
            }
            return retorno;
        }
        public bool DropItemRoom(int nRoom, string itemName, ListaEnlazada inventory)
        { // busca el ítem de nombre itemName en inventory. Si está lo elimina de dicha lista,
          // lo añade a la habitación nRoom y devuelve true; en otro caso devuelve false.
            bool retorno = false;
            int index = GetItemIndex(itemName); // índice general del ítem
            if (inventory.BuscaDato(index)) // si el ítem está en el inventario
            {
                inventory.EliminaElto(index);
                rooms[nRoom].AddItem(index);
                retorno = true;
            }
            return retorno;
        }
        public ListaEnlazada Move(int nRoom, string dir, ListaEnlazada inventory)
        { // intenta el movimiento desde la habitación nRoom en la dirección dir y devuelve una lista
          // con las habitaciones visitadas al hacer ese movimiento (nótese que puede ser más de una
          // debido a los movimientos forzados). Para ello intenta el primer movimiento; si es posible,
          // mientras la habitación destino sea de movimiento forzado, realiza los siguientes movimientos
          // y va guardando los sucesivos números de habitación en una lista, que devolverá al final.

            ListaEnlazada visitadas = new(); // creamos lista de habitaciones visitadas
            int nextRoom = rooms[nRoom].Move(dir, inventory); // intenta el primer movimiento
            if (nextRoom > -1) // si ha sido posible
            {
                visitadas.InsertaFinal(nextRoom); // añadimos habitación a la lista
                if (rooms[nextRoom].ForcedMove()) // si ésta habitacion tiene forzadas, todas sus rutas lo son
                    while (rooms[nextRoom] != null && rooms[nextRoom].ForcedMove()) // mientras tenga rutas y forzadas
                    {
                        nextRoom = rooms[nextRoom].Move("FORCED", inventory); // movemos a forzado
                        visitadas.InsertaFinal(nextRoom); // añadimos la nueva sala a la lista
                    }
            }
            return visitadas; // y devolvemos lista
        }
        #endregion
    }
}
