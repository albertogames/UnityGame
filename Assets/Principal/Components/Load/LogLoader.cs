using UnityEngine;
using System.Collections;

public class LogLoader : MonoBehaviour {

	
	// Use this for initialization
	void Awake () {
		if (CommunicationServer.Init()){
			
			CommunicationServer.getInstance().run();
			
			Parser.Init(Constants.PARSER_CHAR);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//onGUI
	void onGUI(){
		
	}
	
	private void OnApplicationQuit()
    {
        CommunicationServer.Release();
    }
}
