using UnityEngine;
using System.Collections;

public class swordAttack : MonoBehaviour {
	public Rigidbody2D sword;				
	public Sprite dangum;
	
		void Awake()
	{
		if (Input.GetKeyUp (KeyCode.Z)) {
			SpriteRenderer sprRenderer = (SpriteRenderer)renderer;
			sprRenderer.sprite = null;
			BoxCollider2D boxCol = (BoxCollider2D)collider2D;
			boxCol.size = new Vector2(0,0);		
		}
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Z))
		{
			SpriteRenderer sprRenderer = (SpriteRenderer)renderer;
			sprRenderer.sprite = dangum;
			BoxCollider2D boxCol = (BoxCollider2D)collider2D;
			boxCol.size = new Vector2(1.3f, 1);
		}
		if (Input.GetKeyUp (KeyCode.Z)) {
			Awake();	
		}

	}
	void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.tag == "Enemy") {
			col.gameObject.GetComponent<Enemy> ().Hurt ();
		}
	}
}
