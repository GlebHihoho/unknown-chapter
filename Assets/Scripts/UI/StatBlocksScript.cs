using UnityEngine;

namespace UI
{
    public class StatBlocksScript : MonoBehaviour
    {
        [SerializeField] private GameObject _mainStatObject;
        [SerializeField] private GameObject _gameObjectTrue;
        [SerializeField] private GameObject _gameObjectFalse;

        [SerializeField] private int _stat;
        
        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < 5; i++)
            {
                Instantiate(i < _stat ? _gameObjectTrue : _gameObjectFalse, _mainStatObject.transform);
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
