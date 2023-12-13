using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    //class of the enemy object, which implements the methods: movement along the path and damage to the enemy
    
    public float speed = 10f;
    public int life = 20;
    public int harm = 1;
    public int currentLife;

    private Vector2 _target;

    private Transform target;
  
    private int targetIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        target = WayPoints.Points[targetIndex];
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * (speed * Time.deltaTime), Space.World);
        if(Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextPoint();
        }
    }
    void GetNextPoint()
    {
        if(targetIndex >= WayPoints.Points.Length-1)
        {
            Destroy(gameObject);
            return;
        }
        targetIndex++;
        target = WayPoints.Points[targetIndex];
    }
    
    public void SetDamage(int damage)
    {
        currentLife -= damage;
        if (currentLife <= 0)
        {
            Destroy(gameObject);
            // GameStatusControl.CountEnemies--;
        }
    }
    
}
