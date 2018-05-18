using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IApplication
{
    event EventHandler Initialized;
    // Use this for initialization
    void Initialize();
    void Run();
}
