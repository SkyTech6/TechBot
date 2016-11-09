using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		Camera.main.orthographicSize = 10.32f / Screen.width * Screen.height;
	}
}