using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game
{
    public interface ISource
    {
        event EventHandler Capture;
        event EventHandler Dragg;
        event EventHandler Drop;
        Vector3 Position { get; set; }
        Color Color { get; set; }
    }
}