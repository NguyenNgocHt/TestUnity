namespace Framework
{
    public static class PResourceExtensions
    {
        public static PDataUnit<int> GetData(this PResourceType type)
        {
            return PDataResource.GetResourceData(type);
        }

        public static int GetValue(this PResourceType type)
        {
            PDataUnit<int> data = type.GetData();

            return data.Data;
        }

        public static void AddValue(this PResourceType type, int value)
        {
            type.GetData().Data += value;
        }

        public static void SetValue(this PResourceType type, int value)
        {
            type.GetData().Data = value;
        }
    }
}