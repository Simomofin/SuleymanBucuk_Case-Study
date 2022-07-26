using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManagement : MonoBehaviour
{    
    private enum MovingAxis
    {
        xAxis,
        zAxis,
        yAxis
    }
    //To decide which direction to go, decleared booleans below
    private bool rightSide = true, upward = true, forward = true;
    [SerializeField]
    private MovingAxis movingAxis;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float movementSpeed;
    private Vector3 targetLocation = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        if (rotationSpeed == 0)
            rotationSpeed = 5.5f;
        if (movementSpeed == 0)
            movementSpeed = 0.33f;
        //Set target location on x, y and z axis.
        targetLocation = new Vector3(transform.position.x + 1f, 5, transform.position.z + 2);
    }

    private void FixedUpdate()
    {
        if (movingAxis == MovingAxis.xAxis)
            MoveOnXAxis();
        else if (movingAxis == MovingAxis.zAxis)
            MoveOnZAxis();
        else if (movingAxis == MovingAxis.yAxis)
            MoveOnYAxis();

        transform.Rotate(Vector3.forward, rotationSpeed);
    } // FixedUpdate

    private void MoveOnYAxis()
    {
        if (upward)
            transform.position += new Vector3(0, movementSpeed, 0);
        else
            transform.position += new Vector3(0, -movementSpeed, 0);

        if (transform.position.y > targetLocation.y)
            upward = false;
        else if(transform.position.y < 0.5f)
            upward = true;
    }

    private void MoveOnZAxis()
    {
        if (forward)
            transform.position += new Vector3(0, 0, movementSpeed);
        else
            transform.position += new Vector3(0, 0, -movementSpeed);
        if(transform.position.z > targetLocation.z)
            forward = false;
        else if(transform.position.z < targetLocation.z - 4)
            forward = true;

    }

    private void MoveOnXAxis()
    {
        if(rightSide)
            transform.position += new Vector3(movementSpeed, 0, 0);
        else
            transform.position += new Vector3(-movementSpeed, 0, 0);
        if (transform.position.x > targetLocation.x)
            rightSide = false;
        else if(transform.position.x <targetLocation.x  -2)
            rightSide = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Helper.Tags.player))
            GameManager.instance.DecreaseStackAmount(1);
    }
} // Class
