using CodeBase.Logic.Actors;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Logic
{
    public class Finish : MonoBehaviour
    {
        [SerializeField] private Transform[] _finishPositions;

        private const float SortDuration = 0.25f;

        public void FinishRace(Collider other)
        {
            var parent = other.transform.parent;
            parent.GetComponent<ActorsFollower>().StopMoving();
            parent.GetComponent<ActorsParent>().StartVictoryAnim();
            SortFinish(parent.GetComponent<ActorsParent>());
        }

        private void SortFinish(ActorsParent actorsParent)
        {
            for (int i = 0; i < actorsParent.ActorsList.Count; i++)
            {
                actorsParent.ActorsList[i].transform.DOMove(_finishPositions[i].position, SortDuration);
            }
        }
    }
}
