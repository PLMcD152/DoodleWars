using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerSpin : MonoBehaviour {
	
	private Transform target;
	private float speed = 0.5f;
	private float lowSpeed = 1f;
	private float maxSpeed = 0.1f;
	private float scaleAcceleration = 0.2f;
	private Animator anim;
	private Player pl;
	private CameraFollowFinal cF;
	private float mainXScale;


	void Start ()
	{
		anim = GetComponent<Animator> ();
		pl = GetComponent<Player> ();
		cF = GetComponentInChildren<CameraFollowFinal> ();
		target = GameObject.Find ("BossSpawn").transform;
	}

	public void PunkGo()
	{
		cF.enabled = true;
		pl.enabled = true;
		pl.moveIt = false;
		GetComponent<Rigidbody2D>().isKinematic = false;
		if(mainXScale > 0f)
		{
			transform.localScale = new Vector3(1,1,1);
		}
		
		else if(mainXScale < 0f)
		{
			transform.localScale = new Vector3(-1,1,1);
		}
		anim.SetBool ("punkBossSpawn", false);
		Destroy (GameObject.Find ("BossSoldierTeleport (clone)"));
        SceneManager.LoadScene ("Punk Load");
	}

	public void ShipGo()
	{
		cF.enabled = true;
		pl.enabled = true;
		pl.moveIt = false;
		GetComponent<Rigidbody2D>().isKinematic = false;
		if(mainXScale > 0f)
		{
			transform.localScale = new Vector3(1,1,1);
		}
		
		else if(mainXScale < 0f)
		{
			transform.localScale = new Vector3(-1,1,1);
		}
		anim.SetBool ("alienBossSpawn", false);
        SceneManager.LoadScene ("Alien Load");
		Destroy (GameObject.Find ("SpaceshipTeleport (clone)"));
	}

	public void DinoGo()
	{
		cF.enabled = true;
		pl.enabled = true;
		pl.moveIt = false;
		GetComponent<Rigidbody2D>().isKinematic = false;
		if(mainXScale > 0f)
		{
			transform.localScale = new Vector3(1,1,1);
		}
		
		else if(mainXScale < 0f)
		{
			transform.localScale = new Vector3(-1,1,1);
		}
		anim.SetBool("dinoBossSpawn", false);
        SceneManager.LoadScene ("Dino Load");
		Destroy (GameObject.Find ("DinoTeleport (clone)"));
	}

	public void KingGo()
	{
		cF.enabled = true;
		pl.enabled = true;
		pl.moveIt = false;
		GetComponent<Rigidbody2D>().isKinematic = false;
		if(mainXScale > 0f)
		{
			transform.localScale = new Vector3(1,1,1);
		}
		
		else if(mainXScale < 0f)
		{
			transform.localScale = new Vector3(-1,1,1);
		}
		anim.SetBool("finalBattle", false);
        SceneManager.LoadScene ("King Load");
		Destroy (GameObject.Find ("KingFightStart (clone)"));
	}

	public void MachineGo()
	{
		cF.enabled = true;
		pl.enabled = true;
		pl.moveIt = false;
		GetComponent<Rigidbody2D>().isKinematic = false;
		if(mainXScale > 0f)
		{
			transform.localScale = new Vector3(1,1,1);
		}
		
		else if(mainXScale < 0f)
		{
			transform.localScale = new Vector3(-1,1,1);
		}
		anim.SetBool("machineBossSpawn", false);
        SceneManager.LoadScene ("Machine Load");
		Destroy (GameObject.Find ("MachineBossTeleport (clone)"));
	}

	void Update () 
	{
		mainXScale = gameObject.transform.localScale.x;
		if(pl.moveIt)
		{
			cF.enabled = false;
			pl.enabled = false;
			if (lowSpeed > maxSpeed)
			{
				lowSpeed = lowSpeed - scaleAcceleration * Time.deltaTime;
			}

			if(mainXScale > 0f)
			{
				transform.localScale = new Vector3(lowSpeed,lowSpeed,1);
			}

			if(mainXScale < 0f)
			{
				transform.localScale = new Vector3(-lowSpeed,lowSpeed,1);
			}
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, target.position, step);
			GetComponent<Rigidbody2D>().isKinematic = true;
		}

		else if (!pl.moveIt)
		{
			lowSpeed = 1;
		}
	}
}
