using DG.Tweening;
using UnityEngine;

namespace CodeBase.Logic.Drawing
{
    public class TextAnimation : MonoBehaviour
    { 
        [SerializeField] public RectTransform _text;

        private void Start() => 
            Animate();

        private void Animate() => 
            _text.DOScale(Vector3.one * 1.3f, 1f).SetLoops(-1, LoopType.Yoyo);
    }
}
