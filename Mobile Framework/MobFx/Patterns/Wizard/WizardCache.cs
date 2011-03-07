using System;
using System.Collections.Generic;
using ChristianHelle.Framework.WindowsMobile.Patterns;
using System.Diagnostics;

namespace ChristianHelle.Framework.WindowsMobile.Patterns.Wizard
{
    /// <summary>
    /// Helper class for 
    /// </summary>
    public class WizardCache : Dictionary<Type, WizardPresenter>
    {
        /// <summary>
        /// Disposes and removes all items from the collection and
        /// </summary>
        public void RemoveAll()
        {
            foreach (var value in Values)
                value.Dispose();
            Clear();
        }

        /// <summary>
        /// Disposes and removes all non-active items in the collection
        /// </summary>
        public void RemoveUnused()
        {
            var keys = Keys;
            foreach (var key in keys)
            {
                var value = this[key];
                if (value.WizardView.Visible) continue;
                Debug.WriteLine("Releasing: " + value);
                value.CancelWizard();
                value.Dispose();
                this[key] = null;
                Remove(key);
            }
        }
    }
}
