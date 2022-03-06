namespace BeeClicker
{
    public class HiveAudio : Audio
    {
        private void Awake()
        {
            GameManager.Instance.Hive.OnClick += PlayCue;
        }
    }
}
