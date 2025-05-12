using System;
using Game.GameRoot;
using UnityEngine;

namespace Game.GameUI.Root
{
    public class ControllCamera: MonoBehaviour, IInjectable
    {
        [SerializeField] private Camera cam;
        [SerializeField] private float speed = 0.1f;
        [SerializeField] private float minZoom = 30f;
        [SerializeField] private float maxZoom = 60f;
        private float fieldOfView;
        private float zoomVelocity = 0f;
        private TouchMode currentMode = TouchMode.None;
        public int Width;
        public int Height;
        private float lastZoomTime;
        private float touchCoolDown = 0.15f;
        private Vector3 target;

        public void Inject(DIContainer container)
        {
            var enterParams = container.Resolve<GameUIEnterParams>();
            Width = enterParams.Width;
            Height = enterParams.Height;
            target = new Vector3((Width - 1) / 2f, 0, (Height - 1) / 2f);
            fieldOfView = cam.fieldOfView;
            cam.transform.LookAt(target);
        }

        private enum TouchMode
        {
            None,
            Swipe,
            Zoom
        }

        private void LateUpdate()
        {
            switch (Input.touchCount)
            {
                case 1:
                    if (Time.time - lastZoomTime > touchCoolDown)
                    {
                        currentMode = TouchMode.Swipe;
                        HandleSwip(Input.GetTouch(0));
                    } 
                    break;
                case 2:
                    currentMode = TouchMode.Zoom;
                    lastZoomTime = Time.time;
                    HandleZoom(Input.GetTouch(0), Input.GetTouch(1));
                    break;
                default:
                    currentMode = TouchMode.None;
                    break;
            }
        }

        private void HandleSwip(Touch touch)
        {
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 touchPos = touch.deltaPosition;
                float rotation = touchPos.x * speed;
                transform.RotateAround(target, Vector3.up, rotation);
            }
        }
        private void HandleZoom(Touch touch0, Touch touch1)
        {
            // Считаем расстояние между пальцами в этом кадре и предыдущем
            Vector2 prevTouch0 = touch0.position - touch0.deltaPosition;
            Vector2 prevTouch1 = touch1.position - touch1.deltaPosition;

            float prevMagnitude = Vector2.Distance(prevTouch0, prevTouch1);
            float currentMagnitude = Vector2.Distance(touch0.position, touch1.position);

            float difference = currentMagnitude - prevMagnitude;

            fieldOfView -= difference * speed;
            fieldOfView = Mathf.Clamp(fieldOfView, minZoom, maxZoom);
            cam.fieldOfView = Mathf.SmoothDamp(cam.fieldOfView, fieldOfView, ref zoomVelocity, 0.1f);
        }
    }
}