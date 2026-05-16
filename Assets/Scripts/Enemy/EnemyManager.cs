using System;
using DG.Tweening;
using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 5f;
        [SerializeField] private Transform grabPoint;
        [SerializeField] private float grabDuration = 0.2f;
        [SerializeField] private NavMeshAgent agent;
        private bool _isGrabbing;
        private PlayerManager _playerManager;

        private void Awake()
        {
            agent.updateRotation = false;
        }

        private void Start()
        {
            _playerManager = PlayerManager.Instance;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GrabPlayer(!_isGrabbing);
            }
            
            agent.SetDestination(PlayerManager.Instance.transform.position);
            
            if (!_isGrabbing)
                HandleRotation();
        }

        private void HandleRotation()
        {
            Vector3 desiredRotation = agent.desiredVelocity;

            if (desiredRotation.magnitude < Mathf.Epsilon)
            {
                desiredRotation = PlayerManager.Instance.transform.position - transform.position;
            }
            desiredRotation.y = 0;
            Quaternion finalizedRotation = Quaternion.LookRotation(desiredRotation.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, finalizedRotation, rotationSpeed * Time.deltaTime);
        }

        private void GrabPlayer(bool isGrabbing)
        {
            if (isGrabbing)
            {
                _playerManager.EnablePlayerMovement(false);
                _isGrabbing = true;
                _playerManager.transform.parent = grabPoint;
                _playerManager.transform.DOLocalMove(Vector3.zero, grabDuration);
                _playerManager.transform.DORotateQuaternion(grabPoint.rotation, grabDuration);
            }
            else
            {
                _playerManager.EnablePlayerMovement(true);
                _isGrabbing = false;
                _playerManager.transform.parent = _playerManager.originalParent;
            }
        }
    }
}