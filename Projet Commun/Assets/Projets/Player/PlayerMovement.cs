using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 1f;
    [SerializeField] private float jumpPower = 1f;
    [SerializeField] private float gravitation = 1f;


    private float rayCastHitDistance = 1.1f;
    private bool isJump = false;
    private bool isFall = false;
    private Rigidbody2D rb = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Assert our variables are > 0
        if (horizontalSpeed <= 0f || jumpPower <= 0f || gravitation <= 0f)
            Debug.LogError("ERROR : some variables are <= 0f");

        // Horizontal movement
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(0f, rb.velocity.y) + Vector2.left * horizontalSpeed;

            // Change player's direction
            if (transform.localScale.x > 0f)
                transform.localScale = new Vector3(-1f * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(0f, rb.velocity.y) + Vector2.right * horizontalSpeed;

            // Change player's direction
            if (transform.localScale.x < 0f)
                transform.localScale = new Vector3(-1f * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        // Vertical movement
        if (!isJump && Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
            rb.velocity += new Vector2(0f, jumpPower);
        }
        else if (isJump)
        {
            rb.velocity -= new Vector2(0f, gravitation);
            if (!isFall && rb.velocity.y <= 0) 
                isFall = true;
        }

        // Ground check with raycast2D
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, rayCastHitDistance, layerMask);

        if (hit && isFall)
        {
            Debug.DrawRay(transform.position, -Vector2.up * hit.distance, Color.blue, 3f);
            //Debug.Log("Hit the gameObject : " + hit.collider.name + "  AND OF COURSE DISTANCE : " + hit.distance);
            isFall = false;
            isJump = false;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.up) * 1000, Color.red);
            //Debug.Log("Did not Hit");
        }
    }
}
