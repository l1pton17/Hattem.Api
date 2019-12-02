namespace Hattem.Api.Fluent
{
    public sealed class To<T>
    {
        public static readonly To<T> Type = new To<T>();

        private To()
        {
        }
    }
}
