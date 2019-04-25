using System;

public class MonthlyPriceDataSet : IDataset<MonthPriceDatum>
{
	public MonthPriceDatum[] musky_and_price;

	public MonthPriceDatum GetDataAtIndex(int i)
	{
		if ((0 <= i) && (i < musky_and_price.Length))
		{
			return musky_and_price[i];
		}
		else
		{
			throw new IndexOutOfRangeException();
		}
	}
}
