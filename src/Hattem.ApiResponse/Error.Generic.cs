namespace Hattem.ApiResponse
{
    public class Error<TData> : Error
        where TData : class
    {
        public Error(string code, string description, TData data)
            : base(code, description, data)
        {
        }

        protected Error()
        {
        }
    }
}
