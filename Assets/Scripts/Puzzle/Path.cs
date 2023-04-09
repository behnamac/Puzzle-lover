using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Manager;

namespace Puzzle
{
    public class Path : MonoBehaviour
    {
        [SerializeField] private float[] targets;
        [SerializeField] private Material materialBloom;
        [SerializeField] private Renderer[] meshs;

        private bool _canRotate;
        private float _targetRotate;

        public bool Active { get; private set; }

        private void Awake()
        {
            GameManager.onLevelStart += OnLevelStart;
            GameManager.onLevelCompelet += OnLevelCompelet;
        }
        private void Start()
        {
            _targetRotate = transform.localEulerAngles.z;

            CheckRotate();
        }
        private void OnDestroy()
        {
            GameManager.onLevelStart -= OnLevelStart;
            GameManager.onLevelCompelet -= OnLevelCompelet;
        }
        private void OnMouseDown()
        {
            if (!_canRotate) return;
            _targetRotate += 90;
            if (_targetRotate > 360)
                _targetRotate = 90;
            Vector3 targetRotate = new Vector3(transform.localEulerAngles.x,
                transform.localEulerAngles.y, _targetRotate);

            _canRotate = false;
            transform.DOLocalRotate(targetRotate, 0.3f).OnComplete(() => 
            {
                _canRotate = true;
                CheckRotate();
            });
        }

        private void CheckRotate() 
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if(_targetRotate == targets[i])
                {
                    Active = true;
                    PathController.instance.CheckPath();
                    return;
                }
            }

            Active = false;
        }

        private void OnLevelStart() 
        {
            _canRotate = true;
        }
        private void OnLevelCompelet()
        {
            _canRotate = false;

            for (int i = 0; i < meshs.Length; i++)
            {
                meshs[i].material = materialBloom;
            }
        }
    }
}
