using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LogicMap: ILogicMap{
	
	private static LogicMap _instance;
	
	//private List<Cell> _map;
	
	private Cell[,] _map;
	
	private List<Cell> _occupedCells;
	
	private int _tamCellsX;
	private int _tamCellsZ;
	
	private int _tamUnitsX;
	private int _tamUnitsZ;
	
	private float _posX;
	private float _posZ;
	
	private Terrain _terrain;
	
	private int _cellTam;
	
	private LogicMap(){
		_occupedCells = new List<Cell>();
		_occupedCells.Clear();
	}
	
	public static bool Init()
	{
		Assert.Test((_instance != null),"Map.Init: Second initilization of LogicMap is not possible");
	    
		_instance = new LogicMap();
		
		return true;
	}
	
	public static LogicMap getInstance(){
		
		Assert.Test ((_instance == null),"Map.getInstance: Instance not initialized");
		
		return _instance;
	}
	
	public static bool Release()
	{
		_instance = null;
		
		return true;
	}
	
	public override void createLogicMap(Terrain terrain, int cellTam){
		
		
		
		_terrain = terrain;
		
		_posX = terrain.GetPosition().x;
		_posZ = terrain.GetPosition().z;
		
		_tamUnitsX = (int)terrain.terrainData.size.x;
		_tamUnitsZ = (int)terrain.terrainData.size.z;
		
		_tamCellsX = (int)_tamUnitsX/cellTam;
		_tamCellsZ = (int)_tamUnitsZ/cellTam;
		
		_cellTam = cellTam;
		
		//_map = new int[_tamCellsX,_tamCellsZ];
		_map = new Cell[_tamCellsX,_tamCellsZ];
		
		//Inicializar las casillas del mapa a libre
		for (int i = 0; i < _tamCellsX; i ++ ){
			for (int z = 0;z < _tamCellsZ;z++){
				_map[i,z] = new Cell(i,z,i*cellTam + cellTam / 2, z * cellTam + cellTam/2 );
			}
		}
		
	}
	
	public override void calculateCells(){
		//Calcular celdas ocupadas
		calculateCells(_posX + _tamUnitsX/2,_posZ+_tamUnitsX/2,_tamUnitsX/2);
	}
	
	private void calculateCells(float posXCenter, float posZCenter,float sideTam){
	
		int layerMask = 1 << LayerMask.NameToLayer("Terrain");
		layerMask = ~layerMask;
	
		if (Physics.CheckSphere(new Vector3(posXCenter,0,posZCenter),sideTam,layerMask))
		{
			/* 2  3
			 * 
			 * 1  4*/
		
			if ((sideTam*2) != _cellTam){
				//1
				calculateCells (posXCenter - sideTam/2,posZCenter - sideTam/2,sideTam/2);
			
				//2
				calculateCells (posXCenter - sideTam/2,posZCenter + sideTam/2,sideTam/2);
				
				//3
				calculateCells (posXCenter + sideTam/2,posZCenter + sideTam/2,sideTam/2);
				
				//4
				calculateCells (posXCenter + sideTam/2,posZCenter - sideTam/2,sideTam/2);
			
			}else{
				
				setContent((int)((posXCenter - (int)(_cellTam/2))/_cellTam),
							(int)((posZCenter - (int)(_cellTam/2))/_cellTam),
							1);
				
			}
		
		}
		
	}
	
	public override Vector3 getPosition(){
		return new Vector3(_posX,0,_posZ);
	}
	
	public override Vector2 getUnitsSize(){
		return new Vector2(_tamUnitsX,_tamUnitsZ);
	}
	
	public override Vector2 getTamCells(){
		return new Vector2(_tamCellsX,_tamCellsZ);
	}
	
	
	public override Terrain getTerrain(){
		return _terrain;
	}
	
	public void setFree(int cellX,int cellZ){
		
		Assert.Test(!existsCell(cellX,cellZ),"Map.setFree: Invalid Cell");
		
		_map[cellX,cellZ].setContent(0);
		 
	}
	
	public override bool isFree(int cellX, int cellZ){
		
		Assert.Test(!existsCell(cellX,cellZ),"Map.isFree: Invalid Cell");
		
		return (_map[cellX,cellZ].isFree());
		
	
	}
	
	public override void setContent(int cellX, int cellZ, int content){
		
		Assert.Test(!existsCell(cellX,cellZ),"Map.setContent: Invalid Cell");
		if (content != 0){
			_occupedCells.Add(_map[cellX,cellZ]);
		}
		_map[cellX,cellZ].setContent(content);
	}
	
	
	public Vector2 coordsToCell(float coordsX, float coordsZ){

		return new Vector2((int)(coordsX - _posX)/_cellTam,(int)(coordsZ - _posZ)/_cellTam);
	
	}
	
	public Vector2 cellToCoords(int cellX, int cellY){
		
		return new Vector2(cellX * _cellTam + _cellTam / 2, cellY * _cellTam + _cellTam / 2);
	}
	
	public bool existsCell(int cellX, int cellZ){
		
		return (cellX >= 0 && cellX < _tamCellsX &&
				cellZ >= 0 && cellZ < _tamCellsZ);
		
	}
	
	public Cell getCell(int cellX, int cellZ){
		Assert.Test(!existsCell(cellX,cellZ),"Map.getCell: Invalid Cell");
			
		return _map[cellX,cellZ];
	}
	
	
	
	public void print(){
		
		
	}
	
	public int getCellTam(){
		return _cellTam;
	}
	
	public List<Cell> getOccupedCells(){
		return _occupedCells;
	}
	
	public override List<ICell> getAdjacents(int cellX, int cellZ){
		Assert.Test(!existsCell(cellX,cellZ),"Map.getAdjacents: Invalid Cell");
		List<ICell> cellList = new List<ICell>();
		
		bool left = false;
		bool right = false;
		
		//Left
		if (cellX > 0){
			cellList.Add (getCell(cellX-1,cellZ));
			left = true;
		}
		
		//Right
		
		if (cellX < _tamCellsX -1 ){
			cellList.Add (getCell(cellX+1,cellZ));
			right = true;
		}
		
		//Down
		if (cellZ > 0){
			cellList.Add (getCell(cellX,cellZ-1));
			
			if (left){
				cellList.Add (getCell(cellX-1,cellZ-1));
			}
			
			if (right){
				cellList.Add (getCell(cellX+1,cellZ-1));
			}
		}
		
			
		//Up
		
		if (cellZ < _tamCellsZ - 1 ){
			cellList.Add (getCell(cellX,cellZ + 1));
			
			if (left){
				cellList.Add (getCell(cellX-1,cellZ+1));
			}
			
			if (right){
				cellList.Add (getCell(cellX+1,cellZ+1));
			}
		}
		
		
		return cellList;
	}
}
