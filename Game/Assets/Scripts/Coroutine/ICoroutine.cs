using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public interface ICoroutine
    {
        Coroutine StartCoroutine(IEnumerator routine);
        Coroutine StartCoroutine(string name);
        void StopCoroutine(string name);
        void StopCoroutine(Coroutine coroutine);
    }
}
