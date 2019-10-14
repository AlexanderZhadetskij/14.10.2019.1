using System;
using System.Collections.Generic;
using System.Text;
using PseudoEnumerable.Interfaces;

namespace PseudoEnumerable
{
    public static class EnumerableExtension
    {
        #region Implementation through interfaces

        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            IPredicate<TSource> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException();
            }
            foreach (var item in source)
            {
                if (predicate.IsMatching(item))
                {
                    yield return item;
                }

            }
        }

        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            ITransformer<TSource, TResult> transformer)
        {
            return Transform<TSource, TResult>(source, transformer.Transform);
        }

        public static IEnumerable<TSource> OrderAccordingTo<TSource>(this IEnumerable<TSource> source,
            IComparer<TSource> comparer)
        {
            List<TSource> result = new List<TSource>();

            foreach (var item in source)
            {
                result.Add(item);
            }

            Array.Sort<TSource>(result.ToArray(), comparer);

            foreach (var item in result)
            {
                yield return item;
            }
        }

        private static List<TSource> GetCollection<TSource>(IEnumerable<TSource> sourceArray, IPredicate<TSource> predicate)
        {
            List<TSource> result = new List<TSource>();

            foreach (var item in sourceArray)
            {
                if (predicate.IsMatching(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }

        #endregion

        #region Implementation vs delegates

        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            Predicate<TSource> predicate)
        {            
            return Filter<TSource>(source, new Interface2DelegateAdapter<TSource>(predicate).IsMatching);
        }

        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            Converter<TSource, TResult> transformer)
        {
            foreach (var item in source)
            {
                yield return transformer(item);
            }
        }

        public static IEnumerable<TSource> OrderAccordingTo<TSource>(this IEnumerable<TSource> source,
            Comparison<TSource> comparer)
        {
            return OrderAccordingTo<TSource>(source, Comparer<TSource>.Create(comparer));
        }

        #endregion
    }
}