using RpgAdventureGame.Backend.Engine.Core.Waiter.Interface;

namespace RpgAdventureGame.Backend.Engine.Core.Waiter;

public class IntervalWaiter : IWaiter
{
    private readonly TimeProvider timeProvider;

    private TimeSpan prevInterval;
    private DateTime prevTickTimestamp;

    private PeriodicTimer Timer { get; }

    public TimeSpan Interval
    {
        get => Timer.Period;
        set
        {
            GetLastTickDate();
            prevInterval = Timer.Period;
            Timer.Period = value;
        }
    }

    public DateTime NextTick => GetLastTickDate().Add(prevInterval);

    public TimeSpan Delay => NextTick - timeProvider.GetLocalNow();

    public IntervalWaiter(TimeSpan interval, TimeProvider? timeProvider = null)
    {
        this.timeProvider = timeProvider ?? TimeProvider.System;

        prevTickTimestamp = this.timeProvider.GetLocalNow().DateTime;

        Timer = new(prevInterval = interval);
    }

    public async Task AwaitAsync(CancellationToken cancellationToken = default)
        => await Timer.WaitForNextTickAsync(cancellationToken);

    private DateTime GetLastTickDate()
    {
        var incrUntil = timeProvider.GetLocalNow() - prevInterval;
        while (prevTickTimestamp < incrUntil)
            prevTickTimestamp += prevInterval;
        return prevTickTimestamp;
    }

    public override string ToString() => ((IWaiter)this).Info;
}