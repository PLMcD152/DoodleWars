using UnityEngine;
using System.Collections; 
using UnityEngine.UI;


public class SpawnManager : MonoBehaviour
{
	[HideInInspector]
	public GameObject[] spawnPoints;
	private GameObject[] streetsEnemies;
	private GameObject streetsMiniboss;
	private GameObject[] skylineGroundEnemies;
	private GameObject skylineMiniboss;
	private GameObject[] fieldEnemies;
	private GameObject fieldMiniboss;
	private GameObject[] castleEnemies;
	private GameObject castleMiniboss;
	private GameObject sniper;
	private GameObject[] apocEnemies;
	private GameObject apocMiniboss;
	private GameObject jetpack;
	[HideInInspector]
	public GameObject punkBoss;
	[HideInInspector]
	public GameObject alienBoss;
	[HideInInspector]
	public GameObject dinoBoss;
	[HideInInspector]
	public GameObject machineBoss;
	[HideInInspector]
	public GameObject kingBoss; 
	public int hazardCount;
	[HideInInspector]
	public float spawnWait;
	public int waveCount;
	[HideInInspector]
	public AudioClip spawnsound;
	[HideInInspector]
	public AudioClip roundBegin;
	[HideInInspector]
	public AudioClip bossRound;
	[HideInInspector]
	public bool enemiesDestroyed;
	[HideInInspector]
	public bool punkBossSpawned;
	[HideInInspector]
	public bool dinoBossSpawned;
	[HideInInspector]
	public bool machineBossSpawned;
	[HideInInspector]
	public bool alienBossSpawned;
	[HideInInspector]
	public bool kingBossSpawned;
	[HideInInspector]
	public bool punkDead = false;
	[HideInInspector]
	public bool dinoDead = false;
	[HideInInspector]
	public bool machineDead = false;
	[HideInInspector]
	public bool alienDead = false;
	[HideInInspector]
	public bool kingDead = false;
	[HideInInspector]
	public GameObject waveText;
	[HideInInspector]
	public Text waveNumber;
	public int number;
	private GameObject bossSpawn;
	private GameObject s;
	private SpawnCounter sc;
	public bool kToChange = false;
	public Vector3 offset1 = new Vector3(2f, 0f, 0f);
	public Vector3 offset2 = new Vector3(-2f, 0f, 0f);
	public Vector3 offset3 = new Vector3(4f, 0f, 0f);
	private GameObject newSmoke;
    public bool pDead;


	void Awake()
	{
		waveCount = 0;
		PunkBack.punkDead += PunkDead;
		DinoBack.dinoDead += DinoDead;
		SpaceshipBack.shipDead += AlienDead;
		MachineBack.machineDead += MachineDead;
	}

	void Start ()
	{

		StartCoroutine (SpawnWaves ());
		enemiesDestroyed = false; 
		GetComponent<ScoreManager> ();
		waveText = GameObject.Find ("WaveText");
		waveNumber = waveText.GetComponent<Text>();
		waveNumber.text = "0";
		punkBoss = (Resources.Load ("BossSoldier/BossSoldierTeleport"))as GameObject;
		alienBoss = (Resources.Load ("Ship/SpaceshipTeleport"))as GameObject;
		dinoBoss = (Resources.Load ("Dino/DinoTeleport"))as GameObject;
		machineBoss = (Resources.Load ("MachineBoss/MachineBossTeleport"))as GameObject;
		kingBoss = (Resources.Load ("SmudgieKing/KingFightStart"))as GameObject;
		streetsEnemies = new GameObject[] 
		{
			(Resources.Load ("Goon/SmudgieGoonLeft"))as GameObject,
			(Resources.Load ("Goon/SmudgieGoonRight"))as GameObject,
			(Resources.Load ("Punk/Gunner"))as GameObject,
		};
		streetsMiniboss = Resources.Load ("MiniBosses/GunnerMiniboss")as GameObject;
		skylineGroundEnemies = new GameObject[] 
		{
			(Resources.Load ("Goon/SmudgieGoonLeft"))as GameObject,
			(Resources.Load ("Goon/SmudgieGoonRight"))as GameObject,
		};
		jetpack = Resources.Load ("Jetpack/JetpackSmudgie")as GameObject;
		skylineMiniboss = Resources.Load ("MiniBosses/JetpackMiniboss")as GameObject;
		fieldEnemies = new GameObject[] 
		{
			(Resources.Load ("Goon/SmudgieGoonLeft"))as GameObject,
			(Resources.Load ("Goon/SmudgieGoonRight"))as GameObject,
			(Resources.Load ("Ninja/NinjaSmudgie"))as GameObject,
		};
		sniper = Resources.Load ("Punk/Sniper")as GameObject;
		fieldMiniboss = Resources.Load ("MiniBosses/NinjaMiniboss")as GameObject;
		castleEnemies = new GameObject[] 
		{
			(Resources.Load ("Goon/Eye"))as GameObject,
			(Resources.Load ("Goon/SmudgieRunnerLeft"))as GameObject,
			(Resources.Load ("Goon/SmudgieRunnerRight"))as GameObject,
			(Resources.Load ("Punk/Gunner"))as GameObject,
		};
		castleMiniboss = Resources.Load ("MiniBosses/GunnerMiniboss")as GameObject;
		apocEnemies = new GameObject[] 
		{
			(Resources.Load ("Goon/Eye"))as GameObject,
			(Resources.Load ("Goon/SmudgieGoonLeft"))as GameObject,
			(Resources.Load ("Goon/SmudgieGoonRight"))as GameObject,
			(Resources.Load ("Punk/Gunner"))as GameObject,
            (Resources.Load ("Goon/Eye"))as GameObject,
            (Resources.Load ("Goon/SmudgieGoonLeft"))as GameObject,
            (Resources.Load ("Goon/SmudgieGoonRight"))as GameObject,
            (Resources.Load ("Punk/Gunner"))as GameObject,
            (Resources.Load ("Goon/Eye"))as GameObject,
            (Resources.Load ("Goon/SmudgieGoonLeft"))as GameObject,
            (Resources.Load ("Goon/SmudgieGoonRight"))as GameObject,
            (Resources.Load ("MiniBosses/SmudgieGoonMiniboss"))as GameObject,
		};
		apocMiniboss = Resources.Load ("MiniBosses/GunnerMiniboss")as GameObject;
        spawnsound = Resources.Load("DoodleSounds/Spawn") as AudioClip;
        s = Resources.Load ("LevelPrefabs/smoke")as GameObject;
		bossSpawn = GameObject.Find ("BossSpawn");
		sc = GetComponent<SpawnCounter> ();
	}

