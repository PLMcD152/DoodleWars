using UnityEngine;
using System.Collections;

public class NinjaAi : MonoBehaviour {
	
	private float speed = 1f;
	private float backSpeed = 2f;
	private float maxSpeed = 5f;
	private bool walkingRight = true;
	private Animator anim;
	private Transform target;
	private float distance;
	private float ydistance;
	private int jumpHeight = 8;
	private int jumpForward = 4;
	[HideInInspector]
	public bool grounded = false;
	[HideInInspector]
	public float groundRadius = 1f;
	[HideInInspector]
	public Transform groundCheck;
	public LayerMask groundLayer;
	[HideInInspector]
	public Collider2D groundCollider;
	private NinjaHealth eh;
	private AudioClip ninjaJump;
	private bool triggerStop = false;
	private int jumps = 0;
	private float attackTimer;
	private bool attack = false;
	private GameObject log;
    public Collider2D[] colls;

    void Start ()
	{
		eh = GetComponent<NinjaHealth> ();
		anim = GetComponent<Animator> ();
		target = GameObject.FindGameObjectWithTag("Player").transform;
		groundCheck = transform.Find("groundCheck");
		ninjaJump = (Resources.Load ("Ninja/Sounds/NinjaJump")) as AudioClip;
		log = (Resources.Load ("Ninja/Log"))as GameObject;
        colls = GetComponents<Collider2D>();
    }

	void Update () 
	{
		groundCollider = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

		if (groundCollider == null)
		{
			grounded = false;
		}

		else
		{
			grounded = true;
		}

		if (attackTimer > 0f)
		{
			attackTimer -= Time.deltaTime;
		}

		if (attackTimer < 0f)
		{
			attackTimer = 0f;
		}

		if (jumps == 2)
		{
			jumps = 0;
		}

		distance = target.transform.position.x - transform.position.x;
		ydistance = target.transform.position.y - transform.position.y;

		if(distance < 0)
		{
			distance *= -1;
		}

		if(ydistance < 0)
		{
			ydistance*= -1;
		}

		if (target.transform.position.x > transform.position.x && !walkingRight)
		{
			Flip();
			walkingRight = true;
		}

		else if (target.transform.position.x < transform.position.x && walkingRight)
		{
			Flip();
			walkingRight = false;
		}
	}

    void OnCollider2DEnter(Collider2D col)
    {
        if (col.gameObject.CompareTag("Thru"))
        {
            foreach (Collider2D i in colls)
            {
                Physics2D.IgnoreCollision(i, col);
            }
        }
    }

	void FixedUpdate()
	{
		if (!eh.ranged && !eh.hit)
		{
			anim.SetBool("Stunned", false);
            anim.SetBool("Hit", false);
            if (distance < 5 && jumps == 0 && grounded)
			{
				if(target.transform.position.x < transform.position.x && ydistance < 0.5)
				{
					if(!triggerStop)
					{
						anim.SetTrigger("Jump");
						triggerStop = true;
					}
					GetComponent<AudioSource>().PlayOneShot(ninjaJump);
					anim.SetBool("running", false);
					Vector3 v = GetComponent<Rigidbody2D>().velocity;
					v.y = jumpHeight;
					v.x = -jumpForward;
					GetComponent<Rigidbody2D>().velocity = v;
				}
				else if (target.transform.position.x > transform.position.x && ydistance < 0.5)
				{
					if(!triggerStop)
					{
						anim.SetTrigger("Jump");
						triggerStop = true;
					}
					GetComponent<AudioSource>().PlayOneShot(ninjaJump);
					anim.SetBool("running", false);
					Vector3 v = GetComponent<Rigidbody2D>().velocity;
					v.y = jumpHeight;
					v.x = jumpForward;
					GetComponent<Rigidbody2D>().velocity = v;
				}
			}

			else if (jumps == 1 && grounded)
			{
				if(target.transform.position.x < transform.position.x && grounded)
				{
					if(!triggerStop)
					{
						anim.SetTrigger("Attack");
						triggerStop = true;
						attack = true;
					}
					GetComponent<Rigidbody2D>().velocity = new Vector2 (backSpeed, GetComponent<Rigidbody2D>().velocity.y);
				}
				else if (target.transform.position.x > transform.position.x && grounded)
				{
					if(!triggerStop)
					{
						anim.SetTrigger("Attack");
						triggerStop = true;
						attack = true;
					}
					GetComponent<Rigidbody2D>().velocity = new Vector2 (-backSpeed, GetComponent<Rigidbody2D>().velocity.y);
				}
			}

			else if (distance < 7.5 && !attack && attackTimer > 0f && jumps == 0 && grounded)
			{
				if (target.transform.position.x < transform.position.x)
				{
					GetComponent<Rigidbody2D>().velocity = new Vector2 (-maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
					anim.SetBool("walking", false);
					anim.SetBool("running", true);
				}
				else if (target.transform.position.x > transform.position.x)
				{
					GetComponent<Rigidbody2D>().velocity = new Vector2 (maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
					anim.SetBool("walking", false);
					anim.SetBool("running", true);
				}
			}

			else if (distance < 7.5 && distance > 5 && !attack && attackTimer == 0f && jumps == 0 && grounded)
			{
				if (target.transform.position.x < transform.position.x)
				{
					GetComponent<Rigidbody2D>().velocity = new Vector2 (-maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
					anim.SetBool("walking", false);
					anim.SetBool("running", true);
				}
				else if (target.transform.position.x > transform.position.x)
				{
					GetComponent<Rigidbody2D>().velocity = new Vector2 (maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
					anim.SetBool("walking", false);
					anim.SetBool("running", true);
				}
			}

			else if (eh.health == 0f)
			{
				GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 0);
				anim.SetBool("walking", false);
				anim.SetBool("running", false);
				anim.SetBool("Stunned", false);
			}

			else
			{
				if (grounded)
				{
					Patrol ();
				}
			}
		}

		else if (eh.ranged && !eh.hit)
		{
			anim.SetBool("walking", false);
			anim.SetBool("running", false);
            anim.SetBool("Hit", false);
			anim.SetBool("Stunned", true);
			GetComponent<Rigidbody2D>().velocity = new Vector2 (GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y);
		}
		else if (eh.hit && !eh.ranged)
		{
			anim.SetBool("walking", false);
			anim.SetBool("running", false);
			anim.SetBool("Stunned", false);
            anim.SetBool("Hit", true);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, GetComponent<Rigidbody2D>().velocity.y);
        }
	}

	void Patrol()
	{
        if (!eh.ranged && !eh.hit)
        {
            if (distance > 7.5 && target.transform.position.x > transform.position.x)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
                anim.SetBool("walking", true);

            }
            else if (distance > 7.5 && target.transform.position.x < transform.position.x)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
                anim.SetBool("walking", true);
            }
        }
	}

	public void StopJump()
	{
		jumps++;
		attackTimer = 2;
		triggerStop = false;
		anim.SetBool ("running", true);
	}

	public void StopAttack()
	{
		jumps++;
		triggerStop = false;
		attack = false;
		anim.SetBool ("running", true);
	}

	public void Log()
	{
		GameObject newLogObject = (GameObject)Instantiate (log);
		newLogObject.transform.position = transform.position;
	}

	void Flip()
	{
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
