using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace game
{
    public class UIScore : MonoBehaviour, IUIScore
    {
        [SerializeField] Text      _scores;
        [SerializeField] Text  _bestScores;
        public void SetScores(string scores)
        {
            _scores.text = scores;
        }

        public void SetBestScores(string best)
        {
            _bestScores.text = best;
        }
    }
}