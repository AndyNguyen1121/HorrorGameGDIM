using System;
using System.Collections;
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
        [SerializeField] private Transform goalTarget;
        [SerializeField] private Animator animator;
        public int spacePresses;
        public bool canGrab = true;
        
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip grabMusic;
        [SerializeField] private AudioClip bgMusic;
        [SerializeField] private AudioClip grabSFX;

        private void Awake()
        {
            agent.updateRotation = false;
        }

        private void Start()
        {
            _playerManager = PlayerManager.Instance;
            _playerManager.OnSpacePressed += PlayerAttemptToEscape;
        }

        private void OnDisable()
        {
            _playerManager.OnSpacePressed -= PlayerAttemptToEscape;
        }

        private void Update()
        {
            if (_isGrabbing)
            {
                agent.SetDestination(goalTarget.position);
            }
            else
            {
                agent.SetDestination(PlayerManager.Instance.transform.position);
            }
            
            HandleRotation();
        }

        private void HandleRotation()
        {
            Vector3 desiredRotation = agent.desiredVelocity;

            if (!_isGrabbing && desiredRotation.magnitude < Mathf.Epsilon)
            {
                desiredRotation = PlayerManager.Instance.transform.position - transform.position;
            }
            desiredRotation.y = 0;
            Quaternion finalizedRotation = Quaternion.LookRotation(desiredRotation.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, finalizedRotation, rotationSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && !_isGrabbing && canGrab)
            {
                canGrab = false;
                GrabPlayer(true);
            }
        }

        private void GrabPlayer(bool isGrabbing)
        {
            if (isGrabbing)
            {
                _playerManager.EnablePlayerMovement(false);
                _playerManager.EnterStruggleState();
                _isGrabbing = true;
                _playerManager.transform.parent = grabPoint;
                _playerManager.transform.DOLocalMove(Vector3.zero, grabDuration);
                _playerManager.transform.DOLocalRotate(Vector3.zero, grabDuration);
                animator.CrossFade("Grab", 0.1f);
                audioSource.resource = grabMusic;
                audioSource.PlayOneShot(grabSFX);
                audioSource.Play();
            }
            else
            {
                _playerManager.EnablePlayerMovement(true);
                _playerManager.ExitStruggleState();
                _isGrabbing = false;
                _playerManager.transform.parent = _playerManager.originalParent;
                audioSource.resource = bgMusic;
                audioSource.Play();

            }
        }

        private void PlayerAttemptToEscape()
        {
            if (!_isGrabbing)
                return;
            
            ++spacePresses;
            if (spacePresses == 10)
            {
                GrabPlayer(false);
                spacePresses = 0;
                Debug.Log("let go");
                animator.CrossFade("Release", 0.1f);
                StartCoroutine(ActivateGrabCooldown(3f));
            }
        }

        private IEnumerator ActivateGrabCooldown(float duration)
        {
            yield return new WaitForSeconds(duration);
            canGrab = true;
        }
    }
}