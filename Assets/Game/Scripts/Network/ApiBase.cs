using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Network
{
	public abstract class ApiBase
	{
		private const string EndpointBase = "https://fps-castlewar.serveo.net/api/";
		
		protected string Bearer { private get; set; }
		public bool IsAuthenticated => !string.IsNullOrEmpty(Bearer);

		
		protected async UniTask<RequestResult> Post(string api, string data)
		{
			return await MakeRequest(UnityWebRequest.Post(BuildEndpoint(api), data, "application/json"));
		}

		protected async UniTask<RequestResult> Post(string api, WWWForm data)
		{
			return await MakeRequest(UnityWebRequest.Post(BuildEndpoint(api), data));
		}

		protected async UniTask<RequestResult> Get(string api)
		{
			return await MakeRequest(UnityWebRequest.Get(BuildEndpoint(api)));
		}

		protected async UniTask<RequestResult> Put(string api, WWWForm form)
		{
			return await MakeRequest(UnityWebRequest.Put(BuildEndpoint(api), form.data));
		}

		private async UniTask<RequestResult> MakeRequest(UnityWebRequest request)
		{
#if UNITY_EDITOR
			Debug.Log($"API {request.method}: {request.url}");
			if (request.uploadHandler != null)
			{
				var data = Encoding.UTF8.GetString(request.uploadHandler.data);
				Debug.Log($"Data \n{data}");
			}
#endif
			TryApplyBearer(request);
			await request.SendWebRequest();

			RequestResult result = new()
			{
				IsSuccess = request.result == UnityWebRequest.Result.Success,
				Response = request.downloadHandler != null ? request.downloadHandler.text : string.Empty
			};
#if UNITY_EDITOR
			if (!string.IsNullOrEmpty(result.Response)) Debug.Log(result.Response);
#endif
			request.Dispose();
			return result;
		}

		private string BuildEndpoint(string api) => $"{EndpointBase}{api}";

		private void TryApplyBearer(UnityWebRequest request)
		{
			if (IsAuthenticated)
				request.SetRequestHeader("Authorization", $"Bearer {Bearer}");
		}
	}
}