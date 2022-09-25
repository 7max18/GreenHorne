using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private float horizontal;
    private float vertical;

    public float walkSpeed;
    public float runSpeed;
    private float moveMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.Z))
        {
            moveMultiplier = runSpeed;
        }
        else
        {
            moveMultiplier = walkSpeed;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal*moveMultiplier, vertical*moveMultiplier);
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
    }
}
