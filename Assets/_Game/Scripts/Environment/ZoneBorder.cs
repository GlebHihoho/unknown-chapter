using System;
using UnityEngine;

namespace Environment
{
    [RequireComponent(typeof(TriggerVisuals))]
    public class ZoneBorder : MonoBehaviour
    {

        [SerializeField] ZoneData frontalZone;
        [SerializeField] ZoneData backZone;

        public static event Action<ZoneData> OnZoneChanged;


        float enterDot, exitDot;


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
