public class Empleado{

	private string _nombre;
	private string _DNI;

	
	public Empleado(string nombre, string DNI){
		_nombre = nombre;
		_DNI = DNI;
	}
	
	public void setNombre(string nombre){
		_nombre = nombre;
	}
	
	public string getNombre(){
		return _nombre;
	}
	
	public void setDNI(string DNI){
		_DNI = DNI;
	}
	
	public string getDNI(){
		return _DNI;
	}
	

}

