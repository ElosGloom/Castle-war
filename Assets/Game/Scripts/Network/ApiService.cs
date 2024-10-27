using System;
using System.Globalization;
using Common;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Network
{
	public class ApiService : ApiBase
	{
		private bool HasSavedLoginData => PlayerPrefs.HasKey(Constants.Username);

		public async UniTask<bool> TryReLogin()
		{
			if (!HasSavedLoginData)
				return false;

			var result = await Login(PlayerPrefs.GetString(Constants.Username),
				PlayerPrefs.GetString(Constants.Password));

			return result.IsSuccess;
		}

		public async UniTask<RequestResult> Login(string username, string password)
		{
			var form = new WWWForm();
			form.AddField(nameof(username), username);
			form.AddField(nameof(password), password);
			var result = await Post("auth/login", form);

			if (result.IsSuccess)
			{
				Bearer = JsonConvert.DeserializeObject<JObject>(result.Response)["token"]!.ToString();
				SaveLoginData(username, password);
			}

			return result;
		}

		public async UniTask<RequestResult> Register(string username, string password)
		{
			var form = new WWWForm();
			form.AddField(nameof(username), username);
			form.AddField(nameof(password), password);

			var result = await Post("auth/register", form);
			if (result.IsSuccess)
			{
				Bearer = JsonConvert.DeserializeObject<JObject>(result.Response)["token"]!.ToString();
				SaveLoginData(username, password);
			}

			return result;
		}

		public async UniTask<RequestResult> SyncUserData(User user)
		{
			var encodedData = user.Serialize();
			PlayerPrefs.SetString(Constants.UserPrefsKey, encodedData);
			PlayerPrefs.Save();

			if (!IsAuthenticated)
			{
				return new RequestResult
				{
					IsSuccess = false,
					Response = string.Empty
				};
			}
			var form = new WWWForm();
			form.AddField("playtime", user.Playtime.ToString(CultureInfo.InvariantCulture));
			form.AddField("data", encodedData);
			var result = await Post("data/user", form);
			if (result.IsSuccess)
			{
				encodedData = result.Response;
				user.Deserialize(encodedData);
			}

			return result;
		}

		private void SaveLoginData(string username, string password)
		{
			PlayerPrefs.SetString(Constants.Username, username);
			PlayerPrefs.SetString(Constants.Password, password);
			PlayerPrefs.Save();
		}
	}
}