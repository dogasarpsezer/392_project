using UnityEngine;
using WanderExtension;
using Random = UnityEngine.Random;

public class KitchenManager : MonoBehaviour
{
    [SerializeField] private Camera kitchenCamera;
    [SerializeField] private float kitchenCounterHeight;
    [SerializeField] private float cameraDepth;
    [SerializeField] private Vector2 counterBorderX;
    [SerializeField] private Vector2 counterBorderY;
    [SerializeField] private float smoothTime;
    [SerializeField] private LayerMask draggableLayers;
    [SerializeField] private float dragStationMin;
    private Ingredient heldIngredient;

    private Vector3 velocity;
    private void Update()
    {
        var mouseRay = kitchenCamera.ScreenPointToRay(Input.mousePosition);
        
        if (Input.GetMouseButtonDown(0) && !heldIngredient)
        {
            RaycastHit hit;
            if (Physics.Raycast(mouseRay,out hit,100f, draggableLayers))
            {
                var ingredient = hit.collider.gameObject.GetComponent<Ingredient>();
                ingredient.SelectObject();
                heldIngredient = ingredient;
            }
        }

        
        if (heldIngredient)
        {
            Plane plane = new Plane(Vector3.up, new Vector3(0,kitchenCounterHeight,0));
            plane.Raycast(mouseRay, out float distance);
            Vector3 newPosition = mouseRay.GetPoint(distance);
            var current = heldIngredient.transform.position;
            
            if (heldIngredient.Stationed)
            {
                if (Vector3.Distance(newPosition,current) < dragStationMin)
                {
                    newPosition = heldIngredient.StationPos;
                }
                else
                {
                    heldIngredient.NotStation();
                }
            }
            heldIngredient.transform.position = Vector3.SmoothDamp(current, newPosition, ref velocity, smoothTime);
        }
        
        if (Input.GetMouseButtonUp(0) && heldIngredient)
        {
            
            if (heldIngredient.Stationed)
            {
                heldIngredient.station.SetStationReady();
                heldIngredient.gameObject.ChangeLayerRecursive(LayerMask.NameToLayer("IngredientStation"));
            }
            else
            {
                heldIngredient.DeSelectObject();
            }
            heldIngredient = null;
        }
        
       
        
    }

    public void CreateKitchenObject(int count,GameObject spawnedObject)
    {
        var mouseRay = kitchenCamera.ScreenPointToRay(Input.mousePosition);
        var mousePos = mouseRay.GetPoint(cameraDepth - kitchenCamera.transform.position.z);
        
        for (int i = 0; i < count; i++)
        {
            var randomSpawnPos = (Vector3)(Random.insideUnitCircle * 0.75f);
            var spawnPos = mousePos + randomSpawnPos;

            spawnPos.x = Mathf.Clamp(spawnPos.x, counterBorderX.x, counterBorderX.y);
            spawnPos.y = Mathf.Clamp(spawnPos.y, counterBorderY.x, counterBorderY.y);
            spawnPos.z = cameraDepth;
            Instantiate(spawnedObject,spawnPos,Quaternion.identity);
        }
    }
}
