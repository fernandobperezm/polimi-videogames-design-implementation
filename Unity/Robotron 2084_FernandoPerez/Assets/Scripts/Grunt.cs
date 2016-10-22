using UnityEngine;
using System.Collections;

public class Grunt : MonoBehaviour {

	Transform tr;

	GameObject player; // As the grunts follows the player, we need their directions.

	[Header ("Grunt Speed")]
	[Range(25f,200f)]
	public float m_speed = 50f;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		tr = GetComponent<Transform> () as Transform;
	}
	
	// Update is called once per frame
	void Update () {
		// To chase the player, we must know its position to follow that direction.
		Vector3 direction = 
			(player.transform.position - tr.position).normalized;

		// Following the player.
		tr.position = tr.position +  direction * m_speed * Time.deltaTime;
		
	}
}
