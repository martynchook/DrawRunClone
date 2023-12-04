using System.Collections;
using CodeBase.Logic.Actors;
using CodeBase.Logic.Drawing;
using UnityEngine;

namespace CodeBase.Logic
{
    public class Gem : MonoBehaviour, IСollected
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] public ParticleSystem _collectFX;
        [SerializeField] public ParticleSystem _permanentFX;
        [SerializeField] public MeshRenderer _mesh;
        
        private const float DelayToDestroy = 1f;
        
        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;
        }

        private void TriggerEnter(Collider other)
        {
            if (other.GetComponent<Actor>()) 
                Collect();
        }

        private void TriggerExit(Collider other) { }
        
        public void Collect()
        {
            _triggerObserver.Disable();
            _permanentFX.gameObject.SetActive(false);
            _collectFX.Play();
            _mesh.enabled = false;
            StartCoroutine(StartDestroyTimer());
        }
        
        private IEnumerator StartDestroyTimer()
        {
            yield return new WaitForSeconds(DelayToDestroy);
            Destroy(gameObject);
        }
    }
}