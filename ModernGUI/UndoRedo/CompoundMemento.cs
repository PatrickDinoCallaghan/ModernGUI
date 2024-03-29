﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernGUI.UndoRedo
{
    /// <summary>
    /// A class used to group multiple mementos together, which can be pushed on to the undo stack as a single memento. 
    /// With this class, multiple consecutive actions can be recognized as a single action, which are undo as an entity. 
    /// It also implements the <see cref="IMemento&lt;T&gt;"/> interface, which means one <see cref="CompoundMemento&lt;T&gt;"/> can be a 
    /// member of another <see cref="CompoundMemento&lt;T&gt;"/>. Therefore it is possible to create hierachical mementos. 
    /// </summary>
    /// <seealso cref="IMemento&lt;T&gt;"/>
    [Serializable]
    public class CompoundMemento<T> : IMemento<T>
    {
        private List<IMemento<T>> mementos = new List<IMemento<T>>();

        /// <summary>
        /// Adds memento to this complex memento. Note that the order of adding mementos is critical.
        /// </summary>
        /// <param name="m"></param>
        public void Add(IMemento<T> m)
        {
            mementos.Add(m);
        }

        /// <summary>
        /// Gets number of sub-memento contained in this complex memento.
        /// </summary>
        public int Size
        {
            get { return mementos.Count; }
        }

        #region IMemento Members

        /// <summary>
        /// Implicity implememntation of <see cref="IMemento&lt;T&gt;.Restore(T)"/>, which returns <see cref="CompoundMemento&lt;T&gt;"/>
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public CompoundMemento<T> Restore(T target)
        {
            CompoundMemento<T> inverse = new CompoundMemento<T>();
            //starts from the last action
            for (int i = mementos.Count - 1; i >= 0; i--)
                inverse.Add(mementos[i].Restore(target));
            return inverse;
        }

        /// <summary>
        /// Explicity implememntation of <see cref="IMemento&lt;T&gt;.Restore(T)"/>
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        IMemento<T> IMemento<T>.Restore(T target)
        {
            return Restore(target);
        }

        #endregion
    }
}
