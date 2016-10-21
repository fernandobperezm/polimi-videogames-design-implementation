using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {
	Transform tr;


	float m_shot = 100f;

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> () as Transform;
	}
	
	// Update is called once per frame
	void Update () {
		//Checking if the shot goes width farther.
		if (Mathf.Abs (tr.position.x) > 240) {
			Destroy (gameObject);
		}

		// Checking if the shot goes height farther.
		if (Mathf.Abs (tr.position.y) > 110) {
			Destroy (gameObject);
		}
	}

	void FixedUpdate () {
		tr.position += tr.right * Time.fixedDeltaTime * m_shot;
	}
}
