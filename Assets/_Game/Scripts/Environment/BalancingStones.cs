using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(BoxCollider), typeof(NavMeshObstacle))]
public class BalancingStones : Interactable
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
    }
}
