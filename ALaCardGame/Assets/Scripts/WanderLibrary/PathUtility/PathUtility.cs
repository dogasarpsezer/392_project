using System;
using System.Collections.Generic;
using UnityEngine;

namespace WanderPath
{
    public struct PathData
    {
        public int currentIndex;
        public float currentDistance;
        public Vector3 currentPosition;
    }
    
    public static class PathUtility
    {
        public static bool MoveOnPath(List<Vector3> path,ref PathData pathData, float movement)
        {
            movement += pathData.currentDistance;

            while (pathData.currentIndex < path.Count - 1)
            {
                var between = path[pathData.currentIndex + 1] - path[pathData.currentIndex];
                var distance = between.magnitude;
                if (movement < distance)
                {
                    pathData.currentDistance = movement;
                    pathData.currentPosition =
                        path[pathData.currentIndex] + between.normalized * pathData.currentDistance;
                    return false;
                }

                pathData.currentIndex++;
                movement -= distance;
            }

            pathData.currentIndex = path.Count - 2;
            pathData.currentPosition = path[pathData.currentIndex - 1];
            return true;
        }
    }
}