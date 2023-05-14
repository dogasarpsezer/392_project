using System;
using UnityEngine;

namespace WanderAttribute
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false,Inherited = true)]
    public class ButtonAttribute : PropertyAttribute
    {
        private string buttonName;

        public string GetButtonName()
        {
            return buttonName;
        }
        
        public ButtonAttribute(string buttonName)
        {
            this.buttonName = buttonName;
        }
    }
}