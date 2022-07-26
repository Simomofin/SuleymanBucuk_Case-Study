using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject ObjectToFollow;
    [Range(-9f,-15f)]
    public float distanceToFollow;
    private float heightToFollow;
    // Start is called before the first frame update
    void Start()
    {
        if(ObjectToFollow == null)
        {
            ObjectToFollow = GameObject.FindGameObjectWithTag("Player");
            if (ObjectToFollow == null)
                Debug.LogError("Camera can' t find object to follow!");           
        }
        if (distanceToFollow > -9 || distanceToFollow < -15)
            distanceToFollow = -9f;
        heightToFollow = 5f;

    } // Start

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, ObjectToFollow.transform.position.y + heightToFollow, ObjectToFollow.transform.position.z + distanceToFollow);
    } // Update    
} // class
