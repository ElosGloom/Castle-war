using System.Collections.Generic;
using DTO;
using ECS.FSM;
using FPS.Sheets;
using FPS.UI;
using UI;
using UnityEngine;
using Utils;
using VContainer;

namespace ECS.Systems.UI
{
	public class HubUISystem : BaseUIWindowSystem<UIHubWindow>
	{
		private readonly DTOStorage _dtoStorage;
		private readonly IObjectResolver _resolver;
		private readonly IAppStateMachine _appStateMachine;

		[Inject]
		public HubUISystem(DTOStorage dtoStorage, IObjectResolver resolver, IUIService uiService, IAppStateMachine appStateMachine) : base(uiService)
		{
			_dtoStorage = dtoStorage;
			_resolver = resolver;
			_appStateMachine = appStateMachine;
		}

		protected override void OnShow(UIHubWindow window, int entity)
		{
			window.ButtonsProvider.Subscribe("Start", () => _appStateMachine.SetState(AppState.PreBattle));
			var categories = _dtoStorage.GetSingle<CategoryDTO>();
			CreateObserver(window.ArmyParent, categories.Army);
			CreateObserver(window.CurrencyParent, categories.Currency);
			return;

			void CreateObserver(Transform parent, HashSet<string> category)
			{
				var inventoryObserver = _resolver.Resolve<InventoryObserver>();
				inventoryObserver.Init(window.gameObject.GetLifetime(), parent, category);
			}
		}
	}
}