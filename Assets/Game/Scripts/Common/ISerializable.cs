namespace Common
{
	public interface ISerializable
	{
		string Serialize();
		void Deserialize(string serializedData);
	}
}