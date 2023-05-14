using System.Collections.Generic;
using UnityEngine;

namespace WanderDebug
{
    public static class DebugUtility
    {
        public static void DrawPath(List<Vector3> points, Color pathColor)
        {
            if (points.Count <= 1)
            {
                return;
            }
            for (int i = 0; i < points.Count - 1; i++)
            {
                Debug.DrawLine(points[i],points[i+1],pathColor);
            }
        }
        
        public static void DrawPath(Vector3[] points, Color pathColor)
        {
            if (points.Length <= 1)
            {
                return;
            }
            for (int i = 0; i < points.Length - 1; i++)
            {
                Debug.DrawLine(points[i],points[i+1],pathColor);
            }
        }

        public static void DrawCircle(List<Vector3> points, Color pathColor)
        {
            DrawPath(points,pathColor);
            Debug.DrawLine(points[0],points[^1],pathColor);
        }
    }
}