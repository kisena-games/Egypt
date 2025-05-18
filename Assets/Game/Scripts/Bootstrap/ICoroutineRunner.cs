using System;
using System.Collections;
using UnityEngine;

namespace Game.Scripts.Infrastructure.Core
{
    public interface ICoroutineRunner
    {
        public event Action<bool> OnAppPause; 
        public Coroutine StartCoroutine(IEnumerator enumerator);
        public void StopCoroutine(Coroutine coroutine);
    }
}