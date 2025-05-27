using System;
using UnityEngine;

namespace App.Scripts.Core
{
    public class PlayerLoadScene : MonoBehaviour
    {
        public event Action<string> OnLoadScene;

        public void LoadScene(string sceneName)
        {
            OnLoadScene?.Invoke(sceneName);
        }
    }
}