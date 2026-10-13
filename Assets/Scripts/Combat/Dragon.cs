using System.Collections;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    private ProjectileController projectileController;
    [SerializeField] private GameObject dragonMouth;
    [SerializeField] private Transform hero;
    
    void Awake()
    {
        projectileController =  gameObject.GetComponent<ProjectileController>();
        hero = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Start()
    {
        StartCoroutine(LaunchProjectile());
    }

    private IEnumerator LaunchProjectile()
    {
        while (true)
        {
            float waitTime = Random.Range(1f, 3f);
            yield return new WaitForSeconds(waitTime);
            projectileController.GenerateProjectile(dragonMouth.transform, hero);
        }
    }
}