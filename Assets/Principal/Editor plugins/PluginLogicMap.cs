using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class PluginLogicMap : ScriptableWizard{

	public string _terrain = "Terrain";
	
	public int _squareSize = 2;
	
	private LogicMap _logicMap = null; 
	
	private int[,] _mapCells;
	
	private Vector2 _mapUnitSize;
	
	private Vector3 _mapPosition;
	
	private bool init;
	
	private List<Vector3> _mapOccupedCells;
	private List<GameObject> _gameObjectsOccuped;
	
	
	[MenuItem ("Terrain/Generate Logic Map")]
	static void CreateWindow(){
		ScriptableWizard.DisplayWizard(
			"Generate Logic Map",
			typeof(PluginLogicMap),
			"Create",
			"Close");
		
		
	}
	
	
	void OnWizardCreate(){
		/*if (MapServer.getInstance() == null){
		
			MapServer.Init();
			
			Debug.Log("OnWizardOtherButton(Generate was pressed)");	
			
			MapServer.getInstance().loadMap("Terrain/Terrains/" + _terrain,2);
			
			_logicMap = LogicMap.getInstance();
			
			Assert.Test((_logicMap == null),"PluginLogicMap.OnWizardOtherButton: impossible to get logicMap instance");
			
			_mapPosition = _logicMap.getPosition();
			_mapCells = _logicMap.getMap();	
			_mapUnitSize = _logicMap.getUnitsSize();	
		
			
			_mapOccupedCells = _logicMap.getOccupedCells();
			MapServer.getInstance().calculateCells();
			
			Vector3 auxVector1 =  new Vector3(0,0,0);
		
			GameObject auxCube;
			
			foreach (Vector3 occupedCell in _mapOccupedCells){
				
				auxVector1.Set(_mapPosition.x + (float)_squareSize*occupedCell.x,(float)0.5,_mapPosition.z + (float)_squareSize*occupedCell.y);
				Debug.DrawLine(auxVector1, auxVector2,Color.red,1,false);
				
			
				auxVector1.Set(_mapPosition.x + (float)_squareSize*occupedCell.x,(float)0.5,_mapPosition.z + (float)_squareSize*occupedCell.y);
				
				/*auxCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
				auxCube.transform.position = new Vector3(auxVector1.x + (float)_squareSize/2,(float)0.0,auxVector1.z + (float)_squareSize/2);
				auxCube.transform.localScale = new Vector3 ((float)_squareSize,(float)_squareSize,(float)_squareSize);
			
				MonoBehaviour.Instantiate(auxCube);
				
			//	Debug.Log("ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ");	
			}
		}
		int layerMask = 1 << LayerMask.NameToLayer("Terrain");
		layerMask = ~layerMask;
	
		if (Physics.CheckSphere(new Vector3(128,0,128),128,layerMask)){
			int x;
			x = 123;
		}
		*/
	}
	
	void OnWizardOtherButton()
	{
		MapServer.Release();
		init = false;
		Close();

	}
	
	void OnWizardUpdate(){

		
	
	}
	
	void OnDrawGizmos(){
			
			/*Vector3 auxPosition = _mapPosition;
			auxPosition.Set(auxPosition.x,auxPosition.y + (float)0.5,auxPosition.z);
			
			for (int i = 0; i <= _logicMap.getTamCells().x ; i++){
				
				Debug.DrawLine(auxPosition,new Vector3(_mapUnitSize.x,auxPosition.y,auxPosition.z),Color.green,1,false);
				auxPosition.Set (auxPosition.x,auxPosition.y, auxPosition.z + (float)_squareSize);
			
			}
			
			auxPosition = _mapPosition;
			for (int i = 0; i <= _logicMap.getTamCells().y ; i++){	
				
				Debug.DrawLine(auxPosition,new Vector3(auxPosition.x,auxPosition.y,_mapUnitSize.y),Color.green,1,false);
				auxPosition.Set (auxPosition.x + (float) _squareSize,auxPosition.y, auxPosition.z);
			
			}*/	
	}
	
}
