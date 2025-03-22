using UnityEngine;

namespace JMT.Object
{
	public interface ISpawnable
	{
		GameObject Spawn(Vector3 position);
	}
}