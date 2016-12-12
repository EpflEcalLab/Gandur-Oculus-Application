using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

//This class animates a gameobject along the spline at a specific speed.
public class LightSplineAnimator : SplineAnimator
{
	
	public override void FixedUpdate( ) 
	{
		passedTime += Time.deltaTime * speed;
		
		transform.position = spline.GetPositionOnSpline( WrapValue( passedTime + offSet, 0f, 1f, wrapMode ) );
	}
	 
}
