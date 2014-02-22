using UnityEngine;
using System.Collections;

public class ninjaCont : MonoBehaviour {
	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	private float maxSpeed = 10f;				// The fastest the player can travel in the x axis.
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.
	private bool isFalling = false;			// Whether or not the player is grounded.
	public int jumpCount = 2; 
	private bool isSide = false;
	private bool isDown = false;
	private float tempTime = 0.0f;
	private float comboTime1 = 0.0f;
	private float comboTime2 = 0.0f;
	public Sprite player;
	public Sprite crouch;
	void Awake(){
		SpriteRenderer sprRenderer = (SpriteRenderer)renderer;
		sprRenderer.sprite = player;
		BoxCollider2D boxCol = (BoxCollider2D)collider2D;
		boxCol.size = new Vector2(2.08f, 2.46f);
		maxSpeed = 10f;
		}
	void Update(){

		// If the jump button is pressed and the player is grounded then the player should jump.
		
		if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)){
			if(isFalling == false && jumpCount > 0)
				jump();
		}
		if (Input.GetKeyDown(KeyCode.DownArrow)){
			SpriteRenderer sprRenderer= (SpriteRenderer)renderer;
			sprRenderer.sprite = crouch;
			transform.position = new Vector3(transform.position.x, transform.position.y-0.5f, transform.position.z);
			BoxCollider2D boxCol = (BoxCollider2D)collider2D;
			boxCol.size = new Vector2(2.08f, 1.23f);
		}
		if (Input.GetKeyUp(KeyCode.DownArrow)){
			Awake();
		}
	}

	void jump(){
		tempTime = Time.time;
		isFalling = true;
		jumpCount--;
	}

	
	void FixedUpdate ()
	{
		bool isPressed = false;
		// Cache the horizontal input.
		float h = Input.GetAxis("Horizontal");
		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if (h * rigidbody2D.velocity.x < maxSpeed) {
			if(Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.Space))
				isPressed = true;
			if(!isPressed)
				rigidbody2D.AddForce (Vector2.right * h * moveForce);
		}
		
		// If the player's horizontal velocity is greater than the maxSpeed...
		if (Mathf.Abs (rigidbody2D.velocity.x) > maxSpeed) {
			if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.Space))
				isPressed = true;
			if (!isPressed)	
				rigidbody2D.velocity = new Vector2 (Mathf.Sign (rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
		}
		// If the input is moving the player right and the player is facing left...
		if(h > 0 && !facingRight)
			// ... flip the player.
			Flip();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if(h < 0 && facingRight)
			// ... flip the player.
			Flip();
		
		if(isFalling)
		{
			if (isSide)
				rigidbody2D.AddForce (new Vector2 (-h * jumpForce/2, jumpForce* 1.3f));
			
			if(!isSide)
				rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			isFalling = false;
		}
		if (isSide) {
			transform.position = new Vector3(transform.position.x, transform.position.y-0.1f, transform.position.z);
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
			isSide = true;		}
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