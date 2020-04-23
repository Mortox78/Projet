using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float walkSpeed;
    public float jumpForce;
    public AnimationClip walkAnimClip, jumpAnimClip;
    public Animation legsAnim;
    public Transform groundCast;
    public Camera cam;


    private bool canJump, canWalk;
    private bool isWalk, isJump;
    private bool lookingLeft = false;
    private Rigidbody2D rig;
    private Vector2 inputAxis;
    private RaycastHit2D hit;


    void Start ()
    {
        rig = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (hit = Physics2D.Linecast(new Vector2(groundCast.position.x, groundCast.position.y + 0.2f), groundCast.position))
        {
            if (!hit.transform.CompareTag("Player") && !hit.transform.CompareTag("Tree"))
            {
                canJump = true;
                canWalk = true;
            }
        }
        else canJump = false;

        inputAxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (inputAxis.y > 0 && canJump)
        {
            canWalk = false;
            isJump = true;
        }
        if (inputAxis.x > 0 && transform.localScale.x != 1f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (inputAxis.x < 0 && transform.localScale.x != -1f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    void FixedUpdate()
    {
        if (inputAxis.x != 0)
        {
            rig.velocity = new Vector2(inputAxis.x * walkSpeed * Time.deltaTime, rig.velocity.y);

            if (canWalk)
            {
                legsAnim.clip = walkAnimClip;
                legsAnim.Play();
            }
        }

        else
        {
            rig.velocity = new Vector2(0, rig.velocity.y);
        }

        if (isJump)
        {
            rig.AddForce(new Vector2(0, jumpForce));
            legsAnim.clip = jumpAnimClip;
            legsAnim.Play();
            canJump = false;
            isJump = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, groundCast.position);
    }
}
