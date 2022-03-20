namespace Multiformats.Address;

using System;

internal static class Extensions
{
    public static T[] Slice<T>(this T[] array, int offset, int? count = null)
    {
        T[] result = new T[count ?? array.Length - offset];
        Array.Copy(array, offset, result, 0, result.Length);
        return result;
    }
}
