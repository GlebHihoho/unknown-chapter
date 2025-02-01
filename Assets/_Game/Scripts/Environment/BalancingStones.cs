using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(BoxCollider), typeof(NavMeshObstacle))]
public class BalancingStones : Interactable, ISaveable
{

    new BoxCollider collider;
    NavMeshObstacle obstacle;

    [SerializeField] Rigidbody[] stones;

    private void Awake()
    {
        collider = GetComponent<BoxCollider>();
        obstacle = GetComponent<NavMeshObstacle>();
    }

    protected override void PerfomInteraction()
    {
        base.PerfomInteraction();

        foreach (Rigidbody stone in stones) 
        { 
            stone.isKinematic = false;
            
        }

        obstacle.enabled = false;
        collider.enabled = false;
        enabled = false;        
    }

    public void Save(ref SaveData.Save save)
    {

        SaveData.BalancingStones stonesData = save.levels[save.level].balancingStones;

        stonesData.isActive = enabled;

        foreach (Rigidbody stone in stones)
        {
            stonesData.stonesPositions.Add(stone.position);
        }

    }

    public void Load(SaveData.Save save)
    {

        SaveData.BalancingStones stonesData = save.levels[save.level].balancingStones;

        if (!stonesData.isActive)
        {


            for (int i = 0; i < stones.Length; i++)
            {
                if (i < stonesData.stonesPositions.Count) 
                    stones[i].position = stonesData.stonesPositions[i];

                stones[i].isKinematic = false;
            }

            obstacle.enabled = false;
            collider.enabled = false;
            enabled = false;
        }
    }
}
