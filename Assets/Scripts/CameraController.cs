using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   public Transform player;

   Vector3 target, mousePos, refVel, shakeOffset;
    float cameraDist = 3.5f;
    [SerializeField] float smoothTime = 0.0f, zStart;
    //shake
    float shakeMag,shakeTimeEnd;

    Vector3 shakeVector;
    bool shaking;
    void Start() {
        target = player.position;
        zStart = transform.position.z;
    }
    void Update() {
        mousePos = CaptureMousePos();
        target = UpdateTargetPos();
        UpdateCameraPosition();
    }
    Vector3 CaptureMousePos() {
        Vector2 ret = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        ret *= 2;
        ret -= Vector2.one;
        float max = 0.9f;
        if (Mathf.Abs(ret.x) > max || Mathf.Abs(ret.y) > max) {
            ret = ret.normalized;
        }
        return ret;
    }
    Vector3 UpdateTargetPos() {
        Vector3 mouseOffset = mousePos * cameraDist;
        Vector3 ret = player.position + mouseOffset;
        ret.z = zStart;
        return ret;
    }
    void UpdateCameraPosition() {
        Vector3 tempPos;
        tempPos = Vector3.SmoothDamp(transform.position, target,
                                    ref refVel, smoothTime);
        transform.position = tempPos;
    }
    public void Shake(Vector3 direction, float magnitude, float length) {
        shaking = true;
        shakeVector = direction;
        shakeMag = magnitude;
        shakeTimeEnd = Time.time + length;
    }
}
