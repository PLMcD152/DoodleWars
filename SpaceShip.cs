using UnityEngine;
using System.Collections;

public class SpaceShip : MonoBehaviour {

	private int waypointsPassed = 0;
	private float speed = 5f;
	private bool stop = false;
	private bool loop = true;
	private int currentIndex = 0;
	private int stopAfterTotal = 0;
	private bool movingForward = true;
	private GameObject nextWaypoint;
	private Vector2 nextVelocity = new Vector2(0, 0);
	private float nextWaypointTimer;
	private int initialIndex;
	private bool moveForward = true;
	private GameObject parent;
	private GameObject[] waypoints;
	private GameObject shipPoint1;
	private GameObject shipPoint2;
	private GameObject shipPoint3;
	private GameObject shipPoint4;
	private GameObject shipPoint5;
	private GameObject shipPoint6;
	private GameObject shipPoint7;
	private GameObject shipPoint8;
	private GameObject shipPoint9;
	private GameObject shipPoint10;
	private GameObject shipPoint11;
	private GameObject shipPoint12;
	private GameObject shipPoint13;
	private GameObject ship;
	private Animator anim;
	private bool waitAtWaypoint = false;
	private float waitAtWaypointTime = 0.5f;
	private float timer = 0f;
	private bool go;
	public bool tSpawning;
	private Player player;
	private Transform target;
    private GameObject bar;

