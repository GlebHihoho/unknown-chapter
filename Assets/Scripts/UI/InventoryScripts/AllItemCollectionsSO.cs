using System.Collections.Generic;
using DefaultNamespace;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.InventoryScripts
{
    [CreateAssetMenu(fileName = "AllItemCollections", menuName = "ScriptableObjects")]
    public class AllItemCollectionsSO : ScriptableObject
    {
        [FormerlySerializedAs("AllItemsCollections")] public List<ItemSO> _allItemCollections;
    }
}