using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace game
{
    public class UIProgress : MonoBehaviour, IUIProgress
    {
        [SerializeField] Image      _progress;
        public void SetProgress(float progress)
        {
            _progress.fillAmount = progress;
        }
    }
}