using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	// Transform to get player's positions.
	Transform tr;

	// Configuration for player speed.
	[Header("Player Speed")]
	[Range(25f,100f)]
	public float m_speed = 50f;

	// Get the keyboard input.
	float m_horizontal = 0f;
	float m_vertical = 0f;

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> () as Transform;

	}
	
	// Update is called once per frame
	void Update () {
		m_horizontal = Input.GetAxis ("Horizontal");
		m_vertical = Input.GetAxis ("Vertical");
	}

	//
	void FixedUpdate () {
		tr.position += 
			m_horizontal * tr.right * Time.fixedDeltaTime * m_speed +
			m_vertical * tr.up * Time.fixedDeltaTime * m_speed;
	}
}
