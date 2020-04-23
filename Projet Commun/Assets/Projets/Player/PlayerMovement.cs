using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 1f;
    [SerializeField] private float verticalSpeed = 1f;
    [SerializeField] private float jumpTime = 0.5f;


    private bool isJump = false;
    private float jumpTimer = 0f;
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
            //rb.velocity = new Vector2(0f, rb.velocity.y) + Vector2.up * verticalSpeed;
            rb.velocity += Vector2.up * verticalSpeed;
        }

        if (isJump)
        {
            jumpTimer += Time.deltaTime;

            if (jumpTimer >= jumpTime)
            {
                Debug.Log("if if");
                jumpTimer = 0f;
                isJump = false;
                rb.velocity = new Vector2(rb.velocity.x, 0f);
            }
        }
    }

    private void FixedUpdate()
    {

    }
}
