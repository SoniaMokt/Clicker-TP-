using UnityEngine;

namespace BeeClicker
{
    public class Audio : MonoBehaviour
    {

        [SerializeField] private Etienne.Cue _Cue = new Etienne.Cue(null);

        protected virtual void PlayCue()
        {
            GameManager.Instance.AudioPool.Play(_Cue);
        }
    }
}
