using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Logic.Drawing
{
    public class DrawingAreaHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public event Action<PointerEventData> BeginDrag;
        public event Action<PointerEventData> Drag;
        public event Action<PointerEventData> EndDrag;
        
        public void OnBeginDrag(PointerEventData eventData) => 
            BeginDrag?.Invoke(eventData);

        public void OnDrag(PointerEventData eventData)=> 
            Drag?.Invoke(eventData);

        public void OnEndDrag(PointerEventData eventData)=> 
            EndDrag?.Invoke(eventData);
    }
}