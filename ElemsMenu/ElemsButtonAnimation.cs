using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElemsButtonAnimation : MonoBehaviour
{
    public Animator animator;
    
    
    public void pressed()
    {
        animator.SetTrigger("Pressed");
    }

    public void idle()
    {
        animator.SetTrigger("Normal");
    }
}
