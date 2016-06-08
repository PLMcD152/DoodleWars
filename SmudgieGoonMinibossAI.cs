using UnityEngine;
using System.Collections;

public class SmudgieGoonMinibossAI : MonoBehaviour {

	private float Speed = 5f;
	private bool walkingRight;
	private Animator anim;
	private Direction spriteDirection;
	private SmudgieGoonMinibossHealth eh;
	private float ignoreTimer;
	private bool triggerStop = false;
    public float distance;
    public LayerMask mask;

    void Start ()
	{
		eh = GetComponent<SmudgieGoonMinibossHealth> ();
		spriteDirection = Direction.Right;
		anim = GetComponent<Animator> ();
		if (spriteDirection == Direction.Right)
		{
			walkingRight = true;
		}
		else if (spriteDirection == Direction.Left)
		{
			walkingRight = false;
		}
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("EnemyFlipper"))
        {
            Flip();
        }

        if (coll.gameObject.CompareTag("EnemyFlip"))
        {
            Flip();
        }

        if (coll.gameObject.CompareTag("GoonMiniBoss"))
        {
            if (coll.gameObject.GetComponent<Rigidbody2D>().velocity.x == GetComponent<Rigidbody2D>().velocity.x)
            {
                Flip();
                Physics2D.IgnoreLayerCollision(24, 25, true);
                ignoreTimer = 1;
            }

            else if (coll.gameObject.GetComponent<Rigidbody2D>().velocity.x > GetComponent<Rigidbody2D>().velocity.x)
            {
                Flip();
                Physics2D.IgnoreLayerCollision(24, 25, true);
                ignoreTimer = 1;
            }

            else if (coll.gameObject.GetComponent<Rigidbody2D>().velocity.x < GetComponent<Rigidbody2D>().velocity.x)
            {
                Flip();
                Physics2D.IgnoreLayerCollision(24, 25, true);
                ignoreTimer = 1;
            }
        }

        if (coll.gameObject.CompareTag("Enemy"))
        {
            if (coll.gameObject.GetComponent<Rigidbody2D>().velocity.x == GetComponent<Rigidbody2D>().velocity.x)
            {
                Flip();
                Physics2D.IgnoreLayerCollision(24, 25, true);
                ignoreTimer = 1;
            }

            else if (coll.gameObject.GetComponent<Rigidbody2D>().velocity.x > GetComponent<Rigidbody2D>().velocity.x)
            {
                Flip();
                Physics2D.IgnoreLayerCollision(24, 25, true);
                ignoreTimer = 1;
            }

            else if (coll.gameObject.GetComponent<Rigidbody2D>().velocity.x < GetComponent<Rigidbody2D>().velocity.x)
            {
                Flip();
                Physics2D.IgnoreLayerCollision(24, 25, true);
                ignoreTimer = 1;
            }
        }

        if (coll.gameObject.CompareTag("Ninja"))
        {
            Flip();
        }
    }

    void Update()
    {
        if (ignoreTimer > 0f)
        {
            ignoreTimer -= Time.deltaTime;
        }

        if (ignoreTimer < 0f)
        {
            ignoreTimer = 0f;
        }

        if (ignoreTimer == 0f)
        {
            Physics2D.IgnoreLayerCollision(24, 25, false);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("EnemyFlip"))
        {
            Flip();
        }

        if (col.gameObject.CompareTag("Enemy"))
        {
            if (col.gameObject.GetComponent<Rigidbody2D>().velocity.x == GetComponent<Rigidbody2D>().velocity.x)
            {
                Flip();
                Physics2D.IgnoreLayerCollision(24, 25, true);
                ignoreTimer = 1;
            }

            else if (col.gameObject.GetComponent<Rigidbody2D>().velocity.x > GetComponent<Rigidbody2D>().velocity.x)
            {
                Flip();
                Physics2D.IgnoreLayerCollision(24, 25, true);
                ignoreTimer = 1;
            }

            else if (col.gameObject.GetComponent<Rigidbody2D>().velocity.x < GetComponent<Rigidbody2D>().velocity.x)
            {
                Flip();
                Physics2D.IgnoreLayerCollision(24, 25, true);
                ignoreTimer = 1;
            }
        }

        if (col.gameObject.CompareTag("Ninja"))
        {
            Flip();
        }

        if (col.gameObject.CompareTag("Boss1"))
        {
            Flip();
        }

        if (col.gameObject.CompareTag("Boss2"))
        {
            Flip();
        }

        if (col.gameObject.CompareTag("Rex"))
        {
            Flip();
        }

        if (col.gameObject.CompareTag("King"))
        {
            Flip();
        }

        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerHealth>().c = gameObject;
            col.gameObject.GetComponent<PlayerHealth>().GoonDamage();
            Flip();
        }
    }

    void FixedUpdate()
	{
        if (!eh.hit && !eh.ranged)
		{
            anim.SetBool("HitBool", false);
            if (walkingRight)
			{
				GetComponent<Rigidbody2D>().velocity = new Vector2 (Speed, GetComponent<Rigidbody2D>().velocity.y);
				anim.SetBool ("walking", true);
                Vector2 character = new Vector2(transform.position.x + 1, transform.position.y);
                RaycastHit2D hit = Physics2D.Raycast(character, Vector2.right, 5, mask);
                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        distance = Mathf.Abs(hit.point.x - transform.position.x);
                        if (distance < 1)
                        {
                            Flip();
                        }
                    }

                    else if (hit.collider.CompareTag("GoonMiniBoss"))
                    {
                        distance = Mathf.Abs(hit.point.x - transform.position.x);
                        if (distance < 1)
                        {
                            Flip();
                        }
                    }
                }
            } 
			else if (!walkingRight)
			{
				GetComponent<Rigidbody2D>().velocity = new Vector2 (-Speed, GetComponent<Rigidbody2D>().velocity.y);
				anim.SetBool ("walking", true);
                Vector2 character = new Vector2(transform.position.x - 1, transform.position.y);
                RaycastHit2D hit = Physics2D.Raycast(character, Vector2.left, 5, mask);
                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        distance = Mathf.Abs(hit.point.x - transform.position.x);
                        if (distance < 1)
                        {
                            Flip();
                        }
                    }

                    else if (hit.collider.CompareTag("GoonMiniBoss"))
                    {
                        distance = Mathf.Abs(hit.point.x - transform.position.x);
                        if (distance < 1)
                        {
                            Flip();
                        }
                    }
                }
            }
		}

        else if (eh.hit && !eh.ranged)
        {
            anim.SetBool("walking", false);
            anim.SetBool("HitBool", true);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, GetComponent<Rigidbody2D>().velocity.y);
        }

        else if (eh.ranged && !eh.hit)
        {
            anim.SetBool("walking", false);
            anim.SetBool("HitBool", true);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    void Flip()
	{
		walkingRight = !walkingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}