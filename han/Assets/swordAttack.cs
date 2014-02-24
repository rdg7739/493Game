using UnityEngine;
using System.Collections;

public class swordAttack : MonoBehaviour {
	public Rigidbody2D sword;				
	public Sprite dangum;
	
		void Awake()
	{
	
		SpriteRenderer sprRenderer = (SpriteRenderer)renderer;
		sprRenderer.sprite = null;
		BoxCollider2D boxCol = (BoxCollider2D)collider2D;
		boxCol.enabled = false;	
		sprRenderer.transform.Rotate( new Vector3(0, 0, 180)) ; //new Vector3(0,0, obj.rotationZ);

		transform.localScale = new Vector2(transform.localScale.x * 0.25f, transform.localScale.y * 0.25f);
	//	transform.localRotation = new Quaternion(transform.localRotation.x, transform.localRotation.y, 180);

	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Z))
		{
			SpriteRenderer sprRenderer = (SpriteRenderer)renderer;
			sprRenderer.sprite = dangum;
			BoxCollider2D boxCol = (BoxCollider2D)collider2D;
			boxCol.enabled = true;

			}
		if (Input.GetKeyUp (KeyCode.Z)) {
			back();	
		}

	}
	void back()
	{
		if (Input.GetKeyUp (KeyCode.Z)) {
			SpriteRenderer sprRenderer = (SpriteRenderer)renderer;
			sprRenderer.sprite = null;
			BoxCollider2D boxCol = (BoxCollider2D)collider2D;
			boxCol.enabled = false;
		}
	}
	void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.tag == "Enemy") {
			col.gameObject.GetComponent<Enemy> ().Hurt ();
		}
	}
}
