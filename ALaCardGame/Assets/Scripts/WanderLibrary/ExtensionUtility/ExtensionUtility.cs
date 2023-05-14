using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WanderTween;
using Random = UnityEngine.Random;

namespace WanderExtension
{
    public static class ExtensionUtility
    {
        public static Queue<T> ListToShuffleQueue<T>(this List<T> list)
        {
            List<T> listToShuffle = list.CopyToList();
            var shuffledQueue = new Queue<T>();

            while (listToShuffle.Count > 0)
            {
                int index = Random.Range(0,listToShuffle.Count);
                shuffledQueue.Enqueue(listToShuffle[index]);
                listToShuffle.RemoveAt(index);
            }

            return shuffledQueue;
        }

        public static List<T> CopyToList<T>(this List<T> list)
        {
            List<T> newList = new List<T>();
            foreach (var member in list)
            {
                newList.Add(member);
            }

            return newList;
        }
        
        public static void DrawGizmos(this List<Vector3> path,Color color)
        {
            if (path == null)
            {
                return;
            }
            if (path.Count == 0)
            {
                return;
            }
            Gizmos.color = color;

            for (int i = 0; i < path.Count - 1; i++)
            {
                Gizmos.DrawLine(path[i],path[i+1]);
            }
        }
        
        public static void DrawGizmos(this List<Vector3> path,Transform transform,Color color)
        {
            if (path.Count == 0)
            {
                return;
            }
            Gizmos.color = color;

            for (int i = 0; i < path.Count - 1; i++)
            {
                Gizmos.DrawLine(transform.TransformPoint(path[i]),transform.TransformPoint(path[i+1]));
            }
        }

        public static Ray CameraRay(this Camera camera, Vector2 screenPosition)
        {
            return camera.ScreenPointToRay(screenPosition);
        }
        
        public static void ChangeLayerRecursive(this GameObject gameObject,int newLayer)
        {
            gameObject.layer = newLayer;
            foreach (Transform child in gameObject.GetComponentsInChildren<Transform>())
            {
                child.gameObject.layer = newLayer;
            }
        }
        
        public static void RemoveTweenTypeOf<T>(this Transform transform) where T : TweenData
        {
            var tweens = TweenManager.GetTransformTweens(transform);
            if (tweens == null)
            {
                return;
            }

            for (int i = 0; i < tweens.Count; i++)
            {
                if (tweens[i].GetType() == typeof(T))
                {
                    TweenManager.RemoveRegisteredTween(transform,i);
                    i--;
                }
            }
        }
    }
}