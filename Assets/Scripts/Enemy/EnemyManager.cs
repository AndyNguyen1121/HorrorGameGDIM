using System;
using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private Transform grabPoint;
        [SerializeField] private float grabDuration = 0.2f;
        [SerializeField] private NavMeshAgent agent;

        private void Awake()
        {
            agent.updateRotation = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!PlayerManager.Instance.isGrabbed)
                    PlayerManager.Instance.GrabPlayer(grabPoint, grabDuration);
                else
                    PlayerManager.Instance.UngrabPlayer();
            }
            
            agent.SetDestination(PlayerManager.Instance.transform.position);
            HandleRotation();
        }

        private void HandleRotation()
        {
            Vector3 desiredRotation = PlayerManager.Instance.transform.position - transform.position;
            desiredRotation.y = 0;
            transform.rotation = Quaternion.LookRotation(desiredRotation);
        }
    }
}