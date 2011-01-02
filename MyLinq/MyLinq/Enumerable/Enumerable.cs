namespace MyLinq
{
    public static partial class Enumerable
    {
        private static T Identity<T>(T value)
        {
            return value;
        }
    }
}
