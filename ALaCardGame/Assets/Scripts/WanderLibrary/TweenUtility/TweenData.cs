using WanderTimer;
using UnityEngine;

namespace WanderTween
{
    public delegate float EasingMethod(float t);
    public class TweenData
    {
        private EasingMethod easingMethod;
        private TimerUtility tweenTimer;
        public float TweenTime => easingMethod(tweenTimer.NormalizedTime);
        public bool TweenDone => tweenTimer.TimerDone();
        public TweenData(float tweenDuration,EasingMethod easingMethod)
        {
            tweenTimer = new TimerUtility(tweenDuration);
            this.easingMethod = easingMethod;
        }

        public void UpdateTween(float time)
        {
            tweenTimer.Update(time);
        }

        public virtual void ExecuteTween(Transform tweenTransform){}
        public virtual void FinishTween(Transform tweenTransform){}
    }

    public class ScaleBaseTween : TweenData
    {
        protected Vector3 targetScale;
        protected Vector3 originalScale;
        public ScaleBaseTween(float tweenDuration,EasingMethod easeMethod,Vector3 targetScale,Vector3 originalScale) : 
            base(tweenDuration,easeMethod)
        {
            this.targetScale = targetScale;
            this.originalScale = originalScale;
        }
    }
    
    public class PunchTween : ScaleBaseTween
    {
        public override void ExecuteTween(Transform tweenTransform)
        {
            tweenTransform.localScale = Vector3.Lerp(targetScale, originalScale, TweenTime);
        }

        public override void FinishTween(Transform tweenTransform)
        {
            tweenTransform.localScale = originalScale;
        }

        public PunchTween(float tweenDuration, EasingMethod easeMethod, Vector3 targetScale, Vector3 originalScale) 
            : base(tweenDuration, easeMethod, targetScale, originalScale)
        {
        }
    }
    
    public class ScaleToTween : ScaleBaseTween
    {
        public override void ExecuteTween(Transform tweenTransform)
        {
            tweenTransform.localScale = Vector3.Lerp(originalScale, targetScale, TweenTime);
        }
        public override void FinishTween(Transform tweenTransform)
        {
            tweenTransform.localScale = targetScale;
        }
        public ScaleToTween(float tweenDuration, EasingMethod easeMethod, Vector3 targetScale, Vector3 originalScale) 
            : base(tweenDuration, easeMethod, targetScale, originalScale)
        {
        }
    }

    public class MoveBaseTween : TweenData
    {
        protected Vector3 originalPosition;
        protected Vector3 targetPosition;
        
        public MoveBaseTween(float tweenDuration, EasingMethod easingMethod, Vector3 originalPosition, 
            Vector3 targetPosition) : 
            base(tweenDuration, easingMethod)
        {
            this.originalPosition = originalPosition;
            this.targetPosition = targetPosition;
        }
    }

    public class MoveToTween : MoveBaseTween
    {
        public override void ExecuteTween(Transform tweenTransform)
        {
            tweenTransform.position = Vector3.Lerp(originalPosition, targetPosition, TweenTime);
        }

        public override void FinishTween(Transform tweenTransform)
        {
            tweenTransform.position = targetPosition;
        }
        
        public MoveToTween(float tweenDuration, EasingMethod easingMethod, 
            Vector3 originalPosition, Vector3 targetPosition) : base(tweenDuration, easingMethod, 
            originalPosition, targetPosition)
        {
        }
    }

    public class RotateBaseTween : TweenData
    {
        protected Quaternion originalRotation;
        protected Quaternion targetRotation;
        
        public RotateBaseTween(float tweenDuration, EasingMethod easingMethod, 
            Quaternion originalRotation,Quaternion targetRotation) : 
            base(tweenDuration, easingMethod)
        {
            this.originalRotation = originalRotation;
            this.targetRotation = targetRotation;
        }
    }

    public class RotateToTween : RotateBaseTween
    {
        public override void ExecuteTween(Transform tweenTransform)
        {
            tweenTransform.rotation = Quaternion.Lerp(originalRotation,targetRotation,TweenTime);
        }

        public override void FinishTween(Transform tweenTransform)
        {
            tweenTransform.rotation = targetRotation;
        }

        public RotateToTween(float tweenDuration, EasingMethod easingMethod, 
            Quaternion originalRotation, Quaternion targetRotation) : base(tweenDuration, 
            easingMethod, originalRotation, targetRotation)
        {
        }
    }
}