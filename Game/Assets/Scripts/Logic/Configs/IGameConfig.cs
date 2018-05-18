using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game
{
    public interface IGameConfig
    {
        float Timer { get; }
        Color[] Colors { get; }

        void Initialize();
    }
}