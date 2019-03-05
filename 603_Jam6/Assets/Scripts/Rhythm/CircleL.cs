using System.Collections;
using UnityEngine;


public class CircleL : MonoBehaviour {

	// Use this for initialization
	private SpriteRenderer m_sprite;
	private Vector2 velocity;
	public int num = 0;
	public float relate_pos = 0;
	public bool stimulated;
	public float alpha;
	private Vector3 defaultPos;
	void Start ()
	{
		m_sprite = gameObject.GetComponent<SpriteRenderer>();
		stimulated = false;
		relate_pos = transform.parent.transform.parent.transform.localPosition.x + transform.parent.transform.localPosition.x + transform.localPosition.x;
		defaultPos = transform.position;
		// 0.9782f
	}
	
	// Update is called once per frame
	void Update ()
	{
		alpha = m_sprite.color.a;
		//last_y += 2 * Time.deltaTime;
		if (RhythmManager.Instance.okToStart)
		{
			transform.position = new Vector3(transform.position.x + RhythmManager.Instance.speed* Time.deltaTime,transform.position.y,transform.position.z);
		}
		relate_pos = transform.parent.transform.parent.transform.localPosition.x + transform.parent.transform.localPosition.x + transform.localPosition.x;
		if (relate_pos > -0.2f && !stimulated && relate_pos < 0)
		{
			// beat hit
			stimulated = true;
			m_sprite.color = new Color(m_sprite.color.r,m_sprite.color.g,m_sprite.color.b,0);
			Beats._instance.hit = true;
            Beats._instance.clickTolerance = 1;
            Beats._instance.hitColor = new Color(m_sprite.color.r,m_sprite.color.g,m_sprite.color.b,0.7f);
			Beats._instance.OnCircleTrigger();
			StartCoroutine(hit_reset_delay());
		}else if (relate_pos <= -9.84-6.8)
		{
			m_sprite.color = new Color(m_sprite.color.r,m_sprite.color.g,m_sprite.color.b,0f);
			stimulated = false;
		}
		else if (relate_pos < -1 && relate_pos > -7.8)
		{
			m_sprite.color = new Color(m_sprite.color.r, m_sprite.color.g, m_sprite.color.b, 0.7f);
			stimulated = false;
		}

		if (relate_pos > 4.5 - 3.8 && stimulated)
		{
			stimulated = false;
		}
	}

	public void reset()
	{
		transform.position = new Vector3(defaultPos.x,defaultPos.y,defaultPos.z);
		m_sprite.color = new Color(m_sprite.color.r, m_sprite.color.g, m_sprite.color.b, 0.7f);
	}
	
	IEnumerator hit_reset_delay(){
		yield return new WaitForSeconds(0.4f);
		Beats._instance.hit = false;
	}
}
