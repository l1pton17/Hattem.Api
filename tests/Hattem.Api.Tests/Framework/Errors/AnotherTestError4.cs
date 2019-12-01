namespace Hattem.Api.Tests.Framework.Errors
{
    public sealed class AnotherTestError4 : Error
    {
        public static readonly AnotherTestError4 Default = new AnotherTestError4();

        public AnotherTestError4()
            : base("another.test.4", "another.test")
        {
        }
    }
}
