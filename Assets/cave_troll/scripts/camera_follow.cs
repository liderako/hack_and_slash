using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_follow : MonoBehaviour {

	public GameObject charRoot;
	Vector3 offset;

	void Start () {
		offset = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = charRoot.transform.position + offset;
	}
}
