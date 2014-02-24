using UnityEngine;
using System.Collections;

public class poison : MonoBehaviour {

	void Awake()
	{
		Destroy (gameObject,5);
	}
	
}
