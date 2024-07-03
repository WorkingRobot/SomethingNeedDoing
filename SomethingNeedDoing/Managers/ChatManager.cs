using System;
using System.Threading.Channels;

using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Game.Text.SeStringHandling.Payloads;
using Dalamud.Plugin.Services;
using Dalamud.Utility.Signatures;
using FFXIVClientStructs.FFXIV.Client.System.String;
using FFXIVClientStructs.FFXIV.Client.UI;
using SomethingNeedDoing.Misc;

namespace SomethingNeedDoing.Managers;

/// <summary>
/// Manager that handles displaying output in the chat box.
/// </summary>
internal class ChatManager : IDisposable
{
    private readonly Channel<string> chatBoxMessages = Channel.CreateUnbounded<string>();

    [Signature("48 89 5C 24 ?? 57 48 83 EC 20 48 8B FA 48 8B D9 45 84 C9")]
    private readonly SendChatDelegate sendChat = null!;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChatManager"/> class.
    /// </summary>
    public ChatManager()
    {
        Service.GameInteropProvider.InitializeFromAttributes(this);
        Service.Framework.Update += this.FrameworkUpdate;
    }

    private unsafe delegate void SendChatDelegate(UIModule* @this, Utf8String* message, Utf8String* historyMessage = null, bool pushToHistory = false);

    /// <inheritdoc/>
    public void Dispose()
    {
        Service.Framework.Update -= this.FrameworkUpdate;

        this.chatBoxMessages.Writer.Complete();
    }

    /// <summary>
    /// Print a normal message.
    /// </summary>
    /// <param name="message">The message to print.</param>
    public void PrintMessage(string message)
        => Service.ChatGui.Print(new XivChatEntry()
        {
            Type = Service.Configuration.ChatType,
            Message = $"[SND] {message}",
        });

    /// <summary>
    /// Print a happy message.
    /// </summary>
    /// <param name="message">The message to print.</param>
    /// <param name="color">UiColor value.</param>
    public void PrintColor(string message, UiColor color)
        => Service.ChatGui.Print(
            new XivChatEntry()
            {
                Type = Service.Configuration.ChatType,
                Message = new SeString(
                    new UIForegroundPayload((ushort)color),
                    new TextPayload($"[SND] {message}"),
                    UIForegroundPayload.UIForegroundOff),
            });

    /// <summary>
    /// Print an error message.
    /// </summary>
    /// <param name="message">The message to print.</param>
    public void PrintError(string message)
        => Service.ChatGui.Print(new XivChatEntry()
        {
            Type = Service.Configuration.ErrorChatType,
            Message = $"[SND] {message}",
        });

    /// <summary>
    /// Process a command through the chat box.
    /// </summary>
    /// <param name="message">Message to send.</param>
    public async void SendMessage(string message)
    {
        await this.chatBoxMessages.Writer.WriteAsync(message);
    }

    /// <summary>
    /// Clear the queue of messages to send to the chatbox.
    /// </summary>
    public void Clear()
    {
        var reader = this.chatBoxMessages.Reader;
        while (reader.Count > 0 && reader.TryRead(out var _))
            continue;
    }

    private void FrameworkUpdate(IFramework framework)
    {
        if (this.chatBoxMessages.Reader.TryRead(out var message))
        {
            this.SendMessageInternal(message);
        }
    }

    private unsafe void SendMessageInternal(string message)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(message);

        var str = Utf8String.FromString(message);
        str->SanitizeString(0x27F, null);
        this.sendChat(UIModule.Instance(), str);
        str->Dtor(true);
    }
}
