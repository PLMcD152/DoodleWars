using UnityEngine;
using System.Collections;

public class DinoScript : MonoBehaviour {
	
	private float Speed = 7f;
	public bool walkingRight = true;
	private Animator anim;
	public Transform target;
	public Transform offset;
	public float distance;
	public float attackTimer = 4f;
	private DinoHealthScript dinoHealth;
	private DinoRiderScript dinoRider;
	public bool patrolable = true;
	public int attackChecks = 1;
	public bool animating;
	private Player player;
	public bool notAttacking;
	public bool attacking;
	private GameObject explosion;
	private GameObject rex;
	public bool hurt = false;
	public bool triggerStop = false;
	public bool hurtTriggerStop = false;
	private Transform exitDoor;
	private GameObject levelbackport;
	public bool set = false;
    private GameObject bar;
    private GameObject bar2;
    private float dashForce = 5000f;
    private Collider2D[] cols;
    private Collider2D[] colls;
    public bool Attack1Hurt = false;
    public bool Attack2Hurt = false;

    void Start ()
	{
		anim = GetComponent<Animator> ();
		target = GameObject.Find("MrDoodle").transform;
		player = target.GetComponent<Player> ();
		dinoHealth = GetComponentInChildren<DinoHealthScript> ();
		dinoRider = GetComponentInChildren<DinoRiderScript> ();
		offset = GameObject.FindGameObjectWithTag("BossOffset").transform;
		explosion = (Resources.Load ("BossSoldier/BossSoldierExplosion"))as GameObject;
		rex = GameObject.Find ("TrexBody");
		levelbackport = (Resources.Load ("Dino/DinoBack"))as GameObject;
		exitDoor = GameObject.Find ("ExitDoorSpawn").transform;
        cols = GetComponentsInChildren<Collider2D>();
        colls = GetComponents<Collider2D>();
        anim.SetTrigger("Spawning");
		animating = true;
        bar = GameObject.Find("RexBar(Clone)");
        bar2 = GameObject.Find("RdrBar(Clone)");
    }

