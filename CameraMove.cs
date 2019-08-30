using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    public MasterManager masterManager;

    public Transform FollowPlayer;

   // public bool PlayerWork;

    public float dist = 4f;

    public float xSpeed = 220.0f;
    public float ySpeed = 100.0f;

    public float x = 0.0f;
    public float y = 0.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public bool BuildStart;

    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }

    void Start()
    {
        Vector3 angles = transform.eulerAngles;

        x = angles.y;
        y = angles.x;
    }

    void Update()
    {
        if (!BuildStart && !masterManager.PlayerCheck.isWithFire && !masterManager.PlayerCheck.isWithHaze && !masterManager.PlayerCheck.isPaused && TutorialManager.EndTutorial)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            x += Input.GetAxis("Mouse X") * xSpeed * 0.015f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.015f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0, 0.9f, -dist) + FollowPlayer.position + new Vector3(0f, 0f, 0f);

            transform.rotation = rotation;
            transform.position = position;
        }
    }
  
}
