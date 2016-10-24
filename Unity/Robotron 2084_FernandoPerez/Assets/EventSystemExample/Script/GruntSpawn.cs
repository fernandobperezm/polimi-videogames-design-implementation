using UnityEngine;
using System.Collections;

public class GruntSpawn : MonoBehaviour {
	public GameObject m_grunt_prefab;

	void OnEnable() {
		EventManager.StartListening ("Spawn", Spawn);
	}

	void OnDisable() {
		EventManager.StopListening ("Spawn", Spawn);
	}

	// Update is called once per frame
	void Spawn () {
		EventManager.StopListening ("Spawn", Spawn);
		for (int i = 0; i < 10; i++) {
			Vector3 p = new Vector3 ();

			p.x = Random.Range (-200, 200);
			p.y = Random.Range (-120, 120);

			Instantiate (m_grunt_prefab, p, Quaternion.identity);
		}

		EventManager.StartListening ("Spawn", Spawn);

	}
}
