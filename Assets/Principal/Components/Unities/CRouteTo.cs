using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CRouteTo : MonoBehaviour {
	
	private List<IAStarNode> _path;
	
	private Vector3 _target;
	private float _tolerance;
	private Vector3 _actualTarget;
	
	private int _actualNode;
	
	private bool _arrived;
	private bool _startRoute;
	
	private bool _stopRoute;	
	
	private bool _nextPathNode;
	
	
	private LogicMap _logicMap;
	private IAServer _IAServer;

	
	// Use this for initialization
	void Start () {
		_arrived = false;
		_stopRoute = false;
		_nextPathNode = false;
		_stopRoute = false;
		
		_logicMap = MapServer.getInstance().getLogicMap();
		_IAServer = IAServer.getInstance();
	}
	
	// Update is called once per frame
	void Update () {
		if (_startRoute){
			
			_startRoute = false;
			
			//Obtener el primer nodo de la ruta
			IAStarNode auxNode = getNextNode();
			
			//Indicar a la entidad que se mueva hacia el siguiente nodo
			
			if (auxNode != null)
				SendMessage("walkTo",_logicMap.cellToCoords(auxNode.getPosX(),auxNode.getPosY()));	
		
		}
		
		if (_arrived){
			
			_arrived = false;
			
			//Obtener el siguiente nodo de la ruta
			IAStarNode auxNode = getNextNode();
			
			//Si es el último indicar a la entidad que pare 
			//ANIMACION: Indicar que se cambie la animación ya que ya estará parada. En principio no va a haber animaciones pero dejar pos si añaden
			
			if (auxNode == null){
				
			}else{
				gameObject.SendMessage("walkTo",_logicMap.cellToCoords(auxNode.getPosX(),auxNode.getPosY()));
			}
			
			//Si no, indicar a la entidad que se mueva hacia el siguiente nodo
		}
		
		if (_stopRoute){
			//Indicar a la entidad que pare
			gameObject.SendMessage("stopWalkTo");
		}
		
	}
	
	private void routeTo(Vector2 destination){
		
		
		
		_path = _IAServer.calculateRoute(
				_logicMap.coordsToCell(gameObject.transform.position.x,gameObject.transform.position.z),
				_logicMap.coordsToCell(destination.x,destination.y));
		
		
		
		_actualNode = 0;
		_startRoute = true;
		
		

		

	}
	
	//Devuelve el siguiente nodo de la ruta al que hay que ir
	private IAStarNode getNextNode(){
		
		IAStarNode aux = null;
		
		if (_actualNode == _path.Count){
			
			//_stopRoute = false;
			_arrived = false;
			_startRoute = false;
			
			_actualNode = 0;
			
		}else{
			aux = _path[_actualNode];
			_actualNode ++;
		}
		
		
		
		return aux;
	}
	
	//Llamado cuando la entidad llega al destino indicado
	private void arrived(){
		_arrived = true;
	}
	
	
	//Llamado cuando la entidad necesita parar de ejecutar la ruta
	private void stopRoute(){
		_stopRoute = true;
	}
	
	 
	private void finishPath(){
		_nextPathNode = true;
	}
	
	
}
