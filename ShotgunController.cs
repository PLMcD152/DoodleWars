using UnityEngine;
using System.Collections;

public class ShotgunController : MonoBehaviour {

	// Use this for initialization

	[HideInInspector]
	public GameObject playerObject;
	private float bulletSpeed = 15.0f;
	private float selfDestructTimer = 0.0f;
	bool facingRight = true;

	void Start()
	{
		playerObject = GameObject.Find ("MrDoodle");
	}

	public void ShotgunShoot(){
		float mainXScale = playerObject.transform.localScale.x;
		Vector2 bulletForce;
		if(mainXScale< 0.0f)
		{
			bulletForce = new Vector2(bulletSpeed * -1.0f,0.0f);
			Flip ();
		}
		else
		{
			bulletForce = new Vector2(bulletSpeed, 0.0f);
		}
		GetComponent<Rigidbody2D>().velocity = bulletForce;
		selfDestructTimer = Time.time + 0.1f;

		}

	
	// Update is called once per frame
	void Update () 
	{
		if(selfDestructTimer> 0.0f)
		{
			if(selfDestructTimer<Time.time)
			{
				Destroy(gameObject);
			}
		}
	}

	void Flip(){
		facingRight = !facingRight;
		Vector3 theRotation = transform.localEulerAngles;
		theRotation.y *= -1;
		transform.localEulerAngles = theRotation;
		}
}
