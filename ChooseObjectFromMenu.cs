using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseObjectFromMenu : MonoBehaviour
{

    public GameObject Metal;
    public GameObject Wood;
    public GameObject Fire;
    public GameObject Water;
    public GameObject Oil;
    public GameObject Elephant;

    void Start()
    {
        Change_Object(Metal);
    }
    
    public void Choose_Metal()
    {
        Change_Object(Metal);
    }
    
    public void Choose_Wood()
    {
        Change_Object(Wood);
    }
    
    public void Choose_Elephant()
    {
        Change_Object(Elephant);
    }

    public void Choose_Fire()
    {
        Change_Object(Fire);
    }
    
    public void Choose_Water()
    {
        Change_Object(Water);
    }
    
    public void Choose_Oil()
    {
        Change_Object(Oil);
    }

    public void Change_Object(GameObject chosenObject)
    {
        CreateObject.objectToCreate = chosenObject;
    }
}


