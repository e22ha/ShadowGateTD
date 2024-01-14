using System.Collections;
using UnityEngine;

namespace Scenes.StoryMode.LevelScripts
{
    public class Bullet : MonoBehaviour
    {
        //Bullet object class. Realisation of shot, impact, explosion and destruction of the bullet itself

        private Enemy _target;

        private int _killPoint = 1;

        public float speed = 70f;

        public float fadeTime = 1f;

        public GameObject impactEffect;

        public float explodeRadius;

        public void Seek(Enemy target, int killPoint)
        {
            _target = target;
            _killPoint = killPoint;
        }

        private void FixedUpdate()
        {
            if (_target == null)
            {
                // Если цель отсутствует, начинаем процесс исчезновения
                StartCoroutine(FadeOut());
                return;
            }

            // Перемещаем пулю в сторону цели
            var dir = _target.transform.position - transform.position;
            var distanceThisFrame = speed * Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame)
            {
                // Пуля достигла цели
                HitTarget();
                return;
            }

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);

            transform.LookAt(_target.transform);
        }

        private void HitTarget()
        {
            var t = transform;
            Instantiate(impactEffect, t.position, t.rotation);

            if (explodeRadius > 0f)
            {
                Explode();
            }
            else
            {
                Damage(_target);
            }

            Destroy(gameObject);
        }

        private void Explode()
        {
            var colliders = Physics.OverlapSphere(transform.position, explodeRadius);

            foreach (var coll in colliders)
            {
                if (!coll.CompareTag("Enemy")) continue;
                Debug.Log($"Find enemy in {explodeRadius}");
                Damage(coll.GetComponent<Enemy>());
            }
        }

        private void Damage(Enemy target)
        {
            target.SetDamage(_killPoint);
        }

        IEnumerator FadeOut()
        {
            var elapsedTime = 0f;
            var startColor = GetComponent<Renderer>().material.color;

            while (elapsedTime < fadeTime)
            {
                // Интерполяция цвета для плавного затухания
                var t = elapsedTime / fadeTime;
                var lerpedColor = Color.Lerp(startColor, Color.clear, t);
                GetComponent<Renderer>().material.color = lerpedColor;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // По завершении затухания уничтожаем объект пули
            Destroy(gameObject);
        }
    }
}