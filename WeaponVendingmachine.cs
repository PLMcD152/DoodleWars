using UnityEngine;
using System.Collections;

public class WeaponVendingmachine : MonoBehaviour {

	public GameObject[] weaponSpawn;
	public GameObject[] children;
    private AudioClip sound;
    private AudioClip sound2;
    public int money;
	public GameObject notEnough;
	public GameObject notNeeded;
	public GameObject noLevel;
	public GameObject tooMany;
	public Animator nEA;
	public Animator nNA;
	public Animator nLA;
	public Animator tMA;
	public GameObject graphSubMenu;
    public GameObject openShop;
    public GameObject spawnManager;
	public ScoreManager scoreManager;
	public GameObject healthTokenPrefab;
	public GameObject largeHealthTokenPrefab;
	public GameObject shotgunPrefab;
	public GameObject pzookaPrefab;
	public GameObject machinegunPrefab;
	public GameObject staffPrefab;
	public GameObject granadePrefab;
	public GameObject ammoPrefab;
	public GameObject boomerangPrefab;
	public GameObject shotgunAmmoPrefab;
	public GameObject pzookaAmmoPrefab;
	public GameObject machinegunAmmoPrefab;
	public GameObject staffAmmoPrefab;
	public GameObject granadeAmmoPrefab;
    private GameObject boomerang;
    private GameObject shotgun;
    private GameObject shotgunAmmo;
    private GameObject bazooka;
    private GameObject bazookaAmmo;
    private GameObject machinegun;
    private GameObject machinegunAmmo;
    private GameObject staff;
    private GameObject staffAmmo;
    private GameObject granade;
    private GameObject granadeAmmo;
    public bool bazookaBought = false;
	public bool shotgunBought = false;
	public bool staffBought = false;
	public bool granadeBought = false;
	public bool machinegunBought = false;
	public bool boomerangBought = false;
	public GameObject levelPrefab;
	public GameObject pl;
	public Pickup p;
	public PlayerHealth ph;
	public Player play;
	public int levelUps = 0;
	public bool triggered = false;
	public bool tMAB = false;
	public bool nNAB = false;
	public bool nEAB = false;
	public bool nLAB = false;
    private static WeaponVendingmachine instance = null;
	public static WeaponVendingmachine Instance
	{
		get{return instance;}
	}

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
		pl = GameObject.Find ("MrDoodle");
		p = pl.GetComponent<Pickup> ();
		ph = pl.GetComponent<PlayerHealth> ();
		play = pl.GetComponent<Player> ();
		notEnough = GameObject.Find("Not Enough");
		notNeeded = GameObject.Find("Not Needed");
		tooMany = GameObject.Find("Clear Space");
		noLevel = GameObject.Find("No Level");
		nEA = notEnough.GetComponent<Animator> ();
		nNA = notNeeded.GetComponent<Animator> ();
		nLA = noLevel.GetComponent<Animator> ();
		tMA = tooMany.GetComponent<Animator> ();
		healthTokenPrefab = (Resources.Load ("Loot Stuff/HealthPack"))as GameObject;
		pzookaPrefab = (Resources.Load("Weapons/Rocket/Pencil Launcher"))as GameObject;
		shotgunPrefab = (Resources.Load("Weapons/Shotgun/Shotgun"))as GameObject;
		staffPrefab = (Resources.Load("Weapons/Staff/Staff"))as GameObject;
		machinegunPrefab = (Resources.Load("Weapons/Machinegun/MachineGun"))as GameObject;
		granadePrefab = (Resources.Load("Weapons/Granade/Granade"))as GameObject;
		ammoPrefab = (Resources.Load ("Loot Stuff/AmmoPack"))as GameObject;
		boomerangPrefab = (Resources.Load ("Weapons/Boomerang/Boomerang"))as GameObject;
		pzookaAmmoPrefab = (Resources.Load("VendingMachine/BazookaAmmo"))as GameObject;
		shotgunAmmoPrefab = (Resources.Load("VendingMachine/ShotgunAmmo"))as GameObject;
		staffAmmoPrefab = (Resources.Load("VendingMachine/StaffAmmo"))as GameObject;
		machinegunAmmoPrefab = (Resources.Load("VendingMachine/MachinegunAmmo"))as GameObject;
		granadeAmmoPrefab = (Resources.Load("VendingMachine/GranadeAmmo"))as GameObject;
		largeHealthTokenPrefab = (Resources.Load ("VendingMachine/HealthLargeToken"))as GameObject;
		levelPrefab = (Resources.Load ("Loot Stuff/LevelUpIcon"))as GameObject;
        sound = (Resources.Load("VendingMachine/ChaChing")) as AudioClip;
        sound2 = (Resources.Load("VendingMachine/Wrong")) as AudioClip;
        boomerang = GameObject.Find("BoomerangIcon");
        shotgun = GameObject.Find("ShotGunIcon");
        bazooka = GameObject.Find("PZookaIcon");
        machinegun = GameObject.Find("HeavyMachineIcon");
        staff = GameObject.Find("StaffIcon");
        granade = GameObject.Find("GranadeIcon");
        shotgunAmmo = GameObject.Find("ShotGunIconAmmo");
        bazookaAmmo = GameObject.Find("PZookaIconAmmo");
        machinegunAmmo = GameObject.Find("HeavyMachineIconAmmo");
        staffAmmo = GameObject.Find("StaffIconAmmo");
        granadeAmmo = GameObject.Find("GranadeIconAmmo");
        shotgunAmmo.SetActive(false);
        bazookaAmmo.SetActive(false);
        machinegunAmmo.SetActive(false);
        staffAmmo.SetActive(false);
        granadeAmmo.SetActive(false);
        openShop = GameObject.Find("OpenShop");
        openShop.SetActive(false);
        graphSubMenu = GameObject.Find("GraphSubMenu");
        graphSubMenu.SetActive(false);
        children = new GameObject[11];
		weaponSpawn = new GameObject[11];
		weaponSpawn [0] = GameObject.Find ("WeaponSpawn1");
		weaponSpawn [1] = GameObject.Find ("WeaponSpawn2");
		weaponSpawn [2] = GameObject.Find ("WeaponSpawn3");
		weaponSpawn [3] = GameObject.Find ("WeaponSpawn4");
		weaponSpawn [4] = GameObject.Find ("WeaponSpawn5");
		weaponSpawn [5] = GameObject.Find ("WeaponSpawn6");
		weaponSpawn [6] = GameObject.Find ("WeaponSpawn7");
		weaponSpawn [7] = GameObject.Find ("WeaponSpawn8");
		weaponSpawn [8] = GameObject.Find ("WeaponSpawn9");
		weaponSpawn [9] = GameObject.Find ("WeaponSpawn10");
		weaponSpawn [10] = GameObject.Find ("WeaponSpawn11");
	}

	void Update () 
	{
		spawnManager = GameObject.Find ("SpawnManager");
		scoreManager = spawnManager.GetComponent<ScoreManager> ();
		money = scoreManager.money;
	} 

	public void instanHealthToken()
	{
		triggered = false;
		if (money >= 100 && !ph.fullHealth)
		{
            GameObject newhealthToken = Instantiate(healthTokenPrefab);
			scoreManager.BuySmallHealthToken();
            GetComponent<AudioSource>().PlayOneShot(sound);
			if (children [0] == null)
			{
				children [0] = newhealthToken.gameObject;
				newhealthToken.transform.position = weaponSpawn[0].transform.position;
				newhealthToken.transform.parent = weaponSpawn[0].transform;
			}

			else if (children [0] != null && children [1] == null)
			{
				children [1] = newhealthToken.gameObject;
				newhealthToken.transform.position = weaponSpawn[1].transform.position;
				newhealthToken.transform.parent = weaponSpawn[1].transform;
			}

			else if (children [0] != null && children [1] != null && children [2] == null)
			{
				children [2] = newhealthToken.gameObject;
				newhealthToken.transform.position = weaponSpawn[2].transform.position;
				newhealthToken.transform.parent = weaponSpawn[2].transform;
			}

			else if (children [0] != null && children [1] != null && children [2] != null && children [3] == null)
			{
				children [3] = newhealthToken.gameObject;
				newhealthToken.transform.position = weaponSpawn[3].transform.position;
				newhealthToken.transform.parent = weaponSpawn[3].transform;
			}

			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] == null)
			{
				children [4] = newhealthToken.gameObject;
				newhealthToken.transform.position = weaponSpawn[4].transform.position;
				newhealthToken.transform.parent = weaponSpawn[4].transform;
			}

			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] == null)
			{
				children [5] = newhealthToken.gameObject;
				newhealthToken.transform.position = weaponSpawn[5].transform.position;
				newhealthToken.transform.parent = weaponSpawn[5].transform;
			}

			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] == null)
			{
				children [6] = newhealthToken.gameObject;
				newhealthToken.transform.position = weaponSpawn[6].transform.position;
				newhealthToken.transform.parent = weaponSpawn[6].transform;
			}

			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] == null)
			{
				children [7] = newhealthToken.gameObject;
				newhealthToken.transform.position = weaponSpawn[7].transform.position;
				newhealthToken.transform.parent = weaponSpawn[7].transform;
			}

			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] == null)
			{
				children [8] = newhealthToken.gameObject;
				newhealthToken.transform.position = weaponSpawn[8].transform.position;
				newhealthToken.transform.parent = weaponSpawn[8].transform;
			}

			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] == null)
			{
				children [9] = newhealthToken.gameObject;
				newhealthToken.transform.position = weaponSpawn[9].transform.position;
				newhealthToken.transform.parent = weaponSpawn[9].transform;
			}

			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] != null && children [10] == null)
			{
				children [10] = newhealthToken.gameObject;
				newhealthToken.transform.position = weaponSpawn[10].transform.position;
				newhealthToken.transform.parent = weaponSpawn[10].transform;
			}

			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] != null && children [10] != null)
			{
				if(!triggered)
				{
                    GetComponent<AudioSource>().PlayOneShot(sound2);
                    tMA.SetBool("Click", true);
					tMAB = true;
					triggered = true;
				}
			}
		} 

		else if (money >= 100 && ph.fullHealth)
		{
			if(!triggered)
			{
                GetComponent<AudioSource>().PlayOneShot(sound2);
                nNA.SetBool("Click", true);
				nNAB = true;
				triggered = true;
			}
		}

		else
		{
			if(!triggered)
			{
                GetComponent<AudioSource>().PlayOneShot(sound2);
                nEA.SetBool("Click", true);
				nEAB = true;
				triggered = true;
			}
		}
	}

	public void instanLargeHealthToken()
	{
		triggered = false;
		if (money >= 250)
		{
			GameObject newLargeHealthToken = Instantiate (largeHealthTokenPrefab);
			scoreManager.BuyLargeHealthToken();
            GetComponent<AudioSource>().PlayOneShot(sound);
            if (children [0] == null)
			{
				children [0] = newLargeHealthToken.gameObject;
				newLargeHealthToken.transform.position = weaponSpawn[0].transform.position;
				newLargeHealthToken.transform.parent = weaponSpawn[0].transform;
			}
			
			else if (children [0] != null && children [1] == null)
			{
				children [1] = newLargeHealthToken.gameObject;
				newLargeHealthToken.transform.position = weaponSpawn[1].transform.position;
				newLargeHealthToken.transform.parent = weaponSpawn[1].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] == null)
			{
				children [2] = newLargeHealthToken.gameObject;
				newLargeHealthToken.transform.position = weaponSpawn[2].transform.position;
				newLargeHealthToken.transform.parent = weaponSpawn[2].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] == null)
			{
				children [3] = newLargeHealthToken.gameObject;
				newLargeHealthToken.transform.position = weaponSpawn[3].transform.position;
				newLargeHealthToken.transform.parent = weaponSpawn[3].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] == null)
			{
				children [4] = newLargeHealthToken.gameObject;
				newLargeHealthToken.transform.position = weaponSpawn[4].transform.position;
				newLargeHealthToken.transform.parent = weaponSpawn[4].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] == null)
			{
				children [5] = newLargeHealthToken.gameObject;
				newLargeHealthToken.transform.position = weaponSpawn[5].transform.position;
				newLargeHealthToken.transform.parent = weaponSpawn[5].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] == null)
			{
				children [6] = newLargeHealthToken.gameObject;
				newLargeHealthToken.transform.position = weaponSpawn[6].transform.position;
				newLargeHealthToken.transform.parent = weaponSpawn[6].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] == null)
			{
				children [7] = newLargeHealthToken.gameObject;
				newLargeHealthToken.transform.position = weaponSpawn[7].transform.position;
				newLargeHealthToken.transform.parent = weaponSpawn[7].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] == null)
			{
				children [8] = newLargeHealthToken.gameObject;
				newLargeHealthToken.transform.position = weaponSpawn[8].transform.position;
				newLargeHealthToken.transform.parent = weaponSpawn[8].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] == null)
			{
				children [9] = newLargeHealthToken.gameObject;
				newLargeHealthToken.transform.position = weaponSpawn[9].transform.position;
				newLargeHealthToken.transform.parent = weaponSpawn[9].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] != null && children [10] == null)
			{
				children [10] = newLargeHealthToken.gameObject;
				newLargeHealthToken.transform.position = weaponSpawn[10].transform.position;
				newLargeHealthToken.transform.parent = weaponSpawn[10].transform;
			}

			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] != null && children [10] != null)
			{
				if(!triggered)
				{
                    GetComponent<AudioSource>().PlayOneShot(sound2);
                    tMA.SetTrigger("Click");
					triggered = true;
					tMAB = true;
				}
			}
		}

		else if (money >= 250 && ph.fullHealth)
		{
			if(!triggered)
			{
                GetComponent<AudioSource>().PlayOneShot(sound2);
                nNA.SetTrigger("Click");
				triggered = true;
				nNAB = true;
			}
		}

		else
		{
			if(!triggered)
			{
                GetComponent<AudioSource>().PlayOneShot(sound2);
                nEA.SetTrigger("Click");
				triggered = true;
				nEAB = true;
			}
		}
	}

	public void instanShotgun()
	{
		triggered = false;
		if (money >= 1000 && !shotgunBought)
		{
			shotgunBought = true;
            shotgun.SetActive(false);
            shotgunAmmo.SetActive(true);
            GetComponent<AudioSource>().PlayOneShot(sound);
            GameObject newShotgun = Instantiate (shotgunPrefab);
			scoreManager.BuyShotgun();

			if (children [0] == null)
			{
				children [0] = newShotgun.gameObject;
				newShotgun.transform.position = weaponSpawn[0].transform.position;
				newShotgun.transform.parent = weaponSpawn[0].transform;
			}
			
			else if (children [0] != null && children [1] == null)
			{
				children [1] = newShotgun.gameObject;
				newShotgun.transform.position = weaponSpawn[1].transform.position;
				newShotgun.transform.parent = weaponSpawn[1].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] == null)
			{
				children [2] = newShotgun.gameObject;
				newShotgun.transform.position = weaponSpawn[2].transform.position;
				newShotgun.transform.parent = weaponSpawn[2].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] == null)
			{
				children [3] = newShotgun.gameObject;
				newShotgun.transform.position = weaponSpawn[3].transform.position;
				newShotgun.transform.parent = weaponSpawn[3].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] == null)
			{
				children [4] = newShotgun.gameObject;
				newShotgun.transform.position = weaponSpawn[4].transform.position;
				newShotgun.transform.parent = weaponSpawn[4].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] == null)
			{
				children [5] = newShotgun.gameObject;
				newShotgun.transform.position = weaponSpawn[5].transform.position;
				newShotgun.transform.parent = weaponSpawn[5].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] == null)
			{
				children [6] = newShotgun.gameObject;
				newShotgun.transform.position = weaponSpawn[6].transform.position;
				newShotgun.transform.parent = weaponSpawn[6].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] == null)
			{
				children [7] = newShotgun.gameObject;
				newShotgun.transform.position = weaponSpawn[7].transform.position;
				newShotgun.transform.parent = weaponSpawn[7].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] == null)
			{
				children [8] = newShotgun.gameObject;
				newShotgun.transform.position = weaponSpawn[8].transform.position;
				newShotgun.transform.parent = weaponSpawn[8].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] == null)
			{
				children [9] = newShotgun.gameObject;
				newShotgun.transform.position = weaponSpawn[9].transform.position;
				newShotgun.transform.parent = weaponSpawn[9].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] != null && children [10] == null)
			{
				children [10] = newShotgun.gameObject;
				newShotgun.transform.position = weaponSpawn[10].transform.position;
				newShotgun.transform.parent = weaponSpawn[10].transform;
			}

			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] != null && children [10] != null)
			{
				if(!triggered)
				{
                    GetComponent<AudioSource>().PlayOneShot(sound2);
                    tMA.SetTrigger("Click");
					triggered = true;
					tMAB = true;
				}
			}
		}

		else if (money >= 500 && shotgunBought && p.shotgunAmmo < 50 && ph.theShotgun)
		{
			GameObject newShotgunAmmo = Instantiate (shotgunAmmoPrefab);
			scoreManager.BuyShotgunAmmo();
            GetComponent<AudioSource>().PlayOneShot(sound);
            if (children [0] == null)
			{
				children [0] = newShotgunAmmo.gameObject;
				newShotgunAmmo.transform.position = weaponSpawn[0].transform.position;
				newShotgunAmmo.transform.parent = weaponSpawn[0].transform;
			}
			
			else if (children [0] != null && children [1] == null)
			{
				children [1] = newShotgunAmmo.gameObject;
				newShotgunAmmo.transform.position = weaponSpawn[1].transform.position;
				newShotgunAmmo.transform.parent = weaponSpawn[1].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] == null)
			{
				children [2] = newShotgunAmmo.gameObject;
				newShotgunAmmo.transform.position = weaponSpawn[2].transform.position;
				newShotgunAmmo.transform.parent = weaponSpawn[2].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] == null)
			{
				children [3] = newShotgunAmmo.gameObject;
				newShotgunAmmo.transform.position = weaponSpawn[3].transform.position;
				newShotgunAmmo.transform.parent = weaponSpawn[3].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] == null)
			{
				children [4] = newShotgunAmmo.gameObject;
				newShotgunAmmo.transform.position = weaponSpawn[4].transform.position;
				newShotgunAmmo.transform.parent = weaponSpawn[4].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] == null)
			{
				children [5] = newShotgunAmmo.gameObject;
				newShotgunAmmo.transform.position = weaponSpawn[5].transform.position;
				newShotgunAmmo.transform.parent = weaponSpawn[5].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] == null)
			{
				children [6] = newShotgunAmmo.gameObject;
				newShotgunAmmo.transform.position = weaponSpawn[6].transform.position;
				newShotgunAmmo.transform.parent = weaponSpawn[6].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] == null)
			{
				children [7] = newShotgunAmmo.gameObject;
				newShotgunAmmo.transform.position = weaponSpawn[7].transform.position;
				newShotgunAmmo.transform.parent = weaponSpawn[7].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] == null)
			{
				children [8] = newShotgunAmmo.gameObject;
				newShotgunAmmo.transform.position = weaponSpawn[8].transform.position;
				newShotgunAmmo.transform.parent = weaponSpawn[8].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] == null)
			{
				children [9] = newShotgunAmmo.gameObject;
				newShotgunAmmo.transform.position = weaponSpawn[9].transform.position;
				newShotgunAmmo.transform.parent = weaponSpawn[9].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] != null && children [10] == null)
			{
				children [10] = newShotgunAmmo.gameObject;
				newShotgunAmmo.transform.position = weaponSpawn[10].transform.position;
				newShotgunAmmo.transform.parent = weaponSpawn[10].transform;
			}

			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] != null && children [10] != null)
			{
				if(!triggered)
				{
                    GetComponent<AudioSource>().PlayOneShot(sound2);
                    tMA.SetTrigger("Click");
					triggered = true;
					tMAB = true;
				}
			}
		}

		else if (money >= 500 && shotgunBought && p.shotgunAmmo == 0 && !ph.theShotgun)
		{
			if(!triggered)
			{
                GetComponent<AudioSource>().PlayOneShot(sound2);
                nNA.SetTrigger("Click");
				triggered = true;
				nNAB = true;
			}
		}

		else if (money >= 500 && shotgunBought && p.shotgunAmmo == 50)
		{
			if(!triggered)
			{
                GetComponent<AudioSource>().PlayOneShot(sound2);
                nNA.SetTrigger("Click");
				triggered = true;
				nNAB = true;
			}
		}

		else if (money < 1000 && !shotgunBought)
		{
			if(!triggered)
			{
                GetComponent<AudioSource>().PlayOneShot(sound2);
                nEA.SetTrigger("Click");
				triggered = true;
				nEAB = true;
			}
		}

        else if (money < 500 && shotgunBought)
        {
            if (!triggered)
            {
                GetComponent<AudioSource>().PlayOneShot(sound2);
                nEA.SetTrigger("Click");
                triggered = true;
                nEAB = true;
            }
        }
    }

	public void instanpzooka()
	{
		triggered = false;
		if (money >= 1000 && !bazookaBought) 
		{
			bazookaBought = true;
            bazooka.SetActive(false);
            bazookaAmmo.SetActive(true);
            GetComponent<AudioSource>().PlayOneShot(sound);
            GameObject newpzooka = Instantiate (pzookaPrefab);
			scoreManager.BuyBazooka();

			if (children [0] == null)
			{
				children [0] = newpzooka.gameObject;
				newpzooka.transform.position = weaponSpawn[0].transform.position;
				newpzooka.transform.parent = weaponSpawn[0].transform;
			}
			
			else if (children [0] != null && children [1] == null)
			{
				children [1] = newpzooka.gameObject;
				newpzooka.transform.position = weaponSpawn[1].transform.position;
				newpzooka.transform.parent = weaponSpawn[1].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] == null)
			{
				children [2] = newpzooka.gameObject;
				newpzooka.transform.position = weaponSpawn[2].transform.position;
				newpzooka.transform.parent = weaponSpawn[2].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] == null)
			{
				children [3] = newpzooka.gameObject;
				newpzooka.transform.position = weaponSpawn[3].transform.position;
				newpzooka.transform.parent = weaponSpawn[3].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] == null)
			{
				children [4] = newpzooka.gameObject;
				newpzooka.transform.position = weaponSpawn[4].transform.position;
				newpzooka.transform.parent = weaponSpawn[4].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] == null)
			{
				children [5] = newpzooka.gameObject;
				newpzooka.transform.position = weaponSpawn[5].transform.position;
				newpzooka.transform.parent = weaponSpawn[5].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] == null)
			{
				children [6] = newpzooka.gameObject;
				newpzooka.transform.position = weaponSpawn[6].transform.position;
				newpzooka.transform.parent = weaponSpawn[6].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] == null)
			{
				children [7] = newpzooka.gameObject;
				newpzooka.transform.position = weaponSpawn[7].transform.position;
				newpzooka.transform.parent = weaponSpawn[7].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] == null)
			{
				children [8] = newpzooka.gameObject;
				newpzooka.transform.position = weaponSpawn[8].transform.position;
				newpzooka.transform.parent = weaponSpawn[8].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] == null)
			{
				children [9] = newpzooka.gameObject;
				newpzooka.transform.position = weaponSpawn[9].transform.position;
				newpzooka.transform.parent = weaponSpawn[9].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] != null && children [10] == null)
			{
				children [10] = newpzooka.gameObject;
				newpzooka.transform.position = weaponSpawn[10].transform.position;
				newpzooka.transform.parent = weaponSpawn[10].transform;
			}

			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] != null && children [10] != null)
			{
				if(!triggered)
				{
                    GetComponent<AudioSource>().PlayOneShot(sound2);
                    tMA.SetTrigger("Click");
					triggered = true;
					tMAB = true;
				}
			}
		} 

		else if (money >= 500 && bazookaBought && p.bazookaAmmo < 20 && ph.theLauncher)
		{
			GameObject newpzookaAmmo = Instantiate (pzookaAmmoPrefab);
			scoreManager.BuyBazookaAmmo();
            GetComponent<AudioSource>().PlayOneShot(sound);
            if (children [0] == null)
			{
				children [0] = newpzookaAmmo.gameObject;
				newpzookaAmmo.transform.position = weaponSpawn[0].transform.position;
				newpzookaAmmo.transform.parent = weaponSpawn[0].transform;
			}
			
			else if (children [0] != null && children [1] == null)
			{
				children [1] = newpzookaAmmo.gameObject;
				newpzookaAmmo.transform.position = weaponSpawn[1].transform.position;
				newpzookaAmmo.transform.parent = weaponSpawn[1].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] == null)
			{
				children [2] = newpzookaAmmo.gameObject;
				newpzookaAmmo.transform.position = weaponSpawn[2].transform.position;
				newpzookaAmmo.transform.parent = weaponSpawn[2].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] == null)
			{
				children [3] = newpzookaAmmo.gameObject;
				newpzookaAmmo.transform.position = weaponSpawn[3].transform.position;
				newpzookaAmmo.transform.parent = weaponSpawn[3].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] == null)
			{
				children [4] = newpzookaAmmo.gameObject;
				newpzookaAmmo.transform.position = weaponSpawn[4].transform.position;
				newpzookaAmmo.transform.parent = weaponSpawn[4].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] == null)
			{
				children [5] = newpzookaAmmo.gameObject;
				newpzookaAmmo.transform.position = weaponSpawn[5].transform.position;
				newpzookaAmmo.transform.parent = weaponSpawn[5].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] == null)
			{
				children [6] = newpzookaAmmo.gameObject;
				newpzookaAmmo.transform.position = weaponSpawn[6].transform.position;
				newpzookaAmmo.transform.parent = weaponSpawn[6].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] == null)
			{
				children [7] = newpzookaAmmo.gameObject;
				newpzookaAmmo.transform.position = weaponSpawn[7].transform.position;
				newpzookaAmmo.transform.parent = weaponSpawn[7].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] == null)
			{
				children [8] = newpzookaAmmo.gameObject;
				newpzookaAmmo.transform.position = weaponSpawn[8].transform.position;
				newpzookaAmmo.transform.parent = weaponSpawn[8].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] == null)
			{
				children [9] = newpzookaAmmo.gameObject;
				newpzookaAmmo.transform.position = weaponSpawn[9].transform.position;
				newpzookaAmmo.transform.parent = weaponSpawn[9].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] != null && children [10] == null)
			{
				children [10] = newpzookaAmmo.gameObject;
				newpzookaAmmo.transform.position = weaponSpawn[10].transform.position;
				newpzookaAmmo.transform.parent = weaponSpawn[10].transform;
			}

			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] != null && children [10] != null)
			{
				if(!triggered)
				{
                    GetComponent<AudioSource>().PlayOneShot(sound2);
                    tMA.SetTrigger("Click");
					triggered = true;
					tMAB = true;
				}
			}
		}

		else if (money >= 500 && bazookaBought && p.bazookaAmmo == 0 && !ph.theLauncher)
		{
			if(!triggered)
			{
                GetComponent<AudioSource>().PlayOneShot(sound2);
                nNA.SetTrigger("Click");
				triggered = true;
				nNAB = true;
			}
		}

		else if (money >= 500 && bazookaBought && p.bazookaAmmo == 20)
		{
			if(!triggered)
			{
                GetComponent<AudioSource>().PlayOneShot(sound2);
                nNA.SetTrigger("Click");
				triggered = true;
				nNAB = true;
			}
		}

		else if (money < 1000 && !bazookaBought)
		{
			if(!triggered)
			{
                GetComponent<AudioSource>().PlayOneShot(sound2);
                nEA.SetTrigger("Click");
				triggered = true;
				nEAB = true;
			}
		}

        else if (money < 500 && bazookaBought)
        {
            if (!triggered)
            {
                GetComponent<AudioSource>().PlayOneShot(sound2);
                nEA.SetTrigger("Click");
                triggered = true;
                nEAB = true;
            }
        }
    }

	public void instanMachinegun()
	{
		triggered = false;
		if (money >= 3000 && !machinegunBought)
		{
			machinegunBought = true;
            machinegun.SetActive(false);
            machinegunAmmo.SetActive(true);
            GetComponent<AudioSource>().PlayOneShot(sound);
            GameObject newmachinegun = Instantiate (machinegunPrefab);
			scoreManager.BuyMachinegun();

			if (children [0] == null)
			{
				children [0] = newmachinegun.gameObject;
				newmachinegun.transform.position = weaponSpawn[0].transform.position;
				newmachinegun.transform.parent = weaponSpawn[0].transform;
			}
			
			else if (children [0] != null && children [1] == null)
			{
				children [1] = newmachinegun.gameObject;
				newmachinegun.transform.position = weaponSpawn[1].transform.position;
				newmachinegun.transform.parent = weaponSpawn[1].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] == null)
			{
				children [2] = newmachinegun.gameObject;
				newmachinegun.transform.position = weaponSpawn[2].transform.position;
				newmachinegun.transform.parent = weaponSpawn[2].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] == null)
			{
				children [3] = newmachinegun.gameObject;
				newmachinegun.transform.position = weaponSpawn[3].transform.position;
				newmachinegun.transform.parent = weaponSpawn[3].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] == null)
			{
				children [4] = newmachinegun.gameObject;
				newmachinegun.transform.position = weaponSpawn[4].transform.position;
				newmachinegun.transform.parent = weaponSpawn[4].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] == null)
			{
				children [5] = newmachinegun.gameObject;
				newmachinegun.transform.position = weaponSpawn[5].transform.position;
				newmachinegun.transform.parent = weaponSpawn[5].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] == null)
			{
				children [6] = newmachinegun.gameObject;
				newmachinegun.transform.position = weaponSpawn[6].transform.position;
				newmachinegun.transform.parent = weaponSpawn[6].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] == null)
			{
				children [7] = newmachinegun.gameObject;
				newmachinegun.transform.position = weaponSpawn[7].transform.position;
				newmachinegun.transform.parent = weaponSpawn[7].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] == null)
			{
				children [8] = newmachinegun.gameObject;
				newmachinegun.transform.position = weaponSpawn[8].transform.position;
				newmachinegun.transform.parent = weaponSpawn[8].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] == null)
			{
				children [9] = newmachinegun.gameObject;
				newmachinegun.transform.position = weaponSpawn[9].transform.position;
				newmachinegun.transform.parent = weaponSpawn[9].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] != null && children [10] == null)
			{
				children [10] = newmachinegun.gameObject;
				newmachinegun.transform.position = weaponSpawn[10].transform.position;
				newmachinegun.transform.parent = weaponSpawn[10].transform;
			}

			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] != null && children [10] != null)
			{
				if(!triggered)
				{
                    GetComponent<AudioSource>().PlayOneShot(sound2);
                    tMA.SetTrigger("Click");
					triggered = true;
					tMAB = true;
				}
			}
		}

		else if (money >= 1500 && machinegunBought && p.machinegunAmmo < 1000 && ph.theMachinegun)
		{
			GameObject newMachinegunAmmo = Instantiate (machinegunAmmoPrefab);
			scoreManager.BuyMachinegunAmmo();
            GetComponent<AudioSource>().PlayOneShot(sound);
            if (children [0] == null)
			{
				children [0] = newMachinegunAmmo.gameObject;
				newMachinegunAmmo.transform.position = weaponSpawn[0].transform.position;
				newMachinegunAmmo.transform.parent = weaponSpawn[0].transform;
			}
			
			else if (children [0] != null && children [1] == null)
			{
				children [1] = newMachinegunAmmo.gameObject;
				newMachinegunAmmo.transform.position = weaponSpawn[1].transform.position;
				newMachinegunAmmo.transform.parent = weaponSpawn[1].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] == null)
			{
				children [2] = newMachinegunAmmo.gameObject;
				newMachinegunAmmo.transform.position = weaponSpawn[2].transform.position;
				newMachinegunAmmo.transform.parent = weaponSpawn[2].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] == null)
			{
				children [3] = newMachinegunAmmo.gameObject;
				newMachinegunAmmo.transform.position = weaponSpawn[3].transform.position;
				newMachinegunAmmo.transform.parent = weaponSpawn[3].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] == null)
			{
				children [4] = newMachinegunAmmo.gameObject;
				newMachinegunAmmo.transform.position = weaponSpawn[4].transform.position;
				newMachinegunAmmo.transform.parent = weaponSpawn[4].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] == null)
			{
				children [5] = newMachinegunAmmo.gameObject;
				newMachinegunAmmo.transform.position = weaponSpawn[5].transform.position;
				newMachinegunAmmo.transform.parent = weaponSpawn[5].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] == null)
			{
				children [6] = newMachinegunAmmo.gameObject;
				newMachinegunAmmo.transform.position = weaponSpawn[6].transform.position;
				newMachinegunAmmo.transform.parent = weaponSpawn[6].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] == null)
			{
				children [7] = newMachinegunAmmo.gameObject;
				newMachinegunAmmo.transform.position = weaponSpawn[7].transform.position;
				newMachinegunAmmo.transform.parent = weaponSpawn[7].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] == null)
			{
				children [8] = newMachinegunAmmo.gameObject;
				newMachinegunAmmo.transform.position = weaponSpawn[8].transform.position;
				newMachinegunAmmo.transform.parent = weaponSpawn[8].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] == null)
			{
				children [9] = newMachinegunAmmo.gameObject;
				newMachinegunAmmo.transform.position = weaponSpawn[9].transform.position;
				newMachinegunAmmo.transform.parent = weaponSpawn[9].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] != null && children [10] == null)
			{
				children [10] = newMachinegunAmmo.gameObject;
				newMachinegunAmmo.transform.position = weaponSpawn[10].transform.position;
				newMachinegunAmmo.transform.parent = weaponSpawn[10].transform;
			}

			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] != null && children [10] != null)
			{
				if(!triggered)
				{
                    GetComponent<AudioSource>().PlayOneShot(sound2);
                    tMA.SetTrigger("Click");
					triggered = true;
					tMAB = true;
				}
			}
		}

		else if (money >= 1500 && machinegunBought && p.machinegunAmmo == 0 && !ph.theMachinegun)
		{
			if(!triggered)
			{
                GetComponent<AudioSource>().PlayOneShot(sound2);
                nNA.SetTrigger("Click");
				triggered = true;
				nNAB = true;
			}
		}

		else if (money >= 1500 && machinegunBought && p.machinegunAmmo == 1000)
		{
			if(!triggered)
			{
                GetComponent<AudioSource>().PlayOneShot(sound2);
                nNA.SetTrigger("Click");
				triggered = true;
				nNAB = true;
			}
		}

		else if (money < 3000 && !machinegunBought)
		{
			if(!triggered)
			{
                GetComponent<AudioSource>().PlayOneShot(sound2);
                nEA.SetTrigger("Click");
				triggered = true;
				nEAB = true;
			}
		}

        else if (money < 1500 && machinegunBought)
        {
            if (!triggered)
            {
                GetComponent<AudioSource>().PlayOneShot(sound2);
                nEA.SetTrigger("Click");
                triggered = true;
                nEAB = true;
            }
        }
    }

	public void instanstaff()
	{
		triggered = false;
		if (money >= 1500 && !staffBought)
		{
			staffBought = true;
            staff.SetActive(false);
            staffAmmo.SetActive(true);
            GetComponent<AudioSource>().PlayOneShot(sound);
            GameObject newstaff = Instantiate (staffPrefab);
			scoreManager.BuyStaff();

			if (children [0] == null)
			{
				children [0] = newstaff.gameObject;
				newstaff.transform.position = weaponSpawn[0].transform.position;
				newstaff.transform.parent = weaponSpawn[0].transform;
			}
			
			else if (children [0] != null && children [1] == null)
			{
				children [1] = newstaff.gameObject;
				newstaff.transform.position = weaponSpawn[1].transform.position;
				newstaff.transform.parent = weaponSpawn[1].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] == null)
			{
				children [2] = newstaff.gameObject;
				newstaff.transform.position = weaponSpawn[2].transform.position;
				newstaff.transform.parent = weaponSpawn[2].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] == null)
			{
				children [3] = newstaff.gameObject;
				newstaff.transform.position = weaponSpawn[3].transform.position;
				newstaff.transform.parent = weaponSpawn[3].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] == null)
			{
				children [4] = newstaff.gameObject;
				newstaff.transform.position = weaponSpawn[4].transform.position;
				newstaff.transform.parent = weaponSpawn[4].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] == null)
			{
				children [5] = newstaff.gameObject;
				newstaff.transform.position = weaponSpawn[5].transform.position;
				newstaff.transform.parent = weaponSpawn[5].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] == null)
			{
				children [6] = newstaff.gameObject;
				newstaff.transform.position = weaponSpawn[6].transform.position;
				newstaff.transform.parent = weaponSpawn[6].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] == null)
			{
				children [7] = newstaff.gameObject;
				newstaff.transform.position = weaponSpawn[7].transform.position;
				newstaff.transform.parent = weaponSpawn[7].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] == null)
			{
				children [8] = newstaff.gameObject;
				newstaff.transform.position = weaponSpawn[8].transform.position;
				newstaff.transform.parent = weaponSpawn[8].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] == null)
			{
				children [9] = newstaff.gameObject;
				newstaff.transform.position = weaponSpawn[9].transform.position;
				newstaff.transform.parent = weaponSpawn[9].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] != null && children [10] == null)
			{
				children [10] = newstaff.gameObject;
				newstaff.transform.position = weaponSpawn[10].transform.position;
				newstaff.transform.parent = weaponSpawn[10].transform;
			}

			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] != null && children [10] != null)
			{
				if(!triggered)
				{
                    GetComponent<AudioSource>().PlayOneShot(sound2);
                    tMA.SetTrigger("Click");
					triggered = true;
					tMAB = true;
				}
			}
		}

		else if (money >= 750 && staffBought && p.staffAmmo < 40 && ph.theStaff)
		{
			GameObject newStaffAmmo = Instantiate (staffAmmoPrefab);
			scoreManager.BuyStaffAmmo();
            GetComponent<AudioSource>().PlayOneShot(sound);
            if (children [0] == null)
			{
				children [0] = newStaffAmmo.gameObject;
				newStaffAmmo.transform.position = weaponSpawn[0].transform.position;
				newStaffAmmo.transform.parent = weaponSpawn[0].transform;
			}
			
			else if (children [0] != null && children [1] == null)
			{
				children [1] = newStaffAmmo.gameObject;
				newStaffAmmo.transform.position = weaponSpawn[1].transform.position;
				newStaffAmmo.transform.parent = weaponSpawn[1].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] == null)
			{
				children [2] = newStaffAmmo.gameObject;
				newStaffAmmo.transform.position = weaponSpawn[2].transform.position;
				newStaffAmmo.transform.parent = weaponSpawn[2].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] == null)
			{
				children [3] = newStaffAmmo.gameObject;
				newStaffAmmo.transform.position = weaponSpawn[3].transform.position;
				newStaffAmmo.transform.parent = weaponSpawn[3].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] == null)
			{
				children [4] = newStaffAmmo.gameObject;
				newStaffAmmo.transform.position = weaponSpawn[4].transform.position;
				newStaffAmmo.transform.parent = weaponSpawn[4].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] == null)
			{
				children [5] = newStaffAmmo.gameObject;
				newStaffAmmo.transform.position = weaponSpawn[5].transform.position;
				newStaffAmmo.transform.parent = weaponSpawn[5].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] == null)
			{
				children [6] = newStaffAmmo.gameObject;
				newStaffAmmo.transform.position = weaponSpawn[6].transform.position;
				newStaffAmmo.transform.parent = weaponSpawn[6].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] == null)
			{
				children [7] = newStaffAmmo.gameObject;
				newStaffAmmo.transform.position = weaponSpawn[7].transform.position;
				newStaffAmmo.transform.parent = weaponSpawn[7].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] == null)
			{
				children [8] = newStaffAmmo.gameObject;
				newStaffAmmo.transform.position = weaponSpawn[8].transform.position;
				newStaffAmmo.transform.parent = weaponSpawn[8].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] == null)
			{
				children [9] = newStaffAmmo.gameObject;
				newStaffAmmo.transform.position = weaponSpawn[9].transform.position;
				newStaffAmmo.transform.parent = weaponSpawn[9].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] != null && children [10] == null)
			{
				children [10] = newStaffAmmo.gameObject;
				newStaffAmmo.transform.position = weaponSpawn[10].transform.position;
				newStaffAmmo.transform.parent = weaponSpawn[10].transform;
			}

			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] != null && children [10] != null)
			{
				if(!triggered)
				{
                    GetComponent<AudioSource>().PlayOneShot(sound2);
                    tMA.SetTrigger("Click");
					triggered = true;
					tMAB = true;
				}
			}
		}

		else if (money >= 750 && staffBought && p.staffAmmo == 0 && !ph.theStaff)
		{
			if(!triggered)
			{
                GetComponent<AudioSource>().PlayOneShot(sound2);
                nNA.SetTrigger("Click");
				triggered = true;
				nNAB = true;
			}
		}

		else if (money >= 750 && staffBought && p.staffAmmo == 40)
		{
			if(!triggered)
			{
                GetComponent<AudioSource>().PlayOneShot(sound2);
                nNA.SetTrigger("Click");
				triggered = true;
				nNAB = true;
			}
		}

		else if (money < 1500 && !staffBought)
		{
			if(!triggered)
			{
                GetComponent<AudioSource>().PlayOneShot(sound2);
                nEA.SetTrigger("Click");
				triggered = true;
				nEAB = true;
			}
		}

        else if (money < 750 && staffBought)
        {
            if (!triggered)
            {
                GetComponent<AudioSource>().PlayOneShot(sound2);
                nEA.SetTrigger("Click");
                triggered = true;
                nEAB = true;
            }
        }
    }

	public void instangernade()
	{
		triggered = false;
		if (money >= 500 && !granadeBought) 
		{
			granadeBought = true;
            granade.SetActive(false);
            granadeAmmo.SetActive(true);
            GetComponent<AudioSource>().PlayOneShot(sound);
            GameObject newGranade = Instantiate (granadePrefab);
			scoreManager.BuyGranade();

			if (children [0] == null)
			{
				children [0] = newGranade.gameObject;
				newGranade.transform.position = weaponSpawn[0].transform.position;
				newGranade.transform.parent = weaponSpawn[0].transform;
			}
			
			else if (children [0] != null && children [1] == null)
			{
				children [1] = newGranade.gameObject;
				newGranade.transform.position = weaponSpawn[1].transform.position;
				newGranade.transform.parent = weaponSpawn[1].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] == null)
			{
				children [2] = newGranade.gameObject;
				newGranade.transform.position = weaponSpawn[2].transform.position;
				newGranade.transform.parent = weaponSpawn[2].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] == null)
			{
				children [3] = newGranade.gameObject;
				newGranade.transform.position = weaponSpawn[3].transform.position;
				newGranade.transform.parent = weaponSpawn[3].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] == null)
			{
				children [4] = newGranade.gameObject;
				newGranade.transform.position = weaponSpawn[4].transform.position;
				newGranade.transform.parent = weaponSpawn[4].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] == null)
			{
				children [5] = newGranade.gameObject;
				newGranade.transform.position = weaponSpawn[5].transform.position;
				newGranade.transform.parent = weaponSpawn[5].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] == null)
			{
				children [6] = newGranade.gameObject;
				newGranade.transform.position = weaponSpawn[6].transform.position;
				newGranade.transform.parent = weaponSpawn[6].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] == null)
			{
				children [7] = newGranade.gameObject;
				newGranade.transform.position = weaponSpawn[7].transform.position;
				newGranade.transform.parent = weaponSpawn[7].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] == null)
			{
				children [8] = newGranade.gameObject;
				newGranade.transform.position = weaponSpawn[8].transform.position;
				newGranade.transform.parent = weaponSpawn[8].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] == null)
			{
				children [9] = newGranade.gameObject;
				newGranade.transform.position = weaponSpawn[9].transform.position;
				newGranade.transform.parent = weaponSpawn[9].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] != null && children [10] == null)
			{
				children [10] = newGranade.gameObject;
				newGranade.transform.position = weaponSpawn[10].transform.position;
				newGranade.transform.parent = weaponSpawn[10].transform;
			}

			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] != null && children [10] != null)
			{
				if(!triggered)
				{
                    GetComponent<AudioSource>().PlayOneShot(sound2);
                    tMA.SetTrigger("Click");
					triggered = true;
					tMAB = true;
				}
			}
		}

		else if (money >= 250 && granadeBought && p.granadeAmmo < 30 && ph.theGranade)
		{
			GameObject newGranadeAmmo = Instantiate (granadeAmmoPrefab);
			scoreManager.BuyGranadeAmmo();
            GetComponent<AudioSource>().PlayOneShot(sound);
            if (children [0] == null)
			{
				children [0] = newGranadeAmmo.gameObject;
				newGranadeAmmo.transform.position = weaponSpawn[0].transform.position;
				newGranadeAmmo.transform.parent = weaponSpawn[0].transform;
			}
			
			else if (children [0] != null && children [1] == null)
			{
				children [1] = newGranadeAmmo.gameObject;
				newGranadeAmmo.transform.position = weaponSpawn[1].transform.position;
				newGranadeAmmo.transform.parent = weaponSpawn[1].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] == null)
			{
				children [2] = newGranadeAmmo.gameObject;
				newGranadeAmmo.transform.position = weaponSpawn[2].transform.position;
				newGranadeAmmo.transform.parent = weaponSpawn[2].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] == null)
			{
				children [3] = newGranadeAmmo.gameObject;
				newGranadeAmmo.transform.position = weaponSpawn[3].transform.position;
				newGranadeAmmo.transform.parent = weaponSpawn[3].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] == null)
			{
				children [4] = newGranadeAmmo.gameObject;
				newGranadeAmmo.transform.position = weaponSpawn[4].transform.position;
				newGranadeAmmo.transform.parent = weaponSpawn[4].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] == null)
			{
				children [5] = newGranadeAmmo.gameObject;
				newGranadeAmmo.transform.position = weaponSpawn[5].transform.position;
				newGranadeAmmo.transform.parent = weaponSpawn[5].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] == null)
			{
				children [6] = newGranadeAmmo.gameObject;
				newGranadeAmmo.transform.position = weaponSpawn[6].transform.position;
				newGranadeAmmo.transform.parent = weaponSpawn[6].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] == null)
			{
				children [7] = newGranadeAmmo.gameObject;
				newGranadeAmmo.transform.position = weaponSpawn[7].transform.position;
				newGranadeAmmo.transform.parent = weaponSpawn[7].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] == null)
			{
				children [8] = newGranadeAmmo.gameObject;
				newGranadeAmmo.transform.position = weaponSpawn[8].transform.position;
				newGranadeAmmo.transform.parent = weaponSpawn[8].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] == null)
			{
				children [9] = newGranadeAmmo.gameObject;
				newGranadeAmmo.transform.position = weaponSpawn[9].transform.position;
				newGranadeAmmo.transform.parent = weaponSpawn[9].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] != null && children [10] == null)
			{
				children [10] = newGranadeAmmo.gameObject;
				newGranadeAmmo.transform.position = weaponSpawn[10].transform.position;
				newGranadeAmmo.transform.parent = weaponSpawn[10].transform;
			}

			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] != null && children [10] != null)
			{
				if(!triggered)
				{
                    GetComponent<AudioSource>().PlayOneShot(sound2);
                    tMA.SetTrigger("Click");
					triggered = true;
					tMAB = true;
				}
			}
		}

		else if (money >= 250 && granadeBought && p.granadeAmmo == 0 && !ph.theGranade)
		{
			if(!triggered)
			{
                GetComponent<AudioSource>().PlayOneShot(sound2);
                nNA.SetTrigger("Click");
				triggered = true;
				nNAB = true;
			}
		}

		else if (money >= 250 && granadeBought && p.granadeAmmo == 30)
		{
			if(!triggered)
			{
                GetComponent<AudioSource>().PlayOneShot(sound2);
                nNA.SetTrigger("Click");
				triggered = true;
				nNAB = true;
			}
		}

		else if (money < 500 && !granadeBought)
		{
			if(!triggered)
			{
                GetComponent<AudioSource>().PlayOneShot(sound2);
                nEA.SetTrigger("Click");
				triggered = true;
				nEAB = true;
			}
		}

        else if (money < 250 && granadeBought)
        {
            if (!triggered)
            {
                GetComponent<AudioSource>().PlayOneShot(sound2);
                nEA.SetTrigger("Click");
                triggered = true;
                nEAB = true;
            }
        }
    }

	public void instanAmmo()
	{
		triggered = false;
		if (money >= 250)
		{
			GameObject newswordobject = Instantiate (ammoPrefab);
			scoreManager.BuyAmmo();
            GetComponent<AudioSource>().PlayOneShot(sound);
            if (children [0] == null)
			{
				children [0] = newswordobject.gameObject;
				newswordobject.transform.position = weaponSpawn[0].transform.position;
				newswordobject.transform.parent = weaponSpawn[0].transform;
			}
			
			else if (children [0] != null && children [1] == null)
			{
				children [1] = newswordobject.gameObject;
				newswordobject.transform.position = weaponSpawn[1].transform.position;
				newswordobject.transform.parent = weaponSpawn[1].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] == null)
			{
				children [2] = newswordobject.gameObject;
				newswordobject.transform.position = weaponSpawn[2].transform.position;
				newswordobject.transform.parent = weaponSpawn[2].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] == null)
			{
				children [3] = newswordobject.gameObject;
				newswordobject.transform.position = weaponSpawn[3].transform.position;
				newswordobject.transform.parent = weaponSpawn[3].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] == null)
			{
				children [4] = newswordobject.gameObject;
				newswordobject.transform.position = weaponSpawn[4].transform.position;
				newswordobject.transform.parent = weaponSpawn[4].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] == null)
			{
				children [5] = newswordobject.gameObject;
				newswordobject.transform.position = weaponSpawn[5].transform.position;
				newswordobject.transform.parent = weaponSpawn[5].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] == null)
			{
				children [6] = newswordobject.gameObject;
				newswordobject.transform.position = weaponSpawn[6].transform.position;
				newswordobject.transform.parent = weaponSpawn[6].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] == null)
			{
				children [7] = newswordobject.gameObject;
				newswordobject.transform.position = weaponSpawn[7].transform.position;
				newswordobject.transform.parent = weaponSpawn[7].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] == null)
			{
				children [8] = newswordobject.gameObject;
				newswordobject.transform.position = weaponSpawn[8].transform.position;
				newswordobject.transform.parent = weaponSpawn[8].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] == null)
			{
				children [9] = newswordobject.gameObject;
				newswordobject.transform.position = weaponSpawn[9].transform.position;
				newswordobject.transform.parent = weaponSpawn[9].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] != null && children [10] == null)
			{
				children [10] = newswordobject.gameObject;
				newswordobject.transform.position = weaponSpawn[10].transform.position;
				newswordobject.transform.parent = weaponSpawn[10].transform;
			}

			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] != null && children [10] != null)
			{
				if(!triggered)
				{
                    GetComponent<AudioSource>().PlayOneShot(sound2);
                    tMA.SetBool("Click", true);
					triggered = true;
					tMAB = true;
				}
			}
		}
		
		else if (money < 250)
		{
			if(!triggered)
			{
                GetComponent<AudioSource>().PlayOneShot(sound2);
                nEA.SetBool("Click", true);
				triggered = true;
				nEAB = true;
			}
		}
	}

	public void instanBoom()
	{
		triggered = false;
		if (money >= 750 && !boomerangBought)
		{
			boomerangBought = true;
			GameObject newboomerangobject = Instantiate (boomerangPrefab);
			scoreManager.BuyBoomerang();
            boomerang.SetActive(false);
            GetComponent<AudioSource>().PlayOneShot(sound);
            if (children [0] == null)
			{
				children [0] = newboomerangobject.gameObject;
				newboomerangobject.transform.position = weaponSpawn[0].transform.position;
				newboomerangobject.transform.parent = weaponSpawn[0].transform;
			}
			
			else if (children [0] != null && children [1] == null)
			{
				children [1] = newboomerangobject.gameObject;
				newboomerangobject.transform.position = weaponSpawn[1].transform.position;
				newboomerangobject.transform.parent = weaponSpawn[1].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] == null)
			{
				children [2] = newboomerangobject.gameObject;
				newboomerangobject.transform.position = weaponSpawn[2].transform.position;
				newboomerangobject.transform.parent = weaponSpawn[2].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] == null)
			{
				children [3] = newboomerangobject.gameObject;
				newboomerangobject.transform.position = weaponSpawn[3].transform.position;
				newboomerangobject.transform.parent = weaponSpawn[3].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] == null)
			{
				children [4] = newboomerangobject.gameObject;
				newboomerangobject.transform.position = weaponSpawn[4].transform.position;
				newboomerangobject.transform.parent = weaponSpawn[4].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] == null)
			{
				children [5] = newboomerangobject.gameObject;
				newboomerangobject.transform.position = weaponSpawn[5].transform.position;
				newboomerangobject.transform.parent = weaponSpawn[5].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] == null)
			{
				children [6] = newboomerangobject.gameObject;
				newboomerangobject.transform.position = weaponSpawn[6].transform.position;
				newboomerangobject.transform.parent = weaponSpawn[6].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] == null)
			{
				children [7] = newboomerangobject.gameObject;
				newboomerangobject.transform.position = weaponSpawn[7].transform.position;
				newboomerangobject.transform.parent = weaponSpawn[7].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] == null)
			{
				children [8] = newboomerangobject.gameObject;
				newboomerangobject.transform.position = weaponSpawn[8].transform.position;
				newboomerangobject.transform.parent = weaponSpawn[8].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] == null)
			{
				children [9] = newboomerangobject.gameObject;
				newboomerangobject.transform.position = weaponSpawn[9].transform.position;
				newboomerangobject.transform.parent = weaponSpawn[9].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] != null && children [10] == null)
			{
				children [10] = newboomerangobject.gameObject;
				newboomerangobject.transform.position = weaponSpawn[10].transform.position;
				newboomerangobject.transform.parent = weaponSpawn[10].transform;
			}

			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] != null && children [10] != null)
			{
				if(!triggered)
				{
                    GetComponent<AudioSource>().PlayOneShot(sound2);
                    tMA.SetTrigger("Click");
					triggered = true;
					tMAB = true;
				}
			}
		}

		else if (boomerangBought)
		{
			if(!triggered)
			{
                GetComponent<AudioSource>().PlayOneShot(sound2);
                nNA.SetTrigger("Click");
				triggered = true;
				nNAB = true;
			}
		}
		
		else if (money < 750 && !boomerangBought)
		{
			if(!triggered)
			{
                GetComponent<AudioSource>().PlayOneShot(sound2);
                nEA.SetTrigger("Click");
				triggered = true;
				nEAB = true;
			}
		}
	}

	public void instanLvlUp()
	{
		triggered = false;
		if (money >= 1000 && ph.level < 10 && levelUps <= 10)
		{
			levelUps++;
			GameObject newlevelobject = Instantiate (levelPrefab);
			scoreManager.BuyLevelUp();
            GetComponent<AudioSource>().PlayOneShot(sound);
            if (children [0] == null)
			{
				children [0] = newlevelobject.gameObject;
				newlevelobject.transform.position = weaponSpawn[0].transform.position;
				newlevelobject.transform.parent = weaponSpawn[0].transform;
			}
			
			else if (children [0] != null && children [1] == null)
			{
				children [1] = newlevelobject.gameObject;
				newlevelobject.transform.position = weaponSpawn[1].transform.position;
				newlevelobject.transform.parent = weaponSpawn[1].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] == null)
			{
				children [2] = newlevelobject.gameObject;
				newlevelobject.transform.position = weaponSpawn[2].transform.position;
				newlevelobject.transform.parent = weaponSpawn[2].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] == null)
			{
				children [3] = newlevelobject.gameObject;
				newlevelobject.transform.position = weaponSpawn[3].transform.position;
				newlevelobject.transform.parent = weaponSpawn[3].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] == null)
			{
				children [4] = newlevelobject.gameObject;
				newlevelobject.transform.position = weaponSpawn[4].transform.position;
				newlevelobject.transform.parent = weaponSpawn[4].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] == null)
			{
				children [5] = newlevelobject.gameObject;
				newlevelobject.transform.position = weaponSpawn[5].transform.position;
				newlevelobject.transform.parent = weaponSpawn[5].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] == null)
			{
				children [6] = newlevelobject.gameObject;
				newlevelobject.transform.position = weaponSpawn[6].transform.position;
				newlevelobject.transform.parent = weaponSpawn[6].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] == null)
			{
				children [7] = newlevelobject.gameObject;
				newlevelobject.transform.position = weaponSpawn[7].transform.position;
				newlevelobject.transform.parent = weaponSpawn[7].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] == null)
			{
				children [8] = newlevelobject.gameObject;
				newlevelobject.transform.position = weaponSpawn[8].transform.position;
				newlevelobject.transform.parent = weaponSpawn[8].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] == null)
			{
				children [9] = newlevelobject.gameObject;
				newlevelobject.transform.position = weaponSpawn[9].transform.position;
				newlevelobject.transform.parent = weaponSpawn[9].transform;
			}
			
			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] != null && children [10] == null)
			{
				children [10] = newlevelobject.gameObject;
				newlevelobject.transform.position = weaponSpawn[10].transform.position;
				newlevelobject.transform.parent = weaponSpawn[10].transform;
			}

			else if (children [0] != null && children [1] != null && children [2] != null && children [3] != null && children [4] != null && children [5] != null && children [6] != null && children [7] != null && children [8] != null && children [9] != null && children [10] != null)
			{
				if(!triggered)
				{
                    GetComponent<AudioSource>().PlayOneShot(sound2);
                    tMA.SetTrigger("Click");
					triggered = true;
					tMAB = true;
				}
			}
		}

		else if (ph.level >= 10 || levelUps > 10)
		{
			if(!triggered)
			{
                GetComponent<AudioSource>().PlayOneShot(sound2);
                nLA.SetTrigger("Click");
				triggered = true;
				nLAB = true;
			}
		}
		
		else if (money < 1000 && ph.level < 10)
		{
			if(!triggered)
			{
                GetComponent<AudioSource>().PlayOneShot(sound2);
                nEA.SetTrigger("Click");
				triggered = true;
				nEAB = true;
			}
		}
	}

	public void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.CompareTag ("Player"))
		{
            openShop.SetActive(true);
        }
	}

    public void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.CompareTag ("Player"))
		{
            openShop.SetActive(false);
        }
	}
}
