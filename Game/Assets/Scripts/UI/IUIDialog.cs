using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game
{
    public interface IUIDialog 
    {
        event EventHandler ClickBtnOk;
        event EventHandler ClickBtnCancel;
        void ShowDialog();
        void HideDialog();
        void SetResult(string result);
    }
}