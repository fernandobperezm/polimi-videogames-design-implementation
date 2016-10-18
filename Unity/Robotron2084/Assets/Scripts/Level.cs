using UnityEngine;
using System.Collections;

[CreateAssetMenu] // You create the elements from the menu.
public class Level : ScriptableObject {

	[Range(0,100)]
	public int m_no_grunts = 0;

	public Color background = Color.black;

	// Set up settings.
	void onEnable() {
		// do we need to init anyhow?
	}

	//Get settings.
	void onDisable() {

	}


}
