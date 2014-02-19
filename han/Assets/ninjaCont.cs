using UnityEngine;
using System.Collections;

public class ninjaCont : MonoBehaviour {
	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.
	public float jumpHeight = 8;
	private bool isFalling = false;			// Whether or not the player is grounded.
	public int jumpCount = 2; 
	private bool isSide = false;
	private bool isDown = false;
	private float tempTime = 0.0f;
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.C)) {
			transform.position = new Vector3(transform.position.x+10, transform.position.y, transform.position.z);
		}
		if (Input.GetKeyDown (KeyCode.Z)) {
			transform.position = new Vector3(transform.position.x-10, transform.position.y, transform.position.z);
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			tempTime=Time.time;
			transform.position = new Vector3(transform.position.x, transform.position.y-5, transform.position.z);
			isDown = true;
		}
		if ( Time.time -tempTime > 5 && isDown == true) {
			isDown = false;
			transform.position = new Vector3(transform.position.x, transform.position.y+5, transform.position.z);
		}
		// If the jump button is pressed and the player is grounded then the player should jump.
		if (Input.GetKeyDown (KeyCode.UpArrow) && isFalling == false && jumpCount > 0) {
			isFalling = true;
			jumpCount--;
		}
	}

	
	void FixedUpdate ()
	{
		// Cache the horizontal input.
		float h = Input.GetAxis("Horizontal");

		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if(h * rigidbody2D.velocity.x < maxSpeed)
			// ... add a force to the player.
			rigidbody2D.AddForce(Vector2.right * h * moveForce);
		
		// If the player's horizontal velocity is greater than the maxSpeed...
		if(Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
			// ... set the player's velocity to the maxSpeed in the x axis.
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
		
		// If the input is moving the player right and the player is facing left...
		if(h > 0 && !facingRight)
			// ... flip the player.
			Flip();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if(h < 0 && facingRight)
			// ... flip the player.
			Flip();
		
		if(isFalling )
		{
		
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			isFalling = false;
		}
		if(isSide){
			transform.position = new Vector3(transform.position.x, transform.position.y-0.02f, transform.position.z);

		}

	}
	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "ground") {
			isFalling = false;
			jumpCount = 2;
			isSide = false;
		}
		if (col.gameObject.tag == "sideWall") {
			isFalling = false;
			jumpCount = 2;
			isSide = true;
		}
	}

	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
}