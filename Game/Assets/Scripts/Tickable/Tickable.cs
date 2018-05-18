using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace game
{
    public class Tickable : ITickable
    {
        public event EventHandler SecondTick;

        public Tickable(ICoroutine coroutine)
        {
            _coroutine = coroutine;
        }

        public void Initialize()
        {
            _coroutine.StartCoroutine(Tick());
        }
        // Update is called once per frame

        private IEnumerator Tick()
        {
            var tmp = new WaitForSeconds(1);
            while (true)
            {
                yield return tmp;
                SecondTick.SafeRaise(this, EventArgs.Empty);
            }
        }



        private readonly ICoroutine _coroutine;
    }
}