    void Start ()
	{
		parent = transform.parent.gameObject;
		ship = GameObject.Find("Spaceship");
		anim = ship.GetComponent<Animator> ();
		shipPoint1 = (Resources.Load ("Ship/ShipPoint1"))as GameObject;
		shipPoint2 = (Resources.Load ("Ship/ShipPoint2"))as GameObject;
		shipPoint3 = (Resources.Load ("Ship/ShipPoint3"))as GameObject;
		shipPoint4 = (Resources.Load ("Ship/ShipPoint4"))as GameObject;
		shipPoint5 = (Resources.Load ("Ship/ShipPoint5"))as GameObject;
		shipPoint6 = (Resources.Load ("Ship/ShipPoint6"))as GameObject;
		shipPoint7 = (Resources.Load ("Ship/ShipPoint7"))as GameObject;
		shipPoint8 = (Resources.Load ("Ship/ShipPoint8"))as GameObject;
		shipPoint9 = (Resources.Load ("Ship/ShipPoint9"))as GameObject;
		shipPoint10 = (Resources.Load ("Ship/ShipPoint10"))as GameObject;
		shipPoint11 = (Resources.Load ("Ship/ShipPoint11"))as GameObject;
		shipPoint12 = (Resources.Load ("Ship/ShipPoint12"))as GameObject;
		shipPoint13 = (Resources.Load ("Ship/ShipPoint13"))as GameObject;
        bar = GameObject.Find("ShipBar(Clone)");
        GameObject newShipPoint1 = (GameObject)Instantiate (shipPoint1);
		newShipPoint1.transform.parent = GameObject.Find("Spaceship").transform;
		newShipPoint1.transform.localPosition = new Vector3(-10f,4f,0f);
		GameObject newShipPoint2 = (GameObject)Instantiate (shipPoint2);
		newShipPoint2.transform.parent = GameObject.Find("Spaceship").transform;
		newShipPoint2.transform.localPosition = new Vector3(10f,4f,0f);
		GameObject newShipPoint3 = (GameObject)Instantiate (shipPoint3);
		newShipPoint3.transform.parent = GameObject.Find("Spaceship").transform;
		newShipPoint3.transform.localPosition = new Vector3(10f,-4f,0f);
		GameObject newShipPoint4 = (GameObject)Instantiate (shipPoint4);
		newShipPoint4.transform.parent = GameObject.Find("Spaceship").transform;
		newShipPoint4.transform.localPosition = new Vector3(-10f,-4f,0f);
		GameObject newShipPoint5 = (GameObject)Instantiate (shipPoint5);
		newShipPoint5.transform.parent = GameObject.Find("Spaceship").transform;
		newShipPoint5.transform.localPosition = new Vector3(-10f,4f,0f);
		GameObject newShipPoint6 = (GameObject)Instantiate (shipPoint6);
		newShipPoint6.transform.parent = GameObject.Find("Spaceship").transform;
		newShipPoint6.transform.localPosition = new Vector3(-3.5f,-4f,0f);
		GameObject newShipPoint7 = (GameObject)Instantiate (shipPoint7);
		newShipPoint7.transform.parent = GameObject.Find("Spaceship").transform;
		newShipPoint7.transform.localPosition = new Vector3(3.5f,4f,0f);
		GameObject newShipPoint8 = (GameObject)Instantiate (shipPoint8);
		newShipPoint8.transform.parent = GameObject.Find("Spaceship").transform;
		newShipPoint8.transform.localPosition = new Vector3(10f,-4f,0f);
		GameObject newShipPoint9 = (GameObject)Instantiate (shipPoint9);
		newShipPoint9.transform.parent = GameObject.Find("Spaceship").transform;
		newShipPoint9.transform.localPosition = new Vector3(10f,4f,0f);
		GameObject newShipPoint10 = (GameObject)Instantiate (shipPoint10);
		newShipPoint10.transform.parent = GameObject.Find("Spaceship").transform;
		newShipPoint10.transform.localPosition = new Vector3(3.5f,-4f,0f);
		GameObject newShipPoint11 = (GameObject)Instantiate (shipPoint11);
		newShipPoint11.transform.parent = GameObject.Find("Spaceship").transform;
		newShipPoint11.transform.localPosition = new Vector3(-3.5f,4f,0f);
		GameObject newShipPoint12 = (GameObject)Instantiate (shipPoint12);
		newShipPoint12.transform.parent = GameObject.Find("Spaceship").transform;
		newShipPoint12.transform.localPosition = new Vector3(-10f,-4f,0f);
		GameObject newShipPoint13 = (GameObject)Instantiate (shipPoint13);
		newShipPoint13.transform.parent = GameObject.Find("Spaceship").transform;
		newShipPoint13.transform.localPosition = new Vector3(0f,0f,0f);
		waypoints = GameObject.FindGameObjectsWithTag("ShipPoint");
		movingForward = moveForward;
		nextWaypoint = waypoints[currentIndex];
		initialIndex = currentIndex;
		tSpawning = true;
		anim.SetTrigger ("tSpawn");
		target = GameObject.FindGameObjectWithTag("Player").transform;
		player = target.GetComponent<Player> ();
	}
	
	
	public void Update ()
	{
        if (tSpawning)
        {
            GameObject.Find("CF-Complete-Physics-Platformer").GetComponent<TouchController>().b = true;
            GameObject.Find("CF-Complete-Physics-Platformer").GetComponent<TouchController>().a = true;
            bar.SetActive(false);
        }

		if(!tSpawning)
		{
			if(nextWaypointTimer < 0f)
			{
				nextWaypointTimer = 0f;
			}

			if (timer < 0f)
			{
				timer = 0f;
			}
		
			if (timer > 0f)
			{
				go = false;
				timer -= Time.deltaTime;
			}

			if (timer == 0)
			{
				go = true;
			}

			if (timer == 0f && waypointsPassed == 0)
			{
				stop = false;
			}

			if (currentIndex == 0 && timer > 0f)
			{
				stop = true;
				anim.SetBool("Idle", true);
				anim.SetBool("Right", false);
				anim.SetBool("Left", false);
				anim.SetBool("Up", false);
				anim.SetBool("Down", false);
				anim.SetBool("UpRight", false);
				anim.SetBool("UpLeft", false);
				anim.SetBool("DownRight", false);
				anim.SetBool("DownLeft", false);
			}

			else if (currentIndex == 0 && timer == 0f && waypointsPassed == 0)
			{
				anim.SetBool("Idle", false);
				anim.SetBool("Right", false);
				anim.SetBool("Left", false);
				anim.SetBool("Up", false);
				anim.SetBool("Down", false);
				anim.SetBool("UpRight", false);
				anim.SetBool("UpLeft", true);
				anim.SetBool("DownRight", false);
				anim.SetBool("DownLeft", false);
			}

			else if (currentIndex == 0 && timer == 0f && waypointsPassed > 0)
			{
				anim.SetBool("Idle", false);
				anim.SetBool("Right", false);
				anim.SetBool("Left", false);
				anim.SetBool("Up", false);
				anim.SetBool("Down", false);
				anim.SetBool("UpRight", false);
				anim.SetBool("UpLeft", true);
				anim.SetBool("DownRight", false);
				anim.SetBool("DownLeft", false);
				waitAtWaypointTime = 0.5f;
			}

			if (currentIndex == 1)
			{
				anim.SetBool("Idle", false);
				anim.SetBool("Right", true);
				anim.SetBool("Left", false);
				anim.SetBool("Up", false);
				anim.SetBool("Down", false);
				anim.SetBool("UpRight", false);
				anim.SetBool("UpLeft", false);
				anim.SetBool("DownRight", false);
				anim.SetBool("DownLeft", false);
			}

			if (currentIndex == 2)
			{
				anim.SetBool("Idle", false);
				anim.SetBool("Right", false);
				anim.SetBool("Left", false);
				anim.SetBool("Up", false);
				anim.SetBool("Down", true);
				anim.SetBool("UpRight", false);
				anim.SetBool("UpLeft", false);
				anim.SetBool("DownRight", false);
				anim.SetBool("DownLeft", false);
			}

			if (currentIndex == 3)
			{
				anim.SetBool("Idle", false);
				anim.SetBool("Right", false);
				anim.SetBool("Left", true);
				anim.SetBool("Up", false);
				anim.SetBool("Down", false);
				anim.SetBool("UpRight", false);
				anim.SetBool("UpLeft", false);
				anim.SetBool("DownRight", false);
				anim.SetBool("DownLeft", false);
			}

			if (currentIndex == 4)
			{
				anim.SetBool("Idle", false);
				anim.SetBool("Right", false);
				anim.SetBool("Left", false);
				anim.SetBool("Up", true);
				anim.SetBool("Down", false);
				anim.SetBool("UpRight", false);
				anim.SetBool("UpLeft", false);
				anim.SetBool("DownRight", false);
				anim.SetBool("DownLeft", false);
			}

			if (currentIndex == 5)
			{
				anim.SetBool("Idle", false);
				anim.SetBool("Right", false);
				anim.SetBool("Left", false);
				anim.SetBool("Up", false);
				anim.SetBool("Down", false);
				anim.SetBool("UpRight", false);
				anim.SetBool("UpLeft", false);
				anim.SetBool("DownRight", true);
				anim.SetBool("DownLeft", false);
			}

			if (currentIndex == 6)
			{
				anim.SetBool("Idle", false);
				anim.SetBool("Right", false);
				anim.SetBool("Left", false);
				anim.SetBool("Up", false);
				anim.SetBool("Down", false);
				anim.SetBool("UpRight", true);
				anim.SetBool("UpLeft", false);
				anim.SetBool("DownRight", false);
				anim.SetBool("DownLeft", false);
			}

			if (currentIndex == 7)
			{
				anim.SetBool("Idle", false);
				anim.SetBool("Right", false);
				anim.SetBool("Left", false);
				anim.SetBool("Up", false);
				anim.SetBool("Down", false);
				anim.SetBool("UpRight", false);
				anim.SetBool("UpLeft", false);
				anim.SetBool("DownRight", true);
				anim.SetBool("DownLeft", false);
			}

			if (currentIndex == 8)
			{
				anim.SetBool("Idle", false);
				anim.SetBool("Right", false);
				anim.SetBool("Left", false);
				anim.SetBool("Up", true);
				anim.SetBool("Down", false);
				anim.SetBool("UpRight", false);
				anim.SetBool("UpLeft", false);
				anim.SetBool("DownRight", false);
				anim.SetBool("DownLeft", false);
			}

			if (currentIndex == 9)
			{
				anim.SetBool("Idle", false);
				anim.SetBool("Right", false);
				anim.SetBool("Left", false);
				anim.SetBool("Up", false);
				anim.SetBool("Down", false);
				anim.SetBool("UpRight", false);
				anim.SetBool("UpLeft", false);
				anim.SetBool("DownRight", false);
				anim.SetBool("DownLeft", true);
			}

			if (currentIndex == 10)
			{
				anim.SetBool("Idle", false);
				anim.SetBool("Right", false);
				anim.SetBool("Left", false);
				anim.SetBool("Up", false);
				anim.SetBool("Down", false);
				anim.SetBool("UpRight", false);
				anim.SetBool("UpLeft", true);
				anim.SetBool("DownRight", false);
				anim.SetBool("DownLeft", false);
			}

			if (currentIndex == 11)
			{
				anim.SetBool("Idle", false);
				anim.SetBool("Right", false);
				anim.SetBool("Left", false);
				anim.SetBool("Up", false);
				anim.SetBool("Down", false);
				anim.SetBool("UpRight", false);
				anim.SetBool("UpLeft", false);
				anim.SetBool("DownRight", false);
				anim.SetBool("DownLeft", true);
				timer = 6.5f;
			}

			if (currentIndex == 12 && go)
			{
				anim.SetBool("Idle", false);
				anim.SetBool("Right", false);
				anim.SetBool("Left", false);
				anim.SetBool("Up", false);
				anim.SetBool("Down", false);
				anim.SetBool("UpRight", true);
				anim.SetBool("UpLeft", false);
				anim.SetBool("DownRight", false);
				anim.SetBool("DownLeft", false);
			}

			else if (currentIndex == 12 && !go)
			{
				anim.SetBool("Idle", true);
				anim.SetBool("Right", false);
				anim.SetBool("Left", false);
				anim.SetBool("Up", false);
				anim.SetBool("Down", false);
				anim.SetBool("UpRight", false);
				anim.SetBool("UpLeft", false);
				anim.SetBool("DownRight", false);
				anim.SetBool("DownLeft", false);
				waitAtWaypointTime = 5f;
			}

			if (movingForward != moveForward) 
			{
				movingForward = moveForward;
				DetermineNextWaypoint();
			}

			nextWaypoint = waypoints[currentIndex];

			if (nextWaypoint)
			{
				Transform waypointTransform = nextWaypoint.transform;
				nextVelocity = (waypointTransform.position-transform.position).normalized;
			}

			if (stopAfterTotal != 0 && waypointsPassed >= stopAfterTotal)
			{
				stop = true;
			}

			if (waitAtWaypointTime > 0 && waitAtWaypoint)
			{
				if (nextWaypointTimer > 0)
				{
					nextWaypointTimer -= Time.deltaTime;
				} 

				else
				{
					waitAtWaypoint = false;
					stop = false;
				}
			}
		}
	}

