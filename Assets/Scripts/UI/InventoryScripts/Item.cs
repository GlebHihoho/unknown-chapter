using UnityEngine;

namespace UI.InventoryScripts
{
    [System.Serializable]
    public class Item
    {
        public int _id { get; set; }
        public string _name { get; set; }
        public int _count { get; set; }
        public string _img { get; set; }

        public string _description { get; set; }
        public bool _isUsed { get; set; }
        // добавить описание, bool значение одет предмет или нет, максимум два кольца и одно ожерелье
    }
}
