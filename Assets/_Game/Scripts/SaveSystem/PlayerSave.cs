using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSave : MonoBehaviour, ISaveable
{

    public void Load(SaveData.Save save)
    {
        if (save.levels[save.level].playerPosition != Vector3.zero)
        {
            transform.position = save.levels[save.level].playerPosition;
            transform.rotation = save.levels[save.level].playerRotation;
        }
    }

    public void Save(ref SaveData.Save save)
    {
        save.levels[save.level].playerPosition = transform.position;
        save.levels[save.level].playerRotation = transform.rotation;
    }

}
