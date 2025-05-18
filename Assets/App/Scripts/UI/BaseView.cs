using System;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.UI
{
    public class BaseView : MonoBehaviour
    {
        public event Action<BaseView> OnOpen;
        public event Action<BaseView> OnClose;
        public bool IsOpen => gameObject.activeSelf;
        
        [SerializeField] private CanvasScaler _canvasScaler;

        protected void OnValidate()
        {
            _canvasScaler ??= GetComponentInChildren<CanvasScaler>();
        }
        
        public void CorrectCanvasScaler(float match)
        {
            if (_canvasScaler == null)
                return;
            _canvasScaler.matchWidthOrHeight = match;
        }

        public virtual void Initialize()
        {
        }
        
        public virtual void Show()
        {
            transform.SetAsLastSibling();
            ViewOpen();
        }

        public virtual void Hide()
        {
            ViewClose();
        }

        protected void ViewOpen()
        {
            if (IsOpen)
                return;
            gameObject.SetActive(true);
            OnOpen?.Invoke(this);
        }
        
        private void ViewClose()
        {
            OnClose?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}