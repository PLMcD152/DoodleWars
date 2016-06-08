using UnityEngine;
using System.Collections;

public class CameraFollowFinal : MonoBehaviour {
	
	
	private float deadZone = 1.0f;
	public bool followVertical = false;
	public bool followHorizontal = true;
	public float minimumHeight = -2.1f;
	
	public GameObject cam;
	
	void  Start ()
	{
		cam = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	void  Update ()
	{
		if(followHorizontal == true)
		{
			Vector3 temp1 = cam.transform.position;
			temp1.x = transform.position.x + deadZone;
			if (cam.transform.position.x >= transform.position.x + deadZone)
			{
				cam.transform.position = temp1;
			}
			Vector3 temp2 = cam.transform.position;
			temp2.x = transform.position.x - deadZone;
			if (cam.transform.position.x <= transform.position.x - deadZone)
			{
				cam.transform.position = temp2;
			}
		}
		
		if(followVertical == true)
		{
			Vector3 temp1 = cam.transform.position;
			temp1.y = transform.position.y + deadZone;
			if (cam.transform.position.y >= transform.position.y + deadZone)
			{
				cam.transform.position = temp1;
			}
			Vector3 temp2 = cam.transform.position;
			temp2.y = transform.position.y - deadZone;
			if (cam.transform.position.y <= transform.position.y - deadZone)
			{
				cam.transform.position = temp2;
			}
		}
		Vector3 temp = cam.transform.position;
		temp.y = transform.position.y - minimumHeight;
		if(cam.transform.position.y < minimumHeight)
		{
			cam.transform.position = temp;
		}
	}

	void OnLevelWasLoaded()
	{
		if (cam == null)
		{
			cam = GameObject.FindGameObjectWithTag("MainCamera");
		}
	}
}