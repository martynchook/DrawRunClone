using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Logic.Actors
{
    public class ActorsParent : MonoBehaviour
    {
        [SerializeField] private List<Actor> _actors;
        [Space(10f)]
        [SerializeField] private ActorsPositioner _actorsPositioner;
        
        public List<Actor> ActorsList => _actors;

        private void OnValidate()
        {
            _actors.Clear();
            _actors = GetComponentsInChildren<Actor>().ToList();
        }

        private void Awake()
        {
            _actorsPositioner.Init(_actors);
        }

        public void Register(Actor actor)
        {
            if (_actors.Contains(actor))
                return;
            _actors.Add(actor);
        }
        
        public void Unregister(Actor actor)
        {
            if (_actors.Contains(actor))
            {
                _actors.Remove(actor);
            }
        }

        public void StartRunAnim()
        {
            foreach (Actor actor in _actors)
            {
                actor.GetComponent<ActorAnimator>().PlayRun();
            }
        }
        public void StartVictoryAnim()
        {
            foreach (Actor actor in _actors)
            {
                actor.GetComponent<ActorAnimator>().StartVictory();
            }
        }
        
    }
}
