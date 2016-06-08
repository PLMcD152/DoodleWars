using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum Direction { Left, Right };

public class Player : MonoBehaviour 
{
	//[HideInInspector]
	public float hor;							// Get the Horizontal Input.
	[HideInInspector]
	public bool facingRight;					// For determining which way the player is currently facing.
	[HideInInspector]
	public bool grounded = true;				// Whether or not the player is grounded.
	[HideInInspector]
	public bool walking = false;				// Determines if the player is walking.
	[HideInInspector]
	public bool stuckToWall = false;			// Used to stop the player from moving when stuck to a wall.
	[HideInInspector]
	public bool falling = false;
	[HideInInspector]
	public bool dashing = false;				// Determines if the player is dashing.
	[HideInInspector]
	public bool crouching = false;				// Determines if the player is crouching.
	[HideInInspector]
	public bool sliding = false;				// Determines if the player is sliding.
	//[HideInInspector]
	public bool onLadder = false;				// Determines if the player is on a ladder.
	[HideInInspector]
	public GameObject frontCheckTop;			// A position marking to check if the player's front.
	[HideInInspector]
	public GameObject frontCheckBot;			// A position marking to check if the player's front.
	[HideInInspector]
	public Transform groundCheck;				// A position marking to check if the player is grounded.
	[HideInInspector]
	public Collider2D groundCollider;			// Ground collider object.
	[HideInInspector]
	public bool jumpDown = false;				// Determines if the player is about to jump down.
	[HideInInspector]
	public bool jumping = false;				// Determines if the player is jumping.
	[HideInInspector]
	public bool jumpingThrough = false;			// Determines if the player is jumping through a platform.
	[HideInInspector]
	public bool Bazooka = false;
	private AudioClip Launch;
	private GameObject rocketKick;
	private GameObject bulletPrefab;
	private Transform bulletSpawnTransform;
	[HideInInspector]
	public bool Shotgun = false;
	private AudioClip shotgunShot;
	public bool fire = false;
	private GameObject shotgunKick;
	private GameObject shotgunBullet;
	private Transform shotgunBulletSpawn;
	[HideInInspector]
	public bool Staff = false;
	private AudioClip staffShot;
	private AudioClip staffBoom;
	private GameObject staffBullet;
	private Transform staffBulletSpawn;
	[HideInInspector]
	public bool Granade = false;
	private AudioClip granadeShot;
	private GameObject granadeBullet;
	private Transform granadeBulletSpawn;
	[HideInInspector]
	public bool Watergun = false;
	private AudioClip watergunShot;
	private GameObject watergunKick;
	private GameObject watergunBullet;
	private Transform watergunBulletSpawn;
	[HideInInspector]
	public bool Machinegun = false;
	private AudioClip machinegunShot;
	private GameObject machinegunKick;
	private GameObject machinegunBullet;
	private Transform machinegunBulletSpawn;
	[HideInInspector]
	public bool Boomerang = false;
	private AudioClip boomerangShot;
	private GameObject boomerangBullet;
	private Transform boomerangBulletSpawn;
    private float timer;
	[HideInInspector]
	public bool stop = false;
	private static Player instance = null;
	public static Player Instance {
		get { return instance; }
	}
		
	[Tooltip("Select the direction in which the sprites are facing.")]
	public Direction spriteDirection;
	[Tooltip("Select the layer that should be used for platforms.")]
	public LayerMask platformLayer;
	[Tooltip("Select the layer that should be used for the ground.")]
	public LayerMask groundLayer;
	[Tooltip("Set the radius for the ground check. This creates a circle at the feet of the player, checking if anything from the ground or platform layer collides with the circle.")]
	public float groundRadius = 0.12f;
	[Tooltip("Enable or disable player rotation when on a rotated platform (slope).")]
	public bool rotateOnSlope = true;
	[Tooltip("Enable or disable player movement after death.")]
	public bool moveAfterDeath = true;
	[Tooltip("You can enable this to keep the player's velocity when hitting the ground. This is experimental and currently defaults to false.")]
	public bool keepVelocityOnGround = false;
	[Tooltip("The timer used for the remembering the player's velocity when hitting the ground. Don't give this a high value.")]
	public float groundedVelocityTime = 0.05f;
	public bool timedFire = false;
	private PlayerJump playerJump;
	private PlayerWall playerWall;
	private PlayerRun playerRun;
	private PlayerCrouch playerCrouch;
	private PlayerDash playerDash;
	private PlayerPlatform playerPlatform;
	private Quaternion normalRotation;
	[HideInInspector]
	public Animator animator;
	private bool flipAgain = false;
	private float gravityScale = 2f;
	private bool isDead = false;
	private float groundedXVelocity;
	private float groundedTimer;
	private bool wallSliding = false;
	private bool wallRunning = false;
	private bool wallJumping = false;
	private BoxCollider2D boxCollider;
	private CircleCollider2D circleCollider;
	[HideInInspector]
	public float attackTimer;
	private Pickup pickup;
	public float nextFire;
	private float machinegunFireRate = 0.25f;
	public bool boomerangThrowable = true;
	[HideInInspector]
	public GameObject cameraFind;
	[HideInInspector]
	public CameraFollowFinal cam;
	[HideInInspector]
	public GameObject orient;
	[HideInInspector]
	public GameObject came;
	private GameObject power;
	private GameObject boom;
	public GameObject boomOrient;
	public float staffTimer;
	public int staffClicks;
	public bool staffFired = false;
	private GameObject boomSpawn;
	[HideInInspector]
	public bool shp = false;
	public GameObject ss;
	private AudioClip sG;
	private AudioClip wG;
	public GameObject goodSpawn;
	public bool moveIt = false;
	private AudioClip outOfAmmo;
	public bool isAttacking;
    [HideInInspector]
    public GameObject fightButton;
    private bool punk;
    private bool dino;
    private bool ship;
    private bool mach;
    private bool king;
    private Image tape;
    private Text round;
    private Text waveText;
    private Text scoreText;
    private Text scorePoints;
    private Text moneyText;
    private Text moneyPoints;
    private bool fightClicked;
    private Camera minimapCamera;
    private KGFMapSystem minimap;
    [HideInInspector]
    public bool fell;

