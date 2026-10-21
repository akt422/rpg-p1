using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private GameObject projPrefab;
    [SerializeField] private float size_min = 0.6f;
    [SerializeField] private float size_max = 1.5f;
    
    public void GenerateProjectile(Transform spawn, GameObject hero)
    {
        Projectile projectile = Instantiate(projPrefab, spawn.position, Quaternion.identity).GetComponent<Projectile>();
        float randomSize = Random.Range(size_min, size_max);
        projectile.transform.localScale = new Vector3(randomSize, randomSize, 1);
        projectile.Initialize(hero);
    }
}