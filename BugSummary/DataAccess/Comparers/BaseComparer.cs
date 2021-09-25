﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Comparers
{
    public abstract class BaseComparer<T> : IComparer where T : class
    {
        public int Compare(object x, object y)
        {
            var expected = x as T;
            var actual = y as T;

            bool equals = this.ConcreteCompare(expected, actual);

            return equals ? 0 : 1;
        }

        protected abstract bool ConcreteCompare(T expected, T actual);
    }

}
