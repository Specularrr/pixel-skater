using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {

	public int offsetX = 2;					// to avoid visual errors

	// checking if need to instantiate stuff
	public bool hasRightBuddy = false;		
	public bool hasLeftBuddy = false;

	public bool reverseScale = false;		// used if object is not tilable

	public float spriteWidth = 0f;         // the width of our element
	private Camera cam;
	private Transform myTransform;

	void Awake()
	{
		cam = Camera.main;
		myTransform = transform;
	}

	// Use this for initialization
	void Start () {
		SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x;
        //spriteWidth = sRenderer.sprite.bounds.size.x * myTransform.localScale.x;
    }
	
	// Update is called once per frame
	void Update () {
		//does it still need buddies? if not. do nothing
		if (hasLeftBuddy == false || hasRightBuddy == false)
		{
			//calc the cameras extend (half of the width) of what the camera can see in world coordinates
			float camHorizontalExtend = cam.orthographicSize * Screen.width/Screen.height;

			//calc x position where the camera can see the edge of the sprite
			float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth / 2) - camHorizontalExtend;
			float edgeVisiblePositionLeft = (myTransform.position.x + -spriteWidth / 2) + camHorizontalExtend;

			//checking if we can see the end of the element, then calling make a new buddy
			if (cam.transform.position.x >= edgeVisiblePositionRight - offsetX && hasRightBuddy == false)
			{
				MakeNewBuddy(1);
				hasRightBuddy = true;
			}
			else if (cam.transform.position.x <= edgeVisiblePositionLeft + offsetX && hasLeftBuddy == false)
			{
				MakeNewBuddy(-1);
				hasLeftBuddy = true;
			}
		}
	}
	
	//makes a buddy on the side needed
	void MakeNewBuddy(int rightOrLeft)
	{
		// calc new position for buddy
		Vector3 newPosition = new Vector3 (myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);
		//instantiating new buddy and storing in a variable
		Transform newBuddy = Instantiate(myTransform, newPosition, myTransform.rotation) as Transform;

		//invert along x, new instantiation
		if(reverseScale == true)
		{
			newBuddy.localScale = new Vector3(newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);
		}

		newBuddy.parent = myTransform.parent;

		if (rightOrLeft > 0)
		{
			newBuddy.GetComponent<Tiling>().hasLeftBuddy = true;
		}
		else
		{
			newBuddy.GetComponent<Tiling>().hasRightBuddy = true;
		}
	}
}
