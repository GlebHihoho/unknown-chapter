﻿using UnityEngine;

namespace DefaultNamespace
{
    public class DestroyObject : MonoBehaviour
    {
        public void ThisDestroyObject()
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
