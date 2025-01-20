

namespace UI.InventoryScripts
{
    [System.Serializable]
    public class Item
    {
      //  public int ID { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public string Img { get; set; }
        public string Description { get; set; }
        public bool IsUsed { get; set; }
        // добавить описание, bool значение одет предмет или нет, максимум два кольца и одно ожерелье
    }
}
