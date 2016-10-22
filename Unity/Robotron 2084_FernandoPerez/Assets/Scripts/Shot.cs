using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {

	Transform tr;

	// Prefab of the grunt explotion. With this, we can handle the explotion.
	public GameObject m_grunt_explodes;

	[Header ("Shoot speed")]
	[Range (250f,500f)]
	public float m_shot = 300f;

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> () as Transform;
	}
	
	// Update is called once per frame
	void Update () {
		//Checking if the shot goes width farther.
		if (Mathf.Abs (tr.position.x) > 670) {
			gameObject.SetActive (false);
		}

		// Checking if the shot goes height farther.
		if (Mathf.Abs (tr.position.y) > 470) {
			gameObject.SetActive (false);
		}
	}

	// Movement of the shot.
	void FixedUpdate () {
		tr.position += tr.right * Time.fixedDeltaTime * m_shot;
	}

	// Triggered when a collision happens.
	void OnTriggerEnter2D (Collider2D otherGameObject) {
		Debug.Log ("Hit " + otherGameObject.gameObject.name);

		// Instantiation of the grunt explodes game object.
		GameObject go = ObjectPoolingManager.Instance.GetObject (m_grunt_explodes.name);

		// Setting the position and rotation of the grunt exploded in the same as the grunt already shoted.
		go.transform.position = otherGameObject.transform.position;
		go.transform.rotation = otherGameObject.transform.rotation;

		// Activating sound.
		SoundManager.Instance.GruntExplodes ();

		// Destroying the shot and the grunt.
		otherGameObject.gameObject.SetActive (false);
		gameObject.SetActive (false);
	}
}
