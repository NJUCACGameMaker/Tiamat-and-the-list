using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    //移动速度
    private int moveSpeed;
    //摄像机
    private GameObject camera;

    // Use this for initialization
    void Start () {
        InputManager.AddOnLeftMove(LeftMove);
        InputManager.AddOnRightMove(RightMove);
        camera = GameObject.Find("Main Camera");
        moveSpeed = 8;
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void LeftMove()
    {
        float playerX = transform.localPosition.x;
        GameObject bg_1 = GameObject.Find("bg-1");
        float bg_1_x = bg_1.transform.localPosition.x;
        if (playerX >= bg_1_x)
        {
            transform.Translate(Time.deltaTime * Vector3.left * moveSpeed, Space.World);

            Vector3 targetCamPos = new Vector3((transform.position).x, 0, 0);

            if(camera.transform.position.x - 5.3/2.0 >=bg_1_x)
                // 给摄像机移动到应该在的位置的过程中加上延迟效果
                camera.transform.position = new Vector3(Vector3.Lerp(new Vector3(camera.transform.position.x, 0, 0), targetCamPos, moveSpeed * Time.deltaTime/2).x, camera.transform.position.y, camera.transform.position.z);
        }
        
    }

    void RightMove()
    {
        float playerX = transform.localPosition.x;
        GameObject bg_1 = GameObject.Find("bg-1 (23)");
        float bg_1_x = bg_1.transform.localPosition.x;
        if (playerX <= bg_1_x)
        {
            transform.Translate(Time.deltaTime * Vector3.right * moveSpeed, Space.World);

            Vector3 targetCamPos = new Vector3((transform.position).x,0,0);

            if (camera.transform.position.x + 5.3/2.0 <= bg_1_x)
                // 给摄像机移动到应该在的位置的过程中加上延迟效果
                camera.transform.position = new Vector3(Vector3.Lerp(new Vector3(camera.transform.position.x,0,0), targetCamPos, moveSpeed * Time.deltaTime/2).x,camera.transform.position.y, camera.transform.position.z);
        }
        
    }
}
