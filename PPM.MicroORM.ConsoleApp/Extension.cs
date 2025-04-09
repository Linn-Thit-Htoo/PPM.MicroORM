using Newtonsoft.Json;

namespace PPM.MicroORM.ConsoleApp;

public static class Extension
{
    public static string ToJson(this object obj) => JsonConvert.SerializeObject(obj);

    public static T? ToObject<T>(this string jsonStr) => JsonConvert.DeserializeObject<T>(jsonStr);
}
