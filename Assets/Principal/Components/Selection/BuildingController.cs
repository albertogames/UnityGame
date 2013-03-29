using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingController : MonoBehaviour {
	
	private bool _building;
	private Vector3 _firstPoint;
	private Vector3 _secondPoint;
	
	
	public GameObject _greenTile;
	public GameObject _redtile;
	
	public float _basePlaneSize;
	
	private List<GameObject> _tileList;
	
	// Use this for initialization
	void Start () {
		
		_tileList = new List<GameObject>();
		
		_building = false;
		
		_greenTile.transform.localScale = new Vector3(1,1,1);
		
		_redtile.transform.localScale = new Vector3(1,1,1);
		
		_greenTile.transform.localScale = new Vector3(_greenTile.transform.localScale.x * LogicMap.getInstance().getCellTam() / _basePlaneSize*0.75F,
			_greenTile.transform.localScale.y,
			_greenTile.transform.localScale.z * LogicMap.getInstance().getCellTam() / _basePlaneSize*0.75F);
		
		_redtile.transform.localScale = new Vector3(_redtile.transform.localScale.x * LogicMap.getInstance().getCellTam() / _basePlaneSize*0.75F,
			_redtile.transform.localScale.y,
			_redtile.transform.localScale.z * LogicMap.getInstance().getCellTam() / _basePlaneSize*0.75F);

	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetMouseButtonDown(0)){
			
			_building = true;	
				
			PhysicServer.getInstance().raycastFromViewPort(out _firstPoint);
		
		}
		
		if (Input.GetMouseButtonUp(0)){
			
			_building = false;
			
			/*foreach (GameObject obj in _tileList){
				Destroy(obj);
			}
			
			_tileList = new List<GameObject>();*/
		}
		
		if (_building){
			
			foreach (GameObject obj in _tileList){
				Destroy(obj);
			}
			
			_tileList = new List<GameObject>();
			
			PhysicServer.getInstance().raycastFromViewPort(out _secondPoint);
			
			Vector2 firstCell = LogicMap.getInstance().coordsToCell(_firstPoint.x, _firstPoint.z);
			
			Vector2 secondCell = LogicMap.getInstance().coordsToCell(_secondPoint.x, _secondPoint.z);
		
			int auxX = 0;
			int auxY = 0;
			
			if (secondCell.x > firstCell.x){
				auxX = 1;
				
			}else{
				auxX = -1;
			}
			
			if (secondCell.y > firstCell.y){
				auxY = 1;
			}else{
				auxY = -1;
			}
			
			for (int i = (int) firstCell.x; (auxX == 1) ? i <= secondCell.x: i >= secondCell.x; i+= auxX){
				
				Cell auxCell = LogicMap.getInstance().getCell(i,(int) firstCell.y);
	
		
				_tileList.Add((GameObject)Instantiate(
					auxCell.isFree() ? _greenTile : _redtile,
					new Vector3(auxCell.getCoordsX(), 0.1F,auxCell.getCoordsZ()),
					_greenTile.transform.rotation));
			
				auxCell = LogicMap.getInstance().getCell(i,(int) secondCell.y);
				
				_tileList.Add((GameObject)Instantiate(
					auxCell.isFree() ? _greenTile : _redtile,
					new Vector3(auxCell.getCoordsX(), 0.1F,auxCell.getCoordsZ()),
					_greenTile.transform.rotation));
			
			}
			
			
			
			for (int i = (int) firstCell.y;(auxY == 1) ? i <= secondCell.y: i >= secondCell.y; i+=auxY){
				
				Cell auxCell = LogicMap.getInstance().getCell((int) firstCell.x,i);
	
				_tileList.Add((GameObject)Instantiate(
					auxCell.isFree() ? _greenTile : _redtile,
					new Vector3(auxCell.getCoordsX(), 0.1F,auxCell.getCoordsZ()),
					_greenTile.transform.rotation));
			
				auxCell = LogicMap.getInstance().getCell((int) secondCell.x,i);
				
				_tileList.Add((GameObject)Instantiate(
					auxCell.isFree() ? _greenTile : _redtile,
					new Vector3(auxCell.getCoordsX(), 0.1F,auxCell.getCoordsZ()),
					_greenTile.transform.rotation));
				
			}

			
		}
		
		
	}
	
	
}
