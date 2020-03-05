using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private Camera cam;
    private LayerMask chefMask;
    private int selectDistance = 40;
    [SerializeField]
    private float camSpeed = 0.2f;
    private GameObject chefSelected = null;

    Vector2 camRot;

     [SerializeField]
    private float turnSpeed = 3f;

    private bool rotateCam = false;

    private void Awake () {
        cam = GetComponent<Camera>();
        chefMask = 1 << LayerMask.NameToLayer("Chef");
    }


    private void Update () {
        ChefSelect();
        CamMovement();
    }


    private void CamMovement () {
        Transform camTrans = cam.transform;
        Vector3 cameraMov = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
        cameraMov.Normalize();
        cameraMov *= camSpeed * Time.deltaTime;
        cam.transform.position += cameraMov;
        if (Input.GetMouseButtonDown(1)) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            rotateCam = true;
        }
        if (Input.GetMouseButtonUp(1)) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            rotateCam = false;
        }
        if (rotateCam) {
            camRot.x -= Input.GetAxisRaw("Mouse Y") * turnSpeed;
            camRot.y += Input.GetAxisRaw("Mouse X") * turnSpeed;
            camTrans.rotation = Quaternion.Euler(camRot.x, camRot.y, 0);
        }
        if (Input.GetKey(KeyCode.E)) {
            camTrans.position += (transform.up * camSpeed) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Q)) {
            camTrans.position -= (transform.up * camSpeed) * Time.deltaTime;

        }
    }

    private void ChefSelect () {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = cam.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out RaycastHit hit, selectDistance, chefMask)) {

                Debug.DrawLine(ray.origin, hit.point);
                //log hit area to the console
                Debug.Log(hit.point);
                Debug.Log(hit.transform.name);

                chefSelected = hit.transform.gameObject;
            } else {
                chefSelected = null;
            }
        }
    }
}
