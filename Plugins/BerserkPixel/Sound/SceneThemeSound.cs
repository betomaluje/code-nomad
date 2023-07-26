using UnityEngine;

namespace BerserkPixel.Sound
{
    public class SceneThemeSound : MonoBehaviour
    {
        [SerializeField] private string ThemeSound;

        private void Start()
        {
            SoundManager.instance.StopAllBackground();
            SoundManager.instance.Play(ThemeSound);
        }

        public void PlayFx(string name)
        {
            SoundManager.instance.Play(name);
        }
    }
}