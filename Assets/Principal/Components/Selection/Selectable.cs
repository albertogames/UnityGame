using UnityEngine;
using System.Collections;

public class Selectable : MonoBehaviour {
	
	public bool selectable;
	
	// Use this for initialization



	void isSelectable(GameObject sender){
		
		
		//Comprobar ID del jugador
		if (networkView.isMine){
			sender.SendMessage("isSelectableResponse", selectable);
			
			if (selectable){
				transform.renderer.material.SetColor("_OutlineColor", Color.yellow);
			}
			
			
		}
		
	}
	
	void unSelect(){
		transform.renderer.material.SetColor("_OutlineColor",Color.black);
	}

	
}
