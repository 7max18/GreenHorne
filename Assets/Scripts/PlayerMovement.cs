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

    private bool inInteractRange;

    public DisplayDialogue dialogueBox;
    private NPC talkingTo;
    private bool talking;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dialogueBox.gameObject.SetActive(false);
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

        if (Input.GetKeyDown(KeyCode.X) && inInteractRange)
        {
            if (talkingTo != null)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePosition;
                rb.velocity = Vector2.zero;
                dialogueBox.gameObject.SetActive(true);
                dialogueBox.speaker = talkingTo;
                talking = dialogueBox.Display();

                if(!talking)
                {
                    rb.constraints = RigidbodyConstraints2D.None;
                    dialogueBox.gameObject.SetActive(false);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal*moveMultiplier, vertical*moveMultiplier);
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC")) //Add if else for other interactables
        {
            inInteractRange = true;
            talkingTo = collision.gameObject.GetComponent<NPC>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            inInteractRange = false;
            talkingTo = null;
        }
    }
}
