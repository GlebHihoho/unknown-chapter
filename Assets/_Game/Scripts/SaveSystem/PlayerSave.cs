using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class PlayerSave : MonoBehaviour, ISaveable
{

    [SerializeField] CinemachineCamera virtualCamera;

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

        if (save.levels[save.level].cameraPosition != Vector3.zero)
        {
            virtualCamera.ForceCameraPosition(save.levels[save.level].cameraPosition, save.levels[save.level].cameraRotation);
        }
    }

    public void Save(ref SaveData.Save save)
    {
        save.levels[save.level].playerPosition = transform.position;
        save.levels[save.level].playerRotation = transform.rotation;

        save.levels[save.level].cameraPosition = Camera.main.transform.position;
        save.levels[save.level].cameraRotation = Camera.main.transform.rotation;
    }

}
