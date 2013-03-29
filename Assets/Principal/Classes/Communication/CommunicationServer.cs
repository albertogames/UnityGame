using UnityEngine;
using System.Collections;

public class CommunicationServer{
	
	private static CommunicationServer _instance;
	
	private CommunicationServer(){
	}
	// Use this for initialization
	void Start () {
	
	}
	
	public static bool Init(){
		return true;
	}
	
	public void Connect(){
	}
	
	public void Disconncet(){
	}
}
