namespace WanderTween
{
    //TODO ADD MORE TYPES OF TWEEN BESIDES LINEAR https://easings.net/
    public class Easing
    {
        public static float LinearEaseIn(float t)
        {
            return t;
        }
        
        public static float QuadEaseIn(float t)
        {
            return t * t;
        }
    }
}