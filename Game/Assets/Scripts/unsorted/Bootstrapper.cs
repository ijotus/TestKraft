using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game
{
    public class Bootstrapper : MonoBehaviour
    {
        void Start()
        {
            _application.Initialized += OnInitialized;
            _application.Initialize();
        }

        void OnInitialized(object sender,EventArgs e)
        {
            _application.Run();
        }

        private IApplication _application = new Application();
    }
}