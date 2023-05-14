using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Areas : MonoBehaviour
{
    private void OnCollisionExit(Collision other)
    {
        if (other.collider.gameObject.layer == LayerMask.NameToLayer("Ingredient"))
        {
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionStay(Collision other)
    {
        throw new NotImplementedException();
    }
}
