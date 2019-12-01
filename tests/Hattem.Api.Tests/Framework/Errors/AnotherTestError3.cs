namespace Hattem.Api.Tests.Framework.Errors
{
    public sealed class AnotherTestError3 : Error
    {
        public static readonly AnotherTestError3 Default = new AnotherTestError3();

        public AnotherTestError3()
            : base("another.test.3", "another.test")
        {
        }
    }
}
