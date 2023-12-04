using DG.Tweening;
using UnityEngine;

namespace CodeBase.Logic.Actors
{
    [RequireComponent(typeof(Collider))]
    public class FreeActor : MonoBehaviour
    {
        [SerializeField] private Actor _actor;
        [SerializeField] private Collider _collider;
        [SerializeField] private ActorAnimator _animator;
        [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
        [Space(5f)]
        [SerializeField] private Material _baseMaterial;

        private const float RotateDuration = 0.25f;

        private void OnValidate()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        public void Employ(Transform actorsParent)
        {
            _collider.isTrigger = false;
            ChangeMaterial();
            transform.DORotate(Vector3.zero, RotateDuration);
            _animator.PlayRun(); 
            transform.SetParent(actorsParent);
            actorsParent.GetComponent<ActorsParent>().Register(_actor);
            Destroy(this);
        }
        
        private void ChangeMaterial()
        {
            Material newMaterial = _baseMaterial;
            Material[] materials = _skinnedMeshRenderer.materials;
            materials[0] = newMaterial;
            _skinnedMeshRenderer.materials = materials;
        }
    }
}