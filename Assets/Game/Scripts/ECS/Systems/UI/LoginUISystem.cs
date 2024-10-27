using System.Threading.Tasks;
using Common;
using Cysharp.Threading.Tasks;
using ECS.FSM;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Network;
using UI;
using UnityEngine;

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
            if (PlayerPrefs.HasKey("password"))
            {
                TryLogin(PlayerPrefs.GetString(Constants.UserName), PlayerPrefs.GetString(Constants.Password));
                _apiService.Value.UpdateUserAsync(_user.Value).Forget();
                AppStateMachine.SetState<MainMenuState>();
            }
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
               PlayerPrefs.SetString(Constants.UserName,userName);
               PlayerPrefs.SetString(Constants.Password,password);
            }

        }

        private async void TrySignUp(string userName, string password)
        {
          var result= await _apiService.Value.RegisterAsync(userName, password);
          if (result.IsSuccess)
          {
           PlayerPrefs.SetString("userName",userName);
           PlayerPrefs.SetString("password",password);
          }
             
        }
    }
}