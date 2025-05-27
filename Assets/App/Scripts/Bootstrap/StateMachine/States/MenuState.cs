using App.Scripts.Data;
using App.Scripts.Services;
using App.Scripts.Services.View;
using App.Scripts.UI;
using UnityEditor;

namespace App.Scripts.Bootstrap.StateMachine.States
{
    public class MenuState : IState
    {
        private readonly StateMachine _stateMachine;
        public bool IsActive { get; private set; }
        
        private MenuView View => _view ??= ServiceContainer.Container.Get<IViewContainer>().GetView<MenuView>();
        private MenuView _view;
        
        private SettingsView SettingsView => _settingsView ??= ServiceContainer.Container.Get<IViewContainer>().GetView<SettingsView>();
        private SettingsView _settingsView;
        
        private AvtorsView AvtorsView => _avtorsView ??= ServiceContainer.Container.Get<IViewContainer>().GetView<AvtorsView>();
        private AvtorsView _avtorsView;
        
        public MenuState(
            StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            
        }

        public void Enter()
        {
            AddListener();
            View.Show();
            IsActive = true;
        }

        public void Exit()
        {
            RemoveListener();
            IsActive = false;
            View.Hide();
        }

        private void PlayClick()
        {
            _stateMachine.Enter<GameLoadState, GameLoadPayload>(new GameLoadPayload(SceneNameConstants.Training,
                () => _stateMachine.Enter<GameplayState>()));
        }
        
        private void ShowSettings()
        {
            View.Hide();
            SettingsView.Show();
        }
        
        private void ShowAvtors()
        {
            View.Hide();
            AvtorsView.Show();
        }
        
        private void HideSettings()
        {
            View.Show();
            SettingsView.Hide();
        }
        
        private void HideAvtors()
        {
            View.Show();
            AvtorsView.Hide();
        }
        
        private void AddListener()
        {
            AvtorsView.AddListener(HideAvtors);
            SettingsView.AddListener(HideSettings);
            
            View.AddListener(onStart: PlayClick,
                onSettings: ShowSettings,
                onAvtors: ShowAvtors,
                onExit: ExitGame
            );
        }
        
        private void RemoveListener()
        {
            AvtorsView.RemoveListener();
            SettingsView.RemoveListener();
            View.RemoveListener();
        }
        
        private void ExitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else

            Application.Quit();
#endif
        }
    }
}