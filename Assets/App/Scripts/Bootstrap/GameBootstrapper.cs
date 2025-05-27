using System;
using App.Scripts.Data;
using UnityEngine;

namespace App.Scripts.Bootstrap
{
    public class GameBootstrapper : MonoBehaviour, IUpdatableCoroutineRunner
    {
        public static bool DeveloperMode = true;
        public event Action OnUpdate;
        public event Action<bool> OnAppPause;

        public void Update()
        {
            OnUpdate?.Invoke();
        }

        [SerializeField] private InitialData _initialData;

        private Game.Game _game;

        private void Awake()
        {
            _game = new Game.Game(this, _initialData);
            DontDestroyOnLoad(gameObject);
        }
    }
}