    void Awake() 
	{
		if (instance != null && instance != this)
		{
			Destroy(this.gameObject);
			return;
		}
		
		else 
		{
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	void Start ()
    {
        boxCollider = GetComponent<BoxCollider2D>();
		circleCollider = GetComponent<CircleCollider2D>();
		goodSpawn = GameObject.Find ("EndSpawnBack");
		groundCheck = transform.Find("groundCheck");
		frontCheckTop = new GameObject("frontCheckTop");
		frontCheckBot = new GameObject("frontCheckBot");
		playerJump = GetComponent<PlayerJump>();
		playerWall = GetComponent<PlayerWall>();
		playerRun = GetComponent<PlayerRun>();
		playerCrouch = GetComponent<PlayerCrouch>();
		playerDash = GetComponent<PlayerDash>();
		playerPlatform = GetComponent<PlayerPlatform>();
		normalRotation = transform.localRotation;
		animator = GetComponent<Animator>();
        pickup = GetComponent<Pickup>();
		cam = GetComponentInChildren<CameraFollowFinal> ();
		came = GameObject.FindWithTag ("MainCamera");
		frontCheckTop.transform.parent = transform;
		frontCheckBot.transform.parent = transform;
		sG = (Resources.Load ("Weapons/Sounds/ReloadShotgun")) as AudioClip;
		wG = (Resources.Load ("Weapons/Sounds/ReloadWatergun")) as AudioClip;
		bulletPrefab = (Resources.Load("Weapons/Rocket/Player Bullet"))as GameObject;
		rocketKick = (Resources.Load("Weapons/WeaponKicks/RocketKick"))as GameObject;
		shotgunBullet = (Resources.Load("Weapons/Shotgun/ShotgunSpraySystem"))as GameObject;
		shotgunKick = (Resources.Load("Weapons/WeaponKicks/ShotgunKick"))as GameObject;
		staffBullet = (Resources.Load("Weapons/Staff/Fireball"))as GameObject;
		granadeBullet = (Resources.Load("Weapons/Granade/Granade2"))as GameObject;
		watergunBullet = (Resources.Load("Weapons/Watergun/WaterGunBullet"))as GameObject;
		watergunKick = (Resources.Load("Weapons/WeaponKicks/WatergunKick"))as GameObject;
		machinegunBullet = (Resources.Load("Weapons/Machinegun/MachineGunBullet"))as GameObject;
		machinegunKick = (Resources.Load("Weapons/WeaponKicks/MachinegunKick"))as GameObject;
		boomerangBullet = (Resources.Load("Weapons/Boomerang/Boomerang2"))as GameObject;
		power = (Resources.Load("Weapons/Staff/Power"))as GameObject;
		boom = (Resources.Load("Weapons/Staff/Boom"))as GameObject;
		Launch = (Resources.Load ("Weapons/Sounds/DoodleRocketLaunch"))as AudioClip;
		shotgunShot = (Resources.Load ("Weapons/Sounds/DoodleShotgunShot"))as AudioClip;
		staffShot = (Resources.Load ("Weapons/Sounds/DoodleStaffShot"))as AudioClip;
		staffBoom = (Resources.Load ("Weapons/Sounds/DoodleStaffSlam"))as AudioClip;
		granadeShot = (Resources.Load ("Weapons/Sounds/DoodleGranadeThrow"))as AudioClip;
		watergunShot = (Resources.Load ("Weapons/Sounds/DoodleWatergun"))as AudioClip;
		machinegunShot = (Resources.Load ("Weapons/Sounds/DoodleMachinegunFire"))as AudioClip;
		boomerangShot = (Resources.Load ("Weapons/Sounds/DoodleGranadeThrow"))as AudioClip;
		outOfAmmo = (Resources.Load ("Weapons/Sounds/Click"))as AudioClip;
        fightButton = GameObject.Find("Fight");
        fightButton.SetActive(false);
        //transform.position = ss.transform.position;




        if (spriteDirection == Direction.Right)
		{
			facingRight = true;
		} 

		else 
		{
			facingRight = false;
			Flip();
		}
	}

    public void FightClicked()
    {
        GameObject.Find("CF-Complete-Physics-Platformer").GetComponent<TouchController>().b = true;
        GameObject.Find("CF-Complete-Physics-Platformer").GetComponent<TouchController>().a = true;
        tape = GameObject.Find("Tape1").GetComponent<Image>();
        tape.enabled = false;
        round = GameObject.Find("Round").GetComponent<Text>();
        round.enabled = false;
        waveText = GameObject.Find("WaveText").GetComponent<Text>();
        waveText.enabled = false;
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        scoreText.enabled = false;
        scorePoints = GameObject.Find("ScorePoints").GetComponent<Text>();
        scorePoints.enabled = false;
        moneyText = GameObject.Find("MoneyText").GetComponent<Text>();
        moneyText.enabled = false;
        moneyPoints = GameObject.Find("MoneyPoints").GetComponent<Text>();
        moneyPoints.enabled = false;
        minimapCamera = GameObject.Find("camera_minimap").GetComponent<Camera>();
        minimapCamera.enabled = false;
        minimap = GameObject.Find("KGFMapSystem").GetComponent<KGFMapSystem>();
        minimap.itsDataModuleMinimap.itsGlobalSettings.itsHideGUI = true;
        fightClicked = true;
        fightButton.SetActive(false);
        if (king)
        {
            stop = true;
            Stop();
            moveIt = true;
            animator.SetTrigger("FinalBattle");
            animator.SetBool("finalBattle", true);
            king = false;
        }

        if (punk)
        {
            stop = true;
            Stop();
            moveIt = true;
            animator.SetTrigger("PunkBossSpawn");
            animator.SetBool("punkBossSpawn", true);
            punk = false;
        }

        if (dino)
        {
            stop = true;
            Stop();
            moveIt = true;
            animator.SetTrigger("DinoBossSpawn");
            animator.SetBool("dinoBossSpawn", true);
            dino = false;
        }

        if (mach)
        {
            stop = true;
            Stop();
            moveIt = true;
            animator.SetTrigger("MachineBossSpawn");
            animator.SetBool("machineBossSpawn", true);
            mach = false;
        }

        if (ship)
        {
            stop = true;
            Stop();
            moveIt = true;
            shp = true;
            animator.SetTrigger("AlienBossSpawn");
            animator.SetBool("alienBossSpawn", true);
            ship = false;
        }
    }

	public void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag == "Start")
		{
            if(!fightClicked)
            {
                fightButton.SetActive(true);
            }
            king = true;
		}

		else if (col.gameObject.tag == "DinoStart")
		{
            if (!fightClicked)
            {
                fightButton.SetActive(true);
            }
            dino = true;
        }

		else if (col.gameObject.tag == "MachineBossSpawn")
		{
            if (!fightClicked)
            {
                fightButton.SetActive(true);
            }
            mach = true;
        }

		else if (col.gameObject.tag == "BossSoldierSpawn")
		{
            if (!fightClicked)
            {
                fightButton.SetActive(true);
            }
            punk = true;
        }

		else if (col.gameObject.tag == "AlianSpawn")
		{
            if (!fightClicked)
            {
                fightButton.SetActive(true);
            }
            ship = true;
        }

		if (col.gameObject.tag == "DinoBack")
		{
			col.gameObject.GetComponent<DinoBack>().DinoBack1();
            fightClicked = false;
            dino = false;
		}

		if (col.gameObject.tag == "MachineBack")
		{
			col.gameObject.GetComponent<MachineBack>().MachineBack1();
            fightClicked = false;
            mach = false;
        }

		if (col.gameObject.tag == "ShipBack")
		{
			shp = false;
			col.gameObject.GetComponent<SpaceshipBack>().ShipBack1();
            fightClicked = false;
            ship = false;
        }

		if (col.gameObject.tag == "PunkBack")
		{
			col.gameObject.GetComponent<PunkBack>().PunkBack1();
            fightClicked = false;
            punk = false;
        }

		else if (col.gameObject.name == "SpringObject")
		{
			col.gameObject.GetComponent<Spring>().GO ();
		}

		else if (col.gameObject.CompareTag("End"))
		{
			transform.position = goodSpawn.transform.position;
		}
	}

    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Start")
        {
            if (!fightClicked)
            {
                fightButton.SetActive(true);
            }
            king = true;
        }

