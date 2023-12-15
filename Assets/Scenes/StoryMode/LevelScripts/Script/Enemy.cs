using Scenes.StoryMode.Scripts.Script;
using UnityEngine;
using UnityEngine.Events;

namespace Scenes.StoryMode.LevelScripts.Script
{
    public class Enemy : MonoBehaviour
    {
        //class of the enemy object, which implements the methods: movement along the path and damage to the enemy

        public float speed = 10f;
        public int currentLife;

        public int power = 1;
        public int costForHead = 10;

        private Transform _target;

        private int _targetIndex = 0;

        // Start is called before the first frame update
        private void Start()
        {
            _target = WayPoints.Points[_targetIndex];
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            var dir = _target.position - transform.position;
            transform.Translate(dir.normalized * (speed * Time.deltaTime), Space.World);
            if (Vector3.Distance(transform.position, _target.position) <= 0.4f)
            {
                GetNextPoint();
            }
        }

        private void GetNextPoint()
        {
            if (_targetIndex >= WayPoints.Points.Length - 1)
            {
                notDefeated();
                return;
            }

            _targetIndex++;
            _target = WayPoints.Points[_targetIndex];
        }

        public void SetDamage(int damage)
        {
            currentLife -= damage;
            if (currentLife <= 0)
            {
                Defeated();
            }
        }

        // Event for when the enemy is defeated
        public UnityAction OnDefeated;
        public UnityAction OnNotDefeated;
        

        // Method to call when the enemy is defeated
        private void Defeated()
        {
            Destroy(gameObject);
            OnDefeated?.Invoke();
            PlayerStats.UpdateMoney(costForHead);
        }
        private void notDefeated()
        {
            Destroy(gameObject);
            OnNotDefeated?.Invoke();
            PlayerStats.UpdateLives(power);
        }
    }
}