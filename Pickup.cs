using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
	public GameObject attachPoint;
	public bool Equipped = false;
	public bool Sword = false;
	Animator anim;
	public GameObject currentObj = null;
	Player player;
	public GameObject First;
	public GameObject Second;
	public GameObject Third;
	public GameObject Fourth;
	public GameObject Fifth;
	public GameObject Sixth;
	public GameObject Seventh;
	public GameObject Eighth;
	public GameObject wV;
	public WeaponVendingmachine vM;
	PlayerHealth pHealth;

	public float bazookaAmmo;

	public float shotgunAmmo;

	public float staffAmmo;

	public float granadeAmmo;

	public float machinegunAmmo;
	private static Pickup instance = null;
	public static Pickup Instance
	{
		get { return instance; }
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
	
	public void Start()
	{
		anim = GetComponent<Animator>();
		anim.SetBool("Equipped", false);
		anim.SetBool("Bazooka", false);
		anim.SetBool ("ShotGun", false);
		anim.SetBool ("WaterGun", false);
		anim.SetBool ("MachineGun", false);
		anim.SetBool ("Granade", false);
		anim.SetBool ("Staff", false);
		anim.SetBool ("Boomerang", false);
		player = GetComponent<Player> ();
		pHealth = GetComponent<PlayerHealth> ();
		if (wV == null)
		{
			wV = GameObject.Find ("VendingMachine");
		}
		
		if (wV != null)
		{
			vM = wV.GetComponent<WeaponVendingmachine>();
		}
	}

	void Update()
	{
		if (bazookaAmmo > 20)
		{
			bazookaAmmo = 20f;
		}

		if (shotgunAmmo > 50)
		{
			shotgunAmmo = 50;
		}

		if (staffAmmo > 40)
		{
			staffAmmo = 40;
		}

		if (machinegunAmmo > 1000)
		{
			machinegunAmmo = 1000;
		}

		if (granadeAmmo > 30)
		{
			granadeAmmo = 30;
		}
	}

	public void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Weapon")
		{
			pHealth.swipes ++;
			Equipped = true;
			col.gameObject.transform.parent = attachPoint.transform;
			if(col.gameObject.name == "Pencil Launcher" || col.gameObject.name == "Pencil Launcher(Clone)")
			{
				bazookaAmmo = 20f;
				anim.SetBool("Bazooka", true);
				anim.SetBool("Equipped", false);
				anim.SetBool("ShotGun",false);
				anim.SetBool ("WaterGun", false);
				anim.SetBool ("MachineGun", false);
				anim.SetBool ("Granade", false);
				anim.SetBool ("Staff", false);
				anim.SetBool ("Boomerang", false);
				player.Shotgun = false;
				player.Bazooka = true;
				Sword = false;
				player.Staff = false;
				player.Watergun = false;
				player.Granade = false;
				player.Boomerang = false;
				player.Machinegun = false;
				col.gameObject.transform.localPosition = new Vector3(0.29f,-0.35f,0f);
				col.gameObject.transform.localScale = new Vector3(1f,1f,1f);
				col.gameObject.transform.localEulerAngles = new Vector3(0f,0f,160f);
				pHealth.launcher.enabled = true;
				pHealth.ammoEmpty.enabled = false;
				if (First == null)
				{
					First = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second == null)
				{
					Second = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third == null)
				{
					Third = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth == null)
				{
					Fourth = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth != null && Fifth == null)
				{
					Fifth = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth != null && Fifth != null && Sixth == null)
				{
					Sixth = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth != null && Fifth != null && Sixth != null && Seventh == null)
				{
					Seventh = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth != null && Fifth != null && Sixth != null && Seventh != null && Eighth == null)
				{
					Eighth = col.gameObject;
					pHealth.ResetWeapons();
				}

				if (vM.children[0] == col.gameObject)
				{
					vM.children[0] = null;
				}
				
				else if (vM.children[1] == col.gameObject)
				{
					vM.children[1] = null;
				}
				
				else if (vM.children[2] == col.gameObject)
				{
					vM.children[2] = null;
				}
				
				else if (vM.children[3] == col.gameObject)
				{
					vM.children[3] = null;
				}
				
				else if (vM.children[4] == col.gameObject)
				{
					vM.children[4] = null;
				}
				
				else if (vM.children[5] == col.gameObject)
				{
					vM.children[5] = null;
				}
				
				else if (vM.children[6] == col.gameObject)
				{
					vM.children[6] = null;
				}
				
				else if (vM.children[7] == col.gameObject)
				{
					vM.children[7] = null;
				}
				
				else if (vM.children[8] == col.gameObject)
				{
					vM.children[8] = null;
				}
				
				else if (vM.children[9] == col.gameObject)
				{
					vM.children[9] = null;
				}
				
				else if (vM.children[10] == col.gameObject)
				{
					vM.children[10] = null;
				}
			}

			else if(col.gameObject.name == "TicTaxSword" || col.gameObject.name == "TicTaxSword(Clone)")
			{
				anim.SetBool("Equipped", true);
				anim.SetBool("Bazooka", false);
				anim.SetBool("ShotGun",false);
				anim.SetBool ("WaterGun", false);
				anim.SetBool ("MachineGun", false);
				anim.SetBool ("Granade", false);
				anim.SetBool ("Staff", false);
				anim.SetBool ("Boomerang", false);
				player.Bazooka = false;
				player.Shotgun = false;
				Sword = true;
				player.Staff = false;
				player.Watergun = false;
				player.Granade = false;
				player.Boomerang = false;
				player.Machinegun = false;
				col.gameObject.transform.localPosition = new Vector3(-0.02f,-0.45f,0f);
				col.gameObject.transform.localScale = new Vector3 (1f,1.72f,1f);
				col.gameObject.transform.localEulerAngles = new Vector3(0f,0f,265f);
				pHealth.ammoEmpty.enabled = false;
				pHealth.sword.enabled = true;
				if (First == null)
				{
					First = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second == null)
				{
					Second = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third == null)
				{
					Third = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth == null)
				{
					Fourth = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth != null && Fifth == null)
				{
					Fifth = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth != null && Fifth != null && Sixth == null)
				{
					Sixth = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth != null && Fifth != null && Sixth != null && Seventh == null)
				{
					Seventh = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth != null && Fifth != null && Sixth != null && Seventh != null && Eighth == null)
				{
					Eighth = col.gameObject;
					pHealth.ResetWeapons();
				}

				if (vM.children[0] == col.gameObject)
				{
					vM.children[0] = null;
				}
				
				else if (vM.children[1] == col.gameObject)
				{
					vM.children[1] = null;
				}
				
				else if (vM.children[2] == col.gameObject)
				{
					vM.children[2] = null;
				}
				
				else if (vM.children[3] == col.gameObject)
				{
					vM.children[3] = null;
				}
				
				else if (vM.children[4] == col.gameObject)
				{
					vM.children[4] = null;
				}
				
				else if (vM.children[5] == col.gameObject)
				{
					vM.children[5] = null;
				}
				
				else if (vM.children[6] == col.gameObject)
				{
					vM.children[6] = null;
				}
				
				else if (vM.children[7] == col.gameObject)
				{
					vM.children[7] = null;
				}
				
				else if (vM.children[8] == col.gameObject)
				{
					vM.children[8] = null;
				}
				
				else if (vM.children[9] == col.gameObject)
				{
					vM.children[9] = null;
				}
				
				else if (vM.children[10] == col.gameObject)
				{
					vM.children[10] = null;
				}
			}

			else if (col.gameObject.name == "Shotgun" || col.gameObject.name =="Shotgun(Clone)")
			{
				shotgunAmmo = 50f;
				anim.SetBool("ShotGun",true);
				anim.SetBool("Equipped", false);
				anim.SetBool("Bazooka", false);
				anim.SetBool ("WaterGun", false);
				anim.SetBool ("MachineGun", false);
				anim.SetBool ("Granade", false);
				anim.SetBool ("Staff", false);
				anim.SetBool ("Boomerang", false);
				player.Bazooka = false;
				player.Shotgun = true;
				Sword = false;
				player.Staff = false;
				player.Watergun = false;
				player.Granade = false;
				player.Boomerang = false;
				player.Machinegun = false;
				col.gameObject.transform.localPosition = new Vector3(0.10f,-0.65f,0f);
				col.gameObject.transform.localScale = new Vector3(0.17f,0.23f,1f);
				col.gameObject.transform.localEulerAngles = new Vector3(0f,0f,-90f);
				pHealth.ammoEmpty.enabled = false;
				pHealth.shotgun.enabled = true;
				if (First == null)
				{
					First = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second == null)
				{
					Second = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third == null)
				{
					Third = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth == null)
				{
					Fourth = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth != null && Fifth == null)
				{
					Fifth = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth != null && Fifth != null && Sixth == null)
				{
					Sixth = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth != null && Fifth != null && Sixth != null && Seventh == null)
				{
					Seventh = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth != null && Fifth != null && Sixth != null && Seventh != null && Eighth == null)
				{
					Eighth = col.gameObject;
					pHealth.ResetWeapons();
				}

				if (vM.children[0] == col.gameObject)
				{
					vM.children[0] = null;
				}
				
				else if (vM.children[1] == col.gameObject)
				{
					vM.children[1] = null;
				}
				
				else if (vM.children[2] == col.gameObject)
				{
					vM.children[2] = null;
				}
				
				else if (vM.children[3] == col.gameObject)
				{
					vM.children[3] = null;
				}
				
				else if (vM.children[4] == col.gameObject)
				{
					vM.children[4] = null;
				}
				
				else if (vM.children[5] == col.gameObject)
				{
					vM.children[5] = null;
				}
				
				else if (vM.children[6] == col.gameObject)
				{
					vM.children[6] = null;
				}
				
				else if (vM.children[7] == col.gameObject)
				{
					vM.children[7] = null;
				}
				
				else if (vM.children[8] == col.gameObject)
				{
					vM.children[8] = null;
				}
				
				else if (vM.children[9] == col.gameObject)
				{
					vM.children[9] = null;
				}
				
				else if (vM.children[10] == col.gameObject)
				{
					vM.children[10] = null;
				}
			}

			else if(col.gameObject.name == "Staff"|| col.gameObject.name =="Staff(Clone)")
			{
				staffAmmo = 40f;
				anim.SetBool("Bazooka", false);
				anim.SetBool("Equipped", false);
				anim.SetBool("ShotGun",false);
				anim.SetBool ("WaterGun", false);
				anim.SetBool ("MachineGun", false);
				anim.SetBool ("Granade", false);
				anim.SetBool ("Staff", true);
				anim.SetBool ("Boomerang", false);
				player.Shotgun = false;
				player.Bazooka = false;
				Sword = false;
				player.Staff = true;
				player.Watergun = false;
				player.Granade = false;
				player.Boomerang = false;
				player.Machinegun = false;
				col.gameObject.transform.localPosition = new Vector3(0f,-0.55f,0f);
				col.gameObject.transform.localScale = new Vector3(0.45f,0.45f,0.45f);
				col.gameObject.transform.localEulerAngles = new Vector3(0f,180f,75.2f);
				pHealth.ammoEmpty.enabled = false;
				pHealth.staff.enabled = true;
				if (First == null)
				{
					First = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second == null)
				{
					Second = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third == null)
				{
					Third = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth == null)
				{
					Fourth = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth != null && Fifth == null)
				{
					Fifth = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth != null && Fifth != null && Sixth == null)
				{
					Sixth = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth != null && Fifth != null && Sixth != null && Seventh == null)
				{
					Seventh = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth != null && Fifth != null && Sixth != null && Seventh != null && Eighth == null)
				{
					Eighth = col.gameObject;
					pHealth.ResetWeapons();
				}

				if (vM.children[0] == col.gameObject)
				{
					vM.children[0] = null;
				}
				
				else if (vM.children[1] == col.gameObject)
				{
					vM.children[1] = null;
				}
				
				else if (vM.children[2] == col.gameObject)
				{
					vM.children[2] = null;
				}
				
				else if (vM.children[3] == col.gameObject)
				{
					vM.children[3] = null;
				}
				
				else if (vM.children[4] == col.gameObject)
				{
					vM.children[4] = null;
				}
				
				else if (vM.children[5] == col.gameObject)
				{
					vM.children[5] = null;
				}
				
				else if (vM.children[6] == col.gameObject)
				{
					vM.children[6] = null;
				}
				
				else if (vM.children[7] == col.gameObject)
				{
					vM.children[7] = null;
				}
				
				else if (vM.children[8] == col.gameObject)
				{
					vM.children[8] = null;
				}
				
				else if (vM.children[9] == col.gameObject)
				{
					vM.children[9] = null;
				}
				
				else if (vM.children[10] == col.gameObject)
				{
					vM.children[10] = null;
				}
			}

			else if(col.gameObject.name == "WaterGun"|| col.gameObject.name =="WaterGun(Clone)")
			{
				anim.SetBool("Bazooka", false);
				anim.SetBool("Equipped", false);
				anim.SetBool("ShotGun",false);
				anim.SetBool ("WaterGun", true);
				anim.SetBool ("MachineGun", false);
				anim.SetBool ("Granade", false);
				anim.SetBool ("Staff", false);
				anim.SetBool ("Boomerang", false);
				player.Shotgun = false;
				player.Bazooka = false;
				Sword = false;
				player.Staff = false;
				player.Watergun = true;
				player.Granade = false;
				player.Boomerang = false;
				player.Machinegun = false;
				col.gameObject.transform.localPosition = new Vector3(0.18f,-0.6f,0f);
				col.gameObject.transform.localScale = new Vector3(0.34f,0.34f,0.34f);
				col.gameObject.transform.localEulerAngles = new Vector3(0f,0f,277f);
				pHealth.ammoEmpty.enabled = false;
				pHealth.watergun.enabled = true;
				if (First == null)
				{
					First = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second == null)
				{
					Second = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third == null)
				{
					Third = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth == null)
				{
					Fourth = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth != null && Fifth == null)
				{
					Fifth = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth != null && Fifth != null && Sixth == null)
				{
					Sixth = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth != null && Fifth != null && Sixth != null && Seventh == null)
				{
					Seventh = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth != null && Fifth != null && Sixth != null && Seventh != null && Eighth == null)
				{
					Eighth = col.gameObject;
					pHealth.ResetWeapons();
				}

				if (vM.children[0] == col.gameObject)
				{
					vM.children[0] = null;
				}
				
				else if (vM.children[1] == col.gameObject)
				{
					vM.children[1] = null;
				}
				
				else if (vM.children[2] == col.gameObject)
				{
					vM.children[2] = null;
				}
				
				else if (vM.children[3] == col.gameObject)
				{
					vM.children[3] = null;
				}
				
				else if (vM.children[4] == col.gameObject)
				{
					vM.children[4] = null;
				}
				
				else if (vM.children[5] == col.gameObject)
				{
					vM.children[5] = null;
				}
				
				else if (vM.children[6] == col.gameObject)
				{
					vM.children[6] = null;
				}
				
				else if (vM.children[7] == col.gameObject)
				{
					vM.children[7] = null;
				}
				
				else if (vM.children[8] == col.gameObject)
				{
					vM.children[8] = null;
				}
				
				else if (vM.children[9] == col.gameObject)
				{
					vM.children[9] = null;
				}
				
				else if (vM.children[10] == col.gameObject)
				{
					vM.children[10] = null;
				}
			}

			else if(col.gameObject.name == "MachineGun"|| col.gameObject.name =="MachineGun(Clone)")
			{
				machinegunAmmo = 1000f;
				anim.SetBool("Bazooka", false);
				anim.SetBool("Equipped", false);
				anim.SetBool("ShotGun",false);
				anim.SetBool ("WaterGun", false);
				anim.SetBool ("MachineGun", true);
				anim.SetBool ("Granade", false);
				anim.SetBool ("Staff", false);
				anim.SetBool ("Boomerang", false);
				player.Shotgun = false;
				player.Bazooka = false;
				Sword = false;
				player.Staff = false;
				player.Watergun = false;
				player.Granade = false;
				player.Boomerang = false;
				player.Machinegun = true;
				col.gameObject.transform.localPosition = new Vector3(0.26f,-0.75f,0f);
				col.gameObject.transform.localScale = new Vector3(0.3f,0.3f,0.3f);
				col.gameObject.transform.localEulerAngles = new Vector3(0f,0f,3.45f);
				pHealth.ammoEmpty.enabled = false;
				pHealth.machinegun.enabled = true;
				if (First == null)
				{
					First = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second == null)
				{
					Second = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third == null)
				{
					Third = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth == null)
				{
					Fourth = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth != null && Fifth == null)
				{
					Fifth = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth != null && Fifth != null && Sixth == null)
				{
					Sixth = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth != null && Fifth != null && Sixth != null && Seventh == null)
				{
					Seventh = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth != null && Fifth != null && Sixth != null && Seventh != null && Eighth == null)
				{
					Eighth = col.gameObject;
					pHealth.ResetWeapons();
				}

				if (vM.children[0] == col.gameObject)
				{
					vM.children[0] = null;
				}
				
				else if (vM.children[1] == col.gameObject)
				{
					vM.children[1] = null;
				}
				
				else if (vM.children[2] == col.gameObject)
				{
					vM.children[2] = null;
				}
				
				else if (vM.children[3] == col.gameObject)
				{
					vM.children[3] = null;
				}
				
				else if (vM.children[4] == col.gameObject)
				{
					vM.children[4] = null;
				}
				
				else if (vM.children[5] == col.gameObject)
				{
					vM.children[5] = null;
				}
				
				else if (vM.children[6] == col.gameObject)
				{
					vM.children[6] = null;
				}
				
				else if (vM.children[7] == col.gameObject)
				{
					vM.children[7] = null;
				}
				
				else if (vM.children[8] == col.gameObject)
				{
					vM.children[8] = null;
				}
				
				else if (vM.children[9] == col.gameObject)
				{
					vM.children[9] = null;
				}
				
				else if (vM.children[10] == col.gameObject)
				{
					vM.children[10] = null;
				}
			}

			else if(col.gameObject.name == "Boomerang" || col.gameObject.name =="Boomerang(Clone)")
			{
				anim.SetBool("Bazooka", false);
				anim.SetBool("Equipped", false);
				anim.SetBool("ShotGun",false);
				anim.SetBool ("WaterGun", false);
				anim.SetBool ("MachineGun", false);
				anim.SetBool ("Granade", false);
				anim.SetBool ("Staff", false);
				anim.SetBool ("Boomerang", true);
				player.Shotgun = false;
				player.Bazooka = false;
				Sword = false;
				player.Staff = false;
				player.Watergun = false;
				player.Granade = false;
				player.Boomerang = true;
				player.Machinegun = false;
				col.gameObject.transform.localPosition = new Vector3(0.26f,-0.755f,0f);
				col.gameObject.transform.localScale = new Vector3(0.23f,0.23f,0.23f);
				col.gameObject.transform.localEulerAngles = new Vector3(0f,0f,0f);
				pHealth.ammoEmpty.enabled = false;
				pHealth.boomerang.enabled = true;
				if (First == null)
				{
					First = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second == null)
				{
					Second = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third == null)
				{
					Third = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth == null)
				{
					Fourth = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth != null && Fifth == null)
				{
					Fifth = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth != null && Fifth != null && Sixth == null)
				{
					Sixth = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth != null && Fifth != null && Sixth != null && Seventh == null)
				{
					Seventh = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth != null && Fifth != null && Sixth != null && Seventh != null && Eighth == null)
				{
					Eighth = col.gameObject;
					pHealth.ResetWeapons();
				}

				if (vM.children[0] == col.gameObject)
				{
					vM.children[0] = null;
				}
				
				else if (vM.children[1] == col.gameObject)
				{
					vM.children[1] = null;
				}
				
				else if (vM.children[2] == col.gameObject)
				{
					vM.children[2] = null;
				}
				
				else if (vM.children[3] == col.gameObject)
				{
					vM.children[3] = null;
				}
				
				else if (vM.children[4] == col.gameObject)
				{
					vM.children[4] = null;
				}
				
				else if (vM.children[5] == col.gameObject)
				{
					vM.children[5] = null;
				}
				
				else if (vM.children[6] == col.gameObject)
				{
					vM.children[6] = null;
				}
				
				else if (vM.children[7] == col.gameObject)
				{
					vM.children[7] = null;
				}
				
				else if (vM.children[8] == col.gameObject)
				{
					vM.children[8] = null;
				}
				
				else if (vM.children[9] == col.gameObject)
				{
					vM.children[9] = null;
				}
				
				else if (vM.children[10] == col.gameObject)
				{
					vM.children[10] = null;
				}
			}

			else if(col.gameObject.name == "Granade"|| col.gameObject.name =="Granade(Clone)")
			{
				granadeAmmo = 30f;
				anim.SetBool("Bazooka", false);
				anim.SetBool("Equipped", false);
				anim.SetBool("ShotGun",false);
				anim.SetBool ("WaterGun", false);
				anim.SetBool ("MachineGun", false);
				anim.SetBool ("Granade", true);
				anim.SetBool ("Staff", false);
				anim.SetBool ("Boomerang", false);
				player.Shotgun = false;
				player.Bazooka = false;
				Sword = false;
				player.Staff = false;
				player.Watergun = false;
				player.Granade = true;
				player.Boomerang = false;
				player.Machinegun = false;
				col.gameObject.transform.localPosition = new Vector3(0.058f,-0.483f,0f);
				col.gameObject.transform.localScale = new Vector3(0.19f,0.19f,0.19f);
				col.gameObject.transform.localEulerAngles = new Vector3(0f,0f,300f);
				pHealth.ammoEmpty.enabled = false;
				pHealth.granade.enabled = true;
				if (First == null)
				{
					First = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second == null)
				{
					Second = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third == null)
				{
					Third = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth == null)
				{
					Fourth = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth != null && Fifth == null)
				{
					Fifth = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth != null && Fifth != null && Sixth == null)
				{
					Sixth = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth != null && Fifth != null && Sixth != null && Seventh == null)
				{
					Seventh = col.gameObject;
					pHealth.ResetWeapons();
				}

				else if (First != null && Second != null && Third != null && Fourth != null && Fifth != null && Sixth != null && Seventh != null && Eighth == null)
				{
					Eighth = col.gameObject;
					pHealth.ResetWeapons();
				}

				if (vM.children[0] == col.gameObject)
				{
					vM.children[0] = null;
				}
				
				else if (vM.children[1] == col.gameObject)
				{
					vM.children[1] = null;
				}
				
				else if (vM.children[2] == col.gameObject)
				{
					vM.children[2] = null;
				}
				
				else if (vM.children[3] == col.gameObject)
				{
					vM.children[3] = null;
				}
				
				else if (vM.children[4] == col.gameObject)
				{
					vM.children[4] = null;
				}
				
				else if (vM.children[5] == col.gameObject)
				{
					vM.children[5] = null;
				}
				
				else if (vM.children[6] == col.gameObject)
				{
					vM.children[6] = null;
				}
				
				else if (vM.children[7] == col.gameObject)
				{
					vM.children[7] = null;
				}
				
				else if (vM.children[8] == col.gameObject)
				{
					vM.children[8] = null;
				}
				
				else if (vM.children[9] == col.gameObject)
				{
					vM.children[9] = null;
				}
				
				else if (vM.children[10] == col.gameObject)
				{
					vM.children[10] = null;
				}
			}

			if(col.gameObject.name == "Pencil Launcher" || col.gameObject.name == "Pencil Launcher(Clone)")
			{
				pHealth.theLauncher = col.gameObject;
			}
			else if(col.gameObject.name == "TicTaxSword" || col.gameObject.name == "TicTaxSword(Clone)")
			{
				pHealth.theSword = col.gameObject;
			}
			else if(col.gameObject.name == "Shotgun" || col.gameObject.name == "Shotgun(Clone)")
			{
				pHealth.theShotgun = col.gameObject;
			}
			else if(col.gameObject.name == "Staff" || col.gameObject.name == "Staff(Clone)")
			{
				pHealth.theStaff = col.gameObject;
			}
			else if(col.gameObject.name == "Granade" || col.gameObject.name == "Granade(Clone)")
			{
				pHealth.theGranade = col.gameObject;
			}
			else if(col.gameObject.name == "WaterGun" || col.gameObject.name == "WaterGun(Clone)")
			{
				pHealth.theWatergun = col.gameObject;
			}
			else if(col.gameObject.name == "Boomerang" || col.gameObject.name == "Boomerang(Clone)")
			{
				pHealth.theBoomerang = col.gameObject;
			}
			else if(col.gameObject.name == "MachineGun" || col.gameObject.name == "MachineGun(Clone)")
			{
				pHealth.theMachinegun = col.gameObject;
			}
		}
	}
}