using UnityEngine;
using System.Collections;

public class fireball : MonoBehaviour {

	void Awake()
	{
		Destroy (gameObject, 2);
	}
	
	void OnTriggerEnter2D (Collider2D col) 
	{
		// If it hits an enemy...
		if(col.tag == "Player")
		{
			// ... find the Enemy script and call the Hurt function.
			//col.gameObject.GetComponent<Enemy>().Hurt();
			// Destroy the rocket.
			Destroy (gameObject);
		}
		
		// Otherwise if the player manages to shoot himself...
		else if(col.gameObject.tag != "Enemy" && col.gameObject.tag != "weapon")
		{
			Destroy (gameObject);
		}
		
	}
	void Update(){
		rigidbody2D.AddTorque(Random.Range(-10,10));
	}
}

