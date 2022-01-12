using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Utils
{
    /// <summary>
    /// Payload to display Exception Information with XcorrelationId for tracking
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ErrorFormat
    {
        public string XCorrelationId { get; set; }
        public string Message { get; set; }
        public int ErrorCode { get; set; }
    }
}
