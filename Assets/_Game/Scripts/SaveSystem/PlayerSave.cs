using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSave : MonoBehaviour, ISaveable
{

    public void Load(SaveData.Save save)
    {
        if (save.playerPosition != Vector3.zero)
        {
            transform.position = save.playerPosition;
            transform.rotation = save.playerRotation;
        }
    }

    public void Save(ref SaveData.Save save)
    {
        save.playerPosition = transform.position;
        save.playerRotation = transform.rotation;
    }

}
