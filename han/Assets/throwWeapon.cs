using UnityEngine;
using System.Collections;

public class throwWeapon: MonoBehaviour
{
	public Rigidbody2D throwW;				// Prefab of the shootingStar
	public float speed = 20f;				// The speed the rocket will fire at.
	
	
	private ninjaCont playerCtrl;		// Reference to the PlayerControl script.
	
	void Awake()
	{
		playerCtrl = transform.root.GetComponent<ninjaCont>();
	}
	
	
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.C))
		{
			
			// If the player is facing right...
			if(playerCtrl.facingRight)
			{
				throw_(0, speed);
			}
			else
			{
				throw_(180f, -speed);
			}
		}
	
	}

	void throw_(float vec, float speed){
		Rigidbody2D bulletInstance = Instantiate(throwW, transform.position, Quaternion.Euler(new Vector3(0,0,vec))) as Rigidbody2D;
		bulletInstance.velocity = new Vector2(speed, 0);
	}
	

}
