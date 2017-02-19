using UnityEngine;
public interface Weapon 
{
	void Fire (Vector3 direction);
	void AddAmmo (int amt);
}
