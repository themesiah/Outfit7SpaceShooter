using UnityEngine;
using UnityEngine.Audio;

namespace SpaceShooter.UI
{
    public class MixerControl : MonoBehaviour
    {
        [SerializeField]
        private AudioMixerGroup mixerGroup = default;

        private void SetVolume(string property, float volume)
        {
            mixerGroup.audioMixer.SetFloat(property, Mathf.Log10(volume) * 20);
        }

        public void SetMasterVolume(float volume)
        {
            SetVolume("MasterVolume", volume);
        }

        public void SetSFXVolume(float volume)
        {
            SetVolume("SFXVolume", volume);
        }

        public void SetBGMVolume(float volume)
        {
            SetVolume("BGMVolume", volume);
        }
    }
}