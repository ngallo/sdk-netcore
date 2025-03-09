namespace Permguard.Grpc;

using Google.Protobuf.WellKnownTypes;
using System.Collections.Generic;

public static class StructExtensions
{
    public static Dictionary<string, object> ToDictionary(this Struct structData)
    {
        var dict = new Dictionary<string, object>();

        if (structData == null)
        {
            return dict;
        }
        
        foreach (var field in structData.Fields)
        {
            dict[field.Key] = field.Value.ToObject();
        }

        return dict;
    }

    private static object ToObject(this Value value)
    {
        switch (value.KindCase)
        {
            case Value.KindOneofCase.NullValue:
                return null;
            case Value.KindOneofCase.NumberValue:
                return value.NumberValue;
            case Value.KindOneofCase.StringValue:
                return value.StringValue;
            case Value.KindOneofCase.BoolValue:
                return value.BoolValue;
            case Value.KindOneofCase.StructValue:
                return value.StructValue.ToDictionary(); 
            case Value.KindOneofCase.ListValue:
                return value.ListValue.ToObjectList();
            default:
                return null;
        }
    }

    // Metodo di supporto per mappare ListValue a una lista di oggetti
    private static List<object> ToObjectList(this ListValue listValue)
    {
        var list = new List<object>();

        if (listValue != null)
        {
            foreach (var item in listValue.Values)
            {
                list.Add(item.ToObject());
            }
        }

        return list;
    }
}
