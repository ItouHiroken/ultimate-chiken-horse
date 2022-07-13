using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IPause
{
    /// <summary>一時停止のための処理を実装する</summary>
    void Pause();
    /// <summary>再開のための処理を実装する</summary>
    void Resume();
}