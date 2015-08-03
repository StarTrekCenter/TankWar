using UnityEngine;
using System.Collections;

public class TankColor : MonoBehaviour 
{
	public Color color = Color.red;

	void Start () 
	{
		var renderers = this.GetComponentsInChildren<Renderer> ();
		foreach (var renderer in renderers) {
			renderer.material.color = this.color;
		}
	}
}
