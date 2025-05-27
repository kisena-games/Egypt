using System;
using System.Collections;
using App.Scripts.Bootstrap.StateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace App.Scripts.UI
{
    public class LoadView : BaseView
    {
        [SerializeField] private GameObject _loadingScreen;
        [SerializeField] Slider _loadingBar;

        public void UpdateProgressBar(float value)
        {
            _loadingBar.value = value;
        }

        public void ShowLoadingScreen()
        {
            _loadingScreen.SetActive(true);
        }
    }
}
