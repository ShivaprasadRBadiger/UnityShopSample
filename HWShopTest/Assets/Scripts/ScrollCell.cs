using UnityEngine;
using UnityEngine.UI;

internal class ScrollCell : MonoBehaviour
{
	private const string MONTHS_POSTFIX = " MONTHS";
	private const string LOCAL_PRICE_SYMBOL = "₹ ";

	[SerializeField]
	Sprite[] displayIcons;
	[SerializeField]
	Text price;
	[SerializeField]
	Text months;
	[SerializeField]
	Text amount;
	MonthlyPriceDataSet monthlyPriceDataSet;
	CoinPriceDataset coinPriceDataset;


	public void SetScrolldata(ref MonthlyPriceDataSet data)
	{
		this.monthlyPriceDataSet = data;
	}
	public void SetScrolldata(ref CoinPriceDataset data)
	{
		this.coinPriceDataset = data;
	}

	public void UpdateCell(int dataIndex)
	{
		if (monthlyPriceDataSet != null)
		{
			var cellData = monthlyPriceDataSet.GetDataAtIndex(dataIndex);
			price.text = LOCAL_PRICE_SYMBOL + cellData.price.ToString();
			months.text = cellData.months.ToString() + MONTHS_POSTFIX;
		}
		else if (coinPriceDataset != null)
		{
			var cellData = coinPriceDataset.GetDataAtIndex(dataIndex);
			price.text = LOCAL_PRICE_SYMBOL + cellData.product_price.ToString();
			amount.text = cellData.quantity.ToString() + MONTHS_POSTFIX;
		}

	}
}