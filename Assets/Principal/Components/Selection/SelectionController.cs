using UnityEngine;
using System.Collections;

public class SelectionController : MonoBehaviour {

	private bool mouseOnGUI = false;
	private GameObject selected = null;
	private GameObject auxSelected = null;
	private Vector3 point;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!mouseOnGUI){
			
			if (Input.GetMouseButton(0)){
				
				point = new Vector3(-1,-1,-1);
				
				GameObject objeto = PhysicServer.getInstance().raycastFromViewPort(out point);
				
				if (objeto != null){
						auxSelected = objeto;
						
						auxSelected.SendMessage("isSelectable",this.gameObject,SendMessageOptions.DontRequireReceiver);
					
						//Debug.Log("SelectionController: " + auxSelected.name);
						//Debug.Log("Point: " + point);
				}
				
			}
				
			if (Input.GetMouseButton(1)){
				if (selected != null){
					PhysicServer.getInstance().raycastFromViewPort(out point);
					
					//selected.transform.position = new Vector3(point.x,0.5f,point.z);	
					//selected.SendMessage("WalkTo",new Vector2(point.x,point.z));
					selected.SendMessage("routeTo",new Vector2(point.x,point.z));
					//Debug.Log("Move Selected");
				}
				
			}		
		}
		
	}
	
	void OnGUI() {
		//mouseOnGUI = true;
		
	}
	
	void isSelectableResponse(bool isSelectable){
		if (isSelectable){
			
			if (selected != null){
				if (auxSelected.GetInstanceID() != selected.GetInstanceID()){
					selected.SendMessage("unSelect");
				}	
			}
			
			selected = auxSelected;
			//Debug.Log("Seleccionado: " + selected.name );
		}
		//Debug.Log("Mantener seleccionado: ");
	}
	
}
