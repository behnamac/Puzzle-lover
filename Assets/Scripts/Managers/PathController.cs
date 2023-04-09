using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Puzzle;

namespace Manager
{
    public class PathController : MonoBehaviour
    {
        public static PathController instance;

        [SerializeField] private Path[] _paths;

        private void Awake()
        {
            instance = this;

            GameManager.onLoadLevel += OnLoadLevel;
        }
        // Start is called before the first frame update
        private void Start()
        {

        }

        private void OnDestroy()
        {
            GameManager.onLoadLevel -= OnLoadLevel;
        }

        public void CheckPath() 
        {
            for (int i = 0; i < _paths.Length; i++)
            {
                if (!_paths[i].Active) return;
            }

            GameManager.Instance.LevelCompelet();
        }

        private void OnLoadLevel() 
        {
            _paths = FindObjectsOfType<Path>();
        }
    }
}
