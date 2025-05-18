using System;
using UnityEngine.Serialization;

namespace Game.Scripts.Data
{
    [Serializable]
    public class InitialData
    {
        public InitialGameplayData gameplayData;
        public UIInitialData UIInitialData;
    }
}