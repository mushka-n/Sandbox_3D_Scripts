using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowElems : MonoBehaviour
{
    public GameObject animatedObject;
    ElemsAnimation animationController;
 
    public void Start()
    {
        animationController = animatedObject.GetComponent<ElemsAnimation>();
    }
    
    public void OpenMenu()
    {
        animationController.pressed();
    }  
}
