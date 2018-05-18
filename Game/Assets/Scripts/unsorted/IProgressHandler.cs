using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace game
{
    public interface  IProgressHandler
    {
        event EventHandler<EventArgsGeneric<float>> ProgressChanged;
        event EventHandler ProgressDone;

        void Stop();
        void Start();
    }
}