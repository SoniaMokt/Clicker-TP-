using Etienne;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace BeeClicker
{
    public class AudioPool : MonoBehaviour
    {
        [SerializeField] private int _SourceCount = 50;
        [SerializeField] private AudioMixerGroup _AudioMixer;
        private Queue<AudioSource> _Sources = new Queue<AudioSource>();

        private void Awake()
        {
            for(int i = 0; i < _SourceCount; i++)
            {
                GameObject go = new GameObject("Source");
                go.transform.SetParent(transform);
                AudioSource source = go.AddComponent<AudioSource>();
                source.outputAudioMixerGroup = _AudioMixer;
                Enqueue(source);
            }
        }

        public void Enqueue(AudioSource source)
        {
            _Sources.Enqueue(source);
            source.gameObject.SetActive(false);
        }

        private AudioSource Dequeue()
        {
            AudioSource source = _Sources.Dequeue();
            source.gameObject.SetActive(true);
            return source;
        }

        public async void Play(Sound sound)
        {
            AudioSource source = Dequeue();
            source.clip = sound.Clip;
            source.volume = sound.Parameters.Volume;
            source.pitch = sound.Parameters.Pitch;
            source.loop = sound.Parameters.Loop;
            source.spatialBlend = sound.Parameters.SpacialBlend;
            source.Play();
            await System.Threading.Tasks.Task.Delay(Mathf.RoundToInt(sound.Clip.length * 1000));
            if(!Application.isPlaying) return;
            Enqueue(source);
        }
    }
}
