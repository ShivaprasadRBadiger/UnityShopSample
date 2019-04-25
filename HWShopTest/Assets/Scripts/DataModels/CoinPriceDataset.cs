using System;

[Serializable]
public class CoinPriceDataset : IDataset<CoinPriceDatum>
{
	public CoinPriceDatum[] coin_and_price;

	public CoinPriceDatum GetDataAtIndex(int i)
	{
		if ((0 <= i) && (i < coin_and_price.Length))
		{
			return coin_and_price[i];
		}
		else
		{
			throw new IndexOutOfRangeException();
		}
	}
}
