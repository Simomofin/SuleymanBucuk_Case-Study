using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 moveToVector;
    private Animator animator;
    private bool gameFinished = false;
    private SceneManagement sceneManagement;
    public float speedMultiplier = 1;

    private void OnEnable()
    {       
        GameManager.instance.OnDiamondStackFullfilled += StackFulfilled;
        GameManager.instance.OnDiamondStackNOTFullfilled += StackNOTFilled;
    }  
    private void OnDisable()
    {
        GameManager.instance.OnDiamondStackFullfilled -= StackFulfilled;
        GameManager.instance.OnDiamondStackNOTFullfilled -= StackNOTFilled;
    }    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        sceneManagement = GameObject.FindGameObjectWithTag(Helper.Tags.sceneManagement).GetComponent<SceneManagement>();
    }

    private void Update()
    {
        if(sceneManagement.GameBegan && !gameFinished)
        {
            moveToVector = transform.position + TouchControl.Movement();            
        }
    }

    private void FixedUpdate()
    {
        if (sceneManagement.GameBegan && !gameFinished)
            rb.MovePosition(transform.position + (TouchControl.Movement() * speedMultiplier * Time.fixedDeltaTime));

    } // FixedUpdate

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(Helper.Tags.finishLine))
        {
            animator.SetTrigger(Helper.AnimationParameters.dance);
            gameFinished = true;

            sceneManagement.GameHasFinished();            
        }
    }// OnTriggerEnter    

    private void StackFulfilled()
    {        
        animator.SetBool(Helper.AnimationParameters.run2, true);
        animator.SetBool(Helper.AnimationParameters.run1, false);
        speedMultiplier = 1.5f;
    }

    private void StackNOTFilled()
    {        
        animator.SetBool(Helper.AnimationParameters.run1, true);
        animator.SetBool(Helper.AnimationParameters.run2, false);
        speedMultiplier = 1f;
    }

} // Class
