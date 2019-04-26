using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Networking;

public class DatasetUtillity
{
	private const float TOAST_TTL = 3f;
	private const string UNABLE_TO_REACH_SERVER = "UNABLE TO REACH SERVER";

	private class MonoInstance : MonoBehaviour { }
	public static void FetchRemoteDataSet<T>(string dataSetUrl, Action<T> callback) where T : class
	{
		MonoInstance monoInstance = new GameObject("UnityWebRequest", typeof(MonoInstance)).GetComponent<MonoInstance>();
		monoInstance.StartCoroutine(DownloadRoutine(dataSetUrl, callback));
	}

	static IEnumerator DownloadRoutine<T>(string dataSetUrl, Action<T> callback) where T : class
	{
		if (!string.IsNullOrEmpty(dataSetUrl))
		{
			UnityWebRequest dataSetRequest = new UnityWebRequest(dataSetUrl);
			dataSetRequest.downloadHandler = new DownloadHandlerBuffer();
			yield return dataSetRequest.SendWebRequest();
			if (!string.IsNullOrEmpty(dataSetRequest.error))
			{
				Debug.LogError(dataSetRequest.error);
				ToastBehaviour toast = GameObject.FindObjectOfType<ToastBehaviour>();
				if (toast)
				{
					toast.ShowToast(UNABLE_TO_REACH_SERVER, TOAST_TTL);
				}
				callback.Invoke(null as T);
				yield break;
			}
			string jsonData = dataSetRequest.downloadHandler.text;
			T data = JsonUtility.FromJson<T>(jsonData);
			if (data != null)
			{
				callback.Invoke(data as T);
			}
			else
			{
				Debug.LogError("Failed to parse remote dataset into " + typeof(T).Name + " " + jsonData);
			}
		}

	}
}
