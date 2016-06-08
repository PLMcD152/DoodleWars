using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GunnerAi : MonoBehaviour {

	public float attackTimer = 2f;
	private Animator anim;
	public float distance = 0.0f;
	private float ydistance = 0.0f;
	public bool shooting = false;
	public float Speed = 3f;
	private bool walkingRight = true;
	private Shooter shooter;
	private GunnerHealth eh;
	private GameObject target;

	void Start () 
	{
		anim = GetComponent<Animator> ();
		shooter = GetComponentInChildren<Shooter> ();
		eh = GetComponent<GunnerHealth> ();
		target = GameObject.Find ("MrDoodle");
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.CompareTag ("EnemyFlipper"))
		{
			Flip();
		}
	}

	void Update ()
	{
		Transform target = GameObject.FindGameObjectWithTag ("Player").transform;
		distance = target.transform.position.x - transform.position.x;
		ydistance = target.transform.position.y - transform.position.y;

		if (distance < 0)
        {
			distance *= -1;
		}
		
		if (ydistance < 0)
        {
			ydistance *= -1;
		}

		if(target.transform.position.x > transform.position.x && !walkingRight)
		{
			Flip ();
			walkingRight = true;
		}
		if(target.transform.position.x < transform.position.x && walkingRight)
		{
			Flip ();
			walkingRight = false;
		}
		
		if (attackTimer < 0)
		{
			attackTimer = 0;
		}
	}

	void FixedUpdate()
	{
		if (!eh.ranged && !eh.hit)
		{
            anim.SetBool("Stunned", false);
            anim.SetBool("HitBool", false);
            if (distance < 15 && distance > 5)
			{
				attackTimer = 1f;
				anim.SetBool("Idle", false);
				anim.SetBool("Walk", true);
				anim.SetBool ("ShootG", false);
				shooting = false;
				if(!eh.hit)
				{
					if (target.transform.position.x > transform.position.x && walkingRight)
					{
						GetComponent<Rigidbody2D>().velocity = new Vector2 (Speed, GetComponent<Rigidbody2D>().velocity.y);
					}
					else if (target.transform.position.x < transform.position.x && !walkingRight)
					{
						GetComponent<Rigidbody2D>().velocity = new Vector2 (-Speed, GetComponent<Rigidbody2D>().velocity.y);
					}
				}
			}
		
			else if (distance < 5 && !shooting && attackTimer > 0)
			{
				if (attackTimer > 0)
				{
					attackTimer -= Time.deltaTime;
				}
				anim.SetBool("Idle", true);
				anim.SetBool("Walk", false);
				anim.SetBool ("ShootG", false);
				shooting = false;
				if(!eh.hit)
				{
					GetComponent<Rigidbody2D>().velocity = new Vector2 (0, GetComponent<Rigidbody2D>().velocity.y);
				}
			}
		
			else if (distance < 6 && !shooting && attackTimer == 0 && eh.health > 0f)
			{
				anim.SetBool("Idle", false);
				anim.SetBool("Walk", false);
				anim.SetBool ("ShootG", true);
				if(!eh.hit)
				{
					GetComponent<Rigidbody2D>().velocity = new Vector2 (0, GetComponent<Rigidbody2D>().velocity.y);
				}
				shooting = true;
			}
		}

		else if (eh.hit && !eh.ranged)
		{
			anim.SetBool("Idle", false);
			anim.SetBool("Walk", false);
			anim.SetBool ("ShootG", false);
			anim.SetBool("HitBool", true);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, GetComponent<Rigidbody2D>().velocity.y);
        }

		else if (eh.ranged && !eh.hit)
		{
            anim.SetBool("Idle", false);
            anim.SetBool("Walk", false);
            anim.SetBool("ShootG", false);
            anim.SetBool("HitBool", true);
            GetComponent<Rigidbody2D>().velocity = new Vector2 (0f, GetComponent<Rigidbody2D>().velocity.y);
		}
	}

	public void Shooot()
	{
        if (!eh.ranged && !eh.hit)
        {
            shooter.Shoot();
        }
	}

	public void Stop2()
	{
		anim.SetBool ("ShootG", false);
		shooting = false;
		attackTimer = 1f;
	}	

	void Flip()
	{
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
