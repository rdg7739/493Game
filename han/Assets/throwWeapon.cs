using UnityEngine;
using System.Collections;

public class throwWeapon: MonoBehaviour
{
	public Rigidbody2D throwW;				// Prefab of the rocket.
	public float speed = 20f;				// The speed the rocket will fire at.
	
	
	private ninjaCont playerCtrl;		// Reference to the PlayerControl script.
	
	void Awake()
	{
		playerCtrl = transform.root.GetComponent<ninjaCont>();
	}
	
	
	void Update ()
	{
		// If the fire button is pressed...
		if(Input.GetKeyDown(KeyCode.S))
		{
			
			// If the player is facing right...
			if(playerCtrl.facingRight)
			{
				// ... instantiate the rocket facing right and set it's velocity to the right. 
				Rigidbody2D bulletInstance = Instantiate(throwW, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2(speed, 0);
			
			}
			else
			{
				// Otherwise instantiate the rocket facing left and set it's velocity to the left.
				Rigidbody2D bulletInstance = Instantiate(throwW, transform.position, Quaternion.Euler(new Vector3(0,0,180f))) as Rigidbody2D;
				bulletInstance.velocity = new Vector2(-speed, 0);
			
			}
		
		}
	}
	

}
