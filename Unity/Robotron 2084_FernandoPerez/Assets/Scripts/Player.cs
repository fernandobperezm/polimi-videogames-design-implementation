using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	// Transform to get player's positions.
	Transform tr;

	[Header ("Shot Positions")]
	public Transform m_shot_right;
	public Transform m_shot_left;
	public Transform m_shot_up;
	public Transform m_shot_down;

	public GameObject m_shot_prefab;

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

		// Right Shooting.
		if (Input.GetKeyDown (KeyCode.L)) {
			GameObject go = Instantiate (
				m_shot_prefab,
				m_shot_right.position,
				m_shot_right.rotation) as GameObject;
		}

		// Left shooting.
		if (Input.GetKeyDown (KeyCode.J)) {
			GameObject go = Instantiate (
				                m_shot_prefab,
				                m_shot_left.position,
				                m_shot_left.rotation) as GameObject;
		}

		// Up shooting
		if (Input.GetKeyDown (KeyCode.I)) {
			GameObject go = Instantiate (
				m_shot_prefab,
				m_shot_up.position,
				m_shot_up.rotation) as GameObject;
		}

		// Down Shooting
		if (Input.GetKeyDown (KeyCode.K)) {
			GameObject go = Instantiate (
				m_shot_prefab,
				m_shot_down.position,
				m_shot_down.rotation) as GameObject;
		}
	}

	//
	void FixedUpdate () {
		tr.position += 
			m_horizontal * tr.right * Time.fixedDeltaTime * m_speed +
			m_vertical * tr.up * Time.fixedDeltaTime * m_speed;
	}
}
