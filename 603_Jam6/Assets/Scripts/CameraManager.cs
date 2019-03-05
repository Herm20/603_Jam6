using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	// Use this for initialization
	public float smoothTimeY;
	public float smoothTimeX;

	public bool bounds;
	public Vector3 minCameraPos;
	public Vector3 maxCameraPos;

	private Vector2 velocity;
	public  GameObject character;
	private bool shaking;
	private bool locked;
	private float shakeAmount = 0;
	
	public static CameraManager _instance;
	private float shake_magnitude;
	void Start ()
	{
		_instance = this;
		shaking = false;
		locked = false;
	}
     
	
	void FixedUpdate()
	{
		if (!locked)
		{
			float x = 0;
			float y = 0;
			if (shaking)
			{
				x = Random.Range(-1f, 1f) * shake_magnitude;
				y = Random.Range(-1f, 1f) * shake_magnitude;

			}
			float posy = Mathf.SmoothDamp(transform.position.y, character.transform.position.y, ref velocity.y,
				smoothTimeY);
			float posx = Mathf.SmoothDamp(transform.position.x, character.transform.position.x, ref velocity.x,
				smoothTimeX * 2);
			transform.position = new Vector3(posx+x, posy+y, transform.position.z);
			if (bounds)
			{
				transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x)+x,
					Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y)+y,
					transform.position.z
				);
			}
		}
		else
		{
			float posy = Mathf.SmoothDamp(transform.position.y, character.transform.position.y, ref velocity.y,
				smoothTimeY/2);
			float posx = Mathf.SmoothDamp(transform.position.x, character.transform.position.x, ref velocity.x,
				smoothTimeX/2);
			transform.position = new Vector3(posx, posy, transform.position.z);
		}

	}
	
	public void Shake(float duration, float magnitude)
	{
		//StartCoroutine(Shake_event(duration, magnitude));
		shaking = true;
		shake_magnitude = magnitude;
		StartCoroutine(Shake_delay(duration));
	}
	
	public void setLock(bool val)
	{
		locked = val;
	}

	IEnumerator Shake_delay(float duration)
	{
		yield return new WaitForSeconds(duration);
		shaking = false;
	}
}
