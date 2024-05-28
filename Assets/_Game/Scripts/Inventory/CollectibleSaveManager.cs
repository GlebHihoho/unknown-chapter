using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSaveManager : MonoBehaviour, ISaveable
{
    [SerializeField] Collectible[] collectibles;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Load(SaveData.Save save)
    {
        for (int i = 0; i < collectibles.Length; i++)
        {
            if (i < save.collectibles.Count)
                collectibles[i].gameObject.SetActive(save.collectibles[i]);
            else collectibles[i].gameObject.SetActive(true);
        }
    }

    public void Save(ref SaveData.Save save)
    {
        save.collectibles.Clear();

        for (int i = 0; i < collectibles.Length; i++)
        {
            save.collectibles.Add(collectibles[i].gameObject.activeSelf);
        }
    }
}