        else if (col.gameObject.tag == "DinoStart")
        {
            if (!fightClicked)
            {
                fightButton.SetActive(true);
            }
            dino = true;
        }

        else if (col.gameObject.tag == "MachineBossSpawn")
        {
            if (!fightClicked)
            {
                fightButton.SetActive(true);
            }
            mach = true;
        }

        else if (col.gameObject.tag == "BossSoldierSpawn")
        {
            if (!fightClicked)
            {
                fightButton.SetActive(true);
            }
            punk = true;
        }

        else if (col.gameObject.tag == "AlianSpawn")
        {
            if (!fightClicked)
            {
                fightButton.SetActive(true);
            }
            ship = true;
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Start")
        {
            fightButton.SetActive(false);
            king = false;
        }

        else if (col.gameObject.tag == "DinoStart")
        {
            fightButton.SetActive(false);
            dino = false;
        }

        else if (col.gameObject.tag == "MachineBossSpawn")
        {
            fightButton.SetActive(false);
            mach = false;
        }

        else if (col.gameObject.tag == "BossSoldierSpawn")
        {
            fightButton.SetActive(false);
            punk = false;
        }

        else if (col.gameObject.tag == "AlianSpawn")
        {
            fightButton.SetActive(false);
            ship = false;
        }
    }


