using UnityEngine;
using System.Collections;

public class MapServer{
	
	private static MapServer _instance;
	
	public Terrain _terrain;
	public Transform _playerPosition;
	
	private static GameObject _mapGameObject;
	
	//El Mapa tiene que ser cuadrado y cada lado debe ser potencia de 2, al igual que el tamaño de las casillas.
	public int _squareMapSize = 2;
	
	private ILogicMap _map;
	
	private int _iAMap;
	
	MapServer(){
	}
	
	~MapServer(){
		
	}
	
	public static bool Init(){
		Assert.Test((_instance != null),"TerrainLoader.Init: Second initilization of TerrainLoader is not possible");
	    
		_instance = new MapServer();
		
		LogicMap.Init();
		
		return true;	
	}
	
	public static MapServer getInstance(){
		Assert.Test ((_instance == null),"TerrainLoader.getInstance: Instance not initialized");
		
		return _instance;
	}
	
	
	public static bool Release(){
		
		LogicMap.Release();
		
		MonoBehaviour.DestroyImmediate(_mapGameObject);
		
		_instance = null;
		
		return true;
	}

	public void loadMap(string nameMap, int squareMapSize)
	{
	
		_mapGameObject = MonoBehaviour.Instantiate(Resources.Load(nameMap)) as GameObject;
		
		Assert.Test ((_mapGameObject == null), "Terrain named " + nameMap + " doesnt exist");
			
		Terrain terrain = ((Terrain)_mapGameObject.GetComponent(typeof(Terrain)));
		
		Assert.Test ((terrain == null), "Terrain named " + nameMap + " doesnt have terrain component");
		
		float time = Time.realtimeSinceStartup;

		LogicMap.getInstance().createLogicMap(terrain,squareMapSize);
		
		LogicMap.getInstance().calculateCells();
		
		time = Time.realtimeSinceStartup - time;
		
		MonoBehaviour.print("Timeloading: " + time);
		
	}
	
	public void instantiateMap(){
	
	}
	
	public void calculateCells(){
		LogicMap.getInstance().calculateCells();
	}
	
	public LogicMap getLogicMap(){
		return LogicMap.getInstance();
	}
	
}
