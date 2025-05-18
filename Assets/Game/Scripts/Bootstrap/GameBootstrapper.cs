using Game.Scripts.Data;
using UnityEngine;

namespace Game.Scripts.Bootstrap
{
    
    public class GameBootstrapper : MonoBehaviour
    {
        public static bool DeveloperMode = true;
        
        [SerializeField] private InitialData _initialData;
    }
}