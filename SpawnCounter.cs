using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpawnCounter : MonoBehaviour {


	public int enemycount;
	public GameObject countdownGO;
	public Text guicountdown;
	public float countdown = 15;
	private SpawnManager spawnManager;
	public int wave;
	public bool atBoss = false;
	public bool okToChange = false;
    GameObject readyUp;
    public bool b;
    healthwatch he;
	
	void Start ()
	{
		GameObject.Find ("SpawnManager");
		countdownGO = GameObject.Find ("Timer");
		guicountdown = countdownGO.GetComponent<Text> ();
		spawnManager = GetComponent<SpawnManager> ();
        readyUp = GameObject.Find("ReadyUp");
        he = GameObject.Find("healthwathcer").GetComponent<healthwatch>();
	}

	void Update ()
	{
		//GetComponent<SpawnManager> ().hazardCount = enemycount;
		//enemycount = GetComponent<SpawnManager> ().hazardCount;
		wave = spawnManager.waveCount;

		if (countdown < 0)
		{
			countdown = 0;
		}

		if (spawnManager.number > 0)
		{
			countdownGO.SetActive (false);
            readyUp.SetActive(false);
        }

		if (atBoss)
		{
			countdownGO.SetActive (false);
            readyUp.SetActive(false);
        }

		if (countdown == 0 && spawnManager.number == 0 && !atBoss)
		{
			countdownGO.SetActive (false);
            readyUp.SetActive(false);
            spawnManager.enemiesDestroyed = true;
			spawnManager.restart();
		}
		
		if (enemycount == 0 && !atBoss && okToChange)
		{
			count();
			spawnManager.number = 0;
		}

		if (countdown > 0 && !atBoss && !he.paused)
		{
			countdownGO.SetActive (true);
            readyUp.SetActive(true);
            countdown -= Time.deltaTime;
			guicountdown.text = countdown.ToString("f1");
		}

	}

	void DeadEnemy()
	{
		spawnManager.hazardCount --;
	}

	public void count()
	{
		countdown = 15;
	}

    public void Clicked()
    {
        countdown = 0;
    }

    void wavecleared()
	{
		enemycount = 0;
	}

}
	