	void Update()
	{
        if (!pDead)
        {
            spawnPoints = GameObject.FindGameObjectsWithTag("Spawner");
            waveNumber.text = "" + waveCount.ToString();
            GetComponent<SpawnCounter>().enemycount = hazardCount;
        }

		if(waveCount == 5 && !punkDead && !pDead)
		{
            GameObject.Find("GameManager").GetComponent<UpdateStats>().sessionRound = 5;
            GameObject.Find("GameManager").GetComponent<UpdateStats>().roundsplayed();
            PunkRound();
			sc.countdown = 0;
			sc.atBoss = true;
		}

		else if (waveCount == 10 && !alienDead && !pDead)
		{
            GameObject.Find("GameManager").GetComponent<UpdateStats>().sessionRound = 10;
            GameObject.Find("GameManager").GetComponent<UpdateStats>().roundsplayed();
            AlienRound();
			sc.countdown = 0;
			sc.atBoss = true;
		}

		else if (waveCount == 15 && !machineDead && !pDead)
		{
            GameObject.Find("GameManager").GetComponent<UpdateStats>().sessionRound = 15;
            GameObject.Find("GameManager").GetComponent<UpdateStats>().roundsplayed();
            MachineRound();
			sc.countdown = 0;
			sc.atBoss = true;
		}

		else if (waveCount == 20 && !dinoDead && !pDead)
		{
            GameObject.Find("GameManager").GetComponent<UpdateStats>().sessionRound = 20;
            GameObject.Find("GameManager").GetComponent<UpdateStats>().roundsplayed();
            DinoRound();
			sc.countdown = 0;
			sc.atBoss = true;
		}

		else if (waveCount == 25 && !kingDead && !pDead)
		{
            GameObject.Find("GameManager").GetComponent<UpdateStats>().sessionRound = 25;
            GameObject.Find("GameManager").GetComponent<UpdateStats>().roundsplayed();
            KingRound();
			sc.countdown = 0;
			sc.atBoss = true;
		}

		else if(waveCount == 0 && sc.countdown == 0 && !pDead)
		{
			enemiesDestroyed = true;
		}

		else if(waveCount == 1 && !pDead)
		{
            GameObject.Find("GameManager").GetComponent<UpdateStats>().sessionRound = 1;
            GameObject.Find("GameManager").GetComponent<UpdateStats>().roundsplayed();
            if (number < 2)
			{
				sc.okToChange = false;
			}

			else if(number == 2)
			{
				sc.okToChange = true;
			}
		}
		
		else if (waveCount == 2 && !pDead)
		{
            GameObject.Find("GameManager").GetComponent<UpdateStats>().sessionRound = 2;
            GameObject.Find("GameManager").GetComponent<UpdateStats>().roundsplayed();
            if (number < 4)
			{
				sc.okToChange = false;
			}
			
			else if(number == 4)
			{
				sc.okToChange = true;
			}
		}
		
		else if (waveCount == 3 && !pDead)
		{
            GameObject.Find("GameManager").GetComponent<UpdateStats>().sessionRound = 3;
            GameObject.Find("GameManager").GetComponent<UpdateStats>().roundsplayed();
            if (number < 6)
			{
				sc.okToChange = false;
			}
			
			else if(number == 6)
			{
				sc.okToChange = true;
			}
		}
		
		else if (waveCount == 4 && !pDead)
		{
            GameObject.Find("GameManager").GetComponent<UpdateStats>().sessionRound = 4;
            GameObject.Find("GameManager").GetComponent<UpdateStats>().roundsplayed();
            if (number < 8)
			{
				sc.okToChange = false;
			}
			
			else if(number == 8)
			{
				sc.okToChange = true;
			}
		}
		
		else if (waveCount == 6 && !pDead)
		{
            GameObject.Find("GameManager").GetComponent<UpdateStats>().sessionRound = 6;
            GameObject.Find("GameManager").GetComponent<UpdateStats>().roundsplayed();
            if (number < 4)
			{
				sc.okToChange = false;
			}
			
			else if(number == 4)
			{
				sc.okToChange = true;
			}
		}

		else if(waveCount == 7 && !pDead)
		{
            GameObject.Find("GameManager").GetComponent<UpdateStats>().sessionRound = 7;
            GameObject.Find("GameManager").GetComponent<UpdateStats>().roundsplayed();
            if (number < 8)
			{
				sc.okToChange = false;
			}
			
			else if(number == 8)
			{
				sc.okToChange = true;
			}
		}
		
		else if (waveCount == 8 && !pDead)
		{
            GameObject.Find("GameManager").GetComponent<UpdateStats>().sessionRound = 8;
            GameObject.Find("GameManager").GetComponent<UpdateStats>().roundsplayed();
            if (number < 12)
			{
				sc.okToChange = false;
			}
			
			else if(number == 12)
			{
				sc.okToChange = true;
			}
		}
		
		else if (waveCount == 9 && !pDead)
		{
            GameObject.Find("GameManager").GetComponent<UpdateStats>().sessionRound = 9;
            GameObject.Find("GameManager").GetComponent<UpdateStats>().roundsplayed();
            if (number < 16)
			{
				sc.okToChange = false;
			}
			
			else if(number == 16)
			{
				sc.okToChange = true;
			}
		}
		
		else if (waveCount == 11 && !pDead)
		{
            GameObject.Find("GameManager").GetComponent<UpdateStats>().sessionRound = 11;
            GameObject.Find("GameManager").GetComponent<UpdateStats>().roundsplayed();
            if (number < 8)
			{
				sc.okToChange = false;
			}
			
			else if(number == 8)
			{
				sc.okToChange = true;
			}
		}
		
		else if (waveCount == 12 && !pDead)
		{
            GameObject.Find("GameManager").GetComponent<UpdateStats>().sessionRound = 12;
            GameObject.Find("GameManager").GetComponent<UpdateStats>().roundsplayed();
            if (number < 14)
			{
				sc.okToChange = false;
			}
			
			else if(number == 14)
			{
				sc.okToChange = true;
			}
		}

		else if(waveCount == 13 && !pDead)
		{
            GameObject.Find("GameManager").GetComponent<UpdateStats>().sessionRound = 13;
            GameObject.Find("GameManager").GetComponent<UpdateStats>().roundsplayed();
            if (number < 20)
			{
				sc.okToChange = false;
			}
			
			else if(number == 20)
			{
				sc.okToChange = true;
			}
		}
		
		else if (waveCount == 14 && !pDead)
		{
            GameObject.Find("GameManager").GetComponent<UpdateStats>().sessionRound = 14;
            GameObject.Find("GameManager").GetComponent<UpdateStats>().roundsplayed();
            if (number < 28)
			{
				sc.okToChange = false;
			}
			
			else if(number == 28)
			{
				sc.okToChange = true;
			}
		}
		
		else if (waveCount == 16 && !pDead)
		{
            GameObject.Find("GameManager").GetComponent<UpdateStats>().sessionRound = 16;
            GameObject.Find("GameManager").GetComponent<UpdateStats>().roundsplayed();
            if (number < 8)
			{
				sc.okToChange = false;
			}
			
			else if(number == 8)
			{
				sc.okToChange = true;
			}
		}
		
		else if (waveCount == 17 && !pDead)
		{
            GameObject.Find("GameManager").GetComponent<UpdateStats>().sessionRound = 17;
            GameObject.Find("GameManager").GetComponent<UpdateStats>().roundsplayed();
            if (number < 16)
			{
				sc.okToChange = false;
			}
			
			else if(number == 16)
			{
				sc.okToChange = true;
			}
		}
		
		else if (waveCount == 18 && !pDead)
		{
            GameObject.Find("GameManager").GetComponent<UpdateStats>().sessionRound = 18;
            GameObject.Find("GameManager").GetComponent<UpdateStats>().roundsplayed();
            if (number < 24)
			{
				sc.okToChange = false;
			}
			
			else if(number == 24)
			{
				sc.okToChange = true;
			}
		}
		
		else if(waveCount == 19 && !pDead)
		{
            GameObject.Find("GameManager").GetComponent<UpdateStats>().sessionRound = 19;
            GameObject.Find("GameManager").GetComponent<UpdateStats>().roundsplayed();
            if (number < 32)
			{
				sc.okToChange = false;
			}
			
			else if(number == 32)
			{
				sc.okToChange = true;
			}
		}
		
		else if (waveCount == 21 && !pDead)
		{
            GameObject.Find("GameManager").GetComponent<UpdateStats>().sessionRound = 21;
            GameObject.Find("GameManager").GetComponent<UpdateStats>().roundsplayed();
            if (number < 12)
			{
				sc.okToChange = false;
			}
			
			else if(number == 12)
			{
				sc.okToChange = true;
			}
		}
		
		else if (waveCount == 22 && !pDead)
		{
            GameObject.Find("GameManager").GetComponent<UpdateStats>().sessionRound = 22;
            GameObject.Find("GameManager").GetComponent<UpdateStats>().roundsplayed();
            if (number < 22)
			{
				sc.okToChange = false;
			}
			
			else if(number == 22)
			{
				sc.okToChange = true;
			}
		}
		
		else if (waveCount == 23 && !pDead)
		{
            GameObject.Find("GameManager").GetComponent<UpdateStats>().sessionRound = 23;
            GameObject.Find("GameManager").GetComponent<UpdateStats>().roundsplayed();
            if (number < 32)
			{
				sc.okToChange = false;
			}
			
			else if(number == 32)
			{
				sc.okToChange = true;
			}
		}
		
		else if (waveCount == 24 && !pDead)
		{
            GameObject.Find("GameManager").GetComponent<UpdateStats>().sessionRound = 24;
            GameObject.Find("GameManager").GetComponent<UpdateStats>().roundsplayed();
            if (number < 44)
			{
				sc.okToChange = false;
			}
			
			else if(number == 44)
			{
				sc.okToChange = true;
			}
		}
	}


	
	IEnumerator SpawnWaves ()
	{

		while (enemiesDestroyed)
		{
			GetComponent<AudioSource>().PlayOneShot(roundBegin);
			waveCount++;
            for (int i = 0; i < spawnPoints.Length; i++ )
			{
				if (waveCount == 1)
				{
					if(spawnPoints[i].name == "ApocSmallSpawner")
					{
						GameObject aSS = GameObject.Find ("ApocSmallSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						newSmoke = Instantiate (s,spawnPoints[i].transform.position,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aSS.transform.position, spawnRotation);
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					if(spawnPoints[i].name == "BoardwalkSmallSpawner")
					{
						GameObject bSS = GameObject.Find ("BoardwalkSmallSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						newSmoke = Instantiate (s,spawnPoints[i].transform.position,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], bSS.transform.position, spawnRotation);
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					if(spawnPoints[i].name == "DesertSmallSpawner")
					{
						GameObject dSS = GameObject.Find ("DesertSmallSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						newSmoke = Instantiate (s,spawnPoints[i].transform.position,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], dSS.transform.position, spawnRotation);
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					if(spawnPoints[i].name == "FieldsSmallSpawner")
					{
						GameObject fSS = GameObject.Find ("FieldsSmallSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						newSmoke = Instantiate (s,spawnPoints[i].transform.position,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fSS.transform.position, spawnRotation);
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					if(spawnPoints[i].name == "SkylineSmall1Spawner")
					{
						GameObject sS1S = GameObject.Find ("SkylineSmall1Spawner");
						Quaternion spawnRotation = Quaternion.identity;
						newSmoke = Instantiate (s,spawnPoints[i].transform.position,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						Instantiate ((jetpack), sS1S.transform.position, spawnRotation);
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					if(spawnPoints[i].name == "SkylineSmall2Spawner")
					{
						GameObject sS2S = GameObject.Find ("SkylineSmall2Spawner");
						Quaternion spawnRotation = Quaternion.identity;
						newSmoke = Instantiate (s,spawnPoints[i].transform.position,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						Instantiate ((jetpack), sS2S.transform.position, spawnRotation);
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
				}

				if (waveCount == 2)
				{
					if(spawnPoints[i].name == "ApocSmallSpawner")
					{
						GameObject aSS = GameObject.Find ("ApocSmallSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						newSmoke = Instantiate (s,spawnPoints[i].transform.position,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aSS.transform.position, spawnRotation);
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						newSmoke = Instantiate (s,spawnPoints[i].transform.position + offset1,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						GameObject spawn = Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)])as GameObject;
						spawn.transform.position = new Vector3 (aSS.transform.position.x + 2, aSS.transform.position.y);
						spawn.transform.rotation = spawnRotation;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "BoardwalkSmallSpawner")
					{
						GameObject bSS = GameObject.Find ("BoardwalkSmallSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						newSmoke = Instantiate (s,spawnPoints[i].transform.position,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], bSS.transform.position, spawnRotation);
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						newSmoke = Instantiate (s,spawnPoints[i].transform.position + offset1,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						GameObject spawn = Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)])as GameObject;
						spawn.transform.position = new Vector3 (bSS.transform.position.x + 2, bSS.transform.position.y);
						spawn.transform.rotation = spawnRotation;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "DesertSmallSpawner")
					{
						GameObject dSS = GameObject.Find ("DesertSmallSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						newSmoke = Instantiate (s,spawnPoints[i].transform.position,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], dSS.transform.position, spawnRotation);
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						newSmoke = Instantiate (s,spawnPoints[i].transform.position + offset1,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						GameObject spawn = Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)])as GameObject;
						spawn.transform.position = new Vector3 (dSS.transform.position.x + 2, dSS.transform.position.y);
						spawn.transform.rotation = spawnRotation;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsSmallSpawner")
					{
						GameObject fSS = GameObject.Find ("FieldsSmallSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						newSmoke = Instantiate (s,spawnPoints[i].transform.position,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fSS.transform.position, spawnRotation);
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						newSmoke = Instantiate (s,spawnPoints[i].transform.position + offset1,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						GameObject spawn = Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)])as GameObject;
						spawn.transform.position = new Vector3 (fSS.transform.position.x + 2, fSS.transform.position.y);
						spawn.transform.rotation = spawnRotation;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "SkylineSmall1Spawner")
					{
						GameObject sS1S = GameObject.Find ("SkylineSmall1Spawner");
						Quaternion spawnRotation = Quaternion.identity;
						newSmoke = Instantiate (s,spawnPoints[i].transform.position,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						Instantiate ((jetpack), sS1S.transform.position, spawnRotation);
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						newSmoke = Instantiate (s,spawnPoints[i].transform.position + offset1,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						GameObject spawn = Instantiate (jetpack)as GameObject;
						spawn.transform.position = new Vector3 (sS1S.transform.position.x + 2, sS1S.transform.position.y);
						spawn.transform.rotation = spawnRotation;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "SkylineSmall2Spawner")
					{
						GameObject sS2S = GameObject.Find ("SkylineSmall2Spawner");
						Quaternion spawnRotation = Quaternion.identity;
						newSmoke = Instantiate (s,spawnPoints[i].transform.position,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						Instantiate ((jetpack), sS2S.transform.position, spawnRotation);
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						newSmoke = Instantiate (s,spawnPoints[i].transform.position + offset1,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						GameObject spawn = Instantiate (jetpack)as GameObject;
						spawn.transform.position = new Vector3 (sS2S.transform.position.x + 2, sS2S.transform.position.y);
						spawn.transform.rotation = spawnRotation;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
				}

				if (waveCount == 3)
				{
					if(spawnPoints[i].name == "ApocSmallSpawner")
					{
						GameObject aSS = GameObject.Find ("ApocSmallSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						newSmoke = Instantiate (s,spawnPoints[i].transform.position,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aSS.transform.position, spawnRotation);
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						newSmoke = Instantiate (s,spawnPoints[i].transform.position + offset1,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						GameObject spawn = Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)])as GameObject;
						spawn.transform.position = new Vector3 (aSS.transform.position.x + 2, aSS.transform.position.y);
						spawn.transform.rotation = spawnRotation;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						newSmoke = Instantiate (s,spawnPoints[i].transform.position + offset2,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						GameObject spawn2 = Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)])as GameObject;
						spawn2.transform.position = new Vector3 (aSS.transform.position.x - 2, aSS.transform.position.y);
						spawn2.transform.rotation = spawnRotation;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "BoardwalkSmallSpawner")
					{
						GameObject bSS = GameObject.Find ("BoardwalkSmallSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						newSmoke = Instantiate (s,spawnPoints[i].transform.position,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], bSS.transform.position, spawnRotation);
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						newSmoke = Instantiate (s,spawnPoints[i].transform.position + offset1,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], bSS.transform.position  + offset1, spawnRotation);
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						newSmoke = Instantiate (s,spawnPoints[i].transform.position + offset2,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], bSS.transform.position + offset2, spawnRotation);
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "DesertSmallSpawner")
					{
						GameObject dSS = GameObject.Find ("DesertSmallSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						newSmoke = Instantiate (s,spawnPoints[i].transform.position,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], dSS.transform.position, spawnRotation);
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						newSmoke = Instantiate (s,spawnPoints[i].transform.position + offset1,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], dSS.transform.position + offset1, spawnRotation);
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						newSmoke = Instantiate (s,spawnPoints[i].transform.position + offset2,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], dSS.transform.position + offset2, spawnRotation);
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsSmallSpawner")
					{
						GameObject fSS = GameObject.Find ("FieldsSmallSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						newSmoke = Instantiate (s,spawnPoints[i].transform.position,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fSS.transform.position, spawnRotation);
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						newSmoke = Instantiate (s,spawnPoints[i].transform.position + offset1,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fSS.transform.position + offset1, spawnRotation);
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						newSmoke = Instantiate (s,spawnPoints[i].transform.position + offset2,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fSS.transform.position + offset2, spawnRotation);
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "SkylineSmall1Spawner")
					{
						GameObject sS1S = GameObject.Find ("SkylineSmall1Spawner");
						Quaternion spawnRotation = Quaternion.identity;
						newSmoke = Instantiate (s,spawnPoints[i].transform.position,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						Instantiate ((jetpack), sS1S.transform.position, spawnRotation);
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						newSmoke = Instantiate (s,spawnPoints[i].transform.position + offset1,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						Instantiate ((jetpack), sS1S.transform.position + offset1, spawnRotation);
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						newSmoke = Instantiate (s,spawnPoints[i].transform.position + offset2,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						Instantiate ((jetpack), sS1S.transform.position + offset2, spawnRotation);
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "SkylineSmall2Spawner")
					{
						GameObject sS2S = GameObject.Find ("SkylineSmall2Spawner");
						Quaternion spawnRotation = Quaternion.identity;
						newSmoke = Instantiate (s,spawnPoints[i].transform.position,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						Instantiate ((jetpack), sS2S.transform.position, spawnRotation);
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						newSmoke = Instantiate (s,spawnPoints[i].transform.position + offset1,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						Instantiate ((jetpack), sS2S.transform.position + offset1, spawnRotation);
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						newSmoke = Instantiate (s,spawnPoints[i].transform.position + offset2,spawnPoints[i].transform.localRotation)as GameObject;
						newSmoke.transform.parent = spawnPoints[i].transform;
						Instantiate ((jetpack), sS2S.transform.position + offset2, spawnRotation);
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
				}

				if (waveCount == 4)
				{
					if(spawnPoints[i].name == "ApocSmallSpawner")
					{
						GameObject aSS = GameObject.Find ("ApocSmallSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aSS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aSS.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aSS.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aSS.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "BoardwalkSmallSpawner")
					{
						GameObject bSS = GameObject.Find ("BoardwalkSmallSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], bSS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], bSS.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], bSS.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], bSS.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "DesertSmallSpawner")
					{
						GameObject dSS = GameObject.Find ("DesertSmallSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], dSS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], dSS.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], dSS.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], dSS.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsSmallSpawner")
					{
						GameObject fSS = GameObject.Find ("FieldsSmallSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fSS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fSS.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fSS.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fSS.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "SkylineSmall1Spawner")
					{
						GameObject sS1S = GameObject.Find ("SkylineSmall1Spawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate ((jetpack), sS1S.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate ((jetpack), sS1S.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate ((jetpack), sS1S.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate ((jetpack), sS1S.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "SkylineSmall2Spawner")
					{
						GameObject sS2S = GameObject.Find ("SkylineSmall2Spawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate ((jetpack), sS2S.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate ((jetpack), sS2S.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate ((jetpack), sS2S.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate ((jetpack), sS2S.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
				}

				if (waveCount == 6)
				{
					if(spawnPoints[i].name == "ApocMediumSpawner1")
					{
						GameObject aMS1 = GameObject.Find ("ApocMediumSpawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "ApocMediumSpawner2")
					{
						GameObject aMS2 = GameObject.Find ("ApocMediumSpawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (jetpack, aMS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMediumSpawner1")
					{
						GameObject cMS1 = GameObject.Find ("CastleMediumSpawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMediumSpawner2")
					{
						GameObject cMS2 = GameObject.Find ("CastleMediumSpawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMediumSpawner1")
					{
						GameObject fMS1 = GameObject.Find ("FieldsMediumSpawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMediumSpawner2")
					{
						GameObject fMS2 = GameObject.Find ("FieldsMediumSpawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					//sniper 3-5
					if(spawnPoints[i].name == "FieldsMediumSpawner3")
					{
						GameObject fMS3 = GameObject.Find ("FieldsMediumSpawner3");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (sniper, fMS3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        yield return new WaitForSeconds (spawnWait);
					}

					if(spawnPoints[i].name == "SkylineMediumSpawner1")
					{
						GameObject sMS1 = GameObject.Find ("SkylineMediumSpawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (jetpack, sMS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					if(spawnPoints[i].name == "SkylineMediumSpawner2")
					{
						GameObject sMS2 = GameObject.Find ("SkylineMediumSpawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (skylineGroundEnemies[Random.Range(0,skylineGroundEnemies.Length)], sMS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					if(spawnPoints[i].name == "StreetsMedium1Spawner1")
					{
						GameObject sM1S1 = GameObject.Find ("StreetsMedium1Spawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM1S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					if(spawnPoints[i].name == "StreetsMedium1Spawner2")
					{
						GameObject sM1S2 = GameObject.Find ("StreetsMedium1Spawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM1S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					if(spawnPoints[i].name == "StreetsMedium2Spawner1")
					{
						GameObject sM2S1 = GameObject.Find ("StreetsMedium2Spawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM2S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					if(spawnPoints[i].name == "StreetsMedium2Spawner2")
					{
						GameObject sM2S2 = GameObject.Find ("StreetsMedium2Spawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM2S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
				}

				if (waveCount == 7)
				{
					if(spawnPoints[i].name == "ApocMediumSpawner1")
					{
						GameObject aMS1 = GameObject.Find ("ApocMediumSpawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "ApocMediumSpawner2")
					{
						GameObject aMS2 = GameObject.Find ("ApocMediumSpawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (jetpack, aMS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (jetpack, aMS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMediumSpawner1")
					{
						GameObject cMS1 = GameObject.Find ("CastleMediumSpawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMediumSpawner2")
					{
						GameObject cMS2 = GameObject.Find ("CastleMediumSpawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMediumSpawner1")
					{
						GameObject fMS1 = GameObject.Find ("FieldsMediumSpawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMediumSpawner2")
					{
						GameObject fMS2 = GameObject.Find ("FieldsMediumSpawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					if(spawnPoints[i].name == "FieldsMediumSpawner4")
					{
						GameObject fMS4 = GameObject.Find ("FieldsMediumSpawner4");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (sniper, fMS4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "SkylineMediumSpawner1")
					{
						GameObject sMS1 = GameObject.Find ("SkylineMediumSpawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (jetpack, sMS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (jetpack, sMS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "SkylineMediumSpawner2")
					{
						GameObject sMS2 = GameObject.Find ("SkylineMediumSpawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (skylineGroundEnemies[Random.Range(0,skylineGroundEnemies.Length)], sMS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (skylineGroundEnemies[Random.Range(0,skylineGroundEnemies.Length)], sMS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "StreetsMedium1Spawner1")
					{
						GameObject sM1S1 = GameObject.Find ("StreetsMedium1Spawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM1S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM1S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "StreetsMedium1Spawner2")
					{
						GameObject sM1S2 = GameObject.Find ("StreetsMedium1Spawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM1S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM1S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "StreetsMedium2Spawner1")
					{
						GameObject sM2S1 = GameObject.Find ("StreetsMedium2Spawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM2S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM2S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "StreetsMedium2Spawner2")
					{
						GameObject sM2S2 = GameObject.Find ("StreetsMedium2Spawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM2S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM2S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
				}

				if (waveCount == 8)
				{
					if(spawnPoints[i].name == "ApocMediumSpawner1")
					{
						GameObject aMS1 = GameObject.Find ("ApocMediumSpawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMS1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "ApocMediumSpawner2")
					{
						GameObject aMS2 = GameObject.Find ("ApocMediumSpawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (jetpack, aMS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (jetpack, aMS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (jetpack, aMS2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMediumSpawner1")
					{
						GameObject cMS1 = GameObject.Find ("CastleMediumSpawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMS1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMediumSpawner2")
					{
						GameObject cMS2 = GameObject.Find ("CastleMediumSpawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMS2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMediumSpawner1")
					{
						GameObject fMS1 = GameObject.Find ("FieldsMediumSpawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMS1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMediumSpawner2")
					{
						GameObject fMS2 = GameObject.Find ("FieldsMediumSpawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMS2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					if(spawnPoints[i].name == "FieldsMediumSpawner5")
					{
						GameObject fMS5 = GameObject.Find ("FieldsMediumSpawner5");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (sniper, fMS5.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "SkylineMediumSpawner1")
					{
						GameObject sMS1 = GameObject.Find ("SkylineMediumSpawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (jetpack, sMS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (jetpack, sMS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (jetpack, sMS1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "SkylineMediumSpawner2")
					{
						GameObject sMS2 = GameObject.Find ("SkylineMediumSpawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (skylineGroundEnemies[Random.Range(0,skylineGroundEnemies.Length)], sMS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (skylineGroundEnemies[Random.Range(0,skylineGroundEnemies.Length)], sMS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (skylineGroundEnemies[Random.Range(0,skylineGroundEnemies.Length)], sMS2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "StreetsMedium1Spawner1")
					{
						GameObject sM1S1 = GameObject.Find ("StreetsMedium1Spawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM1S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM1S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM1S1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "StreetsMedium1Spawner2")
					{
						GameObject sM1S2 = GameObject.Find ("StreetsMedium1Spawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM1S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM1S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM1S2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "StreetsMedium2Spawner1")
					{
						GameObject sM2S1 = GameObject.Find ("StreetsMedium2Spawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM2S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM2S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM2S1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "StreetsMedium2Spawner2")
					{
						GameObject sM2S2 = GameObject.Find ("StreetsMedium2Spawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM2S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM2S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM2S2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
				}

				if (waveCount == 9)
				{
					if(spawnPoints[i].name == "ApocMediumSpawner1")
					{
						GameObject aMS1 = GameObject.Find ("ApocMediumSpawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMS1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMS1.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "ApocMediumSpawner2")
					{
						GameObject aMS2 = GameObject.Find ("ApocMediumSpawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (jetpack, aMS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (jetpack, aMS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (jetpack, aMS2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (jetpack, aMS2.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMediumSpawner1")
					{
						GameObject cMS1 = GameObject.Find ("CastleMediumSpawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMS1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMS1.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMediumSpawner2")
					{
						GameObject cMS2 = GameObject.Find ("CastleMediumSpawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMS2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMS2.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMediumSpawner1")
					{
						GameObject fMS1 = GameObject.Find ("FieldsMediumSpawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMS1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMS1.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMediumSpawner2")
					{
						GameObject fMS2 = GameObject.Find ("FieldsMediumSpawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMS2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMS2.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					if(spawnPoints[i].name == "FieldsMediumSpawner3")
					{
						GameObject fMS3 = GameObject.Find ("FieldsMediumSpawner3");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (sniper, fMS3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "SkylineMediumSpawner1")
					{
						GameObject sMS1 = GameObject.Find ("SkylineMediumSpawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (jetpack, sMS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (jetpack, sMS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (jetpack, sMS1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (jetpack, sMS1.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "SkylineMediumSpawner2")
					{
						GameObject sMS2 = GameObject.Find ("SkylineMediumSpawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (skylineGroundEnemies[Random.Range(0,skylineGroundEnemies.Length)], sMS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (skylineGroundEnemies[Random.Range(0,skylineGroundEnemies.Length)], sMS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (skylineGroundEnemies[Random.Range(0,skylineGroundEnemies.Length)], sMS2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (skylineGroundEnemies[Random.Range(0,skylineGroundEnemies.Length)], sMS2.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "StreetsMedium1Spawner1")
					{
						GameObject sM1S1 = GameObject.Find ("StreetsMedium1Spawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM1S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM1S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM1S1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM1S1.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "StreetsMedium1Spawner2")
					{
						GameObject sM1S2 = GameObject.Find ("StreetsMedium1Spawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM1S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM1S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM1S2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM1S2.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "StreetsMedium2Spawner1")
					{
						GameObject sM2S1 = GameObject.Find ("StreetsMedium2Spawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM2S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM2S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM2S1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM2S1.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "StreetsMedium2Spawner2")
					{
						GameObject sM2S2 = GameObject.Find ("StreetsMedium2Spawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM2S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM2S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM2S2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sM2S2.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
				}

				if (waveCount == 11)
				{
					if(spawnPoints[i].name == "ApocMinibossSpawner")
					{
						GameObject aMBS = GameObject.Find ("ApocMinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					if(spawnPoints[i].name == "ApocMinibossMinibossSpawner")
					{
						GameObject aMBMBS = GameObject.Find ("ApocMinibossMinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (apocMiniboss, aMBMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					if(spawnPoints[i].name == "CastleMiniboss1Spawner")
					{
						GameObject cMB1S = GameObject.Find ("CastleMiniboss1Spawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMB1S.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMiniboss1MinibossSpawner")
					{
						GameObject cMB1MBS = GameObject.Find ("CastleMiniboss1MinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleMiniboss, cMB1MBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					if(spawnPoints[i].name == "CastleMiniboss2Spawner")
					{
						GameObject cMB2S = GameObject.Find ("CastleMiniboss2Spawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMB2S.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMiniboss2MinibossSpawner")
					{
						GameObject cMB2MBS = GameObject.Find ("CastleMiniboss2MinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleMiniboss, cMB2MBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					if(spawnPoints[i].name == "FieldsMinibossSpawner")
					{
						GameObject fMBS = GameObject.Find ("FieldsMinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMinibossMinibossSpawner")
					{
						GameObject fMBMBS = GameObject.Find ("FieldsMinibossMinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldMiniboss, fMBMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					if(spawnPoints[i].name == "SkylineMinibossSpawner")
					{
						GameObject sMBS = GameObject.Find ("SkylineMinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (jetpack, sMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "SkylineMinibossMinibossSpawner")
					{
						GameObject sMBMBS = GameObject.Find ("SkylineMinibossMinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (skylineMiniboss, sMBMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					//remember spawner2
					if(spawnPoints[i].name == "StreetsMinibossSpawner1")
					{
						GameObject sMBS1 = GameObject.Find ("StreetsMinibossSpawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMBS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "StreetsMinibossMinibossSpawner")
					{
						GameObject sMBMBS = GameObject.Find ("StreetsMinibossMinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsMiniboss, sMBMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

                    if (spawnPoints[i].name == "ApocMediumSpawner1")
                    {
                        GameObject aMS1 = GameObject.Find("ApocMediumSpawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "ApocMediumSpawner2")
                    {
                        GameObject aMS2 = GameObject.Find("ApocMediumSpawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(jetpack, aMS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMediumSpawner1")
                    {
                        GameObject cMS1 = GameObject.Find("CastleMediumSpawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMediumSpawner2")
                    {
                        GameObject cMS2 = GameObject.Find("CastleMediumSpawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMediumSpawner1")
                    {
                        GameObject fMS1 = GameObject.Find("FieldsMediumSpawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMediumSpawner2")
                    {
                        GameObject fMS2 = GameObject.Find("FieldsMediumSpawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    //sniper 3-5
                    if (spawnPoints[i].name == "FieldsMediumSpawner3")
                    {
                        GameObject fMS3 = GameObject.Find("FieldsMediumSpawner3");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(sniper, fMS3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "SkylineMediumSpawner1")
                    {
                        GameObject sMS1 = GameObject.Find("SkylineMediumSpawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(jetpack, sMS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "SkylineMediumSpawner2")
                    {
                        GameObject sMS2 = GameObject.Find("SkylineMediumSpawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(skylineGroundEnemies[Random.Range(0, skylineGroundEnemies.Length)], sMS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMedium1Spawner1")
                    {
                        GameObject sM1S1 = GameObject.Find("StreetsMedium1Spawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM1S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMedium1Spawner2")
                    {
                        GameObject sM1S2 = GameObject.Find("StreetsMedium1Spawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM1S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMedium2Spawner1")
                    {
                        GameObject sM2S1 = GameObject.Find("StreetsMedium2Spawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM2S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMedium2Spawner2")
                    {
                        GameObject sM2S2 = GameObject.Find("StreetsMedium2Spawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM2S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }
                }

				if (waveCount == 12)
				{
					if(spawnPoints[i].name == "ApocMinibossSpawner")
					{
						GameObject aMBS = GameObject.Find ("ApocMinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMBS.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "ApocMinibossMinibossSpawner")
					{
						GameObject aMBMBS = GameObject.Find ("ApocMinibossMinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (apocMiniboss, aMBMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMiniboss1Spawner")
					{
						GameObject cMB1S = GameObject.Find ("CastleMiniboss1Spawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMB1S.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMB1S.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMiniboss1MinibossSpawner")
					{
						GameObject cMB1MBS = GameObject.Find ("CastleMiniboss1MinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleMiniboss, cMB1MBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMiniboss2Spawner")
					{
						GameObject cMB2S = GameObject.Find ("CastleMiniboss2Spawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMB2S.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMB2S.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMiniboss2MinibossSpawner")
					{
						GameObject cMB2MBS = GameObject.Find ("CastleMiniboss2MinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleMiniboss, cMB2MBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMinibossSpawner")
					{
						GameObject fMBS = GameObject.Find ("FieldsMinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMBS.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMinibossMinibossSpawner")
					{
						GameObject fMBMBS = GameObject.Find ("FieldsMinibossMinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldMiniboss, fMBMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "SkylineMinibossSpawner")
					{
						GameObject sMBS = GameObject.Find ("SkylineMinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (jetpack, sMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (jetpack, sMBS.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "SkylineMinibossMinibossSpawner")
					{
						GameObject sMBMBS = GameObject.Find ("SkylineMinibossMinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (skylineMiniboss, sMBMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					//remember spawner2
					if(spawnPoints[i].name == "StreetsMinibossSpawner1")
					{
						GameObject sMBS1 = GameObject.Find ("StreetsMinibossSpawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMBS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					if(spawnPoints[i].name == "StreetsMinibossSpawner2")
					{
						GameObject sMBS2 = GameObject.Find ("StreetsMinibossSpawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMBS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "StreetsMinibossMinibossSpawner")
					{
						GameObject sMBMBS = GameObject.Find ("StreetsMinibossMinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsMiniboss, sMBMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

                    if (spawnPoints[i].name == "ApocMediumSpawner1")
                    {
                        GameObject aMS1 = GameObject.Find("ApocMediumSpawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "ApocMediumSpawner2")
                    {
                        GameObject aMS2 = GameObject.Find("ApocMediumSpawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(jetpack, aMS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(jetpack, aMS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMediumSpawner1")
                    {
                        GameObject cMS1 = GameObject.Find("CastleMediumSpawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMediumSpawner2")
                    {
                        GameObject cMS2 = GameObject.Find("CastleMediumSpawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMediumSpawner1")
                    {
                        GameObject fMS1 = GameObject.Find("FieldsMediumSpawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMediumSpawner2")
                    {
                        GameObject fMS2 = GameObject.Find("FieldsMediumSpawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMediumSpawner4")
                    {
                        GameObject fMS4 = GameObject.Find("FieldsMediumSpawner4");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(sniper, fMS4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "SkylineMediumSpawner1")
                    {
                        GameObject sMS1 = GameObject.Find("SkylineMediumSpawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(jetpack, sMS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(jetpack, sMS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "SkylineMediumSpawner2")
                    {
                        GameObject sMS2 = GameObject.Find("SkylineMediumSpawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(skylineGroundEnemies[Random.Range(0, skylineGroundEnemies.Length)], sMS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(skylineGroundEnemies[Random.Range(0, skylineGroundEnemies.Length)], sMS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMedium1Spawner1")
                    {
                        GameObject sM1S1 = GameObject.Find("StreetsMedium1Spawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM1S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM1S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMedium1Spawner2")
                    {
                        GameObject sM1S2 = GameObject.Find("StreetsMedium1Spawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM1S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM1S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMedium2Spawner1")
                    {
                        GameObject sM2S1 = GameObject.Find("StreetsMedium2Spawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM2S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM2S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMedium2Spawner2")
                    {
                        GameObject sM2S2 = GameObject.Find("StreetsMedium2Spawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM2S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM2S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }
                }

				if (waveCount == 13)
				{
					if(spawnPoints[i].name == "ApocMinibossSpawner")
					{
						GameObject aMBS = GameObject.Find ("ApocMinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMBS.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMBS.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "ApocMinibossMinibossSpawner")
					{
						GameObject aMBMBS = GameObject.Find ("ApocMinibossMinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (apocMiniboss, aMBMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMiniboss1Spawner")
					{
						GameObject cMB1S = GameObject.Find ("CastleMiniboss1Spawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMB1S.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMB1S.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMB1S.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMiniboss1MinibossSpawner")
					{
						GameObject cMB1MBS = GameObject.Find ("CastleMiniboss1MinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleMiniboss, cMB1MBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMiniboss2Spawner")
					{
						GameObject cMB2S = GameObject.Find ("CastleMiniboss2Spawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMB2S.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMB2S.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMB2S.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMiniboss2MinibossSpawner")
					{
						GameObject cMB2MBS = GameObject.Find ("CastleMiniboss2MinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleMiniboss, cMB2MBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMinibossSpawner")
					{
						GameObject fMBS = GameObject.Find ("FieldsMinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMBS.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMBS.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMinibossMinibossSpawner")
					{
						GameObject fMBMBS = GameObject.Find ("FieldsMinibossMinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldMiniboss, fMBMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "SkylineMinibossSpawner")
					{
						GameObject sMBS = GameObject.Find ("SkylineMinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (jetpack, sMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (jetpack, sMBS.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (jetpack, sMBS.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "SkylineMinibossMinibossSpawner")
					{
						GameObject sMBMBS = GameObject.Find ("SkylineMinibossMinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (skylineMiniboss, sMBMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					//remember spawner2
					if(spawnPoints[i].name == "StreetsMinibossSpawner1")
					{
						GameObject sMBS1 = GameObject.Find ("StreetsMinibossSpawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMBS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMBS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "StreetsMinibossSpawner2")
					{
						GameObject sMBS2 = GameObject.Find ("StreetsMinibossSpawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMBS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "StreetsMinibossMinibossSpawner")
					{
						GameObject sMBMBS = GameObject.Find ("StreetsMinibossMinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsMiniboss, sMBMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

                    if (spawnPoints[i].name == "ApocMediumSpawner1")
                    {
                        GameObject aMS1 = GameObject.Find("ApocMediumSpawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMS1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "ApocMediumSpawner2")
                    {
                        GameObject aMS2 = GameObject.Find("ApocMediumSpawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(jetpack, aMS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(jetpack, aMS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(jetpack, aMS2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMediumSpawner1")
                    {
                        GameObject cMS1 = GameObject.Find("CastleMediumSpawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMS1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMediumSpawner2")
                    {
                        GameObject cMS2 = GameObject.Find("CastleMediumSpawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMS2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMediumSpawner1")
                    {
                        GameObject fMS1 = GameObject.Find("FieldsMediumSpawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMS1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMediumSpawner2")
                    {
                        GameObject fMS2 = GameObject.Find("FieldsMediumSpawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMS2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMediumSpawner5")
                    {
                        GameObject fMS5 = GameObject.Find("FieldsMediumSpawner5");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(sniper, fMS5.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "SkylineMediumSpawner1")
                    {
                        GameObject sMS1 = GameObject.Find("SkylineMediumSpawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(jetpack, sMS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(jetpack, sMS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(jetpack, sMS1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "SkylineMediumSpawner2")
                    {
                        GameObject sMS2 = GameObject.Find("SkylineMediumSpawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(skylineGroundEnemies[Random.Range(0, skylineGroundEnemies.Length)], sMS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(skylineGroundEnemies[Random.Range(0, skylineGroundEnemies.Length)], sMS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(skylineGroundEnemies[Random.Range(0, skylineGroundEnemies.Length)], sMS2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMedium1Spawner1")
                    {
                        GameObject sM1S1 = GameObject.Find("StreetsMedium1Spawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM1S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM1S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM1S1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMedium1Spawner2")
                    {
                        GameObject sM1S2 = GameObject.Find("StreetsMedium1Spawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM1S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM1S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM1S2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMedium2Spawner1")
                    {
                        GameObject sM2S1 = GameObject.Find("StreetsMedium2Spawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM2S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM2S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM2S1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMedium2Spawner2")
                    {
                        GameObject sM2S2 = GameObject.Find("StreetsMedium2Spawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM2S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM2S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM2S2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }
                }

				if (waveCount == 14)
				{
					if(spawnPoints[i].name == "ApocMinibossSpawner")
					{
						GameObject aMBS = GameObject.Find ("ApocMinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMBS.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMBS.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMBS.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "ApocMinibossMinibossSpawner")
					{
						GameObject aMBMBS = GameObject.Find ("ApocMinibossMinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (apocMiniboss, aMBMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocMiniboss, aMBMBS.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMiniboss1Spawner")
					{
						GameObject cMB1S = GameObject.Find ("CastleMiniboss1Spawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMB1S.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMB1S.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMB1S.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMB1S.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMiniboss1MinibossSpawner")
					{
						GameObject cMB1MBS = GameObject.Find ("CastleMiniboss1MinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleMiniboss, cMB1MBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleMiniboss, cMB1MBS.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMiniboss2Spawner")
					{
						GameObject cMB2S = GameObject.Find ("CastleMiniboss2Spawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMB2S.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMB2S.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMB2S.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMB2S.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMiniboss2MinibossSpawner")
					{
						GameObject cMB2MBS = GameObject.Find ("CastleMiniboss2MinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleMiniboss, cMB2MBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleMiniboss, cMB2MBS.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMinibossSpawner")
					{
						GameObject fMBS = GameObject.Find ("FieldsMinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMBS.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMBS.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMBS.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMinibossMinibossSpawner")
					{
						GameObject fMBMBS = GameObject.Find ("FieldsMinibossMinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldMiniboss, fMBMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldMiniboss, fMBMBS.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "SkylineMinibossSpawner")
					{
						GameObject sMBS = GameObject.Find ("SkylineMinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (jetpack, sMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (jetpack, sMBS.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (jetpack, sMBS.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (jetpack, sMBS.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "SkylineMinibossMinibossSpawner")
					{
						GameObject sMBMBS = GameObject.Find ("SkylineMinibossMinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (skylineMiniboss, sMBMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (skylineMiniboss, sMBMBS.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					//remember spawner2
					if(spawnPoints[i].name == "StreetsMinibossSpawner1")
					{
						GameObject sMBS1 = GameObject.Find ("StreetsMinibossSpawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMBS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMBS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "StreetsMinibossSpawner2")
					{
						GameObject sMBS2 = GameObject.Find ("StreetsMinibossSpawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMBS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMBS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "StreetsMinibossMinibossSpawner")
					{
						GameObject sMBMBS = GameObject.Find ("StreetsMinibossMinibossSpawner");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsMiniboss, sMBMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsMiniboss, sMBMBS.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

                    if (spawnPoints[i].name == "ApocMediumSpawner1")
                    {
                        GameObject aMS1 = GameObject.Find("ApocMediumSpawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMS1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMS1.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "ApocMediumSpawner2")
                    {
                        GameObject aMS2 = GameObject.Find("ApocMediumSpawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(jetpack, aMS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(jetpack, aMS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(jetpack, aMS2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(jetpack, aMS2.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMediumSpawner1")
                    {
                        GameObject cMS1 = GameObject.Find("CastleMediumSpawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMS1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMS1.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMediumSpawner2")
                    {
                        GameObject cMS2 = GameObject.Find("CastleMediumSpawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMS2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMS2.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMediumSpawner1")
                    {
                        GameObject fMS1 = GameObject.Find("FieldsMediumSpawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMS1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMS1.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMediumSpawner2")
                    {
                        GameObject fMS2 = GameObject.Find("FieldsMediumSpawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMS2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMS2.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMediumSpawner3")
                    {
                        GameObject fMS3 = GameObject.Find("FieldsMediumSpawner3");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(sniper, fMS3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "SkylineMediumSpawner1")
                    {
                        GameObject sMS1 = GameObject.Find("SkylineMediumSpawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(jetpack, sMS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(jetpack, sMS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(jetpack, sMS1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(jetpack, sMS1.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "SkylineMediumSpawner2")
                    {
                        GameObject sMS2 = GameObject.Find("SkylineMediumSpawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(skylineGroundEnemies[Random.Range(0, skylineGroundEnemies.Length)], sMS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(skylineGroundEnemies[Random.Range(0, skylineGroundEnemies.Length)], sMS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(skylineGroundEnemies[Random.Range(0, skylineGroundEnemies.Length)], sMS2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(skylineGroundEnemies[Random.Range(0, skylineGroundEnemies.Length)], sMS2.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMedium1Spawner1")
                    {
                        GameObject sM1S1 = GameObject.Find("StreetsMedium1Spawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM1S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM1S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM1S1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM1S1.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMedium1Spawner2")
                    {
                        GameObject sM1S2 = GameObject.Find("StreetsMedium1Spawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM1S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM1S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM1S2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM1S2.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMedium2Spawner1")
                    {
                        GameObject sM2S1 = GameObject.Find("StreetsMedium2Spawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM2S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM2S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM2S1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM2S1.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMedium2Spawner2")
                    {
                        GameObject sM2S2 = GameObject.Find("StreetsMedium2Spawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM2S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM2S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM2S2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sM2S2.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }
                }

				if (waveCount == 16)
				{
					if(spawnPoints[i].name == "ApocMassiveSpawner1")
					{
						GameObject aMAS1 = GameObject.Find ("ApocMassiveSpawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					if(spawnPoints[i].name == "ApocMassiveSpawner2")
					{
						GameObject aMAS2 = GameObject.Find ("ApocMassiveSpawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					if(spawnPoints[i].name == "ApocMassiveSpawner3")
					{
						GameObject aMAS3 = GameObject.Find ("ApocMassiveSpawner3");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "ApocMassiveSpawner4")
					{
						GameObject aMAS4 = GameObject.Find ("ApocMassiveSpawner4");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMassive1Spawner1")
					{
						GameObject cMA1S1 = GameObject.Find ("CastleMassive1Spawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMassive1Spawner2")
					{
						GameObject cMA1S2 = GameObject.Find ("CastleMassive1Spawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					if(spawnPoints[i].name == "CastleMassive1Spawner3")
					{
						GameObject cMA1S3 = GameObject.Find ("CastleMassive1Spawner3");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					if(spawnPoints[i].name == "CastleMassive1Spawner4")
					{
						GameObject cMA1S4 = GameObject.Find ("CastleMassive1Spawner4");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					if(spawnPoints[i].name == "CastleMassive2Spawner1")
					{
						GameObject cMA2S1 = GameObject.Find ("CastleMassive2Spawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMassive2Spawner2")
					{
						GameObject cMA2S2 = GameObject.Find ("CastleMassive2Spawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMassive2Spawner3")
					{
						GameObject cMA2S3 = GameObject.Find ("CastleMassive2Spawner3");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMassive2Spawner4")
					{
						GameObject cMA2S4 = GameObject.Find ("CastleMassive2Spawner4");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMassive1Spawner1")
					{
						GameObject fMA1S1 = GameObject.Find ("FieldsMassive1Spawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMassive1Spawner2")
					{
						GameObject fMA1S2 = GameObject.Find ("FieldsMassive1Spawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					if(spawnPoints[i].name == "FieldsMassive1Spawner3")
					{
						GameObject fMA1S3 = GameObject.Find ("FieldsMassive1Spawner3");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					if(spawnPoints[i].name == "FieldsMassive1Spawner4")
					{
						GameObject fMA1S4 = GameObject.Find ("FieldsMassive1Spawner4");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					if(spawnPoints[i].name == "FieldsMassive1Spawner5")
					{
						GameObject fMA1S5 = GameObject.Find ("FieldsMassive1Spawner5");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (sniper, fMA1S5.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMassive2Spawner1")
					{
						GameObject fMA2S1 = GameObject.Find ("FieldsMassive2Spawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMassive2Spawner2")
					{
						GameObject fMA2S2 = GameObject.Find ("FieldsMassive2Spawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMassive2Spawner3")
					{
						GameObject fMA2S3 = GameObject.Find ("FieldsMassive2Spawner3");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMassive2Spawner4")
					{
						GameObject fMA2S4 = GameObject.Find ("FieldsMassive2Spawner4");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					//sniper 5-6
					if(spawnPoints[i].name == "FieldsMassive2Spawner5")
					{
						GameObject fMA2S5 = GameObject.Find ("FieldsMassive2Spawner5");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (sniper, fMA2S5.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "SkylineMassiveSpawner1")
					{
						GameObject sMAS1 = GameObject.Find ("SkylineMassiveSpawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (skylineGroundEnemies[Random.Range(0,skylineGroundEnemies.Length)], sMAS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "SkylineMassiveSpawner2")
					{
						GameObject sMAS2 = GameObject.Find ("SkylineMassiveSpawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (skylineGroundEnemies[Random.Range(0,skylineGroundEnemies.Length)], sMAS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "SkylineMassiveSpawner3")
					{
						GameObject sMAS3 = GameObject.Find ("SkylineMassiveSpawner3");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (jetpack, sMAS3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "SkylineMassiveSpawner4")
					{
						GameObject sMAS4 = GameObject.Find ("SkylineMassiveSpawner4");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (jetpack, sMAS4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "StreetsMassiveSpawner1")
					{
						GameObject sMAS1 = GameObject.Find ("StreetsMassiveSpawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "StreetsMassiveSpawner2")
					{
						GameObject sMAS2 = GameObject.Find ("StreetsMassiveSpawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					if(spawnPoints[i].name == "StreetsMassiveSpawner3")
					{
						GameObject sMAS3 = GameObject.Find ("StreetsMassiveSpawner3");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "StreetsMassiveSpawner4")
					{
						GameObject sMAS4 = GameObject.Find ("StreetsMassiveSpawner4");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
				}

				if (waveCount == 17)
				{
					if(spawnPoints[i].name == "ApocMassiveSpawner1")
					{
						GameObject aMAS1 = GameObject.Find ("ApocMassiveSpawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "ApocMassiveSpawner2")
					{
						GameObject aMAS2 = GameObject.Find ("ApocMassiveSpawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "ApocMassiveSpawner3")
					{
						GameObject aMAS3 = GameObject.Find ("ApocMassiveSpawner3");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "ApocMassiveSpawner4")
					{
						GameObject aMAS4 = GameObject.Find ("ApocMassiveSpawner4");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMassive1Spawner1")
					{
						GameObject cMA1S1 = GameObject.Find ("CastleMassive1Spawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMassive1Spawner2")
					{
						GameObject cMA1S2 = GameObject.Find ("CastleMassive1Spawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMassive1Spawner3")
					{
						GameObject cMA1S3 = GameObject.Find ("CastleMassive1Spawner3");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMassive1Spawner4")
					{
						GameObject cMA1S4 = GameObject.Find ("CastleMassive1Spawner4");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMassive2Spawner1")
					{
						GameObject cMA2S1 = GameObject.Find ("CastleMassive2Spawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMassive2Spawner2")
					{
						GameObject cMA2S2 = GameObject.Find ("CastleMassive2Spawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMassive2Spawner3")
					{
						GameObject cMA2S3 = GameObject.Find ("CastleMassive2Spawner3");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMassive2Spawner4")
					{
						GameObject cMA2S4 = GameObject.Find ("CastleMassive2Spawner4");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMassive1Spawner1")
					{
						GameObject fMA1S1 = GameObject.Find ("FieldsMassive1Spawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMassive1Spawner2")
					{
						GameObject fMA1S2 = GameObject.Find ("FieldsMassive1Spawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMassive1Spawner3")
					{
						GameObject fMA1S3 = GameObject.Find ("FieldsMassive1Spawner3");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMassive1Spawner4")
					{
						GameObject fMA1S4 = GameObject.Find ("FieldsMassive1Spawner4");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					if(spawnPoints[i].name == "FieldsMassive1Spawner6")
					{
						GameObject fMA1S6 = GameObject.Find ("FieldsMassive1Spawner6");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (sniper, fMA1S6.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMassive2Spawner1")
					{
						GameObject fMA2S1 = GameObject.Find ("FieldsMassive2Spawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMassive2Spawner2")
					{
						GameObject fMA2S2 = GameObject.Find ("FieldsMassive2Spawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMassive2Spawner3")
					{
						GameObject fMA2S3 = GameObject.Find ("FieldsMassive2Spawner3");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMassive2Spawner4")
					{
						GameObject fMA2S4 = GameObject.Find ("FieldsMassive2Spawner4");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					if(spawnPoints[i].name == "FieldsMassive2Spawner6")
					{
						GameObject fMA2S6 = GameObject.Find ("FieldsMassive2Spawner6");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (sniper, fMA2S6.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "SkylineMassiveSpawner1")
					{
						GameObject sMAS1 = GameObject.Find ("SkylineMassiveSpawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (skylineGroundEnemies[Random.Range(0,skylineGroundEnemies.Length)], sMAS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (skylineGroundEnemies[Random.Range(0,skylineGroundEnemies.Length)], sMAS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "SkylineMassiveSpawner2")
					{
						GameObject sMAS2 = GameObject.Find ("SkylineMassiveSpawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (skylineGroundEnemies[Random.Range(0,skylineGroundEnemies.Length)], sMAS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (skylineGroundEnemies[Random.Range(0,skylineGroundEnemies.Length)], sMAS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "SkylineMassiveSpawner3")
					{
						GameObject sMAS3 = GameObject.Find ("SkylineMassiveSpawner3");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (jetpack, sMAS3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (jetpack, sMAS3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "SkylineMassiveSpawner4")
					{
						GameObject sMAS4 = GameObject.Find ("SkylineMassiveSpawner4");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (jetpack, sMAS4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (jetpack, sMAS4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "StreetsMassiveSpawner1")
					{
						GameObject sMAS1 = GameObject.Find ("StreetsMassiveSpawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "StreetsMassiveSpawner2")
					{
						GameObject sMAS2 = GameObject.Find ("StreetsMassiveSpawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "StreetsMassiveSpawner3")
					{
						GameObject sMAS3 = GameObject.Find ("StreetsMassiveSpawner3");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "StreetsMassiveSpawner4")
					{
						GameObject sMAS4 = GameObject.Find ("StreetsMassiveSpawner4");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
				}

				if (waveCount == 18)
				{
					if(spawnPoints[i].name == "ApocMassiveSpawner1")
					{
						GameObject aMAS1 = GameObject.Find ("ApocMassiveSpawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "ApocMassiveSpawner2")
					{
						GameObject aMAS2 = GameObject.Find ("ApocMassiveSpawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "ApocMassiveSpawner3")
					{
						GameObject aMAS3 = GameObject.Find ("ApocMassiveSpawner3");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS3.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "ApocMassiveSpawner4")
					{
						GameObject aMAS4 = GameObject.Find ("ApocMassiveSpawner4");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS4.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMassive1Spawner1")
					{
						GameObject cMA1S1 = GameObject.Find ("CastleMassive1Spawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMassive1Spawner2")
					{
						GameObject cMA1S2 = GameObject.Find ("CastleMassive1Spawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMassive1Spawner3")
					{
						GameObject cMA1S3 = GameObject.Find ("CastleMassive1Spawner3");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S3.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMassive1Spawner4")
					{
						GameObject cMA1S4 = GameObject.Find ("CastleMassive1Spawner4");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S4.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMassive2Spawner1")
					{
						GameObject cMA2S1 = GameObject.Find ("CastleMassive2Spawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMassive2Spawner2")
					{
						GameObject cMA2S2 = GameObject.Find ("CastleMassive2Spawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMassive2Spawner3")
					{
						GameObject cMA2S3 = GameObject.Find ("CastleMassive2Spawner3");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S3.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMassive2Spawner4")
					{
						GameObject cMA2S4 = GameObject.Find ("CastleMassive2Spawner4");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S4.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMassive1Spawner1")
					{
						GameObject fMA1S1 = GameObject.Find ("FieldsMassive1Spawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMassive1Spawner2")
					{
						GameObject fMA1S2 = GameObject.Find ("FieldsMassive1Spawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMassive1Spawner3")
					{
						GameObject fMA1S3 = GameObject.Find ("FieldsMassive1Spawner3");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S3.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMassive1Spawner4")
					{
						GameObject fMA1S4 = GameObject.Find ("FieldsMassive1Spawner4");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S4.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					if(spawnPoints[i].name == "FieldsMassive1Spawner7")
					{
						GameObject fMA1S7 = GameObject.Find ("FieldsMassive1Spawner7");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (sniper, fMA1S7.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMassive2Spawner1")
					{
						GameObject fMA2S1 = GameObject.Find ("FieldsMassive2Spawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMassive2Spawner2")
					{
						GameObject fMA2S2 = GameObject.Find ("FieldsMassive2Spawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMassive2Spawner3")
					{
						GameObject fMA2S3 = GameObject.Find ("FieldsMassive2Spawner3");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S3.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMassive2Spawner4")
					{
						GameObject fMA2S4 = GameObject.Find ("FieldsMassive2Spawner4");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S4.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					if(spawnPoints[i].name == "FieldsMassive2Spawner5")
					{
						GameObject fMA2S5 = GameObject.Find ("FieldsMassive2Spawner5");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (sniper, fMA2S5.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "SkylineMassiveSpawner1")
					{
						GameObject sMAS1 = GameObject.Find ("SkylineMassiveSpawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (skylineGroundEnemies[Random.Range(0,skylineGroundEnemies.Length)], sMAS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (skylineGroundEnemies[Random.Range(0,skylineGroundEnemies.Length)], sMAS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (skylineGroundEnemies[Random.Range(0,skylineGroundEnemies.Length)], sMAS1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "SkylineMassiveSpawner2")
					{
						GameObject sMAS2 = GameObject.Find ("SkylineMassiveSpawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (skylineGroundEnemies[Random.Range(0,skylineGroundEnemies.Length)], sMAS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (skylineGroundEnemies[Random.Range(0,skylineGroundEnemies.Length)], sMAS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (skylineGroundEnemies[Random.Range(0,skylineGroundEnemies.Length)], sMAS2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "SkylineMassiveSpawner3")
					{
						GameObject sMAS3 = GameObject.Find ("SkylineMassiveSpawner3");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (jetpack, sMAS3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (jetpack, sMAS3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (jetpack, sMAS3.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "SkylineMassiveSpawner4")
					{
						GameObject sMAS4 = GameObject.Find ("SkylineMassiveSpawner4");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (jetpack, sMAS4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (jetpack, sMAS4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (jetpack, sMAS4.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "StreetsMassiveSpawner1")
					{
						GameObject sMAS1 = GameObject.Find ("StreetsMassiveSpawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "StreetsMassiveSpawner2")
					{
						GameObject sMAS2 = GameObject.Find ("StreetsMassiveSpawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "StreetsMassiveSpawner3")
					{
						GameObject sMAS3 = GameObject.Find ("StreetsMassiveSpawner3");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS3.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "StreetsMassiveSpawner4")
					{
						GameObject sMAS4 = GameObject.Find ("StreetsMassiveSpawner4");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS4.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
				}

				if (waveCount == 19)
				{
					if(spawnPoints[i].name == "ApocMassiveSpawner1")
					{
						GameObject aMAS1 = GameObject.Find ("ApocMassiveSpawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS1.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "ApocMassiveSpawner2")
					{
						GameObject aMAS2 = GameObject.Find ("ApocMassiveSpawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS2.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "ApocMassiveSpawner3")
					{
						GameObject aMAS3 = GameObject.Find ("ApocMassiveSpawner3");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS3.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS3.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "ApocMassiveSpawner4")
					{
						GameObject aMAS4 = GameObject.Find ("ApocMassiveSpawner4");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS4.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (apocEnemies[Random.Range(0,apocEnemies.Length)], aMAS4.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMassive1Spawner1")
					{
						GameObject cMA1S1 = GameObject.Find ("CastleMassive1Spawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S1.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMassive1Spawner2")
					{
						GameObject cMA1S2 = GameObject.Find ("CastleMassive1Spawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S2.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMassive1Spawner3")
					{
						GameObject cMA1S3 = GameObject.Find ("CastleMassive1Spawner3");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S3.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S3.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMassive1Spawner4")
					{
						GameObject cMA1S4 = GameObject.Find ("CastleMassive1Spawner4");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S4.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA1S4.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMassive2Spawner1")
					{
						GameObject cMA2S1 = GameObject.Find ("CastleMassive2Spawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S1.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMassive2Spawner2")
					{
						GameObject cMA2S2 = GameObject.Find ("CastleMassive2Spawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S2.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMassive2Spawner3")
					{
						GameObject cMA2S3 = GameObject.Find ("CastleMassive2Spawner3");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S3.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S3.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "CastleMassive2Spawner4")
					{
						GameObject cMA2S4 = GameObject.Find ("CastleMassive2Spawner4");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S4.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (castleEnemies[Random.Range(0,castleEnemies.Length)], cMA2S4.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMassive1Spawner1")
					{
						GameObject fMA1S1 = GameObject.Find ("FieldsMassive1Spawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S1.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMassive1Spawner2")
					{
						GameObject fMA1S2 = GameObject.Find ("FieldsMassive1Spawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S2.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMassive1Spawner3")
					{
						GameObject fMA1S3 = GameObject.Find ("FieldsMassive1Spawner3");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S3.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S3.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMassive1Spawner4")
					{
						GameObject fMA1S4 = GameObject.Find ("FieldsMassive1Spawner4");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S4.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA1S4.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					if(spawnPoints[i].name == "FieldsMassive1Spawner8")
					{
						GameObject fMA1S8 = GameObject.Find ("FieldsMassive1Spawner8");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (sniper, fMA1S8.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMassive2Spawner1")
					{
						GameObject fMA2S1 = GameObject.Find ("FieldsMassive2Spawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S1.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMassive2Spawner2")
					{
						GameObject fMA2S2 = GameObject.Find ("FieldsMassive2Spawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S2.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMassive2Spawner3")
					{
						GameObject fMA2S3 = GameObject.Find ("FieldsMassive2Spawner3");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S3.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S3.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "FieldsMassive2Spawner4")
					{
						GameObject fMA2S4 = GameObject.Find ("FieldsMassive2Spawner4");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S4.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (fieldEnemies[Random.Range(0,fieldEnemies.Length)], fMA2S4.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}

					if(spawnPoints[i].name == "FieldsMassive2Spawner6")
					{
						GameObject fMA2S6 = GameObject.Find ("FieldsMassive2Spawner6");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (sniper, fMA2S6.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "SkylineMassiveSpawner1")
					{
						GameObject sMAS1 = GameObject.Find ("SkylineMassiveSpawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (skylineGroundEnemies[Random.Range(0,skylineGroundEnemies.Length)], sMAS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (skylineGroundEnemies[Random.Range(0,skylineGroundEnemies.Length)], sMAS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (skylineGroundEnemies[Random.Range(0,skylineGroundEnemies.Length)], sMAS1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (skylineGroundEnemies[Random.Range(0,skylineGroundEnemies.Length)], sMAS1.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "SkylineMassiveSpawner2")
					{
						GameObject sMAS2 = GameObject.Find ("SkylineMassiveSpawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (skylineGroundEnemies[Random.Range(0,skylineGroundEnemies.Length)], sMAS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (skylineGroundEnemies[Random.Range(0,skylineGroundEnemies.Length)], sMAS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (skylineGroundEnemies[Random.Range(0,skylineGroundEnemies.Length)], sMAS2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (skylineGroundEnemies[Random.Range(0,skylineGroundEnemies.Length)], sMAS2.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "SkylineMassiveSpawner3")
					{
						GameObject sMAS3 = GameObject.Find ("SkylineMassiveSpawner3");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (jetpack, sMAS3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (jetpack, sMAS3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (jetpack, sMAS3.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (jetpack, sMAS3.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "SkylineMassiveSpawner4")
					{
						GameObject sMAS4 = GameObject.Find ("SkylineMassiveSpawner4");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (jetpack, sMAS4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (jetpack, sMAS4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (jetpack, sMAS4.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (jetpack, sMAS4.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "StreetsMassiveSpawner1")
					{
						GameObject sMAS1 = GameObject.Find ("StreetsMassiveSpawner1");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS1.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "StreetsMassiveSpawner2")
					{
						GameObject sMAS2 = GameObject.Find ("StreetsMassiveSpawner2");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS2.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "StreetsMassiveSpawner3")
					{
						GameObject sMAS3 = GameObject.Find ("StreetsMassiveSpawner3");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS3.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS3.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
					
					if(spawnPoints[i].name == "StreetsMassiveSpawner4")
					{
						GameObject sMAS4 = GameObject.Find ("StreetsMassiveSpawner4");
						Quaternion spawnRotation = Quaternion.identity;
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS4.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
						Instantiate (streetsEnemies[Random.Range(0,streetsEnemies.Length)], sMAS4.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
						number++;
						yield return new WaitForSeconds (spawnWait);
					}
				}

				if (waveCount == 21)
				{
                    if (spawnPoints[i].name == "ApocMinibossSpawner")
                    {
                        GameObject aMBS = GameObject.Find("ApocMinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "ApocMinibossMinibossSpawner")
                    {
                        GameObject aMBMBS = GameObject.Find("ApocMinibossMinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(apocMiniboss, aMBMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMiniboss1Spawner")
                    {
                        GameObject cMB1S = GameObject.Find("CastleMiniboss1Spawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMB1S.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMiniboss1MinibossSpawner")
                    {
                        GameObject cMB1MBS = GameObject.Find("CastleMiniboss1MinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleMiniboss, cMB1MBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMiniboss2Spawner")
                    {
                        GameObject cMB2S = GameObject.Find("CastleMiniboss2Spawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMB2S.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMiniboss2MinibossSpawner")
                    {
                        GameObject cMB2MBS = GameObject.Find("CastleMiniboss2MinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleMiniboss, cMB2MBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMinibossSpawner")
                    {
                        GameObject fMBS = GameObject.Find("FieldsMinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMinibossMinibossSpawner")
                    {
                        GameObject fMBMBS = GameObject.Find("FieldsMinibossMinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldMiniboss, fMBMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "SkylineMinibossSpawner")
                    {
                        GameObject sMBS = GameObject.Find("SkylineMinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(jetpack, sMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "SkylineMinibossMinibossSpawner")
                    {
                        GameObject sMBMBS = GameObject.Find("SkylineMinibossMinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(skylineMiniboss, sMBMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    //remember spawner2
                    if (spawnPoints[i].name == "StreetsMinibossSpawner1")
                    {
                        GameObject sMBS1 = GameObject.Find("StreetsMinibossSpawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMBS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMinibossMinibossSpawner")
                    {
                        GameObject sMBMBS = GameObject.Find("StreetsMinibossMinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsMiniboss, sMBMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "ApocMassiveSpawner1")
                    {
                        GameObject aMAS1 = GameObject.Find("ApocMassiveSpawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "ApocMassiveSpawner2")
                    {
                        GameObject aMAS2 = GameObject.Find("ApocMassiveSpawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "ApocMassiveSpawner3")
                    {
                        GameObject aMAS3 = GameObject.Find("ApocMassiveSpawner3");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "ApocMassiveSpawner4")
                    {
                        GameObject aMAS4 = GameObject.Find("ApocMassiveSpawner4");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMassive1Spawner1")
                    {
                        GameObject cMA1S1 = GameObject.Find("CastleMassive1Spawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMassive1Spawner2")
                    {
                        GameObject cMA1S2 = GameObject.Find("CastleMassive1Spawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMassive1Spawner3")
                    {
                        GameObject cMA1S3 = GameObject.Find("CastleMassive1Spawner3");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMassive1Spawner4")
                    {
                        GameObject cMA1S4 = GameObject.Find("CastleMassive1Spawner4");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMassive2Spawner1")
                    {
                        GameObject cMA2S1 = GameObject.Find("CastleMassive2Spawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMassive2Spawner2")
                    {
                        GameObject cMA2S2 = GameObject.Find("CastleMassive2Spawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMassive2Spawner3")
                    {
                        GameObject cMA2S3 = GameObject.Find("CastleMassive2Spawner3");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMassive2Spawner4")
                    {
                        GameObject cMA2S4 = GameObject.Find("CastleMassive2Spawner4");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive1Spawner1")
                    {
                        GameObject fMA1S1 = GameObject.Find("FieldsMassive1Spawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive1Spawner2")
                    {
                        GameObject fMA1S2 = GameObject.Find("FieldsMassive1Spawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive1Spawner3")
                    {
                        GameObject fMA1S3 = GameObject.Find("FieldsMassive1Spawner3");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive1Spawner4")
                    {
                        GameObject fMA1S4 = GameObject.Find("FieldsMassive1Spawner4");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive1Spawner5")
                    {
                        GameObject fMA1S5 = GameObject.Find("FieldsMassive1Spawner5");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(sniper, fMA1S5.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive2Spawner1")
                    {
                        GameObject fMA2S1 = GameObject.Find("FieldsMassive2Spawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive2Spawner2")
                    {
                        GameObject fMA2S2 = GameObject.Find("FieldsMassive2Spawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive2Spawner3")
                    {
                        GameObject fMA2S3 = GameObject.Find("FieldsMassive2Spawner3");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive2Spawner4")
                    {
                        GameObject fMA2S4 = GameObject.Find("FieldsMassive2Spawner4");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    //sniper 5-6
                    if (spawnPoints[i].name == "FieldsMassive2Spawner5")
                    {
                        GameObject fMA2S5 = GameObject.Find("FieldsMassive2Spawner5");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(sniper, fMA2S5.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "SkylineMassiveSpawner1")
                    {
                        GameObject sMAS1 = GameObject.Find("SkylineMassiveSpawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(skylineGroundEnemies[Random.Range(0, skylineGroundEnemies.Length)], sMAS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "SkylineMassiveSpawner2")
                    {
                        GameObject sMAS2 = GameObject.Find("SkylineMassiveSpawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(skylineGroundEnemies[Random.Range(0, skylineGroundEnemies.Length)], sMAS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "SkylineMassiveSpawner3")
                    {
                        GameObject sMAS3 = GameObject.Find("SkylineMassiveSpawner3");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(jetpack, sMAS3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "SkylineMassiveSpawner4")
                    {
                        GameObject sMAS4 = GameObject.Find("SkylineMassiveSpawner4");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(jetpack, sMAS4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMassiveSpawner1")
                    {
                        GameObject sMAS1 = GameObject.Find("StreetsMassiveSpawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMassiveSpawner2")
                    {
                        GameObject sMAS2 = GameObject.Find("StreetsMassiveSpawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMassiveSpawner3")
                    {
                        GameObject sMAS3 = GameObject.Find("StreetsMassiveSpawner3");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMassiveSpawner4")
                    {
                        GameObject sMAS4 = GameObject.Find("StreetsMassiveSpawner4");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }
                }
				
				if (waveCount == 22)
				{
                    if (spawnPoints[i].name == "ApocMinibossSpawner")
                    {
                        GameObject aMBS = GameObject.Find("ApocMinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMBS.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "ApocMinibossMinibossSpawner")
                    {
                        GameObject aMBMBS = GameObject.Find("ApocMinibossMinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(apocMiniboss, aMBMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMiniboss1Spawner")
                    {
                        GameObject cMB1S = GameObject.Find("CastleMiniboss1Spawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMB1S.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMB1S.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMiniboss1MinibossSpawner")
                    {
                        GameObject cMB1MBS = GameObject.Find("CastleMiniboss1MinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleMiniboss, cMB1MBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMiniboss2Spawner")
                    {
                        GameObject cMB2S = GameObject.Find("CastleMiniboss2Spawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMB2S.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMB2S.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMiniboss2MinibossSpawner")
                    {
                        GameObject cMB2MBS = GameObject.Find("CastleMiniboss2MinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleMiniboss, cMB2MBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMinibossSpawner")
                    {
                        GameObject fMBS = GameObject.Find("FieldsMinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMBS.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMinibossMinibossSpawner")
                    {
                        GameObject fMBMBS = GameObject.Find("FieldsMinibossMinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldMiniboss, fMBMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "SkylineMinibossSpawner")
                    {
                        GameObject sMBS = GameObject.Find("SkylineMinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(jetpack, sMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(jetpack, sMBS.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "SkylineMinibossMinibossSpawner")
                    {
                        GameObject sMBMBS = GameObject.Find("SkylineMinibossMinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(skylineMiniboss, sMBMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    //remember spawner2
                    if (spawnPoints[i].name == "StreetsMinibossSpawner1")
                    {
                        GameObject sMBS1 = GameObject.Find("StreetsMinibossSpawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMBS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMinibossSpawner2")
                    {
                        GameObject sMBS2 = GameObject.Find("StreetsMinibossSpawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMBS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMinibossMinibossSpawner")
                    {
                        GameObject sMBMBS = GameObject.Find("StreetsMinibossMinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsMiniboss, sMBMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "ApocMassiveSpawner1")
                    {
                        GameObject aMAS1 = GameObject.Find("ApocMassiveSpawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "ApocMassiveSpawner2")
                    {
                        GameObject aMAS2 = GameObject.Find("ApocMassiveSpawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "ApocMassiveSpawner3")
                    {
                        GameObject aMAS3 = GameObject.Find("ApocMassiveSpawner3");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "ApocMassiveSpawner4")
                    {
                        GameObject aMAS4 = GameObject.Find("ApocMassiveSpawner4");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMassive1Spawner1")
                    {
                        GameObject cMA1S1 = GameObject.Find("CastleMassive1Spawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMassive1Spawner2")
                    {
                        GameObject cMA1S2 = GameObject.Find("CastleMassive1Spawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMassive1Spawner3")
                    {
                        GameObject cMA1S3 = GameObject.Find("CastleMassive1Spawner3");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMassive1Spawner4")
                    {
                        GameObject cMA1S4 = GameObject.Find("CastleMassive1Spawner4");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMassive2Spawner1")
                    {
                        GameObject cMA2S1 = GameObject.Find("CastleMassive2Spawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMassive2Spawner2")
                    {
                        GameObject cMA2S2 = GameObject.Find("CastleMassive2Spawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMassive2Spawner3")
                    {
                        GameObject cMA2S3 = GameObject.Find("CastleMassive2Spawner3");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMassive2Spawner4")
                    {
                        GameObject cMA2S4 = GameObject.Find("CastleMassive2Spawner4");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive1Spawner1")
                    {
                        GameObject fMA1S1 = GameObject.Find("FieldsMassive1Spawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive1Spawner2")
                    {
                        GameObject fMA1S2 = GameObject.Find("FieldsMassive1Spawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive1Spawner3")
                    {
                        GameObject fMA1S3 = GameObject.Find("FieldsMassive1Spawner3");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive1Spawner4")
                    {
                        GameObject fMA1S4 = GameObject.Find("FieldsMassive1Spawner4");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive1Spawner6")
                    {
                        GameObject fMA1S6 = GameObject.Find("FieldsMassive1Spawner6");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(sniper, fMA1S6.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive2Spawner1")
                    {
                        GameObject fMA2S1 = GameObject.Find("FieldsMassive2Spawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive2Spawner2")
                    {
                        GameObject fMA2S2 = GameObject.Find("FieldsMassive2Spawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive2Spawner3")
                    {
                        GameObject fMA2S3 = GameObject.Find("FieldsMassive2Spawner3");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive2Spawner4")
                    {
                        GameObject fMA2S4 = GameObject.Find("FieldsMassive2Spawner4");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive2Spawner6")
                    {
                        GameObject fMA2S6 = GameObject.Find("FieldsMassive2Spawner6");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(sniper, fMA2S6.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "SkylineMassiveSpawner1")
                    {
                        GameObject sMAS1 = GameObject.Find("SkylineMassiveSpawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(skylineGroundEnemies[Random.Range(0, skylineGroundEnemies.Length)], sMAS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(skylineGroundEnemies[Random.Range(0, skylineGroundEnemies.Length)], sMAS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "SkylineMassiveSpawner2")
                    {
                        GameObject sMAS2 = GameObject.Find("SkylineMassiveSpawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(skylineGroundEnemies[Random.Range(0, skylineGroundEnemies.Length)], sMAS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(skylineGroundEnemies[Random.Range(0, skylineGroundEnemies.Length)], sMAS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "SkylineMassiveSpawner3")
                    {
                        GameObject sMAS3 = GameObject.Find("SkylineMassiveSpawner3");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(jetpack, sMAS3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(jetpack, sMAS3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "SkylineMassiveSpawner4")
                    {
                        GameObject sMAS4 = GameObject.Find("SkylineMassiveSpawner4");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(jetpack, sMAS4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(jetpack, sMAS4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMassiveSpawner1")
                    {
                        GameObject sMAS1 = GameObject.Find("StreetsMassiveSpawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMassiveSpawner2")
                    {
                        GameObject sMAS2 = GameObject.Find("StreetsMassiveSpawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMassiveSpawner3")
                    {
                        GameObject sMAS3 = GameObject.Find("StreetsMassiveSpawner3");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMassiveSpawner4")
                    {
                        GameObject sMAS4 = GameObject.Find("StreetsMassiveSpawner4");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }
                }
				
				if (waveCount == 23)
				{
                    if (spawnPoints[i].name == "ApocMinibossSpawner")
                    {
                        GameObject aMBS = GameObject.Find("ApocMinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMBS.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMBS.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "ApocMinibossMinibossSpawner")
                    {
                        GameObject aMBMBS = GameObject.Find("ApocMinibossMinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(apocMiniboss, aMBMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMiniboss1Spawner")
                    {
                        GameObject cMB1S = GameObject.Find("CastleMiniboss1Spawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMB1S.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMB1S.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMB1S.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMiniboss1MinibossSpawner")
                    {
                        GameObject cMB1MBS = GameObject.Find("CastleMiniboss1MinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleMiniboss, cMB1MBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMiniboss2Spawner")
                    {
                        GameObject cMB2S = GameObject.Find("CastleMiniboss2Spawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMB2S.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMB2S.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMB2S.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMiniboss2MinibossSpawner")
                    {
                        GameObject cMB2MBS = GameObject.Find("CastleMiniboss2MinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleMiniboss, cMB2MBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMinibossSpawner")
                    {
                        GameObject fMBS = GameObject.Find("FieldsMinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMBS.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMBS.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMinibossMinibossSpawner")
                    {
                        GameObject fMBMBS = GameObject.Find("FieldsMinibossMinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldMiniboss, fMBMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "SkylineMinibossSpawner")
                    {
                        GameObject sMBS = GameObject.Find("SkylineMinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(jetpack, sMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(jetpack, sMBS.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(jetpack, sMBS.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "SkylineMinibossMinibossSpawner")
                    {
                        GameObject sMBMBS = GameObject.Find("SkylineMinibossMinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(skylineMiniboss, sMBMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    //remember spawner2
                    if (spawnPoints[i].name == "StreetsMinibossSpawner1")
                    {
                        GameObject sMBS1 = GameObject.Find("StreetsMinibossSpawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMBS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMBS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMinibossSpawner2")
                    {
                        GameObject sMBS2 = GameObject.Find("StreetsMinibossSpawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMBS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMinibossMinibossSpawner")
                    {
                        GameObject sMBMBS = GameObject.Find("StreetsMinibossMinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsMiniboss, sMBMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "ApocMassiveSpawner1")
                    {
                        GameObject aMAS1 = GameObject.Find("ApocMassiveSpawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "ApocMassiveSpawner2")
                    {
                        GameObject aMAS2 = GameObject.Find("ApocMassiveSpawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "ApocMassiveSpawner3")
                    {
                        GameObject aMAS3 = GameObject.Find("ApocMassiveSpawner3");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS3.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "ApocMassiveSpawner4")
                    {
                        GameObject aMAS4 = GameObject.Find("ApocMassiveSpawner4");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS4.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMassive1Spawner1")
                    {
                        GameObject cMA1S1 = GameObject.Find("CastleMassive1Spawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMassive1Spawner2")
                    {
                        GameObject cMA1S2 = GameObject.Find("CastleMassive1Spawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMassive1Spawner3")
                    {
                        GameObject cMA1S3 = GameObject.Find("CastleMassive1Spawner3");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S3.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMassive1Spawner4")
                    {
                        GameObject cMA1S4 = GameObject.Find("CastleMassive1Spawner4");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S4.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMassive2Spawner1")
                    {
                        GameObject cMA2S1 = GameObject.Find("CastleMassive2Spawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMassive2Spawner2")
                    {
                        GameObject cMA2S2 = GameObject.Find("CastleMassive2Spawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMassive2Spawner3")
                    {
                        GameObject cMA2S3 = GameObject.Find("CastleMassive2Spawner3");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S3.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMassive2Spawner4")
                    {
                        GameObject cMA2S4 = GameObject.Find("CastleMassive2Spawner4");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S4.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive1Spawner1")
                    {
                        GameObject fMA1S1 = GameObject.Find("FieldsMassive1Spawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive1Spawner2")
                    {
                        GameObject fMA1S2 = GameObject.Find("FieldsMassive1Spawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive1Spawner3")
                    {
                        GameObject fMA1S3 = GameObject.Find("FieldsMassive1Spawner3");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S3.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive1Spawner4")
                    {
                        GameObject fMA1S4 = GameObject.Find("FieldsMassive1Spawner4");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S4.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive1Spawner7")
                    {
                        GameObject fMA1S7 = GameObject.Find("FieldsMassive1Spawner7");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(sniper, fMA1S7.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive2Spawner1")
                    {
                        GameObject fMA2S1 = GameObject.Find("FieldsMassive2Spawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive2Spawner2")
                    {
                        GameObject fMA2S2 = GameObject.Find("FieldsMassive2Spawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive2Spawner3")
                    {
                        GameObject fMA2S3 = GameObject.Find("FieldsMassive2Spawner3");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S3.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive2Spawner4")
                    {
                        GameObject fMA2S4 = GameObject.Find("FieldsMassive2Spawner4");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S4.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive2Spawner5")
                    {
                        GameObject fMA2S5 = GameObject.Find("FieldsMassive2Spawner5");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(sniper, fMA2S5.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "SkylineMassiveSpawner1")
                    {
                        GameObject sMAS1 = GameObject.Find("SkylineMassiveSpawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(skylineGroundEnemies[Random.Range(0, skylineGroundEnemies.Length)], sMAS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(skylineGroundEnemies[Random.Range(0, skylineGroundEnemies.Length)], sMAS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(skylineGroundEnemies[Random.Range(0, skylineGroundEnemies.Length)], sMAS1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "SkylineMassiveSpawner2")
                    {
                        GameObject sMAS2 = GameObject.Find("SkylineMassiveSpawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(skylineGroundEnemies[Random.Range(0, skylineGroundEnemies.Length)], sMAS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(skylineGroundEnemies[Random.Range(0, skylineGroundEnemies.Length)], sMAS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(skylineGroundEnemies[Random.Range(0, skylineGroundEnemies.Length)], sMAS2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "SkylineMassiveSpawner3")
                    {
                        GameObject sMAS3 = GameObject.Find("SkylineMassiveSpawner3");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(jetpack, sMAS3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(jetpack, sMAS3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(jetpack, sMAS3.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "SkylineMassiveSpawner4")
                    {
                        GameObject sMAS4 = GameObject.Find("SkylineMassiveSpawner4");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(jetpack, sMAS4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(jetpack, sMAS4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(jetpack, sMAS4.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMassiveSpawner1")
                    {
                        GameObject sMAS1 = GameObject.Find("StreetsMassiveSpawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMassiveSpawner2")
                    {
                        GameObject sMAS2 = GameObject.Find("StreetsMassiveSpawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMassiveSpawner3")
                    {
                        GameObject sMAS3 = GameObject.Find("StreetsMassiveSpawner3");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS3.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMassiveSpawner4")
                    {
                        GameObject sMAS4 = GameObject.Find("StreetsMassiveSpawner4");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS4.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }
                }
				
				if (waveCount == 24)
				{
                    if (spawnPoints[i].name == "ApocMinibossSpawner")
                    {
                        GameObject aMBS = GameObject.Find("ApocMinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMBS.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMBS.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMBS.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "ApocMinibossMinibossSpawner")
                    {
                        GameObject aMBMBS = GameObject.Find("ApocMinibossMinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(apocMiniboss, aMBMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocMiniboss, aMBMBS.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMiniboss1Spawner")
                    {
                        GameObject cMB1S = GameObject.Find("CastleMiniboss1Spawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMB1S.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMB1S.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMB1S.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMB1S.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMiniboss1MinibossSpawner")
                    {
                        GameObject cMB1MBS = GameObject.Find("CastleMiniboss1MinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleMiniboss, cMB1MBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleMiniboss, cMB1MBS.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMiniboss2Spawner")
                    {
                        GameObject cMB2S = GameObject.Find("CastleMiniboss2Spawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMB2S.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMB2S.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMB2S.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMB2S.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMiniboss2MinibossSpawner")
                    {
                        GameObject cMB2MBS = GameObject.Find("CastleMiniboss2MinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleMiniboss, cMB2MBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleMiniboss, cMB2MBS.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMinibossSpawner")
                    {
                        GameObject fMBS = GameObject.Find("FieldsMinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMBS.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMBS.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMBS.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMinibossMinibossSpawner")
                    {
                        GameObject fMBMBS = GameObject.Find("FieldsMinibossMinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldMiniboss, fMBMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldMiniboss, fMBMBS.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "SkylineMinibossSpawner")
                    {
                        GameObject sMBS = GameObject.Find("SkylineMinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(jetpack, sMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(jetpack, sMBS.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(jetpack, sMBS.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(jetpack, sMBS.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "SkylineMinibossMinibossSpawner")
                    {
                        GameObject sMBMBS = GameObject.Find("SkylineMinibossMinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(skylineMiniboss, sMBMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(skylineMiniboss, sMBMBS.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    //remember spawner2
                    if (spawnPoints[i].name == "StreetsMinibossSpawner1")
                    {
                        GameObject sMBS1 = GameObject.Find("StreetsMinibossSpawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMBS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMBS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMinibossSpawner2")
                    {
                        GameObject sMBS2 = GameObject.Find("StreetsMinibossSpawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMBS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMBS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMinibossMinibossSpawner")
                    {
                        GameObject sMBMBS = GameObject.Find("StreetsMinibossMinibossSpawner");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsMiniboss, sMBMBS.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsMiniboss, sMBMBS.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "ApocMassiveSpawner1")
                    {
                        GameObject aMAS1 = GameObject.Find("ApocMassiveSpawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS1.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "ApocMassiveSpawner2")
                    {
                        GameObject aMAS2 = GameObject.Find("ApocMassiveSpawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS2.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "ApocMassiveSpawner3")
                    {
                        GameObject aMAS3 = GameObject.Find("ApocMassiveSpawner3");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS3.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS3.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "ApocMassiveSpawner4")
                    {
                        GameObject aMAS4 = GameObject.Find("ApocMassiveSpawner4");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS4.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(apocEnemies[Random.Range(0, apocEnemies.Length)], aMAS4.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMassive1Spawner1")
                    {
                        GameObject cMA1S1 = GameObject.Find("CastleMassive1Spawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S1.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMassive1Spawner2")
                    {
                        GameObject cMA1S2 = GameObject.Find("CastleMassive1Spawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S2.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMassive1Spawner3")
                    {
                        GameObject cMA1S3 = GameObject.Find("CastleMassive1Spawner3");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S3.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S3.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMassive1Spawner4")
                    {
                        GameObject cMA1S4 = GameObject.Find("CastleMassive1Spawner4");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S4.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA1S4.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMassive2Spawner1")
                    {
                        GameObject cMA2S1 = GameObject.Find("CastleMassive2Spawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S1.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMassive2Spawner2")
                    {
                        GameObject cMA2S2 = GameObject.Find("CastleMassive2Spawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S2.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMassive2Spawner3")
                    {
                        GameObject cMA2S3 = GameObject.Find("CastleMassive2Spawner3");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S3.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S3.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "CastleMassive2Spawner4")
                    {
                        GameObject cMA2S4 = GameObject.Find("CastleMassive2Spawner4");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S4.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(castleEnemies[Random.Range(0, castleEnemies.Length)], cMA2S4.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive1Spawner1")
                    {
                        GameObject fMA1S1 = GameObject.Find("FieldsMassive1Spawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S1.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive1Spawner2")
                    {
                        GameObject fMA1S2 = GameObject.Find("FieldsMassive1Spawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S2.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive1Spawner3")
                    {
                        GameObject fMA1S3 = GameObject.Find("FieldsMassive1Spawner3");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S3.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S3.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive1Spawner4")
                    {
                        GameObject fMA1S4 = GameObject.Find("FieldsMassive1Spawner4");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S4.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA1S4.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive1Spawner8")
                    {
                        GameObject fMA1S8 = GameObject.Find("FieldsMassive1Spawner8");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(sniper, fMA1S8.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive2Spawner1")
                    {
                        GameObject fMA2S1 = GameObject.Find("FieldsMassive2Spawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S1.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive2Spawner2")
                    {
                        GameObject fMA2S2 = GameObject.Find("FieldsMassive2Spawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S2.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive2Spawner3")
                    {
                        GameObject fMA2S3 = GameObject.Find("FieldsMassive2Spawner3");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S3.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S3.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive2Spawner4")
                    {
                        GameObject fMA2S4 = GameObject.Find("FieldsMassive2Spawner4");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S4.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(fieldEnemies[Random.Range(0, fieldEnemies.Length)], fMA2S4.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "FieldsMassive2Spawner6")
                    {
                        GameObject fMA2S6 = GameObject.Find("FieldsMassive2Spawner6");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(sniper, fMA2S6.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "SkylineMassiveSpawner1")
                    {
                        GameObject sMAS1 = GameObject.Find("SkylineMassiveSpawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(skylineGroundEnemies[Random.Range(0, skylineGroundEnemies.Length)], sMAS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(skylineGroundEnemies[Random.Range(0, skylineGroundEnemies.Length)], sMAS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(skylineGroundEnemies[Random.Range(0, skylineGroundEnemies.Length)], sMAS1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(skylineGroundEnemies[Random.Range(0, skylineGroundEnemies.Length)], sMAS1.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "SkylineMassiveSpawner2")
                    {
                        GameObject sMAS2 = GameObject.Find("SkylineMassiveSpawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(skylineGroundEnemies[Random.Range(0, skylineGroundEnemies.Length)], sMAS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(skylineGroundEnemies[Random.Range(0, skylineGroundEnemies.Length)], sMAS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(skylineGroundEnemies[Random.Range(0, skylineGroundEnemies.Length)], sMAS2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(skylineGroundEnemies[Random.Range(0, skylineGroundEnemies.Length)], sMAS2.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "SkylineMassiveSpawner3")
                    {
                        GameObject sMAS3 = GameObject.Find("SkylineMassiveSpawner3");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(jetpack, sMAS3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(jetpack, sMAS3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(jetpack, sMAS3.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(jetpack, sMAS3.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "SkylineMassiveSpawner4")
                    {
                        GameObject sMAS4 = GameObject.Find("SkylineMassiveSpawner4");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(jetpack, sMAS4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(jetpack, sMAS4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(jetpack, sMAS4.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(jetpack, sMAS4.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMassiveSpawner1")
                    {
                        GameObject sMAS1 = GameObject.Find("StreetsMassiveSpawner1");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS1.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS1.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS1.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS1.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMassiveSpawner2")
                    {
                        GameObject sMAS2 = GameObject.Find("StreetsMassiveSpawner2");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS2.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS2.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS2.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS2.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMassiveSpawner3")
                    {
                        GameObject sMAS3 = GameObject.Find("StreetsMassiveSpawner3");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS3.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS3.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS3.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS3.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }

                    if (spawnPoints[i].name == "StreetsMassiveSpawner4")
                    {
                        GameObject sMAS4 = GameObject.Find("StreetsMassiveSpawner4");
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS4.transform.position, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS4.transform.position + offset1, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset1, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS4.transform.position + offset2, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset2, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                        Instantiate(streetsEnemies[Random.Range(0, streetsEnemies.Length)], sMAS4.transform.position + offset3, spawnRotation);
                        newSmoke = Instantiate(s, spawnPoints[i].transform.position + offset3, spawnPoints[i].transform.localRotation) as GameObject;
                        newSmoke.transform.parent = spawnPoints[i].transform;
                        GetComponent<AudioSource>().PlayOneShot(spawnsound);
                        hazardCount++;
                        number++;
                        yield return new WaitForSeconds(spawnWait);
                    }
                }
			}
			enemiesDestroyed = false;
		}
	}

	public void restart()
	{
		StartCoroutine (SpawnWaves ());
	}
	

	void PunkRound()
	{
		if(!punkBossSpawned && !punkDead)
		{
			Instantiate (punkBoss, bossSpawn.transform.position, bossSpawn.transform.rotation);
			punkBossSpawned = true;
			hazardCount++;
		}
	}

	void PunkDead()
	{
		if(punkBossSpawned)
		{
			hazardCount--;
            sc.count();
			punkDead = true;
			punkBossSpawned = false;
		}
	}

	void AlienRound()
	{
		if(!alienBossSpawned && !alienDead)
		{
			Instantiate (alienBoss, bossSpawn.transform.position, bossSpawn.transform.rotation);
			alienBossSpawned = true;
			hazardCount++;
		}
	}

	void AlienDead()
	{
		if(alienBossSpawned)
		{
			alienDead = true;
			hazardCount--;
            sc.count();
            alienBossSpawned = false;
		}
	}

	void DinoRound()
	{
		if(!dinoBossSpawned && !dinoDead)
		{
			Instantiate (dinoBoss, bossSpawn.transform.position, bossSpawn.transform.rotation);
			dinoBossSpawned = true;
			hazardCount++;
		}
	}

	void DinoDead()
	{
		if(dinoBossSpawned)
		{
			dinoDead = true;
			hazardCount--;
            sc.count();
            dinoBossSpawned = false;
		}
	}

	void MachineRound()
	{
		if(!machineBossSpawned && !machineDead)
		{
			Instantiate (machineBoss, bossSpawn.transform.position, bossSpawn.transform.rotation);
			machineBossSpawned = true;
			hazardCount++;
		}
	}

	void MachineDead()
	{
		if(machineBossSpawned)
		{
			machineDead = true;
			hazardCount--;
            sc.count();
            machineBossSpawned = false;
		}
	}

	void KingRound()
	{
		if(!kingBossSpawned && !kingDead)
		{
			Instantiate (kingBoss, bossSpawn.transform.position, bossSpawn.transform.rotation);
			kingBossSpawned = true;
			hazardCount++;
		}
	}
}