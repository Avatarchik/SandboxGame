using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AssaultRifleView : MonoBehaviour {
	//组件
	private Transform m_Transform;
	private Animator m_Animator;
	private Camera m_EnvCamera;

	//优化开镜动作
	private Vector3 startPos;
	private Vector3 startRot;
	private Vector3 endPos;
	private Vector3 endRot;

	public Transform M_Transform {
		get { return m_Transform; }
	}

	public Animator M_Animator {
		get { return m_Animator; }
	}

	public Camera M_EnvCamera {
		get { return m_EnvCamera; }
	}

	void Awake () {
		m_Transform = gameObject.GetComponent<Transform> ();
		m_Animator = gameObject.GetComponent<Animator> ();
		m_EnvCamera = GameObject.Find ("EnvCamera").GetComponent<Camera> ();
		//优化开镜
		startPos = m_Transform.localPosition;
		startRot = m_Transform.localRotation.eulerAngles;
		endPos = new Vector3 (-0.065f, -1.85f, 0.25f);
		endRot = new Vector3 (2.8f, 1.3f, 0.08f);
	}

	//进入开镜--动作优化
	public void EnterHoldPos () {
		m_Transform.DOLocalMove (endPos, 0.2f);
		m_Transform.DOLocalRotate (endRot, 0.2f);
		m_EnvCamera.DOFieldOfView (40, 0.2f);
	}

	//退出开镜--动作优化
	public void ExitHoldPose () {
		m_Transform.DOLocalMove (startPos, 0.2f);
		m_Transform.DOLocalRotate (startRot, 0.2f);
		m_EnvCamera.DOFieldOfView (60, 0.2f);
	}
}