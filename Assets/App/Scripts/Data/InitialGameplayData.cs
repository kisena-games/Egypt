using System;
using UnityEngine;

namespace App.Scripts.Data
{
    [Serializable]
    public class InitialGameplayData
    {
        public Transform GameplayParent;
        public PlayerListener Player;
    }
}