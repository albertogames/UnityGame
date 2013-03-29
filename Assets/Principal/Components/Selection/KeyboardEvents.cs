using UnityEngine;
using System.Collections;

public class KeyboardEvents : MonoBehaviour {
	
	public GameObject cube1;
	public GameObject cube2;
	
	private GameObject _loader;
	// Use this for initialization
	void Start () {
		 _loader = GameObject.Find("Loader");
		
		((SelectionController)GetComponent(typeof(SelectionController))).enabled = true;
			((BuildingController)GetComponent(typeof(BuildingController))).enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.N))
    	{	
			Vector3 point = new Vector3(-1,-1,-1);
				
			PhysicServer.getInstance().raycastFromViewPort(out point);
			
			point.y = 5;
			switch (UserVariables.playerID){
				case 1:
					((Load)_loader.GetComponent(typeof(Load))).LoadEntity(cube1,point,cube1.transform.rotation,0);
					break;
				
				case 2:
					((Load)_loader.GetComponent(typeof(Load))).LoadEntity(cube1,point,cube1.transform.rotation,0);
					break;
				
			}
			
		}
		
		if (Input.GetKeyDown(KeyCode.C)){
			((SelectionController)GetComponent(typeof(SelectionController))).enabled = false;
			((BuildingController)GetComponent(typeof(BuildingController))).enabled = true;
		}
		
		if (Input.GetKeyDown(KeyCode.V)){
			((SelectionController)GetComponent(typeof(SelectionController))).enabled = true;
			((BuildingController)GetComponent(typeof(BuildingController))).enabled = false;
		}
			
	}
}
