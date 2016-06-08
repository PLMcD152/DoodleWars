using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyHealth : MonoBehaviour {

	public float health = 30f;
	private AudioClip[] ouchClips;
	private GameObject[] particles;
	private float swordDamageAmount = 35f;
	private float swordBoomDamageAmount = 50f;
	private float shotgunDamageAmount = 50f;
	private float rocketDamage = 100f;
	private float staffDamageAmount = 50f;
	private float staffBoomDamageAmount = 70f;
	private float boomerangDamageAmount = 10f;
	private float granadeDamageAmount = 125f;
	private float watergunDamageAmount = 20f;
	private float machinegunDamageAmount = 10f;
	private float deathSpinMin = -100f;
	private float deathSpinMax = 100f; 
	private Component spawnedEnemy;
	private bool IsDead = false;
	public float dieTimer;
	private Animator anim;
	public bool hit = false;
	[HideInInspector]
	public bool ranged = false;
	[HideInInspector]
	public float rangedTimer;
    [HideInInspector]
    public float hitTimer;
    private GameObject healthPack;
	private GameObject ammo;
	private GameObject poof;
	private GameObject x2;
	private GameObject x3;
	private GameObject x4;
	private GameObject x5;
	private GameObject x6;
	private GameObject x7;
	private GameObject x8;
	private GameObject x9;
	private GameObject x10;
	private GameObject sMGO;
	private ScoreManager sM;
    private Collider2D[] cols;
    private Collider2D[] colls;

    void Start()
    {
		healthPack = (Resources.Load ("Loot Stuff/HealthPack"))as GameObject;
		ammo = (Resources.Load ("Loot Stuff/AmmoPack"))as GameObject;
		poof = (Resources.Load ("Poof"))as GameObject;
		x2 = (Resources.Load ("Multipliers/X 2"))as GameObject;
		x3 = (Resources.Load ("Multipliers/X 3"))as GameObject;
		x4 = (Resources.Load ("Multipliers/X 4"))as GameObject;
		x5 = (Resources.Load ("Multipliers/X 5"))as GameObject;
		x6 = (Resources.Load ("Multipliers/X 6"))as GameObject;
		x7 = (Resources.Load ("Multipliers/X 7"))as GameObject;
		x8 = (Resources.Load ("Multipliers/X 8"))as GameObject;
		x9 = (Resources.Load ("Multipliers/X 9"))as GameObject;
		x10 = (Resources.Load ("Multipliers/X 10"))as GameObject;
		sMGO = GameObject.Find ("SpawnManager");
		sM = sMGO.GetComponent<ScoreManager> ();
        anim = GetComponent<Animator>();
        cols = GetComponentsInChildren<Collider2D>();
        colls = GetComponents<Collider2D>();
        ouchClips = new AudioClip[] 
		{
			(Resources.Load ("Goon/Sounds/GoonHurt1"))as AudioClip,
			(Resources.Load ("Goon/Sounds/GoonHurt2"))as AudioClip,
			(Resources.Load ("Goon/Sounds/GoonHurt3"))as AudioClip
		};
        particles = new GameObject[]
        {
            (Resources.Load("HurtAnim/Flash"))as GameObject,
            (Resources.Load("HurtAnim/FireBurn")) as GameObject,
            (Resources.Load("HurtAnim/Stunned")) as GameObject
        };
	} 

	void Update()
	{
		if (health < 5f && !IsDead)
		{
			Die ();
			health = 0f;
		}

		if (dieTimer > 0f)
		{
			dieTimer -= Time.deltaTime;
		}

        if (hit)
        {
            gameObject.layer = 31;
        }

        else if (!hit)
        {
            gameObject.layer = 24;
        }

        if (dieTimer < 0f)
		{
			dieTimer = 0f;
		}

		if (rangedTimer > 0f)
		{
			rangedTimer -= Time.deltaTime;
		}

		if (rangedTimer == 0f)
		{
			ranged = false;
		}

		if (rangedTimer < 0f)
		{
			rangedTimer = 0f;
		}

        if (hitTimer > 0f)
        {
            hitTimer -= Time.deltaTime;
        }

        if (hitTimer == 0f)
        {
            hit = false;
        }

        if (hitTimer < 0f)
        {
            hitTimer = 0f;
        }

        if (IsDead)
		{
			if (dieTimer == 0f)
			{
				Destroy(gameObject);
			}
		}

		if (sM.x2 && IsDead)
		{
			X2();
		}
		
		else if (sM.x3 && IsDead)
		{
			X3();
		}
		
		else if (sM.x4 && IsDead)
		{
			X4();
		}
		
		else if (sM.x5 && IsDead)
		{
			X5();
		}
		
		else if (sM.x6 && IsDead)
		{
			X6();
		}
		
		else if (sM.x7 && IsDead)
		{
			X7();
		}
		
		else if (sM.x8 && IsDead)
		{
			X8();
		}
		
		else if (sM.x9 && IsDead)
		{
			X9();
		}
		
		else if (sM.x10 && IsDead)
		{
			X10();
		}
	}

	public void TakeRocketDamage (Transform missile)
	{
		if(!hit)
		{
			hit = true;
            hitTimer = 1f;
            health -= rocketDamage;
			GameObject fireburn = Instantiate(particles[1],transform.position,Quaternion.identity)as GameObject;
			fireburn.transform.parent = this.transform;
			int i = Random.Range (0, ouchClips.Length);
            GetComponent<AudioSource>().PlayOneShot(ouchClips[i]);
        }
	}

	public void TakeShotgunDamage (Transform spray)
	{
		if (!hit)
		{
			hit = true;
            hitTimer = 1f;
            health -= shotgunDamageAmount;
			int i = Random.Range (0, ouchClips.Length);
            GetComponent<AudioSource>().PlayOneShot(ouchClips[i]);
        }
	}

	public void TakeMeleeDamage (Transform sword)
	{
		if (!hit)
		{
			hit = true;
            hitTimer = 1f;
            GameObject fireburn = Instantiate(particles[0],transform.position,Quaternion.identity)as GameObject;
			fireburn.transform.parent = this.transform;
			health -= swordDamageAmount;
			int i = Random.Range (0, ouchClips.Length);
            GetComponent<AudioSource>().PlayOneShot(ouchClips[i]);
        }
	}

	public void TakeSwordBoomDamage (Transform swordSlam)
	{
		if (!hit)
		{
			hit = true;
            hitTimer = 1f;
            health -= swordBoomDamageAmount;
			int i = Random.Range (0, ouchClips.Length);
            GetComponent<AudioSource>().PlayOneShot(ouchClips[i]);
        }
	}

	public void TakeBoomerangDamage (Transform boomerang)
	{
		GameObject fireburn = Instantiate(particles[2],transform.position,Quaternion.identity)as GameObject;
		fireburn.transform.parent = this.transform;
		ranged = true;
		rangedTimer = 3f;
		health -= boomerangDamageAmount;
		int i = Random.Range (0, ouchClips.Length);
        GetComponent<AudioSource>().PlayOneShot(ouchClips[i]);
    }
	
	public void TakeWatergunDamage (Transform water)
	{
		if (!hit)
		{
			hit = true;
            hitTimer = 1f;
            health -= watergunDamageAmount;
			int i = Random.Range (0, ouchClips.Length);
            GetComponent<AudioSource>().PlayOneShot(ouchClips[i]);
        }
	}
	
	public void TakeStaffDamage (Transform fireball)
	{
		if (!hit)
		{
			hit = true;
            hitTimer = 1f;
            health -= staffDamageAmount;
			GameObject fireburn = Instantiate(particles[1],transform.position,Quaternion.identity)as GameObject;
			fireburn.transform.parent = this.transform;
			int i = Random.Range (0, ouchClips.Length);
            GetComponent<AudioSource>().PlayOneShot(ouchClips[i]);
        }
	}

	public void TakeStaffBoomDamage (Transform staffSlam)
	{
		if (!hit)
		{
			hit = true;
            hitTimer = 1f;
            health -= staffBoomDamageAmount;
			int i = Random.Range (0, ouchClips.Length);
            GetComponent<AudioSource>().PlayOneShot(ouchClips[i]);
        }
	}
	
	public void TakeGranadeDamage (Transform granade)
	{
		if (!hit)
		{
			hit = true;
            hitTimer = 1f;
            GameObject fireburn = Instantiate(particles[1],transform.position,Quaternion.identity)as GameObject;
			fireburn.transform.parent = this.transform;
			health -= granadeDamageAmount;
			int i = Random.Range (0, ouchClips.Length);
            GetComponent<AudioSource>().PlayOneShot(ouchClips[i]);
        }
	}
	
	public void TakeMachinegunDamage (Transform bullets)
	{
		hit = true;
        hitTimer = 0.5f;
        health -= machinegunDamageAmount;
		int i = Random.Range (0, ouchClips.Length);
        GetComponent<AudioSource>().PlayOneShot(ouchClips[i]);
    }

	public void X2()
	{
		Instantiate(x2, transform.position, transform.rotation);
		sM.x2 = false;
	}
	
	public void X3()
	{
		Instantiate(x3, transform.position, transform.rotation);
		sM.x3 = false;
	}
	
	public void X4()
	{
		Instantiate(x4, transform.position, transform.rotation);
		sM.x4 = false;
	}
	
	public void X5()
	{
		Instantiate(x5, transform.position, transform.rotation);
		sM.x5 = false;
	}
	
	public void X6()
	{
		Instantiate(x6, transform.position, transform.rotation);
		sM.x6 = false;
	}
	
	public void X7()
	{
		Instantiate(x7, transform.position, transform.rotation);
		sM.x7 = false;
	}
	
	public void X8()
	{
		Instantiate(x8, transform.position, transform.rotation);
		sM.x8 = false;
	}
	
	public void X9()
	{
		Instantiate(x9, transform.position, transform.rotation);
		sM.x9 = false;
	}
	
	public void X10()
	{
		Instantiate(x10, transform.position, transform.rotation);
		sM.x10 = false;
	}

	public void Die()
	{
		int i = Random.Range (0, 100);
		if(i >= 25)
		{
			GameObject newPoof = (GameObject)Instantiate (poof);
			newPoof.transform.position = new Vector3 (transform.position.x, transform.position.y);
		}
		
		if(i < 25 && i > 15)
		{
			GameObject newAmmo = (GameObject)Instantiate (ammo);
			newAmmo.transform.position = new Vector3 (transform.position.x, transform.position.y);
		}
		
		if(i <= 15)
		{
			GameObject newHealth = (GameObject)Instantiate (healthPack);
			newHealth.transform.position = new Vector3 (transform.position.x, transform.position.y);
		}
		
		SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer>();
		foreach(SpriteRenderer s in spr)
		{
			s.sortingLayerName = "Foreground";
		}
		dieTimer = 2.0f;
        anim.SetTrigger("Die");
        IsDead = true;
		GetComponent<Rigidbody2D>().isKinematic = false;
		GetComponent<Rigidbody2D>().AddTorque(Random.Range(deathSpinMin,deathSpinMax));
        foreach (Collider2D c in cols)
        {
            c.isTrigger = true;
            c.tag = "Untagged";
        }
        foreach (Collider2D d in colls)
        {
            d.isTrigger = true;
            d.tag = "Untagged";
        }
        GameObject.Find("SpawnManager").SendMessage("DeadEnemy");
		GameObject.Find("SpawnManager").SendMessage("smudgiekill");
		GameObject.Find("GameManager").SendMessage("enemyslain");
	}

	public void Stop()
	{
		GameObject stopSign = GameObject.Find ("SniperBulletSpawn");
		Shootercopy stop = stopSign.GetComponent<Shootercopy> ();
		stop.Stop ();
	}
}