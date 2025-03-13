namespace Permguard.AzReq;

public abstract class Builder
{
    // Deep copy of a Dictionary.
    protected static Dictionary<string,object> DeepCopy(Dictionary<string, object>? source)
    {
        var copy = new Dictionary<string,object>();
        if (source == null) return copy;
        foreach (var key in source.Keys)
        {
            copy[key] = source[key]; 
        }
        return copy;
    }
}
