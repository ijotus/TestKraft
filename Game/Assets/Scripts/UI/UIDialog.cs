using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace game
{
    public class UIDialog : MonoBehaviour, IUIDialog
    {
        [SerializeField] Button _btnOk;
        [SerializeField] Button _btnCancel;
        [SerializeField] Text   _result;

        public event EventHandler ClickBtnOk;
        public event EventHandler ClickBtnCancel;

        public void ShowDialog()
        {
            gameObject.SetActive(true);
        }

        public void HideDialog()
        {
            gameObject.SetActive(false);
        }

        public void SetResult(string result)
        {
            _result.text = result;
        }
        void Awake()
        {
            _btnOk.onClick.AddListener(() => { ClickBtnOk.SafeRaise(this, EventArgs.Empty); });
            _btnCancel.onClick.AddListener(() => { ClickBtnCancel.SafeRaise(this, EventArgs.Empty); });
            HideDialog();
        }


    }
}