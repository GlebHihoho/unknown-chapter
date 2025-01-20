using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects")]
    public class ItemSO : ScriptableObject
    {
        [FormerlySerializedAs("Name")] public string _name;
        [FormerlySerializedAs("Img")] public string _img;
        [FormerlySerializedAs("Description")] public string _description;
        [FormerlySerializedAs("Description")] public int _count;
    }
}
