using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleToCamera : MonoBehaviour {

    public Camera cam;
    public float objectScale = 0.04f;
    private Vector3 initialScale;
	private Vector3 initialPosition;

    // set the initial scale, and setup reference camera
    void Start()
    {
        // record initial scale, use this as a basis
        initialScale = transform.localScale;
		initialPosition = transform.localPosition;

        // if no specific camera, grab the default camera
        if (cam == null)
            cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    // scale object relative to distance from camera plane
    void Update()
    {
        Plane plane = new Plane(cam.transform.forward, cam.transform.position);
        float dist = plane.GetDistanceToPoint(transform.position);
        transform.localScale = initialScale * dist * objectScale;
		transform.localPosition = initialPosition * dist * objectScale;
    }
}
