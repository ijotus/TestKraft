using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;


namespace game
{
    public interface IGameRoot 
    {
        event EventHandler<EventArgsGeneric<int>> ScoreChanged;
        void Start();
    }
}