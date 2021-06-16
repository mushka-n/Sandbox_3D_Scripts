using System;
using System.Collections;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name != "Fire")
        {
            FireProperties objProperties = collider.gameObject.GetComponent<FireProperties>();
            if (collider.tag == "Solid" || collider.tag == "Liquid")
            {
                if (objProperties.state == 0)
                {
                    objProperties.Ignite(transform.gameObject);
                }
            }
        }
    }

    private void Update()
    {
        if (transform.parent)
        {
            if (transform.parent.name == "BoxInsides")
            {
                StartCoroutine(DestroyFire());
            }
        }
    }

    IEnumerator DestroyFire()
    {
        yield return new WaitForSeconds(4f);
        Destroy(transform.gameObject);
    }
}
