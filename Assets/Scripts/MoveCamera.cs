// Credit to damien_oconnell from http://forum.unity3d.com/threads/39513-Click-drag-camera-movement
// for using the mouse displacement for calculating the amount of camera movement and panning code.

using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class MoveCamera : MonoBehaviour
{
	// User Controlled Change
	public float MoveRate;
	public float TurnRate = 90f;

	// Update Change Rate
	public float InternalTurnRate = 80f;
	public float InternalMoveRate = 5f;

	// Camera Rotation
	public float Yaw = 0;
	public float Pitch = 0;
	// Enforced View Limits
	public float PitchMax = 80;
	public float PitchMin = -80;

	private float hAxis;
    private float vAxis;
    private float adAxis;
    private float auAxis;
	private float htAxis;
	private float vtAxis;

	// Target Position
	private Quaternion tRotation;
	private Vector3 tPosition;

	void Update()
    {
        htAxis = Input.GetAxis("HorizontalCam");
        vtAxis = -Input.GetAxis("VerticalCam");
        adAxis = -Input.GetAxis("AltitudeDown");
        auAxis = Input.GetAxis("AltitudeUp");
		hAxis = Input.GetAxis("Horizontal");
		vAxis = Input.GetAxis("Vertical");

		float aAxis = adAxis + auAxis;

		// Compute Yaw/Pitch for the current update
		Yaw += htAxis * TurnRate * Time.deltaTime;
		Pitch += vtAxis * TurnRate * Time.deltaTime;

		// Compute Position Change
		tPosition = transform.position + MoveRate * Time.deltaTime * (
			transform.right * hAxis +
			transform.forward * vAxis +
			Vector3.up * aAxis);

		// Adjust Pitch/Yaw and compute Quaternion
		Pitch = Mathf.Clamp(Pitch, PitchMin, PitchMax);
		//		Yaw = Utils.LimitAngles(Yaw);
		tRotation = Quaternion.Euler(-Pitch, Yaw, 0f);

		// Move/Rotate Camera Rig to computed target position/rotation
		//		transform.rotation = Quaternion.RotateTowards(transform.rotation, tRotation, InternalTurnRate * Time.deltaTime);
		//		transform.position = Vector3.Lerp(transform.position, tPosition, InternalMoveRate * Time.deltaTime);

		transform.rotation = tRotation;
		transform.position = tPosition;

	}
}