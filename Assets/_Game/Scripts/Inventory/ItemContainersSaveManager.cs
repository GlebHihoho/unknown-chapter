using UnityEngine;

public class ItemContainersSaveManager : MonoBehaviour, ISaveable
{

    [SerializeField] ItemsContainer[] containers;


    public void Load(SaveData.Save save)
    {
        for (int i = 0; i < containers.Length; i++)
        {
            if (i < save.levels[save.level].containers.Count)
                containers[i].ToggleContainer(save.levels[save.level].containers[i]);
            else containers[i].ToggleContainer(true);
        }
    }

    public void Save(ref SaveData.Save save)
    {
        save.levels[save.level].containers.Clear();

        for (int i = 0; i < containers.Length; i++)
        {
            save.levels[save.level].containers.Add(containers[i].enabled);
        }
    }
}
