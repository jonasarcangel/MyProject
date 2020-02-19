using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using MyProject.Core.Entities;
using MyProject.Core.Repositories;

namespace MyProject.EntityFrameworkCore
{
    public class Helpers
    {
        public static Predicate<T> Or<T>(params Predicate<T>[] predicates)
        {
            return delegate (T item)
            {
                foreach (Predicate<T> predicate in predicates)
                {
                    if (predicate(item))
                    {
                        return true;
                    }
                }
                return false;
            };
        }

        public static Predicate<T> And<T>(params Predicate<T>[] predicates)
        {
            return delegate (T item)
            {
                foreach (Predicate<T> predicate in predicates)
                {
                    if (!predicate(item))
                    {
                        return false;
                    }
                }
                return true;
            };
        }
    }
}