using UnityEngine;
using WanderExtension;

public class Ingredient : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;

    private Station stationed;
    public bool Stationed => stationed;
    public Station station => stationed;
    
    private Vector3 stationedPos;
    public Vector3 StationPos => stationedPos;
    public void SelectObject()
    {
        rigidbody.isKinematic = true;
        gameObject.ChangeLayerRecursive(LayerMask.NameToLayer("IngredientSelected"));
    }

    public void Station(Vector3 stationPos,Station station)
    {
        stationed = station;
        stationedPos = stationPos;
    }
        
    public void NotStation()
    {
        stationed = null;
    }
    
    public void DeSelectObject()
    {
        rigidbody.isKinematic = false;
        gameObject.ChangeLayerRecursive(LayerMask.NameToLayer("Ingredient"));
    }
}
