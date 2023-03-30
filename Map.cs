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

        }

        public Map()
        {

        }

        public void AddItem(string name, string description, int iniRoom)
        {

        }

        private int GetItemIndex(string name)
        {
            return 0;
        }

        public void AddRoom(int nRoom, string name, string description)
        {

        }

        public void AddRouteRoom(int nRoom, string dir, int destRoom, string condItem)
        {

        }

        public void AddItemRoom(int nRoom, int itemId)
        {

        }

        public string GetInfoRoom(int nRoom)
        {
            return null;
        }

        public string GetItemsRoom(int nRoom)
        {
            return null;
        }

        /// <summary>
        /// ////////////////////////////////////////
        /// </summary>
        public void SetItemsRooms()
        {

        }

        public void WriteMap()
        {

        }
    }
}
