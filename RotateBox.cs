using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBox : MonoBehaviour
{
    public float rotSpeed = 500;

    public GameObject PlayerView;

    public GameObject Wall1;
    public GameObject Wall2;
    public GameObject Wall3;
    public GameObject Wall4;
    
    public GameObject Coll12;
    public GameObject Coll14;
    public GameObject Coll23;
    public GameObject Coll34;


    private void Start()
    {
        Position0();
    }

    void OnMouseDrag()
    {
        if (Input.mousePosition.y<86f)
        {
            float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad * -1;
            PlayerView.transform.Rotate(Vector3.down, rotX);

            float BoxRotationY = PlayerView.transform.rotation.eulerAngles.y;
            const float os = 22.5f;
            if      (BoxRotationY > os*15 || BoxRotationY <=os*1 ) Position7();
            else if (BoxRotationY > os*1  && BoxRotationY <=os*3 ) Position6();
            else if (BoxRotationY > os*3  && BoxRotationY <=os*5 ) Position5();
            else if (BoxRotationY > os*5  && BoxRotationY <=os*7 ) Position4();
            else if (BoxRotationY > os*7  && BoxRotationY <=os*9 ) Position3();
            else if (BoxRotationY > os*9  && BoxRotationY <=os*11) Position2();
            else if (BoxRotationY > os*11 && BoxRotationY <=os*13) Position1();
            else if (BoxRotationY > os*13 && BoxRotationY <=os*15) Position0();
        }
    }


    void FadeOut(Renderer renderer)
    {
        var material = renderer.material;
        var color = material.color;
        material.color = new Color(color.r, color.g, color.b, color.a - (2f * Time.deltaTime));
    }
    
    void FadeIn(Renderer renderer)
    {
        var material = renderer.material;
        var color = material.color;
        material.color = new Color(color.r, color.g, color.b, 0.4f);
    }
    
    
    private void Position0()
    {
        Wall1.layer = 8;
        FadeOut(Wall1.GetComponent<MeshRenderer>());
        Wall2.layer = 1;
        FadeIn(Wall2.GetComponent<MeshRenderer>());
        Wall3.layer = 1;
        FadeIn(Wall2.GetComponent<MeshRenderer>());
        Wall4.layer = 8;
        FadeOut(Wall4.GetComponent<MeshRenderer>());
        
        Coll12.tag = "RayDestroy";
        Coll14.tag = "Untagged";
        Coll23.tag = "RayDestroy";
        Coll34.tag = "RayDestroy";
        Coll12.SetActive(true);
        Coll14.SetActive(false);
        Coll23.SetActive(true);
        Coll34.SetActive(true);
    }
    
    private void Position1()
    {
        Wall1.layer = 1;
        FadeIn(Wall1.GetComponent<MeshRenderer>());
        Wall2.layer = 1;
        FadeIn(Wall2.GetComponent<MeshRenderer>());
        Wall3.layer = 1;
        FadeIn(Wall3.GetComponent<MeshRenderer>());
        Wall4.layer = 8;
        FadeOut(Wall4.GetComponent<MeshRenderer>());
        
        Coll12.tag = "Untagged";
        Coll14.tag = "RayDestroy";
        Coll23.tag = "Untagged";
        Coll34.tag = "RayDestroy";
        Coll12.SetActive(true);
        Coll14.SetActive(true);
        Coll23.SetActive(true);
        Coll34.SetActive(true);
    }
    
    private void Position2()
    {
        Wall1.layer = 1;
        FadeIn(Wall1.GetComponent<MeshRenderer>());
        Wall2.layer = 1;
        FadeIn(Wall2.GetComponent<MeshRenderer>());
        Wall3.layer = 8;
        FadeOut(Wall3.GetComponent<MeshRenderer>());
        Wall4.layer = 8;
        FadeOut(Wall4.GetComponent<MeshRenderer>());
        
        Coll12.tag = "Untagged";
        Coll14.tag = "RayDestroy";
        Coll23.tag = "RayDestroy";
        Coll34.tag = "RayDestroy";
        Coll12.SetActive(true);
        Coll14.SetActive(true);
        Coll23.SetActive(true);
        Coll34.SetActive(false);
    }
    
    private void Position3()
    {
        Wall1.layer = 1;
        FadeIn(Wall1.GetComponent<MeshRenderer>());
        Wall2.layer = 1;
        FadeIn(Wall2.GetComponent<MeshRenderer>());
        Wall3.layer = 8;
        FadeOut(Wall3.GetComponent<MeshRenderer>());
        Wall4.layer = 1;
        FadeIn(Wall4.GetComponent<MeshRenderer>());
        
        Coll12.tag = "Untagged";
        Coll14.tag = "Untagged";
        Coll23.tag = "RayDestroy";
        Coll34.tag = "RayDestroy";
        Coll12.SetActive(true);
        Coll14.SetActive(true);
        Coll23.SetActive(true);
        Coll34.SetActive(true);
    }
    
    private void Position4()
    {
        Wall1.layer = 1;
        FadeIn(Wall1.GetComponent<MeshRenderer>());
        Wall2.layer = 8;
        FadeOut(Wall2.GetComponent<MeshRenderer>());
        Wall3.layer = 8;
        FadeOut(Wall3.GetComponent<MeshRenderer>());
        Wall4.layer = 1;
        FadeIn(Wall4.GetComponent<MeshRenderer>());
        
        Coll12.tag = "RayDestroy";
        Coll14.tag = "Untagged";
        Coll23.tag = "RayDestroy";
        Coll34.tag = "RayDestroy";
        Coll12.SetActive(true);
        Coll14.SetActive(true);
        Coll23.SetActive(false);
        Coll34.SetActive(true);
    }
    
    private void Position5()
    {
        Wall1.layer = 1;
        FadeIn(Wall1.GetComponent<MeshRenderer>());
        Wall2.layer = 8;
        FadeOut(Wall2.GetComponent<MeshRenderer>());
        Wall3.layer = 1;
        FadeIn(Wall3.GetComponent<MeshRenderer>());
        Wall4.layer = 1;
        FadeIn(Wall4.GetComponent<MeshRenderer>());
        
        Coll12.tag = "RayDestroy";
        Coll14.tag = "Untagged";
        Coll23.tag = "RayDestroy";
        Coll34.tag = "Untagged";
        Coll12.SetActive(true);
        Coll14.SetActive(true);
        Coll23.SetActive(true);
        Coll34.SetActive(true);
    }

    private void Position6()
    {
        Wall1.layer = 8;
        FadeOut(Wall1.GetComponent<MeshRenderer>());
        Wall2.layer = 8;
        FadeOut(Wall2.GetComponent<MeshRenderer>());
        Wall3.layer = 1;
        FadeIn(Wall3.GetComponent<MeshRenderer>());
        Wall4.layer = 1;
        FadeIn(Wall4.GetComponent<MeshRenderer>());
        
        Coll12.tag = "RayDestroy";
        Coll14.tag = "RayDestroy";
        Coll23.tag = "RayDestroy";
        Coll34.tag = "Untagged";
        Coll12.SetActive(false);
        Coll14.SetActive(true);
        Coll23.SetActive(true);
        Coll34.SetActive(true);
    }
    
    private void Position7()
    {
        Wall1.layer = 8;
        FadeOut(Wall1.GetComponent<MeshRenderer>());
        Wall2.layer = 1;
        FadeIn(Wall2.GetComponent<MeshRenderer>());
        Wall3.layer = 1;
        FadeIn(Wall3.GetComponent<MeshRenderer>());
        Wall4.layer = 1;
        FadeIn(Wall4.GetComponent<MeshRenderer>());
        
        Coll12.tag = "RayDestroy";
        Coll14.tag = "RayDestroy";
        Coll23.tag = "Untagged";
        Coll34.tag = "Untagged";
        Coll12.SetActive(true);
        Coll14.SetActive(true);
        Coll23.SetActive(true);
        Coll34.SetActive(true);
    }
}

