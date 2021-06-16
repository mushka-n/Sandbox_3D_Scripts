using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

public class DestroyObjectOnCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "ObjectToCreate")
        {
            Destroy(coll.gameObject);
        }
    }
}
