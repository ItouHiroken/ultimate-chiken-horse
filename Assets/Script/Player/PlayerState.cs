using System;

namespace PlayerState
{
    [Flags]
    public enum DeBuff
    {
        Default = 1 << 0,
        Slow = 1 << 1,
        Split = 1 << 2
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
//namespace Player2State
//{
//    [Flags]
//    public enum DeBuff
//    {
//        Default = 1 << 0,
//        Slow = 1 << 1,
//        Split = 1 << 2
//    }
//    [Flags]
//    public enum GetScore
//    {
//        Default = 1 << 0,
//        isGoal = 1 << 1,
//        Solo = 1 << 2,
//        First = 1 << 3,
//        Death = 1 << 4,
//        Coin = 1 << 5
//    }
//}
//namespace Player3State
//{
//    [Flags]
//    public enum DeBuff
//    {
//        Default = 1 << 0,
//        Slow = 1 << 1,
//        Split = 1 << 2
//    }
//    [Flags]
//    public enum GetScore
//    {
//        Default = 1 << 0,
//        isGoal = 1 << 1,
//        Solo = 1 << 2,
//        First = 1 << 3,
//        Death = 1 << 4,
//        Coin = 1 << 5
//    }
//}
//namespace Player4State
//{
//    [Flags]
//    public enum DeBuff
//    {
//        Default = 1 << 0,
//        Slow = 1 << 1,
//        Split = 1 << 2
//    }
//    [Flags]
//    public enum GetScore
//    {
//        Default = 1 << 0,
//        isGoal = 1 << 1,
//        Solo = 1 << 2,
//        First = 1 << 3,
//        Death = 1 << 4,
//        Coin = 1 << 5
//    }
//}