using System;
using UnityEngine.UI;

namespace App.Scripts.UI
{
    public class SavedDataView : BaseView
    {
        public Button BackButton;

        public void AddListener(Action onBack)
        {
            BackButton.onClick.AddListener(() => onBack?.Invoke());
        }

        public void RemoveListener()
        {
            BackButton.onClick.RemoveAllListeners();
        }

    }
}