using UnityEngine;
using System.Collections;

[CreateAssetMenu] // You create the elements from the menu.
public class Level : ScriptableObject {
	[Range(0,100)]
	public int m_no_grunts = 0;

	[Range(0,100)]
	public int m_no_hulks = 0;

	public Color m_background = Color.black;

	// set up.
	void onEnable() {
		
	}

	// get settings.
	void onDisable() {
		
	}
}
