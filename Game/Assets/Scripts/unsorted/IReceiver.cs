using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game
{
    public interface IReceiver
    {
        Vector3 Position { get; set; }

        event EventHandler<EventArgsGeneric<ISource>> CollisionEnter;
        event EventHandler<EventArgsGeneric<ISource>> CollisionExit;
        Color Color { get; set; }
    }
}