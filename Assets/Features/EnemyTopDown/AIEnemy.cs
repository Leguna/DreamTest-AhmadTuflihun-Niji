using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace EnemyTopDown
{
    public class AIEnemy : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float stoppingDistance =10f;
        [SerializeField] private Transform player;
        [SerializeField] private float chaseRange = 5f;

        [SerializeField] private float attackRange = 1f;
        [SerializeField] private float attackCooldown = 2f;

        private Vector3 _startPosition;
        private bool _isChasing;
        private bool _canAttack = true;

        private EnemyState _currentState = EnemyState.Idle;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player").transform;
            _startPosition = transform.position;
        }

        private void Update()
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer < attackRange && _canAttack)
            {
                StartCoroutine(Attack());
            }
            else if (distanceToPlayer < chaseRange)
            {
                ChasePlayer();
            }
            else if (_isChasing)
            {
                ReturnToStart();
            }
        }

        void ChasePlayer()
        {
            _isChasing = true;
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }

        void ReturnToStart()
        {
            transform.position = Vector2.MoveTowards(transform.position, _startPosition, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, _startPosition) < 0.1f)
            {
                _isChasing = false;
            }
        }

        void Patrol()
        {
            if (_currentState == EnemyState.Idle)
            {
                _currentState = EnemyState.Roam;
                Vector2 randomPos = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
                rb.MovePosition(randomPos);
            }
        }

        IEnumerator Attack()
        {
            _canAttack = false;
            Debug.Log("Attacking");
            rb.DOMove(player.position, 0.5f).OnComplete(() => { rb.DOMove(_startPosition, 0.5f); });
            yield return new WaitForSeconds(attackCooldown);
            _canAttack = true;
        }

        public enum EnemyState
        {
            Idle,
            Roam,
            Chase,
            Attack
        }
    }
}