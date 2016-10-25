using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

	[Range (2f,10f)]
	public float m_speed = 2f;

	private Transform tr;
	private Vector3 m_position;

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> () as Transform;
		m_position = tr.position;
	}
	
	// Update is called once per frame
	void Update () {
//		m_position -= tr.right * m_speed * Time.deltaTime;
		tr.position =  m_position - tr.right * Mathf.Repeat (m_speed * Time.time, 20.48f); // This moves the background and set it at the middle modularly.

	}
}
