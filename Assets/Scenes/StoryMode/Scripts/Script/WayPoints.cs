using UnityEngine;

public class WayPoints : MonoBehaviour
{
    public static Transform[] Points;

    private void Awake()
    {
        Points = new Transform[transform.childCount];
        for (var i = 0; i < Points.Length; i++)
        {
            Points[i] = transform.GetChild(i);
            Debug.Log($"x: {Points[i].transform.position.x}; y:{Points[i].transform.position.y}");
        }
    }
}