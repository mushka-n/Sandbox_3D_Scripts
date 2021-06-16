using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideElems : MonoBehaviour
{
    public GameObject animatedObject;
    ElemsAnimation animationController;
 
    public void Start()
    {
        animationController = animatedObject.GetComponent<ElemsAnimation>();
    }
    
    public void CloseMenu()
    {
        animationController.idle();
    }  
}