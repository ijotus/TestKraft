using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game
{
    public interface IUIScore 
    {
        void SetScores(string scores);
        void SetBestScores(string best);
    }
}