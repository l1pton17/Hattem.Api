namespace Hattem.Api.Tests.Framework.Errors
{
    public sealed class TestError : Error
    {
        public static readonly TestError Default = new TestError();

        public TestError()
            : base("test", "test")
        {
        }
    }
}