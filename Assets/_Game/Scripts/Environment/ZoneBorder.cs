using System;
using UnityEngine;

namespace Environment
{
    public class ZoneBorder : MonoBehaviour
    {

        [SerializeField] ZoneData frontalZone;
        [SerializeField] ZoneData backZone;

        public static event Action<ZoneData> OnZoneChanged;


        float enterDot, exitDot;

        MeshRenderer render;


        private void Awake()
        {
            render = GetComponent<MeshRenderer>();
            render.enabled = false;
        }


        private void Start() => GameConsole.instance.OnToggleTriggersView += ToggleTriggerVisibility;
        private void OnDestroy() => GameConsole.instance.OnToggleTriggersView -= ToggleTriggerVisibility;
        private void ToggleTriggerVisibility() => render.enabled = !render.enabled;


        private void OnTriggerEnter(Collider other) => enterDot = DirectionDot(other);

        private void OnTriggerExit(Collider other)
        {

            exitDot = DirectionDot(other);

            float direction = enterDot * exitDot;


            if (direction < 0)
            {
                if (exitDot > 0)
                {
                    OnZoneChanged?.Invoke(frontalZone);
                }
                else
                {
                    OnZoneChanged?.Invoke(backZone);
                }
            }
        }


        private float DirectionDot(Collider target)
        {
            Vector3 directionToTarget = Vector3.Normalize(target.transform.position - transform.position);

            return Vector3.Dot(transform.forward, directionToTarget);
        }
    }
}