    public void stop1()
	{
		staffFired = false;
		isAttacking = false;
		animator.SetBool ("StaffFire", false);
		fire = false;
	}

	public void areaAttack()
	{
		boomOrient = GameObject.Find ("BoomSpawnPoint");
		GameObject newBoom = Instantiate (boom) as GameObject;
		newBoom.transform.position = new Vector3 (boomOrient.transform.position.x, boomOrient.transform.position.y);
		GetComponent<AudioSource>().PlayOneShot(staffBoom,1.0f);
	}

	public void areaAttackStop()
	{
		stop = false;
        Stop();
		animator.SetBool ("StaffBoom", false);
		isAttacking = false;
		Destroy (GameObject.Find ("Power(Clone)"));
		fire = false;
	}
	
	void FixedUpdate()
	{
		if (stuckToWall || onLadder)
		{
			GetComponent<Rigidbody2D>().gravityScale = 0;
		}

		else if (shp)
		{
			GetComponent<Rigidbody2D>().gravityScale = 0.5f;
		}

		else 
		{
			GetComponent<Rigidbody2D>().gravityScale = gravityScale;
		}

		if (keepVelocityOnGround && groundedXVelocity > 0)
		{
			SetXVelocity(groundedXVelocity);
		}
	}
	
	void Update()
	{
        animator.SetBool("grounded", grounded);
        animator.SetBool("walking", walking);
        animator.SetBool("crouching", crouching);
        animator.SetBool("sliding", sliding);
        animator.SetBool("dashing", dashing);
        animator.SetBool("falling", falling);
        animator.SetBool("wall", stuckToWall);
        animator.SetBool("onLadder", onLadder);
        animator.SetBool("jumpingThrough", jumpingThrough);

        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }

        if (timer < 0f)
        {
            timer = 0f;
        }

        if (timer == 0f)
        {
            WSVibrator.Cancel();
        }

        if (orient == null)
        {
            orient = GameObject.Find("KingOrient");
        }

		if (goodSpawn == null)
		{
			goodSpawn = GameObject.Find ("EndSpawnBack");
		}

		if (attackTimer > 0f)
		{
			attackTimer -= Time.deltaTime;
		}

		if (attackTimer == 0f && timedFire)
		{
			timedFire = false;
			fire = false;
		}
		
		if (nextFire > 0f)
		{
			nextFire -= Time.deltaTime;
		}
		
		if (attackTimer < 0f)
		{
			attackTimer = 0f;
		}

		if (staffTimer < 0f)
		{
			staffTimer = 0f;
		}

		if (staffTimer > 0f)
		{
			staffTimer -= Time.smoothDeltaTime;
		}

		if (staffTimer == 0)
		{
			staffClicks = 0;
		}

		if (nextFire < 0f)
		{
			nextFire = 0f;
		}

		Vector2 pos = transform.position;

		if (facingRight)
		{
			frontCheckTop.transform.position = new Vector2 (pos.x + boxCollider.offset.x + (boxCollider.size.x / 2), pos.y + boxCollider.offset.y + (boxCollider.size.y / 2));
			frontCheckBot.transform.position = new Vector2 (pos.x + boxCollider.offset.x + (boxCollider.size.x / 2) + 0.1f, pos.y + boxCollider.offset.y - (boxCollider.size.y / 2) - (circleCollider.radius/2));
		}

		else
		{
			frontCheckTop.transform.position = new Vector2 (pos.x + boxCollider.offset.x - (boxCollider.size.x / 2), pos.y + boxCollider.offset.y + (boxCollider.size.y / 2));
			frontCheckBot.transform.position = new Vector2 (pos.x + boxCollider.offset.x - (boxCollider.size.x / 2) - 0.1f, pos.y + boxCollider.offset.y - (boxCollider.size.y / 2) - (circleCollider.radius/2));
		}

		if (!stop)
		{
			hor = CFInput.GetAxis ("Horizontal");
			animator.SetFloat("horizontal", Mathf.Abs(hor));
		}

