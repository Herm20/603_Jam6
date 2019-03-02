using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;
using File = System.IO.File;

[Serializable] 
public class circleBuffer
{
	public string distance;
	public string index;
}
[Serializable] public class Wrapper
{
	public float TotalTime;
	public beatBuffer[] Items;
}

[Serializable] 
public class beatBuffer {
	public string time;
	public string type;
	public string num;
}

public class RhythmManager : MonoBehaviour
{
	private string path;
	public Color[] colors;
	private bool recorded = false;
	public static RhythmManager Instance;		
	public bool okToStart;
	public GameObject CircleL;
	public GameObject CircleR;
	public GameObject moverL;
	public GameObject moverR;
	public 	float speed = 2f;//TODO: change the hard code here
	private float defaultX; // record the default mover position;
	private int AudioLength;
	private AudioSource player;
	[Header("Use Default Music")]
	public string filename;
	// Use this for initialization
	void Awake()
	{
		path = Application.dataPath;
		okToStart = false;
		Instance = this;
		defaultX = transform.position.x;
		player = GetComponent<AudioSource>();
		//print("Screen height: "+Screen.height);
		RhythmLoad(ReadyToPlay);
	}

	public void ReadyToPlay()
	{
		// trigger when loading finish
		StartCoroutine(play(player));
	}
	
	public void setSpeed(float _speed)
	{
		speed = _speed;
	}

	public void RhythmLoad(System.Action callback)
	{

		path = Application.dataPath+"/RhythmJson/" + filename + ".json";
		StartCoroutine(dataLoadProcess(callback));
		
		if (File.Exists(path))
		{
			Debug.Log("Rhythm Data Load Start..");
			
		}
		else
		{
			Debug.Log("Fail to load File, Not exist");
			okToStart = true;
			if (callback != null)
			{
				callback.Invoke();
			}
		}
	}
	
	private IEnumerator dataLoadProcess(System.Action callback)
	{
		CircleL[] circleListL = GameObject.FindObjectsOfType<CircleL>();
		CircleR[] circleListR = GameObject.FindObjectsOfType<CircleR>();
		resetPos();
		foreach (CircleL circle in circleListL)
		{
			Destroy(circle);
		}
		foreach (CircleR circle in circleListR)
		{
			Destroy(circle);
		}
		string dataAsJson = File.ReadAllText(path); 
		if (dataAsJson != null)
		{
			Wrapper wrapper = JsonUtility.FromJson<Wrapper>(dataAsJson);
			beatBuffer[] circles = wrapper.Items;
			AudioLength = circles.Length;
			foreach (beatBuffer circleBuffer in circles)
			{
				float time = float.Parse(circleBuffer.time);
				Vector3 circlePosition1 = new Vector3(transform.position.x - time * speed,transform.localPosition.y,transform.localPosition.z);
				GameObject temp = Instantiate(CircleL, circlePosition1, transform.rotation, moverL.transform);
				Vector3 circlePosition2 = new Vector3(transform.position.x + time * speed,transform.localPosition.y,transform.localPosition.z);
				GameObject temp1 = Instantiate(CircleR, circlePosition2, transform.rotation, moverR.transform);
				int colorIndex = Random.Range(0, colors.Length);
				temp.GetComponent<SpriteRenderer>().color = colors[colorIndex];
				temp1.GetComponent<SpriteRenderer>().color = colors[colorIndex];
			}
		}
		Debug.Log("Rhythm Data Load Finished..");
		if (callback != null)
		{
			callback.Invoke();
		}
		yield return null;
	}

	public void Begin()
	{
		okToStart = true;
	}
	
	public void End()
	{
		okToStart = false;
	}
	
	public void resetPos()
	{
		moverL.transform.position = new Vector3(defaultX,0,0);
		moverR.transform.position = new Vector3(defaultX,0,0);
	}
	
	void audioFinished()
	{
		//reset audio
		CircleL[] listL = FindObjectsOfType<CircleL>();
		CircleR[] listR = FindObjectsOfType<CircleR>();
		foreach (var circle in listL)
		{
			circle.reset();
		}
		foreach (var circle in listR)
		{
			circle.reset();
		}
		player.time = 0f;
		player.Play();
		Invoke(nameof(audioFinished),player.clip.length);
	}

	IEnumerator play(AudioSource player){
		
		Begin();
		//set a bit of delay to make the rhythm sync with visual effect
		yield return new WaitForSeconds(0.1f);
		player.Play();
		Invoke(nameof(audioFinished),player.clip.length);
	}
}
