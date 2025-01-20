using UnityEngine;

namespace ScriptableObjects
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "SaveFile", menuName = "ScriptableObjects")]
    public class SaveFile : ScriptableObject
    {
        public string FileName;
    }
}