using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject m_coin;

	// Use this for initialization
	void Start () {
		StartCoroutine (SpawnCoins());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator SpawnCoins () {
		for (int i = 0; i < 200; i++) {
			Vector3 p = new Vector3 ();
			p.x = Random.Range (-9f, 9f);
			p.y = gameObject.transform.position.y;
			p.z = gameObject.transform.position.z;
			Instantiate (m_coin, p, Quaternion.identity);
			yield return new WaitForSeconds (Random.Range (0f,0.2f));

			yield return null; // This line avoids Crashes of the Coroutine and therefore, Unity crashes :D
		}
	}
}
