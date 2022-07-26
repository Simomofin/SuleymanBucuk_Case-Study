using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManagement : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed;
    private bool isCollided = false; // To prevent more than once colliding

    // Start is called before the first frame update
    void Start()
    {
        if (rotateSpeed == 0)
            rotateSpeed = 5f;
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up, rotateSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (!isCollided && other.gameObject.CompareTag(Helper.Tags.player))
        {
            isCollided = true;
            GameManager.instance.IncreaseCurrency(5);
            gameObject.SetActive(false);
        }                   
    } // OnTriggerEnter
} // Class
