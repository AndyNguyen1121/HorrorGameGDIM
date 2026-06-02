using System;
using JetBrains.Annotations;
using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class ParentManager : MonoBehaviour
    {
        public static ParentManager Instance;
        private NavMeshAgent agent;
        public event Action OnEndGame;
        public bool gameEnded;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            agent.SetDestination(PlayerManager.Instance.transform.position);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && !gameEnded)
            {
                OnEndGame?.Invoke();
                gameEnded = true;
                Debug.Log("Game Ended");
            }
        }
    }
}