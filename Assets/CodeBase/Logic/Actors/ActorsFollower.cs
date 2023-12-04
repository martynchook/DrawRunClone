using System.Collections.Generic;
using DG.Tweening;
using Dreamteck.Splines;
using UnityEngine;

namespace CodeBase.Logic.Actors
{
    [RequireComponent(typeof(SplineFollower))]
    public class ActorsFollower : MonoBehaviour
    {
        [SerializeField] private SplineFollower _splineFollower;
        
        public void Construct(SplineComputer splineComputer)
        {
            _splineFollower.spline = splineComputer;
        }

        private void Awake()
        {
            if (_splineFollower == null)
                _splineFollower = GetComponent<SplineFollower>();
        }

        private void Start()
        {
            ResetPosition();
        }

        public void StartMoving()
        {
            if (!_splineFollower.follow)
            {
                List<Actor> actors = GetComponent<ActorsParent>().ActorsList;
                foreach (var actor in actors) 
                    actor.transform.DOLocalRotate(Vector3.zero, 0.5f);
                _splineFollower.follow = true;
            }
        }

        public void StopMoving()
        {
            if (_splineFollower.follow)
            {
                List<Actor> actors = GetComponent<ActorsParent>().ActorsList;
                foreach (var actor in actors)
                {
                    actor.transform.DOLocalRotate(Vector3.back * 180f, 0.5f);
                }
                _splineFollower.follow = false;
            }
        }

        private void ResetPosition()
        {
            StopMoving();
            _splineFollower.Restart();
        }
    }
}