		animator.SetFloat("xSpeed",  Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
		animator.SetFloat("ySpeed", GetComponent<Rigidbody2D>().velocity.y);

		if (!OnPlatform())
		{
			groundCollider = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
		}

		if (keepVelocityOnGround && groundedXVelocity > 0)
		{
			if (groundedTimer > 0) 
			{
				groundedTimer -= Time.deltaTime;
			}

			else
			{
				groundedXVelocity = 0;
			}
		}

		if (groundCollider && !jumpingThrough)
		{
			if (keepVelocityOnGround && !grounded)
			{
				groundedXVelocity = GetComponent<Rigidbody2D>().velocity.x;
				groundedTimer = groundedVelocityTime;
			}
			grounded = true;
			jumping = false;
			if (rotateOnSlope && !OnPlatform()) { transform.rotation = groundCollider.transform.localRotation; }
			if (falling)
			{
				animator.SetTrigger("stateComplete");
			}
			falling = false;
		}

		else
		{
			transform.rotation = normalRotation;
			grounded = false;
		}

		if((hor > 0 && !facingRight) || (hor < 0 && facingRight) || flipAgain) 
		{
			Flip();
		}

        if (Staff)
        {
            if (pickup.staffAmmo > 0f && staffClicks == 1 && !staffFired)
            {
                if (staffTimer < 0.3f)
                {
                    Vib100();
                    isAttacking = true;
                    fire = true;
                    animator.SetBool("StaffFire", true);
                    GetComponent<AudioSource>().PlayOneShot(staffShot, 1.0f);
                    GameObject newBullet = Instantiate(staffBullet);
                    StaffController bullCon = newBullet.GetComponent<StaffController>();
                    bullCon.playerObject = gameObject;
                    bullCon.StaffShoot();
                    newBullet.transform.position = staffBulletSpawn.position;
                    pickup.staffAmmo--;
                    staffFired = true;
                }
            }
        }

        if (CFInput.GetButtonDown("Fire1"))
        {
            Fire();
        }

        if(CFInput.GetButton("Fire1") && Machinegun)
        {
            machinegunBulletSpawn = GameObject.Find("MachineGunSpawnPoint").transform;
            if (!onLadder && !stuckToWall)
            {
                if (nextFire == 0f && pickup.machinegunAmmo > 0f)
                {
                    Vib50();
                    isAttacking = true;
                    nextFire = machinegunFireRate;
                    animator.SetBool("MachineGunFire", true);
                    GetComponent<AudioSource>().PlayOneShot(machinegunShot, 1.0f);
                    GameObject newBullet = Instantiate(machinegunBullet);
                    GameObject newKick = Instantiate(machinegunKick);
                    MachineGunController bullCon = newBullet.GetComponent<MachineGunController>();
                    ParticleFlip kickCon = newKick.GetComponent<ParticleFlip>();
                    bullCon.playerObject = gameObject;
                    kickCon.playerObject = gameObject;
                    bullCon.MachineGunShoot();
                    kickCon.MachineGunShoot();
                    newBullet.transform.position = machinegunBulletSpawn.position;
                    newKick.transform.position = machinegunBulletSpawn.position;
                    pickup.machinegunAmmo--;
                }

                else if (pickup.machinegunAmmo == 0f)
                {
                    isAttacking = false;
                    animator.SetBool("MachineGunFire", false);
                    GetComponent<AudioSource>().PlayOneShot(outOfAmmo);
                }
            }
        }

        if (CFInput.GetButtonUp("Fire1") && Machinegun)
        {
            isAttacking = false;
            animator.SetBool("MachineGunFire", false);
        }
    }

