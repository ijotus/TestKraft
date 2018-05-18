using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game
{
    public class UIRoot : MonoBehaviour, IUIRoot
    {
        public void AddChild(Transform child)
        {
            child.SetParent(transform, false);
        }
    }
}