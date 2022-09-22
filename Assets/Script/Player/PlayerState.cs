using System;

namespace PlayerState
{
    [Flags]
    public enum DeBuff
    {
        Default = 1,
        Slow = 1,
        Split = 1,
    }
    [Flags]
    public enum GetScore
    {
        Default = 1 << 0,
        isGoal = 1 << 1,//10
        Solo = 1 << 2,//20
        First = 1 << 3,//5
        Death = 1 << 4,//5
        Coin = 1 << 5//5
    }
}