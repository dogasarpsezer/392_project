using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace WanderAttribute.Editor
{
    [CustomEditor(typeof(ScriptableObject),true)]
    public class ScriptableButtonAttributeEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            ScriptableObject targetScriptable = (ScriptableObject) target;

            var methods = targetScriptable.GetType()
                .GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (var method in methods)
            {
                var attributes = method.GetCustomAttributes(typeof(ButtonAttribute),true);

                foreach (var buttonAtt in attributes)
                {
                    if (GUILayout.Button(((ButtonAttribute)buttonAtt).GetButtonName()))
                    {
                        method.Invoke(targetScriptable, null);
                    }
                }
                
            }
        }
    }
}