using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Functionals : MonoBehaviour
{
    public GameObject boxInsides;

    public void Clear_Box()
    {
        foreach (Transform child in boxInsides.transform) Destroy(child.gameObject);
    }
}
