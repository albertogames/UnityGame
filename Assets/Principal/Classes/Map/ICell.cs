using UnityEngine;
using System.Collections;

public abstract class ICell {

	public abstract bool isFree();
	public abstract int getPosX();
	public abstract int getPosZ();
}
