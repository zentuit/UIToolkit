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
		
		
		//if( parent != null )
			_transformReference.position = localPosition;
		//else
		//	_transformReference.position = position;
		
		UITransform parentTransform = this;
		while( parentTransform.parent != null )
		{
			parentTransform = parentTransform.parent;
			
			if( parentTransform.parent != null )
				_transformReference.position += parentTransform.localPosition;
			else
				_transformReference.position += parentTransform.position;
			
			Debug.LogError( "parent.localPosition: " + parentTransform.localPosition + ", final position: " + _transformReference.position + ", localPosition: " + localPosition );
		}
		
		var res = _transformReference.TransformPoint( point );
		res.z = position.z;
		return res;
	}

}
