using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {
	
	public GameObject shot;
	private int force = 100;

	void Start()
	{
		shot = (Resources.Load ("Punk/Shot")) as GameObject;
	}
	
	void Update ()
	{
		Transform target = GameObject.FindGameObjectWithTag ("Player").transform;
		Vector2 v_diff = (target.transform.position - transform.position);
		float atan2 = Mathf.Atan2 (v_diff.y, v_diff.x);
		transform.rotation = Quaternion.Euler (0f, 0f, atan2 * Mathf.Rad2Deg);
	}
	
	public void Shoot()
	{
		Transform target = GameObject.FindGameObjectWithTag("Player").transform;
		
		GameObject bullet = Instantiate(shot, transform.position, transform.rotation) as GameObject;
		
		Vector2 direction = bullet.GetComponent<Rigidbody2D>().transform.position - target.transform.position;
		
		bullet.GetComponent<Rigidbody2D>().AddForceAtPosition(direction.normalized *- force, transform.position);
		
		bullet.transform.eulerAngles = transform.eulerAngles;
		
		Destroy(bullet, 8);
	}
}