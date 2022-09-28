using System;

namespace PlayerState
{
    [Flags]
    public enum DeBuff
    {
        Default,
        Slow,
        Split,
    }
    [Flags]
    public enum GetScore
    {
        Default = 1 << 0,
        isGoal = 1 << 1,
        Solo = 1 << 2,
        First = 1 << 3,
        Death = 1 << 4,
        Coin = 1 << 5
    }
}