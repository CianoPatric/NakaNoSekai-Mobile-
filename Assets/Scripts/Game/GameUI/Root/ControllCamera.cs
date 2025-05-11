using System;
using UnityEngine;

namespace Game.GameUI.Root
{
    public class ControllCamera: MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private float zoomSpeed = 0.1f;
        [SerializeField] private float minZoom = 3f;
        [SerializeField] private float maxZoom = 15f;
        private float targetZoom;
        private float zoomVelocity = 0f;

        private void Awake()
        {
            cam.transform.LookAt(new Vector3(3, 0, 3));
            targetZoom = cam.fieldOfView;
        }

        private void LateUpdate()
        {
            if (Input.touchCount == 2)
            {
                Touch touch0 = Input.GetTouch(0);
                Touch touch1 = Input.GetTouch(1);

                // Считаем расстояние между пальцами в этом кадре и предыдущем
                Vector2 prevTouch0 = touch0.position - touch0.deltaPosition;
                Vector2 prevTouch1 = touch1.position - touch1.deltaPosition;

                float prevMagnitude = (prevTouch0 - prevTouch1).magnitude;
                float currentMagnitude = (touch0.position - touch1.position).magnitude;

                float difference = prevMagnitude - currentMagnitude;

                targetZoom += difference * zoomSpeed;
                targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
            }
            cam.fieldOfView = Mathf.SmoothDamp(cam.fieldOfView, targetZoom, ref zoomVelocity, 0.1f);
        }
    }
}