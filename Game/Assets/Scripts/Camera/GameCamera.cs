using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game
{
    public class GameCamera : MonoBehaviour, ICamera
    {
        void Awake()
        {
            _camera = GetComponentInChildren<Camera>(true);
        }

        public void AdjustSize(float size)
        {
            _camera.orthographicSize = size;
        }

        public Vector3 WorldToScreenPoint(Vector3 pos)
        {
            return _camera.WorldToScreenPoint(pos);
        }

        private Camera _camera;
    }
}