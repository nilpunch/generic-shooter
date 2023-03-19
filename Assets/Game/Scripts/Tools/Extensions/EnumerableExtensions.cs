using System;
using System.Collections.Generic;

public static class EnumerableExtensions
{
    ///<summary>Wraps this object instance into an IEnumerable&lt;T&gt; consisting of a single item.</summary>
    ///<typeparam name="T">Type of the object.</typeparam>
    ///<param name="item">The instance that will be wrapped. </param>
    ///<returns>An IEnumerable&lt;T&gt; consisting of a single item.</returns>
    public static IEnumerable<T> Yield<T>(this T item)
    {
        yield return item;
    }

    ///<summary>Finds the index of the first item matching an expression in an enumerable.</summary>
    ///<typeparam name="T">Type of the object.</typeparam>
    ///<param name="items">The enumerable to search.</param>
    ///<param name="predicate">The expression to test the items against.</param>
    ///<returns>The index of the first matching item, or -1 if no items match.</returns>
    public static int IndexOf<T>(this IEnumerable<T> items, Func<T, bool> predicate)
    {
        if (items == null)
            throw new ArgumentNullException(nameof(items));
        if (predicate == null)
            throw new ArgumentNullException(nameof(predicate));

        int index = 0;
        foreach (var item in items)
        {
            if (predicate(item))
                return index;
            index++;
        }

        return -1;
    }

    ///<summary>Finds the index of the first occurrence of an item in an enumerable.</summary>
    ///<typeparam name="T">Type of the object.</typeparam>
    ///<param name="items">The enumerable to search.</param>
    ///<param name="item">The item to find.</param>
    ///<returns>The index of the first matching item, or -1 if the item was not found.</returns>
    public static int IndexOf<T>(this IEnumerable<T> items, T item)
    {
        if (items == null)
            throw new ArgumentNullException(nameof(items));

        return items.IndexOf(i => EqualityComparer<T>.Default.Equals(item, i));
    }

    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        foreach (var item in source)
            action.Invoke(item);
    }

    public static IEnumerable<TResult> Pairwise<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TSource, TResult> resultSelector)
    {
        using var it = source.GetEnumerator();

        if (it.MoveNext() == false)
            yield break;

        TSource previous = it.Current;

        while (it.MoveNext())
        {
            yield return resultSelector(previous, it.Current);
            previous = it.Current;
        }
    }

    public static IEnumerable<TResult> DistinctPairs<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TSource, TResult> resultSelector)
    {
        using var first = source.GetEnumerator();
        using var second = source.GetEnumerator();

        while (first.MoveNext())
        {
            second.Reset();

            while (second.MoveNext())
                if (EqualityComparer<TSource>.Default.Equals(second.Current, first.Current))
                    break;

            while (second.MoveNext())
                yield return resultSelector(first.Current, second.Current);
        }
    }

    public static T GetMin<T>(this IEnumerable<T> items, Func<T, float> action)
    {
        if (items == null)
            throw new ArgumentNullException(nameof(items));

        float min = float.MaxValue;
        T minItem = default;

        foreach (var item in items)
        {
            float value = action.Invoke(item);
            if (min > value)
            {
                min = value;
                minItem = item;
            }
        }

        return minItem;
    }

    public static T GetMax<T>(this IEnumerable<T> items, Func<T, float> action)
    {
        if (items == null)
            throw new ArgumentNullException(nameof(items));

        float max = float.MinValue;
        T maxItem = default;

        foreach (var item in items)
        {
            float value = action.Invoke(item);
            if (max < value)
            {
                max = value;
                maxItem = item;
            }
        }

        return maxItem;
    }
}
