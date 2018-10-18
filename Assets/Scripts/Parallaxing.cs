using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour {

	public Transform[] backgrounds;     // array of back/foregounds
	private float[] parallaxScales;     // proportion of cameras movement to move background by
	public float smoothing = 1f;		// how smooth the parallax will be

	private Transform cam;              // reference to the main cameras transform
	private Vector3 previousCamPos;     // pos of camera in prev frame

	void Awake(){
		//set up camera reference
		cam = Camera.main.transform;
	}

	// Use this for initialization
	void Start () {
		previousCamPos = cam.position;

		parallaxScales = new float[backgrounds.Length];

		for(int i = 0; i < backgrounds.Length; i++)
		{
			parallaxScales[i] = backgrounds[i].position.z * -1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//for ea background
		for(int i = 0; i < backgrounds.Length; i++)
		{
			//the parallax is the opposite of the camera movement because the previous grame multiplied by the scale
			float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

			//set a target x position which is the current position plus the parallax
			float backgroundTargetPosX = backgrounds[i].position.x + parallax;

			//create target position with new x
			Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

			//(lerp) fade between current and target position
			backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
			
		}

		//set previousCamPos to the camera's position at the end of the frame
		previousCamPos = cam.position;
	}
}
