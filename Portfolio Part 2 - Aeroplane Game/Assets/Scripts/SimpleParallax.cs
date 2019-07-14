using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleParallax : MonoBehaviour {

    //Camera Reference
    public GameObject parallaxTarget;

    //Define the height of our tiles
    public float tileHeight;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        for(int i=0; i<transform.childCount;i++)
        {
            GameObject currentTile = transform.GetChild(i).gameObject;
            if(parallaxTarget.transform.position.y - currentTile.transform.position.y >= tileHeight)
            {
                currentTile.transform.position = new Vector2(0, currentTile.transform.position.y + transform.childCount * tileHeight);
            }
        }

	}
}
