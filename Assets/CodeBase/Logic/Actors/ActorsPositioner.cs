using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Logic.Actors
{
    public class ActorsPositioner : MonoBehaviour
    {
        [SerializeField] private Transform _placementArea; 
        [SerializeField] private float _arrangeDuration = 0.5f;
        
        private List<Actor> _actors;
        private List<Vector2> _currentPositions;
        
        public void Init(List<Actor> actors)
        {
            _actors = actors;
        }

        public void ArrangeByPosition(List<Vector2> drawingAreaPoints)
        {
            float index = (float) drawingAreaPoints.Count / _actors.Count;
            Vector3 areaLocalScale = _placementArea.localScale;

            for (int i = 0; i < _actors.Count; i++)
            {
                Transform actor = _actors[i].transform;
                int pointId = Mathf.Clamp(Mathf.FloorToInt((i * index)), 0, drawingAreaPoints.Count - 1);
                Vector2 point = drawingAreaPoints[pointId];
                Vector3 position = new Vector3(areaLocalScale.x * point.x - (areaLocalScale.x / 2), actor.position.y, areaLocalScale.z * point.y - (areaLocalScale.z / 2));
                actor.DOLocalMove(position, _arrangeDuration);
            }

        }
        
    }
}