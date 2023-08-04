using UnityEngine;
using UnityEngine.Serialization;

namespace UI.InventoryScripts
{
    [System.Serializable]
    public class Item
    {
        public int _id;
        public string _name;
        public int _count;
        public Sprite _img;

        public string _description;
        public bool _isUsed;
        // добавить описание, bool значение одет предмет или нет, максимум два кольца и одно ожерелье
    }
}
