using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreSelection : MonoBehaviour
{

	[SerializeField]
	Toggle defaultState;

	[SerializeField]
	Toggle kitPanelButton, hwCoinPanelButton, monthsPricePanel;

	[SerializeField]
	Animator kitPanel, hwCoinPanel, monthPricePanel;
	private void Awake()
	{
		kitPanelButton.onValueChanged.AddListener((bool value) => HandleToggleChange(kitPanelButton, value));
		hwCoinPanelButton.onValueChanged.AddListener((bool value) => HandleToggleChange(hwCoinPanelButton, value));
		monthsPricePanel.onValueChanged.AddListener((bool value) => HandleToggleChange(monthsPricePanel, value));
		if (defaultState)
		{
			defaultState.isOn = true;
			HandleToggleChange(defaultState, true);
		}
	}

	private void HandleToggleChange(Toggle panelButton, bool value)
	{
		switch (panelButton.gameObject.name)
		{
			case "Special":
				if (value)
				{
					kitPanel.SetTrigger("Enter");
				}
				else
				{
					kitPanel.SetTrigger("Exit");
				}
				break;
			case "Hitcoins":
				if (value)
				{
					hwCoinPanel.SetTrigger("Enter");
				}
				else
				{
					hwCoinPanel.SetTrigger("Exit");
				}
				break;
			case "Musketeer":
				if (value)
				{
					monthPricePanel.SetTrigger("Enter");
				}
				else
				{
					monthPricePanel.SetTrigger("Exit");
				}
				break;
		}
	}
}
