using System;
using System.Collections;
using Obi;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    public static GameObject objectToCreate;
    public  Transform BoxInsides;
    
    public Collider Wall1;
    public Collider Wall2;
    public Collider Wall3;
    public Collider Wall4;
    
    private RaycastHit hit;
    private Collider[] Walls;
    private ObiEmitter emitter;

    ////////////////////////////// Update for every 0.1s //////////////////////////////

    // If player interacts with box objects instatiate
    private void Start()
    {
        StartCoroutine(MainCour());
        Walls = new Collider[] {Wall1, Wall2, Wall3, Wall4};
    }
    
    // Counter for every 0.1 seconds
    IEnumerator MainCour() {
        while (true) {
            yield return new WaitForSeconds(0.1f);
            MyUpdate();
        }
    }                                                                                                    
    
    // Happens every 0.1 seconds
    void MyUpdate()
    {
        if (Input.GetMouseButton(0) || Input.touchCount > 0)
            CastRay();
        else {
            if (objectToCreate.CompareTag("Liquid")) {
                emitter = objectToCreate.GetComponent<ObiEmitter>();
                emitter.speed = 0;
            }
        }
    }
    

    /////////////////////////////////// Main Functions ///////////////////////////////////

    // If player interacts with the box casts ray to cursor
    private void CastRay()
    {
        // Casts ray from camera to chosen point
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var layerMask = 1 << 8 | 1 << 11 ;
        layerMask = ~layerMask;

        // If ray hits a proper collider generates object
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)
                && !hit.collider.CompareTag("RayDestroy")
                && CheckForWallCollision(CalculateObjectPosition(hit.point, hit.normal))
                && !(objectToCreate.CompareTag("Phenomenon") && hit.collider.CompareTag("Phenomenon")))
            ChangeParametersAndCreate(
                CalculateObjectPosition(hit.point, hit.normal),
                new Vector3(hit.transform.rotation.eulerAngles.x, hit.transform.rotation.eulerAngles.y, 0f));
    }

    void ChangeParametersAndCreate(Vector3 objpos, Vector3 objrotation)
    {
        if (objectToCreate.CompareTag("Solid"))
            SpawnObject(objpos, objrotation);
        else if (objectToCreate.CompareTag("Phenomenon")) {
            objrotation.x += 90;
            CastPhenomenon(objpos, objrotation);
        } 
        else if (objectToCreate.CompareTag("Liquid"))
            MoveEmitter(objpos);
    }

    // Calculates a point where object should be spawned
    Vector3 CalculateObjectPosition(Vector3 hitPoint, Vector3 hitNormal)
    {
        Vector3 otcLocalScale = objectToCreate.transform.localScale;
        if (objectToCreate.CompareTag("Liquid") || objectToCreate.CompareTag("Phenomenon"))
            hitNormal *= 0.25f;
        else {
            hitNormal.x*= otcLocalScale.x/2;
            hitNormal.y*= otcLocalScale.y/2;
            hitNormal.z*= otcLocalScale.z/2;
        }
        return hitPoint+hitNormal;
    }

    // Checks if object will glitch into box walls on instatiate
    bool CheckForWallCollision(Vector3 objpos)
    {
        for (int i = 0; i < 4; i++) {
            Vector3 cp = Walls[i].ClosestPoint(objpos);
            if (cp == objpos) return false;
        }
        return true;
    }

    // Generates object on given point (with extra fixing)
    private void SpawnObject(Vector3 objpos, Vector3 objrotation)
    {
        Instantiate(
            objectToCreate,                    // GameObject to create
            objpos,                            // Vector3 as its position
            Quaternion.Euler(objrotation),     // Quaternion as its rotation values
            BoxInsides                         // Transform as its parent
        );
    }
  
    // Generates phenomenon on given point
    public void CastPhenomenon(Vector3 objpos, Vector3 objrotation)
    {
        Instantiate(
            objectToCreate,                 // GameObject to create
            objpos,                         // Vector3 as its position
            Quaternion.Euler(objrotation),  // Quaternion as its rotation values
            BoxInsides                      // Transform as its parent
        );
    }

    // Moves Emmiter to a given point
    private void MoveEmitter(Vector3 objpos)
    {
        objectToCreate.transform.position = objpos;
        emitter.speed = 4f;
    }

}