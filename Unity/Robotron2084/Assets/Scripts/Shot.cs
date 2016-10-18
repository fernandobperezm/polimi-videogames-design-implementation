using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {

	public float m_speed = 200f;
	public GameObject m_grunt_explodes;

	Transform tr;

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> () as Transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		tr.position = tr.position +
		tr.right * m_speed * Time.fixedDeltaTime;

		if (Mathf.Abs (tr.position.x) > 220f)
			gameObject.SetActive(false);
		if (Mathf.Abs (tr.position.y) > 120f)
			gameObject.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("HIT " + other.gameObject.name);
		GameObject go = ObjectPoolingManager.Instance.GetObject (m_grunt_explodes.name);
		go.transform.position = other.gameObject.transform.position;
		go.transform.rotation = other.gameObject.transform.rotation;
		SoundManager.Instance.GruntExplodes ();
		//Destroy (other.gameObject);
		other.gameObject.SetActive(false);
		GameManager.Instance.Scored (100);
		//Destroy (gameObject);
		gameObject.SetActive(false);
	}
}
