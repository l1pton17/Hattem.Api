using System;
using System.Diagnostics;

namespace Hattem.Api
{
    /// <summary>
    /// Error
    /// </summary>
    [DebuggerDisplay("{Code}: {Description}")]
    public class Error
    {
        /// <summary>
        /// Data
        /// </summary>
        public object Data { get; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; }

        protected Error()
        {
        }

        public Error(string code, string description)
        {
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Code = code ?? throw new ArgumentNullException(nameof(code));
        }

        public Error(string code, string description, object data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Code = code ?? throw new ArgumentNullException(nameof(code));
        }

        public override string ToString() => $"{Code}: {Description}. Data: {Data}";
    }
}