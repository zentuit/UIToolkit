using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class UILayoutContainer : UIVerticalLayout
{	
	
	protected List<UIAbstractContainer> _layouts = new List<UIAbstractContainer>();
	
	public UILayoutContainer( int spacing ) : base( spacing )
	{
	}
	
	public void addLayout(params UIAbstractContainer[] children) {
			Debug.Log("layout count "+children.Length);
		Debug.Log(children[0]);
		Debug.Log(children[1]);
		beginUpdates();
		foreach (UIAbstractContainer item in children) {
			Debug.Log("adding item: "+item);
			item.parentUIObject = this;
			_layouts.Add(item);
		}
		endUpdates();
	}
	
	/// <summary>
	/// Responsible for laying out the child Layouts who will in turn 
	/// layout their child sprites
	/// </summary>
	protected override void layoutChildren()
	{
		Debug.Log("In UILayoutContainer's layoutChildren");
		if( _suspendUpdates )
			return;
		
		var i = 0;
		var lastIndex = _layouts.Count;
		foreach( var item in _layouts )
		{
			// we add spacing for all but the first and last
			if( i != 0 && i != lastIndex )
				_height += _spacing;
			
			// layouts don't have gameObjectOriginInCenter method
			var xPos = 0;
			var yPosModifier = 0;
			
			item.localPosition = new Vector3( _edgeInsets.left + xPos, -( _height + yPosModifier ), item.position.z );

			// all items get their height added
			_height += item.height;
			
			// width will just be the width of the widest item
			if( _width < item.width )
				_width = item.width;
			
			i++;
		}
		
		// add the right and bottom edge inset to finish things off
		_width += _edgeInsets.right;
		_height += _edgeInsets.bottom;
		
	}

	
}
