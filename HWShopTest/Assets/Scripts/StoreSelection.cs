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
	Text title;
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
				kitPanel.SetBool("Shown", value);
				title.text = string.Empty;
				break;
			case "Hitcoins":
				hwCoinPanel.SetBool("Shown", value);
				title.text = panelButton.gameObject.name.ToUpper();
				break;
			case "Musketeer":
				monthPricePanel.SetBool("Shown", value);
				title.text = panelButton.gameObject.name.ToUpper();
				break;
		}
	}
}
