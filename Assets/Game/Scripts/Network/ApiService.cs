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
		public async UniTask<RequestResult> Login(string username, string password)
		{
			var form = new WWWForm();
			form.AddField(nameof(username), username);
			form.AddField(nameof(password), password);
			var result = await Post("auth/login", form);

			if (result.IsSuccess)
				Bearer = JsonConvert.DeserializeObject<JObject>(result.Response)["token"]!.ToString();

			return result;
		}

//do re login with cached password hash. or update jwt token 

		public async UniTask<RequestResult> Register(string username, string password)
		{
			var form = new WWWForm();
			form.AddField(nameof(username), username);
			form.AddField(nameof(password), password);

			var result = await Post("auth/register", form);
			if (result.IsSuccess)
				Bearer = JsonConvert.DeserializeObject<JObject>(result.Response)["token"]!.ToString();

			return result;
		}

		public async UniTask<RequestResult> SyncUserData(User user)
		{
			var form = new WWWForm();
			form.AddField("playtime", user.Playtime.ToString(CultureInfo.InvariantCulture));
			var encodedData = user.Serialize();
			form.AddField("data", encodedData);
			var result = await Post("data/user", form);
			if (result.IsSuccess)
			{
				encodedData = result.Response;
				user.Deserialize(encodedData);
			}

			PlayerPrefs.SetString(Constants.UserPrefsKey, encodedData);
			PlayerPrefs.Save();
			return result;
		}
	}
}