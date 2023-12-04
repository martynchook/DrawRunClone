using System;
using System.Collections;
using CodeBase.Logic.Obstacles;
using UnityEngine;

namespace CodeBase.Logic.Actors
{
    public class ActorElimination : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
        [SerializeField] private Rigidbody _rigidbody;
        [Space(5f)]
        public Material _eliminationMaterial;
        
        public event Action Happend;
        
        private const float DelayToDestroy = 3f;

        private void Start()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            _triggerObserver.TriggerEnter += other =>
            {
                if (other.GetComponent<Obstacle>())
                    Elimination();
            };
        }

        public void Elimination()
        {
            ActorsParent actorsParent = transform.GetComponentInParent<ActorsParent>();
            if (actorsParent != null)
                actorsParent.Unregister(GetComponent<Actor>());
            transform.parent = null;
            ChangeMaterial();
            _rigidbody.isKinematic = false;
            _rigidbody.useGravity = true;
            Happend?.Invoke();
            StartCoroutine(DestroyTimer());
        }
        
        private void ChangeMaterial()
        {
            Material newMaterial = _eliminationMaterial;
            Material[] materials = _skinnedMeshRenderer.materials;
            materials[0].color = Color.gray;
            _skinnedMeshRenderer.materials = materials;
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(DelayToDestroy);
            Destroy(gameObject);
        }
    }
}