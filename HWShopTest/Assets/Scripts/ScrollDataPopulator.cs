using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(InfiniteScrollRect))]
public class ScrollDataPopulator : MonoBehaviour
{

	[SerializeField]
	private string dataSetUrl;
	[SerializeField]
	DatasetType datasetType;

	ScrollCell[] scrollCells;
	private InfiniteScrollRect infScrollRect;


	private MonthlyPriceDataSet monthsPriceDataset;
	private CoinPriceDataset coinsPriceDataset;


	void Start()
	{
		infScrollRect = GetComponent<InfiniteScrollRect>();
		switch (datasetType)
		{
			case DatasetType.MonthsPriceDataSet:
				DatasetUtillity.FetchRemoteDataSet<MonthlyPriceDataSet>(dataSetUrl, onDataSetAquired);
				break;
			case DatasetType.CoinPriceDataSet:
				DatasetUtillity.FetchRemoteDataSet<CoinPriceDataset>(dataSetUrl, onDataSetAquired);
				break;
		}
		scrollCells = GetComponentsInChildren<ScrollCell>();
	}

	private void onDataSetAquired(MonthlyPriceDataSet dataSet)
	{
		if (dataSet == null)
		{
			return;
		}
		monthsPriceDataset = dataSet;
		for (int i = 0; i < scrollCells.Length; i++)
		{
			scrollCells[i].SetScrolldata(ref monthsPriceDataset);
		}

		infScrollRect.virtualElements = monthsPriceDataset.musky_and_price.Length;
		infScrollRect.Init();
	}
	private void onDataSetAquired(CoinPriceDataset dataSet)
	{
		if (dataSet == null)
		{
			return;
		}
		coinsPriceDataset = dataSet;
		for (int i = 0; i < scrollCells.Length; i++)
		{
			scrollCells[i].SetScrolldata(ref coinsPriceDataset);
		}

		infScrollRect.virtualElements = coinsPriceDataset.coin_and_price.Length;
		infScrollRect.Init();
	}

}