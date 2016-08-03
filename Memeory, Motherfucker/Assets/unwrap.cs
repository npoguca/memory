using UnityEngine;
using System.Collections;

public class unwrap : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	public void Unwrap()
    {
        GetComponent<Animator>().Play("slide");

    }
	// Update is called once per frame
	void Update () {
	    
	}
}
