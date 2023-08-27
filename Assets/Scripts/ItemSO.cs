﻿using System.Collections.Generic;
using UI.InventoryScripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects")]
    public class ItemSO : ScriptableObject
    {
        [FormerlySerializedAs("Name")] public string _name;
        [FormerlySerializedAs("Img")] public string _img;
        [FormerlySerializedAs("Description")] public string _description;
    }
}