using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using WanderTween;

namespace WanderTween
{
    public class TweenManager : MonoBehaviour
    {
        static Dictionary<Transform,List<TweenData>> registeredTweens;
        private void Awake()
        {
            registeredTweens = new Dictionary<Transform, List<TweenData>>();
        }

        public static void RegisterPunchTween(Transform tweenTransform,Vector3 originalScale,Vector3 targetScale,
            float shrinkBackTime,EasingMethod easingMethod)
        {
            AddTween(tweenTransform,new PunchTween(shrinkBackTime, easingMethod, targetScale, originalScale));
        }
        
        public static void RegisterScaleToTween(Transform tweenTransform,Vector3 originalScale,Vector3 targetScale,
            float scaleTime,EasingMethod easingMethod)
        {
            AddTween(tweenTransform,new ScaleToTween(scaleTime,easingMethod,targetScale,originalScale));
        }

        public static void RegisterMoveToTween(Transform tweenTransform,Vector3 originalPosition,Vector3 targetPosition,
            float moveToTime,EasingMethod easingMethod)
        {
            AddTween(tweenTransform,new MoveToTween(moveToTime,easingMethod,originalPosition,targetPosition));
        }

        public static void RegisterRotateToTween(Transform tweenTransform,Quaternion originalRotation,
            Quaternion targetRotation, float rotateToTime,EasingMethod easingMethod)
        {
            AddTween(tweenTransform,new RotateToTween(rotateToTime,easingMethod,originalRotation,
                targetRotation));
        }

        static void AddTween(Transform tweenTransform,TweenData tweenData)
        {
            if (!registeredTweens.TryAdd(tweenTransform,new List<TweenData>(){tweenData}))
            {
                registeredTweens[tweenTransform].Add(tweenData);
            }
        }

        public static int TweenCount(Transform tweenTransform)
        {
            if (registeredTweens.ContainsKey(tweenTransform))
            {
                return registeredTweens[tweenTransform].Count;
            }
            
            return 0;
        }
        
        public static List<TweenData> GetTransformTweens(Transform tweenTransform)
        {
            registeredTweens.TryGetValue(tweenTransform,out List<TweenData> tweens);
            return tweens;
        }
        public static void RemoveRegisteredTween(Transform tweenTransform,int index)
        {
            registeredTweens[tweenTransform].RemoveAt(index);
        }
        
        private void Update()
        {
            List<Transform> tweenTransformsToRemoveFromDict = new List<Transform>();
            foreach (var tweenTransform in registeredTweens.Keys)
            {
                for (int i = 0; i < registeredTweens[tweenTransform].Count; i++)
                {
                    var tweenData = registeredTweens[tweenTransform][i];

                    if (!tweenTransform)
                    {
                        RemoveRegisteredTween(tweenTransform,i);
                        i--;
                        continue;
                    }
                
                    tweenData.ExecuteTween(tweenTransform);
                    tweenData.UpdateTween(Time.deltaTime);
                    if (tweenData.TweenDone)
                    {
                        tweenData.FinishTween(tweenTransform);
                        RemoveRegisteredTween(tweenTransform,i);
                        i--;
                    }
                }
                
                if (registeredTweens[tweenTransform].Count == 0)
                {
                    tweenTransformsToRemoveFromDict.Add(tweenTransform);
                }
            }

            foreach (var tweenTransform in tweenTransformsToRemoveFromDict)
            {
                registeredTweens.Remove(tweenTransform);
            }
            
        }
    }
}