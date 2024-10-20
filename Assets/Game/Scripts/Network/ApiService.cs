using Common;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Network
{
	public class ApiService : ApiBase
	{
		public async UniTask<RequestResult> LoginAsync(string username, string password)
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

		public async UniTask<RequestResult> RegisterAsync(string username, string password)
		{
			var form = new WWWForm();
			form.AddField(nameof(username), username);
			form.AddField(nameof(password), password);

			var result = await Post("auth/register", form);
			if (result.IsSuccess)
				Bearer = JsonConvert.DeserializeObject<JObject>(result.Response)["token"]!.ToString();

			return result;
		}

		public async UniTask<RequestResult> SetSavedDataAsync(string encodedData)
		{
			var form = new WWWForm();
			form.AddField("data", encodedData);
			return await Post("data/user", form);
		}

		public async UniTask<RequestResult> UpdateUserAsync(User user)
		{
			var result = await Get("data/user");
			if (string.IsNullOrEmpty(result.Response))
				return result;

			user.Deserialize(result.Response);
			return result;
		}
	}
}