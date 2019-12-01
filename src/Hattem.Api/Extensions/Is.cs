namespace Hattem.Api.Extensions
{
    public sealed class Is<T>
    {
        public static readonly Is<T> Type = new Is<T>();

        private Is()
        {
        }
    }
}