	void Update () 
	{
        if(animating)
        {
            GameObject.Find("CF-Complete-Physics-Platformer").GetComponent<TouchController>().b = true;
            GameObject.Find("CF-Complete-Physics-Platformer").GetComponent<TouchController>().a = true;
            bar.SetActive(false);
            bar2.SetActive(false);
        }

		if(!animating && !hurt)
		{
			if (attackTimer < 0f)
			{
				attackTimer = 0f;
			}

			if (attackChecks == 6)
			{
				attackChecks = 1;
			}

			if (attackTimer > 0f && distance < 5f)
			{
				attackTimer -= Time.deltaTime;
			}
	
			if (attackTimer == 0 && patrolable)
			{
				notAttacking = true;
			}

            anim.SetFloat("xSpeed",  Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));

			distance = target.transform.position.x - transform.position.x;

			if(target.transform.position.x > offset.transform.position.x && !walkingRight && patrolable && !notAttacking)
			{
				StartCoroutine(Flip ());
				walkingRight = true;
			}
		
			else if(target.transform.position.x < offset.transform.position.x && walkingRight && patrolable && !notAttacking)
			{
				StartCoroutine(Flip ());
				walkingRight = false;
			}

			if(distance < 0f)
			{
				distance *= -1f;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (attackChecks > 3 && attackChecks < 5)
		{
			if(col.gameObject.CompareTag ("EnemyFlip"))
			{
				Hurt();
			}
		}
	}

	void FixedUpdate()
	{
		if(!animating && !hurt)
		{
			if (distance > 3f && patrolable)
			{
				if (dinoRider.health >= 5f && dinoHealth.health >= 5f)
				{
					if (target.transform.position.x > transform.position.x && walkingRight && patrolable && !attacking)
					{
						GetComponent<Rigidbody2D>().velocity = new Vector2 (Speed, GetComponent<Rigidbody2D>().velocity.y);
						anim.SetBool("BossWalk", true);
						anim.SetBool("BossIdle", false);
					}

					else if (target.transform.position.x < transform.position.x && !walkingRight && patrolable && !attacking)
					{
						GetComponent<Rigidbody2D>().velocity = new Vector2 (-Speed, GetComponent<Rigidbody2D>().velocity.y);
						anim.SetBool("BossWalk", true);
						anim.SetBool("BossIdle", false);
					}
				}
			}

			if (distance < 3f && attackTimer > 0f && !attacking)
			{
				if (dinoRider.health >= 5f && dinoHealth.health >= 5f)
				{
					anim.SetBool("BossIdle", true);
					anim.SetBool("BossWalk", false);
					GetComponent<Rigidbody2D>().velocity = new Vector2 (GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y);
				}
			}

			if (distance <= 3 && attackTimer == 0 && attackChecks == 1 || attackChecks == 2 || attackChecks == 3)
			{
				if(notAttacking)
				{
					if (dinoRider.health >= 5f && dinoHealth.health >= 5f)
					{
						if(!triggerStop)
						{
							anim.SetTrigger ("BossAttack1");
							triggerStop = true;
						}
						attacking = true;
						anim.SetBool("BossWalk", false);
						anim.SetBool("BossIdle", false);
						GetComponent<Rigidbody2D>().velocity = new Vector2 (GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y);
					}
				}
			}

			if (attackChecks == 4)
			{
				if(notAttacking)
				{
					if (dinoRider.health >= 5f && dinoHealth.health >= 5f)
					{
						anim.SetBool("BossWalk", false);
						anim.SetBool("BossIdle", false);
						if(!triggerStop)
						{
							anim.SetTrigger ("BossAttack2");
							triggerStop = true;
						}
						attacking = true;
						GetComponent<Rigidbody2D>().velocity = new Vector2 (GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y);
					}
				}
			}

			if (attackChecks == 5)
			{
				if (notAttacking)
				{
					if (dinoRider.health >= 5f && dinoHealth.health >= 5f)
					{
						if(!triggerStop)
						{
							anim.SetTrigger ("BossYawn");
							triggerStop = true;
						}
						attacking = true;
						anim.SetBool("BossWalk", false);
						anim.SetBool("BossIdle", false);
						GetComponent<Rigidbody2D>().velocity = new Vector2 (GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y);
					}
				}
			}
		}
	}

	public void ExplodeRider()
	{
		dinoRider.RiderExplode ();
	}

	public void Hurt()
	{
		triggerStop = false;
		hurt = true;
		anim.SetBool("BossWalk", false);
		anim.SetBool("BossIdle", false);
		GetComponent<Rigidbody2D>().velocity = new Vector2 (GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y);
		if(!hurtTriggerStop)
		{
			anim.SetTrigger ("BossHurt");
			hurtTriggerStop = true;
		}
	}
	
	IEnumerator Flip()
	{
		patrolable = false;
		anim.SetBool("BossWalk", false);
		anim.SetBool("BossIdle", true);
		yield return new WaitForSeconds (1.5f);
		//walkingRight = !walkingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
        TrexTail tTail = GameObject.Find("TrexTailPlat").GetComponent<TrexTail>();
        tTail.iScale*= -1;
        TrexBody tBody = GameObject.Find("TrexBodyPlat").GetComponent<TrexBody>();
        tBody.iScale *= -1;
        transform.localScale = theScale;
		patrolable = true;
		yield break;
	}

	public void DIE()
	{
		if(!set)
		{
			anim.SetTrigger("BossDeath");
            foreach (Collider2D c in cols)
            {
                c.tag = "Untagged";
            }
            foreach (Collider2D d in colls)
            {
                d.tag = "Untagged";
            }
            set = true;
		}
		attacking = true;
		anim.SetBool("BossWalk", false);
		anim.SetBool("BossIdle", false);
		GetComponent<Rigidbody2D>().velocity = new Vector2 (GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y);
	}

	public void Explode()
	{
		Instantiate(explosion, rex.transform.position, rex.transform.rotation);
		Instantiate (levelbackport, exitDoor.position, exitDoor.rotation);
        PlayerPrefs.SetInt("Dino", 1);
		Destroy (GameObject.Find ("DinoBoss"));
	}

	public void StopSpawnAnim()
	{
		anim.SetBool("BossWalk", true);
		animating = false;
        GameObject.Find("CF-Complete-Physics-Platformer").GetComponent<TouchController>().b = false;
        GameObject.Find("CF-Complete-Physics-Platformer").GetComponent<TouchController>().a = false;
        bar.SetActive(true);
        bar2.SetActive(true);
        player.enabled = true;
		player.stop = false;
        player.Stop();
	}

	public void StopBossAttack1()
	{
		attacking = false;
		triggerStop = false;
		notAttacking = false;
		attackTimer = 3f;
		attackChecks ++;
	}

	public void StopBossAttack2()
	{
		attacking = false;
		triggerStop = false;
		notAttacking = false;
		attackTimer = 3f;
		attackChecks ++;
	}

	public void StopBossYawn()
	{
		attacking = false;
		triggerStop = false;
		notAttacking = false;
		attackTimer = 3f;
		attackChecks ++;
	}

	public void StopHurt()
	{
		attacking = false;
		hurt = false;
		hurtTriggerStop = false;
		notAttacking = false;
		attackTimer = 3f;
		attackChecks ++;
	}

    public void Attack1DamageYes()
    {
        Attack1Hurt = true;
    }

    public void Attack1DamageNo()
    {
        Attack1Hurt = false;
    }

    public void Attack2DamageYes()
    {
        Attack2Hurt = true;
    }

    public void Attack2DamageNo()
    {
        Attack2Hurt = false;
    }

    public void Go()
	{
		if(!walkingRight)
		{
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-dashForce, 10.0f));
        }
		
		else if (walkingRight)
		{
            GetComponent<Rigidbody2D>().AddForce(new Vector2(dashForce, 10.0f));
        }
	}
}