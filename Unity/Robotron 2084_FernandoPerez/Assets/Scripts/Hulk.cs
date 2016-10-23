using UnityEngine;
using System.Collections;

public class Hulk : MonoBehaviour {
	// Hulks doesn't chase the player, they have a circular movement around they creation spot. But they have three lives.

	private int m_hulk_lives;
	float m_hulk_angle = 0f; // angle
	float m_hulk_ratio = 0f; // ratio

	Transform tr;

	//GameObject player; // As the hulk follows the player, we need their directions.

//	[Header ("Hulk Speed")]
//	[Range(25f,200f)]
//	public float m_speed = 100f;

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> () as Transform;
		m_hulk_lives = 3;
	}

	void OnEnable() {
		m_hulk_lives = 3;
	}
	
	// Update is called once per frame
	void Update () {
		
		// Circular movement.
		m_hulk_angle += Time.deltaTime;
		m_hulk_ratio = Random.Range (2f, 3f);

		Vector3 np = new Vector3 ();
		np.x = Mathf.Cos (m_hulk_angle) * m_hulk_ratio;
		np.y = Mathf.Sin (m_hulk_angle) * m_hulk_ratio;
		np.z = transform.position.z;

		tr.transform.position += np;
		tr.transform.rotation = Quaternion.identity;
		
	}

	// OnShot is a function that returns the remaining lives of a Hulk after being shot.
	public int OnShot () {
//		m_hulk_lives -= 1;
//		return m_hulk_lives;
		Debug.Log ("Hulk reamining lives: " + (m_hulk_lives -= 1).ToString ());
		return m_hulk_lives -= 1;
	}
}
