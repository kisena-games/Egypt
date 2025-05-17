using System;
using UnityEngine;

namespace Game.Scripts.Infrastructure.Data
{
    [Serializable]
    public class InitialGameplayData
    {
        public Transform GameplayParent;
        public GameObject Player;
        public GameObject HealthBarUI;
        public GameObject UnventoryUI;
        public GameObject Что_то_Ешчё;
    }
}