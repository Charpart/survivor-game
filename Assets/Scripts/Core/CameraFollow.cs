using UnityEngine;

namespace Core
{
    public class CameraFollow : MonoBehaviour
    {
        private Transform _target;

        public float distance = 6f;
        public float height = 3f;

        public float rotationX = 20f;
        public float rotationY = 0f;

        public float positionSmooth = 5f;
        public float rotationSmooth = 5f;

        private void Start()
        {
            if (!_target) return;
            ForceFollow();
        }
        
        public void SetTarget(Transform target)
        {
            _target = target;
            ForceFollow();
        }

        private void ForceFollow()
        {
            transform.position = 
                _target.position 
                + Quaternion.Euler(rotationX, rotationY, 0f) 
                * new Vector3(0f, height, -distance);
            
            transform.rotation = Quaternion.LookRotation(_target.position - transform.position);
        }

        void LateUpdate()
        {
            if (!_target) return;

            Vector3 offset = new Vector3(0f, height, -distance);
            Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0f);
            Vector3 desiredPosition = _target.position + rotation * offset;

            var deltaTime = Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, positionSmooth * deltaTime);

            Quaternion lookRotation = Quaternion.LookRotation(_target.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSmooth * deltaTime);
        }
    }
}