using Dalamud.Game.ClientState.Objects;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using SomethingNeedDoing.Managers;

namespace SomethingNeedDoing
{
    /// <summary>
    /// Dalamud and plugin services.
    /// </summary>
    internal class Service
    {
        /// <summary>
        /// Gets or sets the plugin itself.
        /// </summary>
        internal static SomethingNeedDoingPlugin Plugin { get; set; } = null!;

        /// <summary>
        /// Gets or sets the plugin configuration.
        /// </summary>
        internal static SomethingNeedDoingConfiguration Configuration { get; set; } = null!;

        /// <summary>
        /// Gets or sets the plugin chat manager.
        /// </summary>
        internal static ChatManager ChatManager { get; set; } = null!;

        /// <summary>
        /// Gets or sets the plugin event framework manager.
        /// </summary>
        internal static GameEventManager GameEventManager { get; set; } = null!;

        /// <summary>
        /// Gets or sets the plugin macro manager.
        /// </summary>
        internal static MacroManager MacroManager { get; set; } = null!;

        /// <summary>
        /// Gets the Dalamud plugin interface.
        /// </summary>
        [PluginService]
        internal static IDalamudPluginInterface Interface { get; private set; } = null!;

        /// <summary>
        /// Gets the Dalamud chat gui.
        /// </summary>
        [PluginService]
        internal static IChatGui ChatGui { get; private set; } = null!;

        /// <summary>
        /// Gets the Dalamud client state.
        /// </summary>
        [PluginService]
        internal static IClientState ClientState { get; private set; } = null!;

        /// <summary>
        /// Gets the Dalamud command manager.
        /// </summary>
        [PluginService]
        internal static ICommandManager CommandManager { get; private set; } = null!;

        /// <summary>
        /// Gets the Dalamud condition manager.
        /// </summary>
        [PluginService]
        internal static ICondition Condition { get; private set; } = null!;

        /// <summary>
        /// Gets the Dalamud data manager.
        /// </summary>
        [PluginService]
        internal static IDataManager DataManager { get; private set; } = null!;

        /// <summary>
        /// Gets the Dalamud framework.
        /// </summary>
        [PluginService]
        internal static IFramework Framework { get; private set; } = null!;

        /// <summary>
        /// Gets the Dalamud game gui.
        /// </summary>
        [PluginService]
        internal static IGameGui GameGui { get; private set; } = null!;

        /// <summary>
        /// Gets the Dalamud keystate.
        /// </summary>
        [PluginService]
        internal static IKeyState KeyState { get; private set; } = null!;

        /// <summary>
        /// Gets the Dalamud object table.
        /// </summary>
        [PluginService]
        internal static IObjectTable ObjectTable { get; private set; } = null!;

        /// <summary>
        /// Gets the Dalamud target manager.
        /// </summary>
        [PluginService]
        internal static ITargetManager TargetManager { get; private set; } = null!;

        /// <summary>
        /// Gets the Dalamud game interop provider.
        /// </summary>
        [PluginService]
        internal static IGameInteropProvider GameInteropProvider { get; private set; } = null!;

        /// <summary>
        /// Gets the Dalamud plugin log.
        /// </summary>
        [PluginService]
        internal static IPluginLog Log { get; private set; } = null!;
    }
}
