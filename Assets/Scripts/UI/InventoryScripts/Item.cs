using UnityEngine;

namespace UI.InventoryScripts
{
    [System.Serializable]
    public class Item
    {
        public int id;
        public string name;
        public int count;
        public Sprite img;

        public string description;
        public bool isUsed;
        // добавить описание, bool значение одет предмет или нет, максимум два кольца и одно ожерелье
    }
}
