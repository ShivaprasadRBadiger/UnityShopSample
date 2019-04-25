using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Networking;

public class DatasetUtillity
{
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
				Debug.LogError("Failed to fetch remote dataset " + dataSetRequest.error);
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
