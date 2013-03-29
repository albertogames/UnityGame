using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectionControllerIA : MonoBehaviour {

	private bool mouseOnGUI = false;
	private GameObject selected = null;
	private GameObject auxSelected = null;
	private Vector2 initialPos = new Vector2(0,0);
	private Vector2 finalPos = new Vector2(0,0);
	private Vector3 point = new Vector3(0,0,0);
	
	public Transform cube1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!mouseOnGUI){
			
			Vector2 auxPos = new Vector2(0,0);
			if (Input.anyKey){
				PhysicServer.getInstance().raycastFromViewPort(out point);
				auxPos = LogicMap.getInstance().coordsToCell(point.x,point.z);	
			}
			
			if (Input.GetMouseButton(0)){ 
				initialPos = auxPos;		
			}
				
			if (Input.GetMouseButton(1)){
				finalPos = auxPos;
			}
			
			Debug.Log("Ini: " + initialPos.ToString() + " Final: " + finalPos.ToString());	
			
			if (Input.GetKeyUp(KeyCode.C)){
				
				
				List<IAStarNode> route = IAServer.getInstance().calculateRoute(initialPos,finalPos);
				foreach (IAStarNode node in route){
					Instantiate(cube1,new Vector3(node.getPosX()*2,1,node.getPosY()*2),cube1.transform.rotation);
				}
			}
			
			
		
		}
		
	}
	
	void OnGUI() {
		//mouseOnGUI = true;
		
	}
	
}

