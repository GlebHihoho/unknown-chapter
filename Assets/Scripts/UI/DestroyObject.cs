using DefaultNamespace;
using UnityEngine;

namespace UI
{
    public class DestroyObject : MonoBehaviour
    {
        [SerializeField]private QuickItemView _quickItemView;
        private Transform _player;

        private void Start()
        {
            _player = GameObject.FindWithTag("Player").transform;
            _quickItemView = FindObjectOfType<QuickItemView>();
        }

        public void ThisDestroyObject()
        {
            _quickItemView.SetIsInventoryOpen(false);
            Destroy(transform.parent.gameObject);
        }
    }
}