	void FixedUpdate()
	{
		if (stop)
		{
			parent.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
		}

		else 
		{
			parent.GetComponent<Rigidbody2D>().velocity = nextVelocity * speed;
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject == nextWaypoint)
		{
			DetermineNextWaypoint();
			waypointsPassed++;
		}
	}

	void DetermineNextWaypoint()
	{
		if (moveForward && waypoints.Length == currentIndex+1)
		{
			if (loop)
			{
				currentIndex = -1;
			}

			else
			{
				moveForward = movingForward = false;
			}
		}

		else if (!moveForward && currentIndex == 0)
		{
			if (loop) 
			{
				currentIndex = waypoints.Length;
			} 

			else
			{
				moveForward = movingForward = true;
			}
		}

		if (moveForward) 
		{
			currentIndex++;
		} 

		else
		{
			currentIndex--;
		}

		if (waitAtWaypointTime > 0)
		{
			nextWaypointTimer = waitAtWaypointTime;
			waitAtWaypoint = true;
			stop = true;
		}
	}

	public void ResetIndex()
	{
		currentIndex = initialIndex;
	}

	public void SpawnStop()
	{
		tSpawning = false;
		player.stop = false;
        player.Stop();
        GameObject.Find("CF-Complete-Physics-Platformer").GetComponent<TouchController>().b = false;
        GameObject.Find("CF-Complete-Physics-Platformer").GetComponent<TouchController>().a = false;
        bar.SetActive(true);
    }
}