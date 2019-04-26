using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
[RequireComponent(typeof(Image))]
public class CustomToggleBehaviour : MonoBehaviour
{
	[SerializeField]
	Color offColor, onColor;
	private Toggle toggle;
	private void Awake()
	{
		toggle = GetComponent<Toggle>();
		toggle.onValueChanged.AddListener(ColorChanger);
	}

	private void ColorChanger(bool isSelected)
	{
		if (isSelected)
		{
			toggle.GetComponent<Image>().color = onColor;
		}
		else
		{
			toggle.GetComponent<Image>().color = offColor;
		}
	}
}
