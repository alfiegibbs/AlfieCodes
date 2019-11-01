namespace AlfieCodes.Models
{
    public class TagCloudEntry
    {
        public string Name { get; }
        public int Count { get; }

        public TagCloudEntry( string name, int count )
        {
            Name = name;
            Count = count;
        }
    }
}
