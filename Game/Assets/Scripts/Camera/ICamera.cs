using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game
{
    public interface ICamera
    {
        void AdjustSize(float size);
        Vector3 WorldToScreenPoint(Vector3 pos);
    }
}