using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class ResourcesManagerInternal : IResourcesManager
    {
        public event EventHandler Initialized;
        public void Initialize()
        {
            Initialized.SafeRaise(this, EventArgs.Empty);
        }
        public T Load<T>(string path) where T : UnityEngine.Object
        {
            return Resources.Load<T>(path);
        }
        public object Load(string path, Type systemTypeInstance)
        {
            return Resources.Load(path,systemTypeInstance);
        }
    }
}