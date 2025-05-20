using System;
using System.Collections;
using App.Scripts.Services;
using UnityEngine;

namespace App.Scripts.Bootstrap
{
    public interface IUpdatableCoroutineRunner : IService
    {
        public event Action OnUpdate;
        public event Action<bool> OnAppPause; 
        public Coroutine StartCoroutine(IEnumerator enumerator);
        public void StopCoroutine(Coroutine coroutine);
    }
}