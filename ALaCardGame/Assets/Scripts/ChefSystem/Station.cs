using UnityEngine;
using WanderTimer;
using WanderTween;

public class Station : MonoBehaviour
{
    [SerializeField] protected Vector3 stationaryLocalPos;
    [SerializeField] protected Transform utilTransform;
    [SerializeField] protected Vector3 utilitySetPos;
    protected Vector3 utilityInitPos;
    protected Quaternion utilityInitRot;
    protected Vector3 target;
    [SerializeField] protected float utilitySetTime;
    [SerializeField] protected Vector3 utilityRotation;
    [SerializeField] protected TimerUtility tapTimer;
    [SerializeField] protected KeyCode tapKey;
    [SerializeField] protected GameObject tapObject;

    protected float delay;
    

    protected virtual void Awake()
    {
        tapObject.SetActive(false);
        utilityInitPos = utilTransform.position;
        target = utilityInitPos + utilitySetPos;
        utilityInitRot = utilTransform.rotation;
    }

    protected bool stationActive = false;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position + stationaryLocalPos,0.15f);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("IngredientSelected"))
        {
            var ingredient = other.gameObject.GetComponent<Ingredient>();
            ingredient.Station(transform.position + stationaryLocalPos,this);
        }
    }

    public void SetStationReady()
    {
        TweenManager.RegisterMoveToTween(utilTransform,utilityInitPos,target,
            utilitySetTime,Easing.QuadEaseIn);
        TweenManager.RegisterRotateToTween(utilTransform,utilTransform.rotation,
            Quaternion.Euler(utilityRotation), utilitySetTime,Easing.QuadEaseIn);
    }
}
