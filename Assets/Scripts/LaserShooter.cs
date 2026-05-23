using UnityEngine;
using UnityEngine.Pool;

public class LaserShooter : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab;
    private ObjectPool<GameObject> _laserPool;

    private void Awake()
    {
        _laserPool = new ObjectPool<GameObject>(
            createFunc: () =>
            {
                GameObject laser = Instantiate(laserPrefab);
                laser.GetComponent<SimpleLaser>().onExpire = () => _laserPool.Release(laser);
                return laser;
            },
            actionOnGet: (laser) => laser.SetActive(true),
            actionOnRelease: (laser) => laser.SetActive(false),
            actionOnDestroy: (laser) => Destroy(laser),
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 50
        );
    }

    public void Shoot (Transform firePoint, BaseShip owner)
    {
        GameObject laser = _laserPool.Get();
        laser.transform.position = firePoint.position;
        laser.transform.rotation = firePoint.rotation;
        SimpleLaser sl = laser.GetComponent<SimpleLaser>();
        sl.ownerShooter = owner;
        sl.Launch();
    }
}
