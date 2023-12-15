using Scenes.StoryMode.Scripts.Script;
using Unity.VisualScripting;
using UnityEngine;

namespace Scenes.StoryMode.LevelScripts.Script
{
    public class Tower : MonoBehaviour
    {
        //class Tower with properties and find target, shoot func
    
        private Enemy _target;
    
        public float range = 1f;

        public float fireRate = 1f;
        private float _fireCountdown;

        public GameObject bulletPrefab;
        public Transform firePoint;

        public string enemyTag;

        public int damage;
        public int price;
        public string nameTower;

        // Start is called before the first frame update
        private void Start()
        {
            InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
            PlayerStats.UpdateMoney(-price);
        }

        // Update is called once per frame
        private void UpdateTarget()
        {
            var enemies = new GameObject[]{};
            if (enemyTag == "AllEnemy")
            {
                enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
                enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy Fly"));
            }
            else
            {
                enemies = GameObject.FindGameObjectsWithTag(enemyTag);
            }
        

            var shortestDist = Mathf.Infinity;
            GameObject nearestEnemy = null;

            foreach (var enemy in enemies)
            {
                var distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (!(distanceToEnemy < shortestDist)) continue;
                shortestDist = distanceToEnemy;
                nearestEnemy = enemy;
            }


            if (nearestEnemy == null) return;
            {
                // Debug.Log("nearestEnemy != null");
                var enemy = nearestEnemy.GetComponent<Enemy>();
                if (enemy != null)
                {
                    // Debug.Log("Enemy != null");
                    // Debug.Log("sh: "+ shortestDist);
                    if (shortestDist <= range)
                    {
                        // Debug.Log("shortestDist");
                        _target = enemy;
                        // Debug.Log("found");
                    }
                    else
                    {
                        // Debug.Log("not found");
                        _target = null;
                    }
                }
                else
                {
                    // Debug.Log("not found");
                    _target = null;
                }
            }
        }


        private void FixedUpdate()
        {
            if (_target == null)
            {
                return;
            }

            //Vector3 dir = _target.transform.position - transform.position;
            //Quaternion lookRotation = Quaternion.LookRotation(dir);
            //Vector3 rotation = Quaternion.Lerp(head.rotation, lookRotation, Time.deltaTime*speedRotation).eulerAngles;
            //head.rotation = Quaternion.Euler(0f, rotation.y, 0f);

            // Debug.Log("f: "+_fireCountdown);

            if (_fireCountdown <= 0f)
            {
                Shoot();
                _fireCountdown = 1f / fireRate;
            }

            _fireCountdown -= Time.deltaTime;

        }

        private void Shoot()
        {
            var bulletGo = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            var bullet = bulletGo.GetComponent<Bullet>();
            if(bullet != null) bullet.Seek(_target, damage);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}
