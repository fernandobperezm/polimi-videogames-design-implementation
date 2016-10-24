using UnityEngine;
using System.Collections;

public class Hulk : Living {

	[Header ("Hulk Exploding prefab.")]
	public GameObject m_hulk_explodes;

	// Hulks doesn't chase the player, they have a circular movement around they creation spot. But they have three lives.
	private int m_hulk_lives;
	float m_hulk_angle = 0f; // angle
	float m_hulk_ratio = 0f; // ratio

	Transform tr;

	// Use this for initialization
	void Start () {
		tr = GetComponent<Transform> () as Transform;
		this.m_no_lives = 3;
	}

	void OnEnable() {
		EventManager.StartListening ("Explode",Explode);
		this.m_no_lives = 3;
	}

	void Explode() {
		EventManager.StopListening ("Explode",Explode);
		StartCoroutine (ExplodeCoroutine ());
	}

	IEnumerator ExplodeCoroutine () {
		yield return new WaitForSeconds (0.01f);
		GameObject go = ObjectPoolingManager.Instance.GetObject (m_hulk_explodes.name);
		go.transform.position = gameObject.transform.position;
		go.transform.rotation = gameObject.transform.rotation;
		gameObject.SetActive (false);
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
}
