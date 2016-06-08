using UnityEngine;
using System.Collections;

public class PlayerDash : MonoBehaviour {

	[Header("Dash settings")]
	[Tooltip("Set the force of the dash.")]
	public float dashForce = 900f;
	[Tooltip("Set the duration of the dash. The player can't move during this duration.")]
	public float dashTime = 0.2f;
	[Tooltip("Set the cooldown duration after performing a dash. The player can't dash again while the cooldown is active.")]
	public float cooldownTime = 0.5f;
	[Header("Air dashing")]
	[Tooltip("Enable or disable dashing while in the air.")]
	public bool airDash = true;
	[Tooltip("You can set a limit for the amount of dashes in the air by enabling this variable and changing the variable below.")]
	public bool airDashLimit = true;
	[Tooltip("When there is an air dashing limit, you can set the amount of air dashes here.")]
	public int airDashTotal = 1;


	private bool dash = false;					// Boolean that determines if a dash should be performed.
	private bool dashAllowed = false;			// Boolean that determines if a dash is allowed.
	private float dashTimer;					// Timer used to count down the dashTime.
	private float cooldownTimer;				// Timer used to count down the cooldownTime.
	private bool runCooldownTimer = false;		// Boolean that determines if the cooldown timer should run.
	private int totalAirDashes;					// Determines how many air dashes are currently allowed.
	public AudioClip dashsound;
	public Transform dashparticleloc;
	public GameObject dashparticle;
	private Player player;						// Get the Player class.

	void Start ()
	{
		player = GetComponent<Player>();
	}
		
	void Update ()
	{
		if (player.grounded)
		{
			totalAirDashes = airDashTotal;
		}

        SetDashAllowed();


		if (player.dashing)
		{
			dashAllowed = false;

			if (GetComponent<Rigidbody2D>().velocity.x == 0)
			{
				dashTimer = 0;
			}

			if (dashTimer > 0) 
			{
				dashTimer -= Time.deltaTime;
			}

			else 
			{
				runCooldownTimer = true;

				player.Dash(false);
			}
		}

		if (runCooldownTimer)
		{
			dashAllowed = false;

			if (cooldownTimer > 0)
			{
				cooldownTimer -= Time.deltaTime;
			}

			else
			{
				runCooldownTimer = false;

				SetDashAllowed();
			}
		}

		else if (CFInput.GetButtonDown("Dash") && dashAllowed && !player.dashing && !player.crouching && !player.stuckToWall && !player.falling && !player.sliding && !player.onLadder && !player.stop)
		{
			dash = true;
            GetComponent<AudioSource>().PlayOneShot(dashsound);
            Instantiate(dashparticle,dashparticleloc.position,dashparticleloc.localRotation);
            if(PlayerPrefs.GetInt("Vibrate") == 0)
            {
                player.Vib650();
            }
		}
	}
	
	void FixedUpdate ()
	{
		if (dash) 
		{
			dash = false;

			player.UnstickFromPlatform();

			player.Dash(true);

			if(player.facingRight)
			{
				GetComponent<Rigidbody2D>().AddForce(new Vector2(dashForce, 250.0f));
			}

			else if(!player.facingRight)
			{
				GetComponent<Rigidbody2D>().AddForce(new Vector2(-dashForce, 250.0f));
			}

			dashTimer = dashTime;
			cooldownTimer = cooldownTime;

			if (airDashLimit && airDashTotal > 0)
			{
				totalAirDashes--;
			}
		}
	}
	
	void SetDashAllowed()
	{
		if (!player.grounded && !airDash)
		{
			dashAllowed = false;
		} 

		else if (!player.grounded && airDashLimit)
		{
			if (totalAirDashes > 0)
			{
				dashAllowed = true;
			}

			else
			{
				dashAllowed = false;
			}
		} 

		else 
		{
			dashAllowed = true;
		}
	}
	
	public void ResetAirDashLimit() 
	{
		totalAirDashes = airDashTotal;
	}
}
