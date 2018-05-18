using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace game
{
    public interface  ITickable 
    {
        event EventHandler SecondTick;
        void Initialize();
    }
}
