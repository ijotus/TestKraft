using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public interface IResourcesManager
    {
        event EventHandler Initialized;
        T Load<T>(string path) where T : UnityEngine.Object;
        object Load(string path, Type systemTypeInstance);
        void Initialize();
    }
}