using UnityEngine;
using System.Collections;

public class MonsterAttack : MonoBehaviour {
	public Rigidbody2D throwW;				// Prefab of the shootingStar
	public Transform poison;
	public float speed = 20f;				// The speed the rocket will fire at.
	
	public float attackTime = 3.0f;
	private float tempTime;
	private ninjaCont playerCtrl;		// Reference to the PlayerControl script.

	void Awake(){
		tempTime = Time.time;
		playerCtrl = transform.root.GetComponent<ninjaCont>();
	}

	
	void Update ()
	{
		if(throwW != null && Time.time - tempTime > attackTime)
		{

			Rigidbody2D bulletInstance = Instantiate(throwW, transform.position, Quaternion.Euler(new Vector3(0,0,-180))) as Rigidbody2D;
			bulletInstance.velocity = new Vector2(-speed, 0);
			tempTime = Time.time;
	
		}
		if(poison != null && Time.time - tempTime > attackTime)
		{
				Rigidbody2D bulletInstance = Instantiate(poison, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
				//bulletInstance.velocity = new Vector2(0, 0);
				tempTime = Time.time;
		}

	}
}
