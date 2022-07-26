using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondController : MonoBehaviour
{
    private float maxYPosition = 0.99f, minYPosition = 0.55f;
    private bool upWard = true;
    [SerializeField]
    private float movementSpeed = 0.01f;
    [SerializeField]
    private float rotateSpeed = 0.01f;
    private bool isCollided = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (upWard)
            transform.position += new Vector3(0, movementSpeed, 0);
        else
            transform.position -= new Vector3(0, movementSpeed, 0);

        if(transform.position.y > maxYPosition)
            upWard = false;
        if (transform.position.y < minYPosition)
            upWard = true;

        transform.Rotate(Vector3.up, rotateSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Helper.Tags.player) && !isCollided)
        {
            isCollided = true;
            GameManager.instance.IncreaseStackAmount(1, true);
            gameObject.SetActive(false);
        }
    } // OnTriggerEnter
}
