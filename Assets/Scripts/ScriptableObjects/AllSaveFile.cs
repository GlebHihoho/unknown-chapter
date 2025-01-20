using UnityEngine;
using System.Collections.Generic;

namespace ScriptableObjects
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "AllSaveFile", menuName = "ScriptableObjects")]
    public class AllSaveFile : ScriptableObject
    {
        public List<SaveFile> _saveFile = new();
    }
}