using UnityEngine;

namespace InTheForest
{
    public class Parallax : MonoBehaviour
    {
        public Camera cam;
        public Transform subject;
        Material mat;

        public float unitsPerTile = 3.2f;

        Vector2 camStartPosition;
        float startZ;

        Vector2 travel => (Vector2)cam.transform.position - camStartPosition;

        float distanceFromSubject => transform.position.z - subject.position.z;
        float clippingPlane => (cam.transform.position.z + (distanceFromSubject > 0 ? cam.farClipPlane : cam.nearClipPlane));
        float parallaxFactor => 1 - Mathf.Abs(distanceFromSubject) / clippingPlane;


        public void Start()
        {
            camStartPosition = cam.transform.position;
            startZ = transform.position.z;
            mat = GetComponent<Renderer>().material;
        }

        public void LateUpdate()
        {
            Vector2 newPos = travel * parallaxFactor;
            transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, startZ);

            Vector2 textureOffset = newPos / unitsPerTile;

            mat.SetTextureOffset("_MainTex", new Vector2(textureOffset.x, 0));
        }
    }

}
