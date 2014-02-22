using UnityEngine;
using System.Collections;

public class sword : MonoBehaviour {
	private ninjaCont playerCtrl;		
	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	private float maxSpeed = 10f;
	void Awake()
	{
		playerCtrl = transform.root.GetComponent<ninjaCont>();
	}


	void Update ()
	{
		if(Input.GetKeyUp(KeyCode.Z))
			Destroy (gameObject);
	}
	void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.tag == "Enemy") {
			col.gameObject.GetComponent<Enemy> ().Hurt ();
		}

}
}