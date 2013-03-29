using UnityEngine;
using System.Collections;

public class InitialMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		
			if (GUI.Button (new Rect(10,10,100,30),"Single Player"))
			{
				
				
			
			}
			if (GUI.Button (new Rect(10,50,100,30),"Multi Player"))
			{
				
				
				
				
			}
			
			if (GUI.Button (new Rect(10,90,100,30),"Settings"))
			{
			
				
				
				
			}
		
			if (GUI.Button (new Rect(10,130,100,30),"Exit"))
			{
				Application.Quit();
			}
		
	
	}
	
	
}
