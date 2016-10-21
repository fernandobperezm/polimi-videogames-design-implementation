using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager Instance = null; // Instance for singleton.

	[Header ("Prefabs for Pooling.")]
	public GameObject m_shot;
	public GameObject m_grunt;
	public GameObject m_grunt_explodes;

	void Awake () {
		//Singleton.
		if (Instance == null) {
			Instance = this;
		} else {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		// Start pooling prefabs, like shots, grunts and exploded grunts.
		ObjectPoolingManager.Instance.CreatePool (m_shot, 100, 100);
		ObjectPoolingManager.Instance.CreatePool (m_grunt, 100, 100);
		ObjectPoolingManager.Instance.CreatePool (m_grunt_explodes, 100, 100);

		// Creating a fized number of grunt forming a circle with center on the players position.
		for (int i = 0; i < 10; i++) {			
			GameObject go = ObjectPoolingManager.Instance.GetObject (m_grunt.name);

			float a = Random.Range (0, Mathf.PI * 2);
			float r = Random.Range (80f, 150f);

			Vector3 np = new Vector3 ();
			np.x = Mathf.Cos (a) * r;
			np.y = Mathf.Sin (a) * r;
			np.z = transform.position.z;

			go.transform.position = np;
			go.transform.rotation = Quaternion.identity;
		}
	}
//
//	IEnumerator NextLevel() {
//		InitLevel ();
//	}
//
//	void InitLevel() {
//		
//	}
	// Update is called once per frame
	void Update () {
	
	}
}
