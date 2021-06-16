using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElemsAnimation : MonoBehaviour
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
