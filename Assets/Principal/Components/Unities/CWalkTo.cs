using UnityEngine;
using System.Collections;

public class CWalkTo : MonoBehaviour {
	
	public float _velocity;
	public float _gap;
	
	private bool _walkTo;
	private Vector2 _destination;

	
	// Use this for initialization
	void Start () {
		_walkTo = false;
	}
	
	// Update is called once per frame
	void Update () {
		//Si se está moviendo a algún sitio
		if (_walkTo){
			
			
			Vector3 actualPosition = gameObject.transform.position;
			
			if ( Mathf.Sqrt(Mathf.Pow((actualPosition.x - _destination.x),2) + Mathf.Pow((actualPosition.z - _destination.y),2)) < _gap){
			
				//si ha llegado al destino
				_walkTo = false;
				
				//Indicar que se ha llegado al destino
				gameObject.SendMessage("arrived");
			
			}else{
				//si no ha llegado al destino
				//Obtener vector dirección
				Vector2 direction = new Vector2(_destination.x - actualPosition.x, _destination.y - actualPosition.z);
				direction.Normalize();
				direction = direction * _velocity * Time.deltaTime;
				//Mover la entidad en base al tiempo pasado
				gameObject.transform.position = new Vector3(actualPosition.x + direction.x, actualPosition.y, actualPosition.z + direction.y);

			}
			
			
		}
	}
	
	private void walkTo(Vector2 destination){
		_destination = destination;
		_walkTo = true;
	}
	
	private void stopWalkTo(){
		_walkTo = false;
	}
	
	
}
