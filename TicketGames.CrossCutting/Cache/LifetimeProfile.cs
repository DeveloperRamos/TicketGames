namespace TicketGames.CrossCutting.Cache
{
    public enum LifetimeProfile
    {
        Shortest = 1,
        Short = 5,
        Moderate = 15,
        Long = 30,
        Longest = 60,
        TwoHours = 120,
        FourHours = 240,
        LongTime = 720,
        OneDay = 1440,
        Forever = 5000000 // ~10 anos
    }
}
