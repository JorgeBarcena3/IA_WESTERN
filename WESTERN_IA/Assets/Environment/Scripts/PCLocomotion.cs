using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCLocomotion : MonoBehaviour
{
    public GameObject RootElement;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    var x = Input.GetAxis("Horizontal") * Time.deltaTime * 10.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 10.0f;
	    var y = Input.GetAxis("Rotate") * Time.deltaTime * 180.0f;
        RootElement.transform.Rotate(0, y, 0);
        RootElement.transform.Translate(x, 0, z);
        
    }
}
