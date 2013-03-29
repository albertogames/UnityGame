using UnityEngine;
using System.Collections.Generic;

public abstract class ILogicMap {
	
	//Las usa la clase IAMap para poder crear el mapa de IA
	public abstract Vector2 getTamCells();
	
	
	//Lo usa la clase TerrainLoader para la creación del mapa lógico a partir del terreno
	public abstract void createLogicMap(Terrain terrain,int cellTam);
	public abstract void setContent(int cellX, int cellZ, int content);
	public abstract Vector3 getPosition();
	public abstract Vector2 getUnitsSize();
	public abstract Terrain getTerrain();
	public abstract void calculateCells();
	
	//Las usa el servidor de IA para calcular las rutas del A*
	public abstract bool isFree(int cellX, int cellZ);
	public abstract List<ICell> getAdjacents(int cellX, int cellY);	
	

}
