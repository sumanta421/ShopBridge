using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace ShopBridge.Utils
{
    [ExcludeFromCodeCoverage]
    public class ShopBridgeException : Exception
    {
        private ShopBridgeErrorCode? errorCode;
        public int? StatusCode { get; set; }
        private string message;

        public ShopBridgeException(ShopBridgeErrorCode errorCode,int statusCode)
        {
            StatusCode = statusCode;
            this.errorCode = errorCode;
        }

        public ShopBridgeException(string message,int statusCode) : base(message)
        {
            this.message = message;
        }

        public ShopBridgeException(string message, Exception innerException) : base(message, innerException)
        {
            this.message = message;
        }

        public ErrorFormat GenerateErrorPayload()
        {
            return new ErrorFormat
            {
                ErrorCode = errorCode != null ? (int)errorCode:-1,
                Message = errorCode!=null ? errorCode.GetEnumDescription():message
            };
        }
    }
}