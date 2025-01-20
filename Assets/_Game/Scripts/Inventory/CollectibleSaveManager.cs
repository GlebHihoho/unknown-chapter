using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSaveManager : MonoBehaviour, ISaveable
{
    [SerializeField] Collectible[] collectibles;


    public void Load(SaveData.Save save)
    {
        for (int i = 0; i < collectibles.Length; i++)
        {
            if (i < save.levels[save.level].collectibles.Count)
                collectibles[i].gameObject.SetActive(save.levels[save.level].collectibles[i]);
            else collectibles[i].gameObject.SetActive(true);
        }
    }

    public void Save(ref SaveData.Save save)
    {
        save.levels[save.level].collectibles.Clear();

        for (int i = 0; i < collectibles.Length; i++)
        {
            save.levels[save.level].collectibles.Add(collectibles[i].gameObject.activeSelf);
        }
    }
}
