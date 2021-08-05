using System.Text.Json;
using Blazored.SessionStorage.Serialization;
using Blazored.SessionStorage.StorageOptions;
using Microsoft.Extensions.Options;

namespace Mitsukeru
{
    internal class SystemTextJsonSerializer : IJsonSerializer
    {
        private readonly JsonSerializerOptions _options = new();

        public SystemTextJsonSerializer()
        {
            _options.WriteIndented = false;
        }

        public T? Deserialize<T>(string data) 
            => JsonSerializer.Deserialize<T>(data, _options);

        public string Serialize<T>(T data)
            => JsonSerializer.Serialize(data, _options);
    }
}