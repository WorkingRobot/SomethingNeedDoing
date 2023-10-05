using System;
using System.Threading;

using Dalamud.Game.ClientState.Conditions;

namespace SomethingNeedDoing.Managers;

/// <summary>
/// Manager that handles game events.
/// </summary>
internal class GameEventManager : IDisposable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GameEventManager"/> class.
    /// </summary>
    public GameEventManager()
    {
        Service.Condition.ConditionChange += this.Condition_ConditionChange;
    }

    /// <summary>
    /// Gets a waiter that is released when an action or crafting action is received through the Event Framework.
    /// </summary>
    public ManualResetEvent DataAvailableWaiter { get; } = new(false);

    /// <inheritdoc/>
    public void Dispose()
    {
        Service.Condition.ConditionChange -= this.Condition_ConditionChange;
        this.DataAvailableWaiter.Dispose();
    }

    private void Condition_ConditionChange(ConditionFlag flag, bool value)
    {
        if (flag == ConditionFlag.Crafting40 && !value)
            this.DataAvailableWaiter.Set();
    }
}
