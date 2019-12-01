namespace Hattem.Api.Tests.Framework.Errors
{
    public sealed class AnotherTestError5 : Error
    {
        public static readonly AnotherTestError5 Default = new AnotherTestError5();

        public AnotherTestError5()
            : base("another.test.5", "another.test")
        {
        }
    }
}
