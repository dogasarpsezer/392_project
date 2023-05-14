using System;
using UnityEditor;
using UnityEngine;

namespace WanderApp.Editor
{
    public class FrameUtility
    {
        [MenuItem("Wander Lib/ App Controller/ FPS/ Frame Rate 15")]
        public static void FrameRate15()
        {
            Application.targetFrameRate = 15;
        }
        
        [MenuItem("Wander Lib/ App Controller/ FPS/ Frame Rate 30")]
        public static void FrameRate30()
        {
            Application.targetFrameRate = 30;

        }
        
        [MenuItem("Wander Lib/ App Controller/ FPS/ Frame Rate 60")]
        public static void FrameRate60()
        {
            Application.targetFrameRate = 60;

        }
        
        [MenuItem("Wander Lib/ App Controller/ FPS/ Frame Rate 120")]
        public static void FrameRate120()
        {
            Application.targetFrameRate = 120;

        }
        
        [MenuItem("Wander Lib/ App Controller/ FPS/ Frame Rate MAX")]
        public static void FrameRateMax()
        {
            Application.targetFrameRate = Int32.MaxValue;
        }
    }
}