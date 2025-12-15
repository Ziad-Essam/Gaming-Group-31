using UnityEngine;

namespace InTheForest
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;

        // Update is called once per frame
        void Update()
        {
            Vector3 newPos = new Vector3(target.position.x, 0f, -10f);
            transform.position = newPos;
        }
    }

}
