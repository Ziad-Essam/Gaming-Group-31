using UnityEngine;

public class Gate : MonoBehaviour
{
    public void Open()
    {
        gameObject.SetActive(false);
    }
}