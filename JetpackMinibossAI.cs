using UnityEngine;
using System.Collections;

public class JetpackMinibossAI : MonoBehaviour {

	float xspeed = 7.0f;
	float stop = 0.0f;
	private Transform target;
	public bool direction = true;
	public float distance;
	public float yDistance;
	private JetpackMinibossHealth eh;
	private Animator anim;
	[HideInInspector]
	public bool triggerStop = false;
	private bool flewBack = false;
    public float lerpMoving = 0;
    [HideInInspector]
    public Player p;

    void  Start ()
	{
		anim = GetComponent<Animator> ();
		eh = GetComponent<JetpackMinibossHealth> ();
		target = GameObject.FindGameObjectWithTag("Player").transform;
        p = GameObject.Find("MrDoodle").GetComponent<Player>();
    }
	

	void  Update ()
	{
		distance = target.transform.position.x - transform.position.x;
		yDistance = target.transform.position.y - transform.position.y;
		
		if(distance < 0f)
		{
			distance *= -1f;
		}

        if (!eh.hit && !eh.ranged)
        {
            anim.SetBool("Hit", false);
            anim.SetBool("Stunned", false);
        }

        if (target.transform.position.x > transform.position.x && direction == false)
		{
			Flip();
		}
		
		else if(target.transform.position.x < transform.position.x && direction == true)
		{
			Flip();
		}
	}
	
	void FixedUpdate()
	{
        if (!eh.hit && !eh.ranged && !p.falling && eh.health > 5f)
        {
            if (yDistance < 3 && yDistance > 1)
            {
                lerpMoving = 0;
                transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(Time.time * 6) / 15, transform.position.z);
            }

            if (yDistance > 3 && distance < 10f)
            {
                lerpMoving += Time.deltaTime * 0.1f;
                transform.position = new Vector3(transform.position.x, transform.position.y - lerpMoving, transform.position.z);
            }

            if (yDistance < 1 && distance < 10f)
            {
                lerpMoving += Time.deltaTime * 0.1f;
                transform.position = new Vector3(transform.position.x, transform.position.y + lerpMoving, transform.position.z);
            }

            else if (distance > 10f && yDistance > 3)
            {
                lerpMoving += Time.deltaTime * 0.1f;
                transform.position = new Vector3(transform.position.x, transform.position.y - lerpMoving, transform.position.z);
            }

            else if (distance > 10f && yDistance < 1)
            {
                lerpMoving += Time.deltaTime * 0.1f;
                transform.position = new Vector3(transform.position.x, transform.position.y + lerpMoving, transform.position.z);
            }

            if (distance < 10f && distance > 3f)
            {
                flewBack = false;
                if (!direction)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(-xspeed, 0);
                    anim.SetBool("Idle", false);
                    anim.SetBool("MoveF", true);
                    anim.SetBool("MoveB", false);
                }
                else if (direction)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(xspeed, 0);
                    anim.SetBool("Idle", false);
                    anim.SetBool("MoveF", true);
                    anim.SetBool("MoveB", false);
                }
            }

            else if (distance > 10f && yDistance < 3 && yDistance > 1)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(stop, 0);
                anim.SetBool("Idle", true);
                anim.SetBool("MoveF", false);
                anim.SetBool("MoveB", false);
                flewBack = false;
            }

            else if (distance < 3f && distance > 2f && target.GetComponent<Rigidbody2D>().velocity.x < 0.5f && target.GetComponent<Rigidbody2D>().velocity.x > -1f)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(stop, 0);
                anim.SetBool("Idle", true);
                anim.SetBool("MoveF", false);
                anim.SetBool("MoveB", false);
                flewBack = false;
            }

            else if (distance < 3f && distance > 2f && target.GetComponent<Rigidbody2D>().velocity.x > 0.5f)
            {
                flewBack = false;
                if (!direction)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(-xspeed, 0);
                    anim.SetBool("Idle", false);
                    anim.SetBool("MoveF", true);
                    anim.SetBool("MoveB", false);
                }
                else if (direction)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(xspeed, 0);
                    anim.SetBool("Idle", false);
                    anim.SetBool("MoveF", true);
                    anim.SetBool("MoveB", false);
                }
            }

            else if (distance < 3f && distance > 2f && target.GetComponent<Rigidbody2D>().velocity.x < -1f)
            {
                flewBack = false;
                if (!direction)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(-xspeed, 0);
                    anim.SetBool("Idle", false);
                    anim.SetBool("MoveF", true);
                    anim.SetBool("MoveB", false);
                }
                else if (direction)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(xspeed, 0);
                    anim.SetBool("Idle", false);
                    anim.SetBool("MoveF", true);
                    anim.SetBool("MoveB", false);
                }
            }

            else if (distance < 2f)
            {
                if (!direction && !flewBack)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(xspeed, 0);
                    anim.SetBool("Idle", false);
                    anim.SetBool("MoveF", false);
                    anim.SetBool("MoveB", true);
                }

                else if (direction && !flewBack)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(-xspeed, 0);
                    anim.SetBool("Idle", false);
                    anim.SetBool("MoveF", false);
                    anim.SetBool("MoveB", true);
                }

                else if (!direction && flewBack)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(-xspeed, 0);
                    anim.SetBool("Idle", false);
                    anim.SetBool("MoveF", true);
                    anim.SetBool("MoveB", false);
                }

                else if (direction && flewBack)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(xspeed, 0);
                    anim.SetBool("Idle", false);
                    anim.SetBool("MoveF", true);
                    anim.SetBool("MoveB", false);
                }
            }
        }

        if (eh.ranged && !eh.hit)
        {
            anim.SetBool("Idle", false);
            anim.SetBool("MoveF", false);
            anim.SetBool("MoveB", false);
            anim.SetBool("Hit", false);
            anim.SetBool("Stunned", true);
            GetComponent<Rigidbody2D>().velocity = new Vector2(stop, 0);
        }

        if (eh.hit && !eh.ranged)
        {
            anim.SetBool("Idle", false);
            anim.SetBool("MoveF", false);
            anim.SetBool("MoveB", false);
            anim.SetBool("Stunned", false);
            anim.SetBool("Hit", true);
            GetComponent<Rigidbody2D>().velocity = new Vector2(stop, 0);
        }
    }

    void Flip()
	{
		flewBack = !flewBack;
		direction = !direction;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}