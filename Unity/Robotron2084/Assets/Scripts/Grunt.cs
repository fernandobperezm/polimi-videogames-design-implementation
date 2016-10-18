using UnityEngine;
using System.Collections;

public class Grunt : MonoBehaviour {

	GameObject player;
	public float m_speed = 50f;

	Transform tr; 

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player") as GameObject;
		tr = GetComponent<Transform> () as Transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = 
			(player.transform.position - tr.position).normalized;
		tr.position = tr.position + direction * m_speed * Time.deltaTime;
	}
}
