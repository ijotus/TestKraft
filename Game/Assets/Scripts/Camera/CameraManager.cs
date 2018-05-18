using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace game
{
    public class CameraManager : ICameraManager
    {
        public float CoefficientScale { get { return _cofficientScale; } }

        public CameraManager(IResourcesManager resourcesManager,ITickable tickable )
        {
            _resourcesManager = resourcesManager;
#if UNITY_EDITOR
            tickable.SecondTick += OnTick;
#endif
        }
        // Use this for initialization
        public void Initialize()
        {
           var go   = _resourcesManager.Load<GameObject>("prefabs/Camera");
           var inst =  GameObject.Instantiate(go);
            _camera = inst.GetComponent<ICamera>();
            UpdateCameraSize();
        }
        public Vector3 WorldToScreenPoint(Vector3 pos)
        {
            return _camera.WorldToScreenPoint(pos);
        }


        public void UpdateCameraSize(float pixelsPerUnit = 100)
        {
            var aspect = (float)Screen.width / (float)Screen.height;
            var baseSize = (float)Screen.width / ((aspect * 2.0f) * pixelsPerUnit);
            float width = 2208;//TODO GET FROM UIROOT

            var orthographicSize = width / ((aspect * 2.0f) * pixelsPerUnit);
            _camera.AdjustSize(orthographicSize);
            _cofficientScale = orthographicSize / baseSize;
        }


        void OnTick(object sender, EventArgs e)
        {
            UpdateCameraSize();
        }

        private float _cofficientScale = 0;
        private ICamera _camera;
        private readonly IResourcesManager _resourcesManager;
    }
}
