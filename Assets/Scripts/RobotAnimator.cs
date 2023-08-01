using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAnimator : MonoBehaviour
{

    Animator anim;


    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("Selected", true);
    }


    public void Selected()
    {
        anim.SetBool("Selected", true);
    }
    public void Deselected()
    {
        anim.SetBool("Selected", false);
    }

   

    // Update is called once per frame
    void Update()
    {
    }
}
