using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPM.MiniORM.ConsoleApp
{
    public static class Extension
    {
        public static string ToJson(this object obj) => JsonConvert.SerializeObject(obj, Formatting.Indented);

        public static T? ToObject<T>(this string jsonStr) => JsonConvert.DeserializeObject<T>(jsonStr);
    }
}
