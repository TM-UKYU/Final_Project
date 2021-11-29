using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListUtil
{
    public static void Replace<T>(this IList<T> self, int index1, int index2)
    {
        if (self.Count <= index1 || self.Count <= index2)
            return;

        var cache = self[index1];
        self[index1] = self[index2];
        self[index2] = cache;
    }
}