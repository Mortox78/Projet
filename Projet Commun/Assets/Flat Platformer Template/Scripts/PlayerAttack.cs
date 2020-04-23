using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerAttack : MonoBehaviour
{
    public float attackSpeed = 0.1f;
    public AnimationClip attackAnimClip = null;
    public Animation attackAnim = null;

    private bool canAttack = true;
    private float attackSpeedTimer = 0f;
    private BoxCollider2D hitRange = null;
    private SpriteRenderer hitRangeSprite = null;

    // Start is called before the first frame update
    void Start()
    {
        hitRange = GetComponent<BoxCollider2D>();
        hitRangeSprite = GetComponent<SpriteRenderer>();
        hitRange.enabled = false;
        hitRangeSprite.enabled = false;
        attackAnim.clip = attackAnimClip;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canAttack)
        {
            attackSpeedTimer += Time.deltaTime;
            if (attackSpeedTimer >= attackSpeed)
            {
                canAttack = true;
                attackSpeedTimer = 0f;
                hitRange.enabled = false;
                hitRangeSprite.enabled = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            canAttack = false;
            hitRange.enabled = true;
            hitRangeSprite.enabled = true;
            attackAnim.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tree"))
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawCube(hitRange.transform.position, Vector3.one);
    }
}
