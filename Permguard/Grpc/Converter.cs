using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;

namespace Permguard.Grpc;

public class Converter
{
    public static Struct FromDictionary(Dictionary<string, object>? dict)
    {
        var structObj = new Struct();
        
        if (dict != null)
        {
            foreach (var kvp in dict)
            {
                object value = kvp.Value;
                var convertedValue = ConvertToValue(value);
                structObj.Fields.Add(kvp.Key, convertedValue);
            }
        }

        return structObj;
    }

    public static RepeatedField<Struct> ToRepeatedField(List<Dictionary<string, object>?> items)
    {
        var repeatedField = new RepeatedField<Struct>();

        if (items != null)
        {
            foreach (var item in items)
            {
                repeatedField.Add(Converter.FromDictionary(item));
            }
        }

        return repeatedField;
    }
    
    private static Value ConvertToValue(object value)
    {
        switch (value)
        {
            case string str:
                return new Value { StringValue = str };
            case bool boolean:
                return new Value { BoolValue = boolean };
            case double dbl:
                return new Value { NumberValue = dbl };
            case int integer:
                return new Value { NumberValue = integer };
            case long l:
                return new Value { NumberValue = l };
            case DateTime dateTime:
                return new Value { StringValue = dateTime.ToString("o") }; // Formato ISO 8601
            case Dictionary<string, object> dict:
                // Ricorsivamente converte i dizionari
                return new Value { StructValue = ToStruct(dict) };
            default:
                return new Value(); 
        }
    }
    
    private static Struct ToStruct(Dictionary<string, object> dictionary)
    {
        var structObj = new Struct();

        foreach (var kvp in dictionary)
        {
            structObj.Fields.Add(kvp.Key, ConvertToValue(kvp.Value));
        }

        return structObj;
    }
    
    public static Dictionary<string, object> ToDictionary(Struct structObj)
    {
        var dict = new Dictionary<string, object>();
    
        if (structObj.Fields != null)
        {
            foreach (var kvp in structObj.Fields)
            {
                var value = ConvertFromValue(kvp.Value);
                dict.Add(kvp.Key, value);
            }
        }

        return dict;
    }

    private static object ConvertFromValue(Value value)
    {
        if (value.HasNullValue) 
            return null;
    
        switch (value.KindCase)
        {
            case Value.KindOneofCase.StringValue:
                return value.StringValue;
            case Value.KindOneofCase.NumberValue:
                return value.NumberValue;
            case Value.KindOneofCase.BoolValue:
                return value.BoolValue;
            case Value.KindOneofCase.NullValue:
                return null;
            default:
                throw new InvalidOperationException("Unsupported value type.");
        }
    }
}