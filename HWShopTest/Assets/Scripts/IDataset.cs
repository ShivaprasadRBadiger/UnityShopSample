public interface IDataset<T> where T : class
{
	T GetDataAtIndex(int i);
}