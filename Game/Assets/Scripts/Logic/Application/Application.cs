using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace game
{
    public class Application : IApplication
    {
        public event EventHandler Initialized;
        // Use this for initialization
        public void Initialize()
        {
            _coroutine          = new GameObject("Coroutines").AddComponent<MonoCoroutine>();
            var tickable        = new Tickable(_coroutine);
            _resourcesManager   = new ResourcesManagerInternal();
            _cameraManager      = new CameraManager(_resourcesManager, tickable);
            var config          = new GameConfig(); 
            _gameRoot           = new GameRoot(_resourcesManager,tickable, config, _coroutine, _cameraManager);

            _resourcesManager.Initialized += (a, b) => 
            {
                _cameraManager.Initialize();
                tickable.Initialize();
                Initialized.SafeRaise(this, EventArgs.Empty);
            };
            _resourcesManager.Initialize();
           
        }
        public void Run()
        {
            _gameRoot.Start();
        }

        private ICoroutine        _coroutine;
        private IResourcesManager _resourcesManager;
        private IGameRoot         _gameRoot;
        private ICameraManager    _cameraManager;
    }
}