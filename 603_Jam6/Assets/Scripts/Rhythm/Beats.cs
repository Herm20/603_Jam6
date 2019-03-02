using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Beats : MonoBehaviour {

	public int clickToleranceInterval;
	public bool inBeat = false;
	public bool halfMiss = false;
	public bool hit; // the flag for circle hit
	public Color hitColor; // the flag for circle hit
	//public Transform m_SpriteRenderer_Child;
	public int clickTolerance;
	public static Beats _instance;
	private bool flipped = false;
	[SerializeField]private Animator anim;
	[SerializeField]private GameObject ring;

	private int counter; // temp use
	void Start()
	{
		_instance = this;
		counter = 0;
	}

	void Update()
	{

		// to check the beat is on or not. call Beats._instance.inBeat. if true, then peak
		if (clickTolerance > 0 && clickTolerance < clickToleranceInterval)
		{
			clickTolerance++;
			inBeat = true;
			if (clickTolerance > clickToleranceInterval - 5)
			{
				// nearly hit
				halfMiss = true;
			}
			else
			{
				halfMiss = false;
			}
		}
		else if (clickTolerance >= clickToleranceInterval)
		{
			clickTolerance = 0;
			inBeat = false;
		}
	}


	public void OnCircleTrigger()
	{
		// trigger every time circle hits
		counter++;
		if (counter % 2 == 0)
		{
			anim.Play("SimpleaAbsorb",-1,0);
		}
		else
		{
			anim.Play("SimpleaAbsorbV",-1,0);
		}


		anim.gameObject.GetComponent<SpriteRenderer>().color = hitColor;
		Destroy(Instantiate(ring,transform.position,transform.rotation),0.5f);
	}

}
