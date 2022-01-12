using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Utils
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets Description Attributes from Enums
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string GetEnumDescription(this Enum val)
        {
            var propInfo = val.GetType().GetField(val.ToString());

            var descriptionAttributes = (DescriptionAttribute[])propInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : propInfo.ToString();
        }
    }
}
