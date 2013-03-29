using UnityEngine;
using System.Collections;

using System.IO;
using System.Net.Sockets;
using System.Text;

//Clase encargada de inicializar los diferentes ser
public class Load : MonoBehaviour {
	public Terrain _delete;
	
	private LineRenderer _lineRenderer;
	private LogicMap _logicMap;
	
	// Use this for initialization
	void Awake () {
		
		//Inicializar Servidores de Physic y de IA
		
		PhysicServer.Init();
		
		IAServer.Init();
		
		//Inicializamos el Terrain Loader
		MapServer.Init();
		
		//Cargamos el mapa
		MapServer.getInstance().loadMap("Terrain/Terrains/Terrain",4);
		
		IAServer.getInstance().setIAMap(LogicMap.getInstance());
		
		_logicMap = LogicMap.getInstance();
		
		
	}
	
	void Start(){
		/*
		_lineRenderer = gameObject.AddComponent<LineRenderer>();
		_lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		_lineRenderer.SetColors(Color.white, Color.white);
		_lineRenderer.SetWidth(0.2F, 0.2F);
		*/	
		byte[] message = new byte[200];
		
		int bytesRead;
		
		ASCIIEncoding encoder = new ASCIIEncoding();
		
		TcpClient client  = new TcpClient ("127.0.0.1",9999);
		
		NetworkStream clientStream = client.GetStream();
		
		byte[] buffer = encoder.GetBytes("Hello Server!");
		
		clientStream.Write(buffer, 0 , buffer.Length);
		
		bytesRead = clientStream.Read(message,0,200);
		
		//message has successfully been received
	    
	    Debug.Log("From Server: " + encoder.GetString(message, 0, bytesRead));
		
	}
	
	public void LoadEntity(GameObject prefab, Vector3 position, Quaternion rotation, int group){
		if (UserVariables.online){
			Network.Instantiate(prefab,position,rotation,group);
		}else{
			Instantiate(prefab,position,rotation);
		}
	}
	
	void Update(){
		/*
		Vector3 auxPosition = _logicMap.getPosition(); 
		auxPosition.Set(auxPosition.x,auxPosition.y + (float)0.5,auxPosition.z);
		transform.position = _logicMap.getPosition();
		
		_lineRenderer.SetVertexCount((int)_logicMap.getTamCells().x * 2 + (int)_logicMap.getTamCells().y * 2);
		
		int i = 0;
		
		_lineRenderer.SetPosition(0, auxPosition);
		
		i++;
		
		for (; i <= (_logicMap.getTamCells().x * 2) ; i++){
			
			if (i % 2 == 0){
				    auxPosition.z += _logicMap.getCellTam();                                                            
			}else{
				if (i % 4 == 1){
					auxPosition.x += _logicMap.getUnitsSize().x;
				}else if (i % 4 == 3){
					auxPosition.x -= _logicMap.getUnitsSize().x;
				}
			}
			_lineRenderer.SetPosition(i,auxPosition);
			
		}
		
		
		for (; i < ((int)_logicMap.getTamCells().x * 2 + (int)_logicMap.getTamCells().y * 2) ; i++){
			
			if (i % 2 == 0){
				    auxPosition.x += _logicMap.getCellTam();                                                            
			}else{
				if (i % 4 == 1){
					auxPosition.z -= _logicMap.getUnitsSize().y;
				}else if (i % 4 == 3){
					auxPosition.z += _logicMap.getUnitsSize().y;
				}
			}
			_lineRenderer.SetPosition(i,auxPosition);
			
		}
		
		*/
	}
}