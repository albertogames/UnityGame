using UnityEngine;
using System.Collections;

public  class PhysicServer{
	
	private static PhysicServer _instance;
	
	public static bool Init(){
		Assert.Test((_instance != null),"PhysicServer.Init: Second initilization of IAServer is not possible");
	    
		_instance = new PhysicServer();
	
		return true;	
	}
	
	public  static PhysicServer getInstance(){
		Assert.Test ((_instance == null),"PhysicServer.getInstance: Instance not initialized");
		
		return _instance;
	}
	
	public  GameObject raycastFromViewPort(out Vector3 point){
		
		point = Vector3.zero;
		GameObject aux = null;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
    	RaycastHit hit;	
    	if(Physics.Raycast (ray, out hit, 500)){
        	Debug.DrawLine (ray.origin, hit.point);
        	//Nombre del objeto
			//string objectTouching = hit.collider.name;
        	aux = hit.collider.gameObject;
			point = hit.point;
		}
		return aux ;
	}
	
}
