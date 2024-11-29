using UnityEngine;

[RequireComponent(typeof(NPCManager))]
public class NPCSaveManager : MonoBehaviour, ISaveable
{

    //[SerializeField] ConversationPartner[]  characters;

    NPCManager manager;


    private void Awake()
    {
        manager = GetComponent<NPCManager>();
    }


    public void Load(SaveData.Save save)
    {
        for (int i = 0; i < manager.Characters.Length; i++)
        {
            if (i < save.levels[save.level].characters.Count)
                manager.Characters[i].SetTalkIndex(save.levels[save.level].characters[i].talkIndex);
            else manager.Characters[i].SetTalkIndex(0);
        }
    }

    public void Save(ref SaveData.Save save)
    {
        save.levels[save.level].characters.Clear();

        for (int i = 0; i < manager.Characters.Length; i++)
        {
            SaveData.Character character;

            character.talkIndex = manager.Characters[i].TalkIndex;

            save.levels[save.level].characters.Add(character);
        }
    }
}
