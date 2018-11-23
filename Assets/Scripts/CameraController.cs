using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject Player;   //引用游戏角色物体

    private Vector3 offset;    //存放偏移值

	// Use this for initialization
	void Start () {
        offset = transform.position - Player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {    //LateUpdate在其内部代码执行完毕后再执行
        transform.position = Player.transform.position + offset;
	}
}
