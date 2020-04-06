using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ProductsList.Extensions
{
    public static class SessionExtension
    {
        public static T GetObject<T>(this ISession session, string key)
        {
            var json = session.GetString(key);

            if (json == null)
                return default;

            return JsonConvert.DeserializeObject<T>(json);
        }

        public static void SetObject<T>(this ISession session, string key, T @object)
        {
            var json = JsonConvert.SerializeObject(@object);
            session.SetString(key, json);
        }
    }
}
