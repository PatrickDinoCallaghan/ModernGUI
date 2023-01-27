using System;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel.Design;

namespace ModernGUI.Controls
{
	/// <summary>
	/// Summary description for AutoCompleteTriggerCollection.
	/// </summary>
	[Serializable]
	public class AutoCompleteTriggerCollection : CollectionBase
	{

		public class AutoCompleteTriggerCollectionEditor : CollectionEditor
		{
			public AutoCompleteTriggerCollectionEditor(Type type) : base(type)
			{
			}

			protected override bool CanSelectMultipleInstances()
			{
				return false;
			}

			protected override Type[] CreateNewItemTypes()
			{
				return new Type[] {typeof(ShortCutTrigger), typeof(TextLengthTrigger)};
			}

			protected override Type CreateCollectionItemType()
			{
				return typeof(ShortCutTrigger);
			}
		}

		public AutoCompleteTrigger this[int index]
		{
			get
			{
				return this.InnerList[index] as AutoCompleteTrigger;
			}
		}

		public void Add(AutoCompleteTrigger item)
		{
			this.InnerList.Add(item);
		}

		public void Remove(AutoCompleteTrigger item)
		{
			this.InnerList.Remove(item);
		}

		public virtual TriggerState OnTextChanged(string text)
		{
			foreach (AutoCompleteTrigger trigger in this.InnerList)
			{
				TriggerState state = trigger.OnTextChanged(text);
				if (state != TriggerState.None)
				{
					return state;
				}
			}
			return TriggerState.None;
		}
		
		public virtual TriggerState OnCommandKey(Keys keyData)
		{
			foreach (AutoCompleteTrigger trigger in this.InnerList)
			{
				TriggerState state = trigger.OnCommandKey(keyData);
				if (state != TriggerState.None)
				{
					return state;
				}
			}
			return TriggerState.None;
		}
		


	}
}
