using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InKinWalk : MonoBehaviour {

	public Transform legUp;
	public Transform legLow;
	public Transform ankle;
	public Transform knee;
	public Transform target;
	
	public Vector3 legUp_OffsetRotation;
	public Vector3 legLow_OffsetRotation;
	public Vector3 ankle_OffsetRotation;

	public bool ankleMatchesTargetRotation = true;

	public bool debug;

	float angle;
	float legUp_Length;
	float legLow_Length;
	float leg_Length;
	float targetDistance;
	float adyacent;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void LateUpdate () {
		if(legUp != null && legLow != null && ankle != null && knee != null && target != null){
			legUp.LookAt (target, knee.position - legUp.position);
			legUp.Rotate (legUp_OffsetRotation);

			Vector3 cross = Vector3.Cross (knee.position - legUp.position, legLow.position - legUp.position);



			legUp_Length = Vector3.Distance (legUp.position, legLow.position);
			legLow_Length =  Vector3.Distance (legLow.position, ankle.position);
			leg_Length = legUp_Length + legLow_Length;
			targetDistance = Vector3.Distance (legUp.position, target.position);
			targetDistance = Mathf.Min (targetDistance, leg_Length - leg_Length * 0.001f);

			adyacent = ((legUp_Length * legUp_Length) - (legLow_Length * legLow_Length) + (targetDistance * targetDistance)) / (2*targetDistance);

			angle = Mathf.Acos (adyacent / legUp_Length) * Mathf.Rad2Deg;

			legUp.RotateAround (legUp.position, cross, -angle);

			legLow.LookAt(target, cross);
			legLow.Rotate (legLow_OffsetRotation);

			if(ankleMatchesTargetRotation){
				ankle.rotation = target.rotation;
				ankle.Rotate (ankle_OffsetRotation);
			}
			
			if(debug){
				if (legLow != null && knee != null) {
					Debug.DrawLine (legLow.position, knee.position, Color.blue);
				}

				if (legUp != null && target != null) {
					Debug.DrawLine (legUp.position, target.position, Color.red);
				}
			}
					
		}
		
	}

	void OnDrawGizmos(){
		if (debug) {
			if(legUp != null && knee != null && ankle != null && target != null && knee != null){
				Gizmos.color = Color.gray;
				Gizmos.DrawLine (legUp.position, legLow.position);
				Gizmos.DrawLine (legLow.position, ankle.position);
				Gizmos.color = Color.red;
				Gizmos.DrawLine (legUp.position, target.position);
				Gizmos.color = Color.blue;
				Gizmos.DrawLine (legLow.position, knee.position);
			}
		}
	}

}
