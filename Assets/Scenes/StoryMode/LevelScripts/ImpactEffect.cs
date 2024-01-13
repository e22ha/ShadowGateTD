using UnityEngine;

public class ImpactEffect : MonoBehaviour
{
 
    //script for destroying the effect of the explosion
    
    public float lifetime = 2f;

    private void Start()
    {
        DestroyAfterSec();
    }

    private void DestroyAfterSec()
    {
        Destroy(gameObject, lifetime);
    }

}
