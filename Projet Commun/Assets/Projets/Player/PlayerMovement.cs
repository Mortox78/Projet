using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 1f;
    [SerializeField] private float jumpPower = 1f;
    [SerializeField] private float gravitation = 1f;


    private bool isJump = false;
    private Rigidbody2D rb = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(0f, rb.velocity.y) + Vector2.left * horizontalSpeed ;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(0f, rb.velocity.y) + Vector2.right * horizontalSpeed;
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        if (!isJump && Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
            rb.velocity += new Vector2(0f, jumpPower);
        }
        else if (isJump)
        {
            rb.velocity -= new Vector2(0f, gravitation);
        }

        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1.1f, layerMask);

        if (hit)
        {
            Debug.DrawRay(transform.position, -Vector2.up * hit.distance, Color.yellow);
            Debug.Log("Hit the gameObject : " + hit.collider.name + "  AND OF COURSE DISTANCE : " + hit.distance);
            isJump = false;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.up) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }


    }
}
