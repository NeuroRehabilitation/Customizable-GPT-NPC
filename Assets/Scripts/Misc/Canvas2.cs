using UnityEngine;

public class Canvas2 : MonoBehaviour
{
    public string canvas2Name = "Canvas2";
    private GameObject canvas2;

    private void Start()
    {
        canvas2 = GameObject.Find(canvas2Name);
        if (canvas2 != null)
        {
            canvas2.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canvas2 != null)
        {
            canvas2.SetActive(true);
        }
    }
}
