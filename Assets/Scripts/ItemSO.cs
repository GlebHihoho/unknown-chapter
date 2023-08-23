using UnityEngine;

namespace DefaultNamespace
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects")]
    public class ItemSO : ScriptableObject
    {
        
        //  public int ID { get; set; }
        public string Name;
        public int Count;
        public string Img;
        public string Description;
        public bool IsUsed;
        // добавить описание, bool значение одет предмет или нет, максимум два кольца и одно ожерелье
    }
}