using UnityEngine;
using System.Collections;


public class UITransform
{
	private static Transform _transformReference;
	
	public Vector3 position;
	public Vector3 localPosition;
	public Vector3 eulerAngles;
	public Vector3 localScale = new Vector3( 1, 1, 1 );
	public Quaternion rotation = Quaternion.identity;
	
	public UITransform parent;
	
	
	static UITransform()
	{
		var go = new GameObject();
		_transformReference = go.transform;
	}

	
	public Vector3 TransformPoint( Vector3 point )
	{
		_transformReference.position = position;
		_transformReference.localScale = localScale;
		_transformReference.rotation = rotation;
		
		if( parent != null )
		{
			_transformReference.localPosition = parent.position + position;
		}
		
		return _transformReference.TransformPoint( point );
	}

}
