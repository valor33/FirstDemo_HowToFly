using UnityEngine;

/// <summary>
/// 收集物接口
/// 所有可收集的道具都需要实现此接口
/// </summary>
public interface ICollectible
{
    /// <summary>
    /// 当道具被收集时调用
    /// </summary>
    /// <param name="collector">收集者对象（通常是玩家）</param>
    void OnCollected(GameObject collector);

    /// <summary>
    /// 是否可以被收集
    /// 用于控制收集条件（如冷却时间、状态检查等）
    /// </summary>
    bool CanBeCollected { get; }
}