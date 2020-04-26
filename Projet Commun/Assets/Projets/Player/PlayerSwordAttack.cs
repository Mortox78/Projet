using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordAttack : MonoBehaviour
{
    [SerializeField] private float attackSpeed = 1f;

    private float attackTimer = 0f;
    private bool  isAttack = false;
    private BoxCollider2D attackCollider = null;

    // Start is called before the first frame update
    void Start()
    {
        attackCollider = GetComponent<BoxCollider2D>();
        if (attackCollider != null)
        {
            attackCollider.isTrigger = true;
            attackCollider.enabled = false;
        }
        else
        {
            Debug.LogError("attackCollider == null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttack && Input.GetKeyDown(KeyCode.Mouse0))
        {
            isAttack = true;
            attackCollider.enabled = true;
        }
        if (isAttack)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackSpeed)
            {
                attackTimer = 0f;
                isAttack = false;
                attackCollider.enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
    }
}
