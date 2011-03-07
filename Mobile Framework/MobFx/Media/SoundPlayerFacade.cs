namespace ChristianHelle.Framework.WindowsMobile.Media
{
    /// <summary>
    /// Represents the sound player as a facade
    /// </summary>
    public static class SoundPlayerFacade
    {
        private static ISoundPlayer instance;
        private static readonly object syncLock = new object();

        /// <summary>
        /// Instance of the sound player
        /// </summary>
        public static ISoundPlayer Instance
        {
            get
            {
                lock (syncLock)
                {
                    if (instance == null)
                    {
                        instance = SoundPlayerFactory.Create();
                        MobileApplication.DispoableResources.Add("SoundPlayerFacade", instance);
                    }
                    return instance;
                }
            }
        }
    }
}