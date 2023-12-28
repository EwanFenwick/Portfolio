using NaughtyAttributes;
using UnityEngine;

namespace Portfolio.Tweening {
    #region Nested Classes

    public abstract class Tween<TComponent, TValue> : Tween<TComponent, TValue, TValue>
        where TComponent : Component { }

    #endregion

    public abstract class Tween<TComponent, TStart, TEnd> : BaseTween
        where TComponent : Component {
        
        #region Editor Variables
#pragma warning disable 0649

        [SerializeField, Required] protected TComponent _component;
        [SerializeField] protected bool _startIsStatic;
        [SerializeField] protected bool _endIsStatic;
        [SerializeField, ShowIf("_startIsStatic")] protected TStart _start;
        [SerializeField, ShowIf("_endIsStatic")] protected TEnd _end;

#pragma warning restore 0649
        #endregion


        #region Public Methods

        public virtual void SetDynamicTarget(TEnd target)
            => _end = target;

        #endregion

        #region Protected Methods

        protected override void ResetComponentProgress(bool resetToEnd) {
            if(_startIsStatic) {
                base.ResetComponentProgress(resetToEnd);
            } else {
                ResetToDynamicStart();
            }
        }

        protected abstract void ResetToDynamicStart();

        #endregion
    }
}
