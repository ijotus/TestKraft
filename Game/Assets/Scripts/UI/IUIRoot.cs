using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace game
{
    public interface IUIRoot
    {
        void AddChild(Transform transform);
    }
}