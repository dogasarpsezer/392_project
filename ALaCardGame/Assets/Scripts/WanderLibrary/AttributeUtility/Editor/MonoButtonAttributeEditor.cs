using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace WanderAttribute.Editor
{
    [CustomEditor(typeof(MonoBehaviour),true)]
    public class MonoButtonAttributeEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            MonoBehaviour targetMono = (MonoBehaviour) target;

            var methods = targetMono.GetType()
                .GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (var method in methods)
            {
                var attributes = method.GetCustomAttributes(typeof(ButtonAttribute),true);

                foreach (var buttonAtt in attributes)
                {
                    if (GUILayout.Button(((ButtonAttribute)buttonAtt).GetButtonName()))
                    {
                        targetMono.Invoke(method.Name,0);
                    }
                }
                
            }

        }
    }
}