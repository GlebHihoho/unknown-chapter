using UnityEngine;

public class TriggersManager : MonoBehaviour, ISaveable
{

    [SerializeField] Trigger[] triggers;


    public void Save(ref SaveData.Save save)
    {
        for (int i = 0; i < triggers.Length; i++)
        {
            if (i < save.levels[save.level].triggers.Count)
                triggers[i].gameObject.SetActive(save.levels[save.level].triggers[i]);
            else triggers[i].gameObject.SetActive(true);
        }
    }

    public void Load(SaveData.Save save)
    {
        save.levels[save.level].triggers.Clear();

        for (int i = 0; i < triggers.Length; i++)
        {
            save.levels[save.level].triggers.Add(triggers[i].gameObject.activeSelf);
        }
    }
}
