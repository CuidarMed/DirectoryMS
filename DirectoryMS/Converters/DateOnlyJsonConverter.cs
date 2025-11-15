using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DirectoryMS.Converters
{
    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        private const string DateFormat = "yyyy-MM-dd";

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var dateString = reader.GetString();
            if (string.IsNullOrWhiteSpace(dateString))
            {
                return default;
            }

            // Intentar parsear el formato ISO (YYYY-MM-DD)
            if (DateOnly.TryParseExact(dateString, DateFormat, null, System.Globalization.DateTimeStyles.None, out var date))
            {
                return date;
            }

            // Si no funciona, intentar parsear como DateOnly normal
            if (DateOnly.TryParse(dateString, out var parsedDate))
            {
                return parsedDate;
            }

            throw new JsonException($"No se pudo convertir el string '{dateString}' a DateOnly. Formato esperado: {DateFormat}");
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(DateFormat));
        }
    }

    public class NullableDateOnlyJsonConverter : JsonConverter<DateOnly?>
    {
        private const string DateFormat = "yyyy-MM-dd";

        public override DateOnly? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var dateString = reader.GetString();
            if (string.IsNullOrWhiteSpace(dateString))
            {
                return null;
            }

            // Intentar parsear el formato ISO (YYYY-MM-DD)
            if (DateOnly.TryParseExact(dateString, DateFormat, null, System.Globalization.DateTimeStyles.None, out var date))
            {
                return date;
            }

            // Si no funciona, intentar parsear como DateOnly normal
            if (DateOnly.TryParse(dateString, out var parsedDate))
            {
                return parsedDate;
            }

            throw new JsonException($"No se pudo convertir el string '{dateString}' a DateOnly. Formato esperado: {DateFormat}");
        }

        public override void Write(Utf8JsonWriter writer, DateOnly? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
            {
                writer.WriteStringValue(value.Value.ToString(DateFormat));
            }
            else
            {
                writer.WriteNullValue();
            }
        }
    }
}

