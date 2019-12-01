namespace Hattem.Api.Tests.Framework.Errors
{
    public sealed class AnotherTestError : Error
    {
        public static readonly AnotherTestError Default = new AnotherTestError();

        public AnotherTestError()
            : base("another.test", "another.test")
        {
        }
    }
}
