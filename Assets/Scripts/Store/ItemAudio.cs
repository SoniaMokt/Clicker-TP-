namespace BeeClicker.Store
{
    public class ItemAudio : Audio
    {
        private void Start()
        {
            GetComponent<Item>().OnLevelUp += PlayCue;
        }
    }
}
