using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerSave : MonoBehaviour, ISaveable
{

    public void Load(SaveData.Save save)
    {
        if (save.levels[save.level].playerPosition != Vector3.zero)
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();

            agent.enabled = false;
            transform.position = save.levels[save.level].playerPosition;
            transform.rotation = save.levels[save.level].playerRotation;
            agent.enabled = true;
        }
    }

    public void Save(ref SaveData.Save save)
    {
        save.levels[save.level].playerPosition = transform.position;
        save.levels[save.level].playerRotation = transform.rotation;
    }

}
