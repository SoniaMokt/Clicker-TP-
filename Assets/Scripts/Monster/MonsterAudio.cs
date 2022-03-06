namespace BeeClicker
{
    public class MonsterAudio : Audio
    {

        private void Awake()
        {
            GameManager.Instance.Monster.OnClick += PlayCue;
        }
    }
}
