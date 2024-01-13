using Scenes.StoryMode.LevelScripts.Script;
using UnityEngine;
using UnityEngine.Events;

namespace Scenes.StoryMode.LevelScripts
{
    public class Enemy : MonoBehaviour
    {
        public float speed = 10f;
        public int currentLife;

        public int power = 1;
        public int costForHead = 10;

        private Vector3 _target;
        private int _targetIndex = 0;
        private Vector3[] _pathNodes;
        private float _spacing;

        private const float StoppingDistance = 0.4f;
        private const float WaypointReachedRadius = 0.1f;

        // Start is called before the first frame update
        private void Start()
        {
            _target = _pathNodes[_targetIndex] * _spacing + new Vector3(0f, 0.4f, 0f);
        }

        public void Initialize(Vector3[] pathNodes, float spacing)
        {
            _pathNodes = pathNodes;
            _spacing = spacing;
            _target = _pathNodes[_targetIndex] * _spacing + new Vector3(0f, 0.4f, 0f);
        }

// Update is called once per frame
        private void FixedUpdate()
        {
            MoveToTarget();
        }

        private void MoveToTarget()
        {
            var dir = _target - transform.position;
            transform.Translate(dir.normalized * (speed * Time.deltaTime), Space.World);

            // Добавлен код для остановки перед следующей точкой
            if (!(Vector3.Distance(transform.position, _target) <= StoppingDistance)) return;
            if (!WaypointReached()) return;
            GetNextPoint();
            ApplyRandomOffset();
        }

        private void ApplyRandomOffset()
        {
            // Добавлен код для случайного смещения целевой точки
            var xOffset = Random.Range(-0.2f, 0.2f);
            var zOffset = Random.Range(-0.2f, 0.2f);
            _target += new Vector3(xOffset, 0f, zOffset);
        }

        private bool WaypointReached()
        {
            // Добавлен код для определения, достигли ли целевой точки
            return Vector3.Distance(transform.position, _target) <= WaypointReachedRadius;
        }

        private void GetNextPoint()
        {
            if (_targetIndex >= _pathNodes.Length - 1)
            {
                NotDefeated();
                return;
            }

            _targetIndex++;
            _target = _pathNodes[_targetIndex] * _spacing + new Vector3(0f, 0.4f, 0f);
            ;
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

        private void NotDefeated()
        {
            Destroy(gameObject);
            OnNotDefeated?.Invoke();
            PlayerStats.UpdateLives(power);
        }
    }
}