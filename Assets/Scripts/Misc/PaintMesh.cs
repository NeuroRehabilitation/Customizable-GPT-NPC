using UnityEngine;

public class PaintMesh : MonoBehaviour
{
    public Color color = Color.red;
    private Mesh mesh;
    private Color[] colors;
    private float startTime;
    private float updateInterval = 1f;
    private int closestVertexIndex = 0;

    private void Start()
    {
        Invoke("FindMesh", 5f);
        startTime = Time.time;
    }

    private void FindMesh()
    {
        GameObject terrain = GameObject.Find("Terrain(Clone)");
        mesh = terrain.GetComponent<MeshFilter>().mesh;
        colors = new Color[mesh.vertices.Length];
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            colors[i] = Color.white;
        }
        mesh.colors = colors;
    }

    private void Update()
    {
        if (Time.time - startTime >= 6f)
        {
            if (Time.time - startTime >= updateInterval)
            {
                 
                Vector3 currentPosition = transform.position;
                if (Vector3.Distance(currentPosition, mesh.vertices[closestVertexIndex]) > 0.1f)
                {
                    closestVertexIndex = GetClosestVertex(currentPosition);
                }
                colors[closestVertexIndex] = color;
                mesh.colors = colors;
                startTime = Time.time;
            }
        }
    }

    private int GetClosestVertex(Vector3 position)
    {
        float minDistance = float.MaxValue;
        int closestVertexIndex = 0;
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            float distance = Vector3.Distance(position, mesh.vertices[i]);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestVertexIndex = i;
            }
        }
        return closestVertexIndex;
    }
}
