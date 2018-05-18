using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace game
{
    public class UITime : MonoBehaviour, IUITime
    {
        void Awake()
        {
            _time = GetComponentInChildren<Text>();
        }
        public void SetTime(string time)
        {
            _time.text = time;
        }

        Text _time;
    }
}