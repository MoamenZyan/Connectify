
using Newtonsoft.Json;

namespace Connectify.Infrastructure.Services.RedisService;

public static class JsonConverterConfigurations
{
    public static JsonSerializerSettings options = new JsonSerializerSettings()
    {
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        PreserveReferencesHandling = PreserveReferencesHandling.All,
    };
}