    public void Fire()
    {
        if (!stop)
        {
            if (Bazooka == true)
            {
                bulletSpawnTransform = GameObject.Find("PLSpawn").transform;
                if (!fire && !timedFire && !onLadder && !stuckToWall)
                {
                    if (pickup.bazookaAmmo > 0f)
                    {
                        Vib100();
                        isAttacking = true;
                        fire = true;
                        animator.SetBool("BazookaFire", true);
                        GetComponent<AudioSource>().PlayOneShot(Launch, 1.0f);
                        GameObject newBullet = (GameObject)Instantiate(bulletPrefab);
                        GameObject newKick = (GameObject)Instantiate(rocketKick);
                        PlayerBulletController bullCon = newBullet.GetComponent<PlayerBulletController>();
                        ParticleFlip kickCon = newKick.GetComponent<ParticleFlip>();
                        bullCon.playerObject = gameObject;
                        kickCon.playerObject = gameObject;
                        bullCon.launchBullet();
                        kickCon.launchBullet();
                        newBullet.transform.position = bulletSpawnTransform.position;
                        newKick.transform.position = bulletSpawnTransform.position;
                        pickup.bazookaAmmo--;
                    }

                    else if (pickup.bazookaAmmo == 0f)
                    {
                        GetComponent<AudioSource>().PlayOneShot(outOfAmmo);
                    }
                }
            }

            if (Shotgun == true)
            {
                shotgunBulletSpawn = GameObject.Find("ShotGunSpawnPoint").transform;
                if (!fire && !onLadder && !stuckToWall)
                {
                    if (pickup.shotgunAmmo > 0f)
                    {
                        Vib100();
                        isAttacking = true;
                        fire = true;
                        animator.SetBool("ShotgunFire", true);
                        GetComponent<AudioSource>().PlayOneShot(shotgunShot, 1.0f);
                        GameObject newBullet = (GameObject)Instantiate(shotgunBullet);
                        GameObject newKick = (GameObject)Instantiate(shotgunKick);
                        ShotgunController bullCon = newBullet.GetComponent<ShotgunController>();
                        ParticleFlip kickCon = newKick.GetComponent<ParticleFlip>();
                        bullCon.playerObject = gameObject;
                        kickCon.playerObject = gameObject;
                        bullCon.ShotgunShoot();
                        kickCon.ShotgunShoot();
                        newBullet.transform.position = shotgunBulletSpawn.position;
                        newKick.transform.position = shotgunBulletSpawn.position;
                        pickup.shotgunAmmo--;
                    }

                    else if (pickup.shotgunAmmo == 0f)
                    {
                        GetComponent<AudioSource>().PlayOneShot(outOfAmmo);
                    }
                }
            }

            if (Watergun == true)
            {
                watergunBulletSpawn = GameObject.Find("WaterGunSpawnPoint").transform;
                if (!fire && !onLadder && !stuckToWall)
                {
                    Vib100();
                    isAttacking = true;
                    fire = true;
                    animator.SetBool("WaterGunFire", true);
                    GetComponent<AudioSource>().PlayOneShot(watergunShot, 1.0f);
                    GameObject newBullet = (GameObject)Instantiate(watergunBullet);
                    GameObject newKick = (GameObject)Instantiate(watergunKick);
                    WaterGunController bullCon = newBullet.GetComponent<WaterGunController>();
                    ParticleFlip kickCon = newKick.GetComponent<ParticleFlip>();
                    bullCon.playerObject = gameObject;
                    kickCon.playerObject = gameObject;
                    bullCon.WaterGunShoot();
                    kickCon.WaterGunShoot();
                    newBullet.transform.position = watergunBulletSpawn.position;
                    newKick.transform.position = watergunBulletSpawn.position;
                }
            }

            if (Staff)
            {
                staffBulletSpawn = GameObject.Find("StaffSpawnPoint").transform;
                if (!fire && !onLadder && !stuckToWall)
                {
                    staffClicks++;
                    if (staffClicks == 1)
                    {
                        staffTimer = 0.5f;
                    }

                    else if (pickup.staffAmmo > 5f && staffClicks == 2 && !staffFired)
                    {
                        if (staffTimer > 0.3f)
                        {
                            Vib500();
                            isAttacking = true;
                            fire = true;
                            stop = true;
                            Stop();
                            animator.SetBool("StaffBoom", true);
                            GameObject newPower = Instantiate(power);
                            newPower.transform.position = staffBulletSpawn.position;
                            pickup.staffAmmo -= 5;
                        }
                    }

                    else if (pickup.staffAmmo == 0f)
                    {
                        GetComponent<AudioSource>().PlayOneShot(outOfAmmo);
                    }
                }
            }

            if (Granade == true)
            {
                granadeBulletSpawn = GameObject.Find("GranadeSpawnPoint").transform;
                if (!fire && !timedFire && !onLadder && !stuckToWall)
                {
                    if (pickup.granadeAmmo > 0f)
                    {
                        isAttacking = true;
                        fire = true;
                        animator.SetBool("GranadeFire", true);
                    }

                    else if (pickup.granadeAmmo == 0f)
                    {
                        GetComponent<AudioSource>().PlayOneShot(outOfAmmo);
                    }
                }
            }

            if (Boomerang == true)
            {
                boomerangBulletSpawn = GameObject.Find("BoomerangSpawnPoint").transform;
                if (!fire && !timedFire && !onLadder && !stuckToWall)
                {
                    isAttacking = true;
                    fire = true;
                    animator.SetBool("BoomerangFire", true);
                }
            }
        }
    }

	public void MachinegunStop()
	{
		isAttacking = false;
		animator.SetBool ("MachineGunFire", false);
	}

	public void ShotgunStop()
	{
		isAttacking = false;
		animator.SetBool ("ShotgunFire", false);
		fire = false;
	}

	public void BazookaStop()
	{
		isAttacking = false;
		animator.SetBool ("BazookaFire", false);
		attackTimer = 0.7f;
		timedFire = true;
	}

	public void BoomerangFire()
	{
        Vib100();
        GetComponent<AudioSource>().PlayOneShot(boomerangShot);
		GameObject newBullet = Instantiate (boomerangBullet);
		newBullet.transform.position = boomerangBulletSpawn.position;
	}

	public void BoomerangStop()
	{
		isAttacking = false;
		animator.SetBool ("BoomerangFire", false);
		attackTimer = 0.5f;
		timedFire = true;
	}

	public void ReloadShotgun()
	{
		GetComponent<AudioSource>().PlayOneShot (sG);
	}

	public void ReloadWatergun()
	{
		GetComponent<AudioSource>().PlayOneShot (wG);
	}

	public void GranadeFire()
	{
        Vib100();
		GetComponent<AudioSource>().PlayOneShot(granadeShot,1.0f);
		GameObject newBullet = (GameObject)Instantiate (granadeBullet);
		GranadeController bullCon = newBullet.GetComponent<GranadeController> ();
		bullCon.playerObject = gameObject;
		bullCon.GranadeShoot ();
		newBullet.transform.position = granadeBulletSpawn.position;
		pickup.granadeAmmo --;
	}

	public void GranadeStop()
	{
		isAttacking = false;
		animator.SetBool ("GranadeFire", false);
		attackTimer = 0.2f;
		timedFire = true;
	}

	public void WatergunStop()
	{
		isAttacking = false;
		animator.SetBool ("WaterGunFire", false);
		fire = false;
	}
	
	public void Dead() 
	{
		isDead = true;
		SetJumps(0);
		animator.SetTrigger("Die");

		if (!moveAfterDeath)
		{ 
			playerRun.enabled = false;
            playerDash.enabled = false;
            GetComponent<Fight>().enabled = false;
            stop = true;
            Stop();
			GetComponent<Rigidbody2D>().velocity = new Vector2 (0f, 0f);
		}
	}
	
