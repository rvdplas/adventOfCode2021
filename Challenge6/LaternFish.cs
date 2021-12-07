namespace Challenge6
{
    public class LaternFish
    {
        private const int NewFishTimer = 8;
        private const int ExistingFishTimer = 6;
        public int Timer { get; private set; }

        public LaternFish()
        {
            Timer = NewFishTimer;
        }

        public LaternFish(int timer)
        {
            Timer = timer;
        }

        public bool DayPasted()
        {
            Timer--;
            if (Timer == -1)
            {
                ResetTimer();
                return true;
            }

            return false;
        }

        private void ResetTimer()
        {
            Timer = ExistingFishTimer;
        }
    }
}