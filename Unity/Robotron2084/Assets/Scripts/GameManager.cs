using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager Instance = null;

	[Header("Prefabs for Object Pooling.")]
	public GameObject m_shot;
	public GameObject m_grunt;
	public GameObject m_grunt_explodes;

	public int CurrentLevel {get { return m_current_level; }}
	private int m_current_level = 0;

	//reference to the canvas.
	[Header("Loading Screen")]
	public GameObject m_loading_screen;
	public Text m_level_text;

	[Header("Gameplay Screen")]
	public GameObject m_gameplay_screen;
	public GameObject m_score_screen;
	public Text m_score_text;

	public int CurrentScore { get { return m_current_score; } }
	private int m_current_score = 0;

	private int m_no_grunts_alive = 0;

	// Waiting time
	[Range(0f,4f)]
	public float m_loading_time = 2f;

	[Header("Levels")]
	public Level[] m_levels;


	void Awake() {
		// Singleton.
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

		// Setting off all screens.
		m_loading_screen.SetActive (false);
		m_gameplay_screen.SetActive (false);
		m_score_screen.SetActive (false);

		m_current_score = 0;
		// Start coroutines.
		StartCoroutine (NextLevel());
		
	}

	IEnumerator NextLevel() {
		m_current_level = m_current_level + 1;
		m_level_text.text = "LEVEL " + m_current_level.ToString ();
		m_loading_screen.SetActive (true);
		m_gameplay_screen.SetActive (false);
		m_score_screen.SetActive (false);

		yield return new WaitForSeconds (m_loading_time);

		m_loading_screen.SetActive (false);
		InitLevel (m_current_level);
		m_gameplay_screen.SetActive (true);
		m_score_screen.SetActive (true);
	}

	void InitLevel (int level) {
		int no_grunts = 0;
		if (level <= 4)
			no_grunts = m_levels [level - 1].m_no_grunts;
		else
			no_grunts = m_levels [3].m_no_grunts;

		// no_grunts = m_levels [(int)Mathf.Min (level-1,3)].m_no_grunts;

		m_no_grunts_alive = no_grunts;

		for (int i = 0; i < no_grunts; i++) {			
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

	public void Scored(int score){
		m_current_score = m_current_score + score;
		m_score_text.text = m_current_score.ToString ();
		m_no_grunts_alive = m_no_grunts_alive - 1;


		if (m_no_grunts_alive == 0) {
			StartCoroutine (NextLevel ());
		}
	}

}
