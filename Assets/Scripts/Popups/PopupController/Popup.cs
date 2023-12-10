using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Portfolio.Popups {
    public abstract class Popup : MonoBehaviour {
        
        //[SerializeField] SomeCustomAnimator openAnimation;

        public async UniTaskVoid Open() {
            //enable gameobject
            gameObject.SetActive(true);

            //await the end of the opening animation
            //await UniTask.WaitUntil(() => isFinishedPlaying == true);
            //or await UniTask.WaitUntilValueChanged(openAnimation, x => x.isFinishedPlaying);
            await UniTask.Yield(); //TEMP
        }

        public async UniTaskVoid Close() {
            //await the end of the closing animation
            await UniTask.Yield(); //TEMP

            //disable gameobject
            gameObject.SetActive(false);
        }
    }
}