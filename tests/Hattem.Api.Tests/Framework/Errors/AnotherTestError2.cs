namespace Hattem.Api.Tests.Framework.Errors
{
    public sealed class AnotherTestError2 : Error
    {
        public static readonly AnotherTestError2 Default = new AnotherTestError2();

        public AnotherTestError2()
            : base("another.test.2", "another.test")
        {
        }
    }
}
