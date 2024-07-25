#nullable disable

using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
//using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;

namespace Pk.Com.Jazz.ECP.Utilities
{
    public static class Common
    {
        public static string ToYesNo(this bool booleanValue)
        {
            return booleanValue ? "Yes" : "No";
        }

        public static string ToYesNo(this bool? booleanValue)
        {
            return booleanValue?? false ? "Yes" : "No";
        }

        public static bool ToBool(this string stringvalue)
        {
            return stringvalue == "Yes";
        }

        public static string IsNull(this string? aValue)
        {
            if (string.IsNullOrWhiteSpace(aValue) || aValue == "0")
            {
                return "-none-";
            }
            else
            {
                return aValue;
            }
        }

        private static IEnumerable<ModelError> ModelErrors(ModelStateDictionary modelState)
        {
            return modelState.Values.SelectMany(v => v.Errors);
        }


        public static string IsNull(this double? aValue)
        {
            if (!aValue.HasValue)
            {
                return "<span class=\"badge badge-light\">-none-</span>";
            }
            else
            {
                return aValue.Value.ToString();
            }
        }

        public static ITempDataDictionary PushMessage(ITempDataDictionary tempData, string pushType, string pushMessage)
        {

            tempData["IsPush"] = true;
            tempData["PushType"] = pushType;
            tempData["PushMessage"] = pushMessage;

            return tempData;
        }

        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        //TODO: make this class working
        //public static string Pluralize(this string value, int count)
        //{
        //    if (count == 1)
        //    {
        //        return value;
        //    }
        //    return PluralizationService
        //        .CreateService(new CultureInfo("en-US"))
        //        .Pluralize(value);
        //}

        public static void CopyPropertiesTo<T, TU>(this T source, TU dest, string exclude)
        {
            var exc = exclude.Split(',');
            var sourceProps = typeof(T).GetProperties().Where(x => x.CanRead).ToList();
            var destProps = typeof(TU).GetProperties()
                    .Where(x => x.CanWrite)
                    .ToList();

            foreach (var sourceProp in sourceProps)
            {
                if (!exc.Where(x => x.Equals(sourceProp.Name)).Any())
                {
                    if (destProps.Any(x => x.Name == sourceProp.Name))
                    {
                        var p = destProps.First(x => x.Name == sourceProp.Name);
                        if (p.CanWrite)
                        { // check if the property can be set or no.
                            p.SetValue(dest, sourceProp.GetValue(source, null), null);
                        }
                    }

                }
            }

        }

    }

}
