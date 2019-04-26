using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToastBehaviour : MonoBehaviour
{
	[SerializeField]
	Animator toastAnimator;
	[SerializeField]
	Text toastText;
	Coroutine currentHideRoutine;

	public void ShowToast(string message, float ttl)
	{
		toastText.text = message;
		toastAnimator.SetTrigger("Show");
		if (currentHideRoutine != null)
		{
			StopCoroutine(currentHideRoutine);
		}
		currentHideRoutine = StartCoroutine(HideToastAfterDelay(ttl));
	}

	private IEnumerator HideToastAfterDelay(float ttl)
	{
		yield return new WaitForSeconds(ttl);
		toastAnimator.SetTrigger("Hide");
	}
}
