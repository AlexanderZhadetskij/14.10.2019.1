using PseudoEnumerable.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoEnumerable
{
    class Interface2DelegateAdapter<TSource> : IPredicate<TSource>
    {
        private readonly Predicate<TSource> predicate;
        public Interface2DelegateAdapter(Predicate<TSource> predicate)
        {
            this.predicate = predicate;
        }

        public bool IsMatching(TSource item)
        {
            return predicate(item);
        }
    }
}
