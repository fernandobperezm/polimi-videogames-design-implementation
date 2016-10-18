using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager Instance = null;

	public GameObject m_shot;
	public GameObject m_grunt;
	public GameObject m_grunt_explodes;

	void Awake() {
		if (Instance == null) {
			Instance = this;
		} else {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		ObjectPoolingManager.Instance.CreatePool (m_shot, 100, 100);
		ObjectPoolingManager.Instance.CreatePool (m_grunt, 100, 100);
		ObjectPoolingManager.Instance.CreatePool (m_grunt_explodes, 100, 100);

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
	
	// Update is called once per frame
	void Update () {
	
	}
}
