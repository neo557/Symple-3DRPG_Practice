using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float distance = 7f;
    public float height = 2.5f;
    public float mouseSensitivity = 7f;

    public float minPitch = -20f;
    public float maxPitch = 60f;

    private float yaw;
    private float pitch;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (target == null)
        {
            Debug.LogError("CameraTPS; Target not set");
            enabled = false;
            return;
        }

        //初期角度
        yaw = transform.eulerAngles.y;
        pitch = 20f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        //マウス入力
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity * 100f * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity * 100f * Time.deltaTime;

        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        //回転
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        //位置計算
        Vector3 offset = rotation * new Vector3(0, height, -distance);
        transform.position = target.position + offset;

        //lookPlayer
        transform.LookAt(target.position + Vector3.up * height);
    }
}
