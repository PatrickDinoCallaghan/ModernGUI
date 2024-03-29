﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernGUI.UndoRedo
{

    /// <summary>
    /// The most generic interface of a memento class. A memento is a state that can be saved and restored.
    /// See details on <b>Memento Design Pattern</b> concept 
    /// <a href="http://www.google.com/search?q=memento+design+pattern+&amp;btnG=Search">here</a>.
    /// </summary>
    /// <remarks>
    /// <b>Design Considerations:</b>
    /// The common memento struture a state property and sometimes an action property. 
    /// In this design those properties are not explicitly , though essentially the 
    /// In this design, if a memento stores a state and doesn't has the ability to Restore the target to the state, 
    /// we have to introduce another class or a class per type of memento. 
    /// With the memento itslef supporting Retore of the states, the state and action are not necessary to be exposed as public properties. 
    /// And only one class for each type of memento is required, which is simpler to read and easier to maintain.
    /// <b>NOTE</b> that every class that implements this interface should be serializable, 
    /// by either annotate it with "[Serializabl]" or manuallying implementing the serialization methods. 
    /// This requirement should be full filled in order to support the serialization of <see cref="UndoRedoHistory&lt;T&gt;"/>.
    /// </remarks>
    public interface IMemento<T>
    {

        /// <summary>
        /// Restores target to the state memorized by this memento. Here shows an exapmle of usage.
        /// <code>
        /// public void TestMemento(IMemento&lt;Object&gt; memento, Object target) 
        /// {
        ///     Object oldObj = target.Clone();
        ///     IMemento&lt;Object&gt; previousState = memento.Restore(target);
        ///     Object newObj = previousState.Restore(target);
        ///     Debug.Assert(oldObj.Equals(newObj));
        /// }
        /// </code>
        /// </summary>
        /// <returns>A memento of the state before restoring, which is refered to as a <i>Inverse Memento</i> of this memento.</returns>
        /// <remarks>
        /// Being able to restore its own state, undo can be implemented using an undo stack. But that's not enough for implementing redo. 
        /// The returned inverse memento is the key to support redo.
        /// </remarks>
        IMemento<T> Restore(T target);

    }
}
