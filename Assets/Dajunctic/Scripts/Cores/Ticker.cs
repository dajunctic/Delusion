namespace Dajunctic
{
    public class Ticker
    {
        
    }

    [Flags]
    public enum TickMode
    {
        None = 0,
        EarlyTick = 1,
        Tick = 2,
        FixedTick = 4,
        LateTick = 8,
    }
}