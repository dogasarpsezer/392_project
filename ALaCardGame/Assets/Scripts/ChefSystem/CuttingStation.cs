using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using WanderTween;

public class CuttingStation : Station
{
    [SerializeField] private Vector3 cutPos;
    [SerializeField] private Vector3 cutRot;
    [SerializeField] private float speed;
    private Camera camera;
    protected override void Awake()
    {
        base.Awake();
        cutPos = utilTransform.position + cutPos;
        camera = Camera.main;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(utilTransform.position + cutPos,0.15f);
    }
    
    private void Update()
    {
        if (!stationActive)
        {
            if (TweenManager.TweenCount(utilTransform) > 0)
            {
               tapTimer.Restart();
               stationActive = true;
            }
        }
        
        if (stationActive && TweenManager.TweenCount(utilTransform) == 0)
        {
            tapTimer.Update(Time.deltaTime);
            if (tapTimer.TimerDone())
            {
                if (!tapObject.activeInHierarchy)
                {
                    tapObject.SetActive(true);
                }

                if (Input.GetKeyDown(tapKey))
                {
                    TweenManager.RegisterMoveToTween(utilTransform,target,cutPos,speed,
                        Easing.QuadEaseIn);
                    TweenManager.RegisterRotateToTween(utilTransform,utilTransform.rotation,
                        Quaternion.Euler(cutRot), speed,
                        Easing.QuadEaseIn);
                }
                delay += Time.deltaTime;
            }
        }
        
    }
}
