using UnityEngine;
using System.Collections;

public class spawner : MonoBehaviour {
	int i = 0;
	public Transform target;
	public float spawnTime = 3.0f;
	public float unit = 10;
	private float tempTime;
	public string monster;
	void Awake(){
				tempTime = Time.time;
		}
	void Update () {	

		float distance = Vector2.Distance(target.position, transform.position);
		if(distance < unit && Time.time - tempTime > spawnTime ){
			GameObject gameObj = null;
			gameObj = (GameObject) Instantiate(Resources.Load(monster));
			gameObj.transform.position = new Vector2(transform.position.x, transform.position.y);
			tempTime = Time.time;
		}
		//Debug.Log (i++ + "    "+ target.position);
	}
}