	void Flip()
	{
		if (!dashing && !sliding && !(isDead && !moveAfterDeath))
		{
			flipAgain = false;
			AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
			if (stuckToWall && (info.IsName("PlayerWallRun") || info.IsName("PlayerWallSlide"))) 
			{
				flipAgain = true;
				if (playerWall.wallJump.enabled)
				{
					animator.CrossFade("PlayerWallJump", 0f);
				} 

				else
				{
					animator.CrossFade("Jump/Fall", 0f);
				}
			} 

			else if (!stuckToWall && info.IsName("PlayerWallJump"))
			{
				flipAgain = true;
			} 

			else
			{
				facingRight = !facingRight;
				Vector2 theScale = transform.localScale;
				theScale.x *= -1;
				transform.localScale = theScale;
			}
		}
	}
	
	public void SetXVelocity(float xVel)
	{
		if (!grounded)
		{
			bool isWallJumping = playerWall && playerWall.wallJump.enabled && playerWall.isWallJumping;
			if (isWallJumping)

				playerWall.isWallJumping = false;
		}
		GetComponent<Rigidbody2D>().velocity = new Vector2(xVel, GetComponent<Rigidbody2D>().velocity.y);
	}
	
	public void SetYVelocity(float yVel) 
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, yVel);
	}
	
	public bool CanWalkAndRunInAir()
	{
		if (playerJump) 
		{
			return playerJump.airMovement.walkAndRun;
		}

		else
		{
			return true;
		}
	}
	
	public bool AirWalk()
	{
		return playerJump && playerJump.walkingOnJump;
	}
	
	public float GetAirSpeedFactor()
	{
		if (playerJump) 
		{
			return playerJump.airMovement.speedFactor;
		} 

		else
		{
			return 1.0f;
		}
	}
	
	public void Walk(bool walk) 
	{
		if (!grounded && playerJump && !playerJump.airMovement.resetOnWall && !CanWalkAndRunInAir())
			return;
			
		walking = walk;
	}
	
	public float GetSpeed(bool withPlatform = true)
	{
		return playerRun.GetSpeed(withPlatform);
	}
	
	public float GetSpeedOnMovingPlatform(float speed)
	{
		if (OnMovingPlatform())
		{
			float xVel = GetPlatform().GetComponent<Rigidbody2D>().velocity.x;
			if ((hor < 0 && xVel < 0) || (hor > 0 && xVel > 0))
			{
				speed = Mathf.Abs(xVel) + speed;
			}

			else if ((hor > 0 && xVel < 0) || (hor < 0 && xVel > 0))
			{
				speed = Mathf.Abs(Mathf.Abs(xVel) - speed);
			}

			else if (hor == 0)
			{
				speed = Mathf.Abs(xVel);
			}
		}

		return speed;
	}

		// Get the player's speed in the air if movement in the air is using force.
		public float GetSpeedInAir(float speed) {
			// If the player isn't grounded.
			if (!grounded) {
				// Change the speed based on the boomerang X factor when the player is boomerang wall jumping.
				if (playerWall && playerWall.boomerangJump) {
					speed = speed * playerWall.wallJump.boomerangFactorX;
				// Or else change the speed based on the speed factor in the air.
				} else {
					speed = speed * GetAirSpeedFactor();
				}
			}

			// Return the speed.
			return speed;
		}

		// Use the change factor to determine the amount of control for the player.
		public float GetMovementForce(float force) {
			// If the player isn't grounded, not wall jumping and the force should be changed by a factor...
			if (!grounded && playerJump && playerJump.airMovement.changeFactor != 1.0 && !(playerWall && playerWall.wallJump.enabled && playerWall.isWallJumping)) {
				// ... set the new force.
				force = force * playerJump.airMovement.changeFactor;
			}
			// Return the force.
			return force;
		}
		// ###          ###
		// ################


		// #################
		// ### PLATFORMS ###
		// ###           ###
		// Unstick the player from the platform.
		public void UnstickFromPlatform() {
			playerPlatform.Unstick();
		}

		// Get the current platform.
		public GameObject GetPlatform() {
			return playerPlatform.currentPlatform;
		}

		// Check if the player is on a platform.
		public bool OnPlatform() {
			return playerPlatform.OnPlatform();
		}

		// Check if the player is on a moving platform.
		public bool OnMovingPlatform() {
			return playerPlatform.OnMovingPlatform();
		}
		
		// Check if the player is on a sinking platform.
		public bool OnSinkingPlatform() {
			return playerPlatform.OnSinkingPlatform();
		}

		// Check if the player is stuck to the platform.
		public bool IsStuckToPlatform() {
			return playerPlatform.IsStuckToPlatform();
		}
		
		// Check if the player is on a platform where the player is stuck to the X velocity.
		public bool IsStuckToPlatformX() {
			return playerPlatform.IsStuckToPlatformX();
		}

		// Return if the player should keep its speed when jumping from the platform.
		public bool KeepSpeedOnJump() {
			return playerPlatform.KeepSpeedOnJump();
		}
		// ###          ###
		// ################
		// ###          ###
		// ################


		// ###############
		// ### JUMPING ###
		// ###         ###
		// Change the total amount of jumps.
		public void SetJumps(int jumps) {
			if (playerJump) { playerJump.jumps = jumps; }
		}

		// Reset the total amount of jumps to the default value.
		public void ResetJumps() {
			if (playerJump) { SetJumps(playerJump.doubleJumping.totalJumps); }
		}

		// Make the player jump.
		public void Jump() {
			if (playerJump) { playerJump.InitJump(); }
		}

		// Make the player fall down.
		public void Fall()
        {
			SetJumps(0);
			falling = true;
            playerDash.enabled = false;
            cam.enabled = false;
            animator.SetTrigger("startFalling");
		}

	public void AfterFalling()
	{
        Debug.Log("Should Be Working");
        playerDash.enabled = true;
        cam.enabled = true;
        fell = true;
    }

    public void Stop()
    {
        if (stop)
        {
            GetComponent<PlayerRun>().enabled = false;
            GetComponent<PlayerJump>().enabled = false;
            animator.SetFloat("xSpeed", 0f);
            animator.SetFloat("horizontal", 0f);
        }

        else if (!stop)
        {
            GetComponent<PlayerRun>().enabled = true;
            GetComponent<PlayerJump>().enabled = true;
        }
    }

		// Get the jump factor for the player.
		public float GetJumpFactor() {
			// Get the boomerang jump Y factor if the player is boomerang jumping.
			if (playerWall && playerWall.boomerangJump) {
				return playerWall.wallJump.boomerangFactorY;
			// Or else just return 1.0.
			} else {
				return 1.0f;
			}
		}
		// ###         ###
		// ###############


		// ############
		// ### WALL ###
		// ###      ###
		// Set the stuckToWall variable to make sure the player is stuck against a wall.
		public void StuckToWall(bool stuck) {
			stuckToWall = stuck;
		}

		// Set the wall sliding variables used by the animator.
		public void SetWallAnimation(bool run, bool slide, bool jump) {
			// Call the trigger 'stateComplete' when wall interaction is finished.
			if (((wallRunning && !run) || (wallSliding && !slide) || (wallJumping && !jump)) && !run && !slide && !jump) {
				animator.SetTrigger("stateComplete");
			}

			// Trigger the wall running animation when the player is wall running.
			if (run && !wallRunning) {
				animator.SetTrigger("startWallRun");
			}

			// Set the wall interaction variables to remember what the player is doing.
			wallRunning = run;
			wallSliding = slide;
			wallJumping = jump;

			// Set the animator values for all wall interaction.
			animator.SetBool("wallRunning", run);
			animator.SetBool("wallSliding", slide);
			animator.SetBool("wallJumping", jump);
		}
		// ###      ###
		// ############


		// ###############
		// ### DASHING ###
		// ###         ###
		// Function to set the dashing variable.
		public void Dash(bool dash) {
			// Call the trigger 'stateComplete' when dashing is finished.
			if (dashing && !dash) {
				animator.SetTrigger("stateComplete");
			// Start the dash when dashing.
			} 
			
			else if (dash && !dashing)
			{
				animator.SetTrigger("startDash");
				
			}
			dashing = dash;
		}

		// Function to reset the air dashing limit.
		public void ResetAirDashLimit() {
			if (playerDash) { playerDash.ResetAirDashLimit(); }
		}
		// ###        ###
		// ##############

		// #################
		// ### CROUCHING ###
		// ###           ###
		// Function to set the crouching variable based on if the player is crouching.
		public void Crouch(bool crouch) {
			crouching = crouch;
		}
		// Function to set the sliding variable based on if the player is sliding.
		public void Slide(bool slide) {
			// Call the trigger 'startSlide' when sliding.
			if (!sliding && slide) {
				animator.SetTrigger("startSlide");
			}
			sliding = slide;
		}
		// Function to get the crouch speed.
		public float GetCrouchSpeed() {
			if (playerCrouch) {
				return playerCrouch.crouchSpeed;
			} else {
				return 0;
			}
		}
		// ###           ###
		// #################

		// #################
		// ### CROUCHING ###
		// ###           ###
		public void OnLadder() 
		{
			
			onLadder = true;
			animator.SetTrigger("triggerLadder");
		}

	void OnLevelWasLoaded()
    {
		if (ss == null)
        {
			ss = GameObject.FindWithTag("StartSpawn");
		}
	}

    public void Vib50()
    {
        if (PlayerPrefs.GetInt("Vibrate") == 0)
        {
            timer = 0.5f;
            WSVibrator.Vibrate(50);
        }
    }

    public void Vib100()
    {
        if (PlayerPrefs.GetInt("Vibrate") == 0)
        {
            timer = 0.5f;
            WSVibrator.Vibrate(100);
        }
    }

    public void Vib500()
    {
        if (PlayerPrefs.GetInt("Vibrate") == 0)
        {
            timer = 0.5f;
            WSVibrator.Vibrate(500);
        }
    }

    public void Vib650()
    {
        if (PlayerPrefs.GetInt("Vibrate") == 0)
        {
            timer = 0.5f;
            WSVibrator.Vibrate(650);
        }
    }

    public void Vib1000()
    {
        if (PlayerPrefs.GetInt("Vibrate") == 0)
        {
            timer = 0.5f;
            WSVibrator.Vibrate(1000);
        }
    }
    // ###           ###
    // #################
}