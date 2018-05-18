using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game
{
    public interface ICameraManager
    {
        // Use this for initialization
        void Initialize();
        Vector3 WorldToScreenPoint(Vector3 pos);
    }
}
