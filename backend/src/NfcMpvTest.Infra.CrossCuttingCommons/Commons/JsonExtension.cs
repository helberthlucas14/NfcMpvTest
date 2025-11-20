using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace NfcMpvTest.Infra.CrossCuttingCommons.Commons
{
    public static class JsonExtension

    {
        public static JsonSerializerSettings JsonSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    Formatting = Formatting.None,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    FloatFormatHandling = FloatFormatHandling.DefaultValue,
                    FloatParseHandling = FloatParseHandling.Decimal,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    NullValueHandling = NullValueHandling.Ignore,
                    Converters = new[] { new IsoDateTimeConverter { DateTimeStyles = System.Globalization.DateTimeStyles.AssumeLocal } }
                };
            }
        }
        public static string ToJson(this object objToJson, bool useSettings = false)
        {
            if (useSettings)
            {
                return JsonConvert.SerializeObject(objToJson, JsonSettings);
            }

            return JsonConvert.SerializeObject(objToJson);
        }
    }
}
