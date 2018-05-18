using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace game
{
    public class ProgressHandler : IProgressHandler
    {
        const float MAX_TIME = 5;
        public event EventHandler<EventArgsGeneric<float>> ProgressChanged;
        public event EventHandler ProgressDone;
        public ProgressHandler(ICoroutine coroutine)
        {
            _coroutine = coroutine;
        }

        public void Start()
        {
            _data.Initialize(1);
            ProgressChanged.SafeRaise(this,_data);
            _isRun = true;
            _currCoroutine =  _coroutine.StartCoroutine(Progress());
        }
        public void Stop()
        {
            _isRun = false;
            _coroutine.StopCoroutine(_currCoroutine);
        }

        IEnumerator Progress()
        {
            while(_isRun)
            {
                var max = Mathf.Max(2,UnityEngine.Random.value * MAX_TIME);
                var current = max;
                while(current >= 0)
                {
                    _data.Initialize(current/max);
                    ProgressChanged.SafeRaise(this, _data);
                    current -= Time.deltaTime;
                    yield return null;
                }
                ProgressDone.SafeRaise(this, EventArgs.Empty);
                yield return null;
            }
        }

        Coroutine _currCoroutine;
        private bool _isRun;
        EventArgsGeneric<float> _data = new EventArgsGeneric<float>(0);
        private readonly ICoroutine _coroutine;
    }
}