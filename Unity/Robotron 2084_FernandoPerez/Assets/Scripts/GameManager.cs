using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager Instance = null; // Instance for singleton.

	[Header ("Prefabs for Pooling.")]
	public GameObject m_shot;
	public GameObject m_grunt;
	public GameObject m_grunt_explodes;
	public GameObject m_hulk;
	public GameObject m_hulk_explodes;
	public GameObject m_player_explodes;

	// Level Management.
	public int CurrentLevel {get {return m_current_level;}}
	private int m_current_level = 0;

	// Loading Screen.
	[Header ("Loading Screen")]
	public GameObject m_loading_screen;
	public Text m_level_text;

	// Gameplay Screen.
	[Header ("Gameplay Screen")]
	public GameObject m_gameplay_screen;
	public GameObject m_score_screen;
	public Text m_score;

	// Gameover Screen.
	[Header ("Gameover Screen")]
	public GameObject m_gameover_screen;
	public Text m_gameover_score;

	// Gameover Screen.
	[Header ("YouWin Screen")]
	public GameObject m_youwin_screen;
	public Text m_youwin_score;

	// Score.
	public int CurrentScore {get { return m_current_score;}}
	private int m_current_score = 0;

	// Managing enemies alive.
	private int m_no_enemies_alive = 0;

	// Waiting time.
	[Header ("Waiting time.")]
	[Range(0f,4f)]
	public float m_loading_time = 2f;

	[Header ("Levels")]
	public Level[] m_levels;
	int m_no_levels = 10;

	[Header ("Lives Sprites")]
	public GameObject[] m_array_lives;

	[Header ("Player and Lives")]
	public GameObject m_player;
	public GameplayScreen m_GamePlay_Screen;

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
		ObjectPoolingManager.Instance.CreatePool (m_hulk, 100, 100);
		ObjectPoolingManager.Instance.CreatePool (m_hulk_explodes, 100, 100);
		ObjectPoolingManager.Instance.CreatePool (m_player_explodes,1,1);

		m_current_score = 0;
		m_current_level = 0;

		m_GamePlay_Screen.FillLives ();

		m_loading_screen.SetActive (false);
		m_gameplay_screen.SetActive (false);
		m_score_screen.SetActive (false);
		m_youwin_screen.SetActive (false);
		m_gameover_screen.SetActive (false);

		m_player.SetActive (true);

		// Start coroutines.
		StartCoroutine (NextLevel ());
	}

	IEnumerator NextLevel() {
		m_current_level += 1;
		m_level_text.text = "WAVE-" + m_current_level.ToString ();
		m_loading_screen.SetActive (true);
		m_gameplay_screen.SetActive (false);
		m_score_screen.SetActive (false);
		m_youwin_screen.SetActive (false);
		m_gameover_screen.SetActive (false);

		yield return new WaitForSeconds (m_loading_time);

		m_loading_screen.SetActive (true);
		m_gameplay_screen.SetActive (true);
		m_score_screen.SetActive (true);
		m_gameover_screen.SetActive (false);
		InitLevel (m_current_level);
	
	}

	void InitLevel(int level) {
		// Number of monsters, given the levels assets (scriptable object).
		int no_grunts = 0;
		int no_hulks = 0;

		if (level <= m_no_levels) {
			no_grunts = m_levels [level - 1].m_no_grunts;
			no_hulks = m_levels[level-1].m_no_hulks;
		} else {
			OnWin ();
		}

		m_no_enemies_alive = no_grunts + no_hulks;

		// Creating a fized number of grunt forming a circle with center on the players position.
		for (int i = 0; i < no_grunts; i++) {			
			GameObject go = ObjectPoolingManager.Instance.GetObject (m_grunt.name);
			float a;
			float r;
			Vector3 np = new Vector3 ();

			do {
				a = Random.Range (0, Mathf.PI * 2);
				r = Random.Range (160f, 300f);

				np.x = Mathf.Cos (a) * r;
				np.y = Mathf.Sin (a) * r;

			} while ( (Mathf.Abs (np.x - m_player.transform.position.x) < 100) || (Mathf.Abs (np.y - m_player.transform.position.y) < 50) );

			np.z = transform.position.z;

			go.transform.position = np;
			go.transform.rotation = Quaternion.identity;
		}

		for (int i = 0; i < no_hulks; i++) {			
			GameObject go = ObjectPoolingManager.Instance.GetObject (m_hulk.name);

			Vector3 np = new Vector3 ();
			np.x = ((Random.Range (0,2) * 2) - 1) * Random.Range (400f,600f);
			np.y = ((Random.Range (0,2) * 2) - 1) * Random.Range (100f,200f);
			np.z = transform.position.z;

			go.transform.position = np;
			go.transform.rotation = Quaternion.identity;
		}
	}

	public void Scored(int score) {
		m_current_score += score;
		m_score.text = m_current_score.ToString ();
		m_no_enemies_alive -= 1;

		if (m_no_enemies_alive == 0){
			StartCoroutine(NextLevel ());
		}
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateLives(int lives_to_empty) {
		m_GamePlay_Screen.EmptyFixedLives (lives_to_empty);
	}

	public void OnWin() {
		m_youwin_screen.SetActive (true);
		m_gameover_screen.SetActive (false);
		m_loading_screen.SetActive (false);
		m_gameplay_screen.SetActive (false);
		m_score_screen.SetActive (false);

		m_youwin_score.text = "Your Score: " + m_current_score.ToString ();
		EventManager.TriggerEvent ("Explode");
	}

	public void OnGameover() {
		m_gameover_screen.SetActive (true);
		m_youwin_screen.SetActive (false);
		m_loading_screen.SetActive (false);
		m_gameplay_screen.SetActive (false);
		m_score_screen.SetActive (false);

		m_gameover_score.text = "Your Score: " + m_current_score.ToString ();
		EventManager.TriggerEvent ("Explode");
	}

	public void OnPlayAgain() {
		Start ();
	}

	public void OnMenu() {
		SceneManager.LoadScene ("MenuScreen");
	}
}
