using System.Collections.Generic;
using CodeBase.Logic.Actors;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI.Extensions;

namespace CodeBase.Logic.Drawing
{
    public class DrawingArea : MonoBehaviour
    { 
        [SerializeField] private UILineRenderer _lineRenderer;
        [SerializeField] private DrawingAreaHandler _handler;
        [SerializeField] private RectTransform _image;
        public float OffsetX => (Screen.width - _image.rect.width) / 2;
        public float OffsetY => 40f;

        private ActorsPositioner _actorsPositioner;
        private ActorsFollower _actorsFollower;
        private ActorsParent _actorsParent;
        private Vector3 _mousePos;
        private List<Vector2> _drawingPoints = new();
        private List<Vector2> _drawingPointsRelative = new();
        
        private const float MinOffset = 5f;

        public void Construct(ActorsPositioner actorsPositioner, ActorsFollower actorsFollower, ActorsParent actorsParent)
        {
            _actorsPositioner = actorsPositioner;
            _actorsFollower = actorsFollower;
            _actorsParent = actorsParent;
        }
        
        private void Start()
        {
            UpdateLine();
            Subscribe();
        }

        private void Subscribe()
        {
            _handler.BeginDrag += OnBeginDrag;
            _handler.Drag += OnDrag;
            _handler.EndDrag += OnEndDrag;
        }
        
        private void OnBeginDrag(PointerEventData eventData)
        {
            _mousePos = eventData.position;
            ClearPointsList();
        }

        private void OnDrag(PointerEventData eventData)
        {
            if (Vector3.Distance(_mousePos, eventData.position) > MinOffset)
            {
                _mousePos = eventData.position;
                AddPointsToLists(eventData.position);
            }
        }

        private void OnEndDrag(PointerEventData eventData)
        {
            _actorsPositioner.ArrangeByPosition(_drawingPointsRelative);
            _actorsFollower.StartMoving();
            _actorsParent.StartRunAnim();
            ClearPointsList();
            UpdateLine();
        }

        private void ClearPointsList()
        {
            _drawingPoints.Clear();
            _drawingPointsRelative.Clear();
            UpdateLine();
        }

        private void AddPointsToLists(Vector2 point)
        {
            _drawingPoints.Add(point);
            _drawingPointsRelative.Add(ScreenPointToRelativeDrawingArea(point));
            UpdateLine();
        }
        
        private Vector2 ScreenPointToRelativeDrawingArea(Vector2 point)
        {
            var rect = _image.rect;
            float posX = point.x - OffsetX;
            float posY = point.y - OffsetY;
            return (new Vector2(posX / rect.width, posY / rect.height));
        }

        private void UpdateLine()
        {
            if (_drawingPoints == null) _drawingPoints = new List<Vector2>();
            if (_drawingPointsRelative == null) _drawingPointsRelative = new List<Vector2>();
            _lineRenderer.Points = _drawingPoints.ToArray();
        }
    }
}