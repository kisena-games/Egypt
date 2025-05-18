using App.Scripts.Bootstrap.StateMachine.States;
using UnityEngine;
using UnityEngine.UI;
using App.Scripts.Bootstrap.StateMachine;

namespace App.Scripts.UI
{
    public class DebugView : BaseView
    {
        [SerializeField] private Button _getCubebutton;
        [SerializeField] private Button _getAllElementsButton;

        public void Initialize(App.Scripts.Bootstrap.StateMachine.StateMachine stateMachine, GameplayState gameplayState)
        {
            
        }

        public virtual void Hide()
        {
            base.Hide();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public override void Show()
        {
            base.Show();
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        
        private void OnEnable()
        {
            Show();
        }
    }
}
