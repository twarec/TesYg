using System.Collections;
using System.Collections.Generic;

public static class YGCollections
{
    public static void ForeAche<T>(this IEnumerable<T> enumerable, System.Action<T> action)
    {
        foreach (var v in enumerable)
            action(v);
    }
}
