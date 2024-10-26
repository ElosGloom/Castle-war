using System.Threading.Tasks;
using Common;
using Cysharp.Threading.Tasks;
using ECS.FSM;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Network;
using UI;

namespace ECS.Systems.UI
{
    public class LoginUISystem : BaseUIWindowSystem<UILoginWindow>
    {  private EcsFilter _filter;
        private EcsWorldInject _world;
        private EcsCustomInject<User> _user;
        private EcsCustomInject<ApiService> _apiService;
        private EcsCustomInject<RuntimeData> _runtimeData;
        
        
        protected override void OnShow(UILoginWindow window, int entity)
        {
            window.ButtonsProvider.Subscribe("Close", () =>
            {
                AppStateMachine.SetState<MainMenuState>();
            });
            
            window.ButtonsProvider.Subscribe("Login", () =>
            {
                TryLogin(window.loginInput.text, window.passwordInput.text);
            });
            
            window.ButtonsProvider.Subscribe("SignUp", () =>
            {
                TrySignUp(window.loginInput.text, window.passwordInput.text);
            });
            
        }

        private async void TryLogin(string userName, string password)
        {
            var result = await _apiService.Value.LoginAsync(userName, password);
            if (result.IsSuccess)
            {
               AppStateMachine.SetState<MainMenuState>();
            }

        }

        private async void TrySignUp(string userName, string password)
        {
           await _apiService.Value.RegisterAsync(userName, password);
             
        }
    }
}