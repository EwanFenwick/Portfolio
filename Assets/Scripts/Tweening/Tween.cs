using UnityEngine;

namespace Portfolio.Tweening {
    #region Nested Classes

    public abstract class Tween<TComponent, TValue> : Tween<TComponent, TValue, TValue>
        where TComponent : Component { }

    #endregion

    public abstract class Tween<TComponent, TFrom, TTo> : BaseTween
        where TComponent : Component {
        
        #region Editor Variables
#pragma warning disable 0649

        [SerializeField] protected TComponent _component;
        [SerializeField] protected TFrom _from;
        [SerializeField] protected TTo _to;

#pragma warning restore 0649
        #endregion

        #region Protected Methods

        protected override void ResetComponentProgress() => UpdateComponentProgress(AnimationCurve.Evaluate(0f));

        #endregion
    }
}
