using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using System.IO;
using System.Reflection;
using Dalamud.Interface.Windowing;
using TestPlugin.Windows;

namespace TestPlugin
{
    public sealed class Plugin : IDalamudPlugin
    {
        public string Name => "Test Plugin";
        private const string CommandName = "/testcommand";
        private DalamudPluginInterface PluginInterface { get; init; }
        private CommandManager CommandManager { get; init; }
        public Configuration Configuration { get; init; }
        public WindowSystem WindowSystem = new("TestPlugin");

        public Plugin(
            [RequiredVersion("1.0")] DalamudPluginInterface pluginInterface,
            [RequiredVersion("1.0")] CommandManager commandManager)
        {
            PluginInterface = pluginInterface;
            CommandManager = commandManager;

            Configuration = PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
            Configuration.Initialize(PluginInterface);

            var imagePath = Path.Combine(PluginInterface.AssemblyLocation.Directory?.FullName!, "logo-ffxivita.svg");
            var ffxivitaImage = PluginInterface.UiBuilder.LoadImage(imagePath);
            
            WindowSystem.AddWindow(new ConfigWindow(this));
            WindowSystem.AddWindow(new MainWindow(this, ffxivitaImage));

            CommandManager.AddHandler(CommandName,
                                      new CommandInfo(OnCommand)
                                      {
                                          HelpMessage = "Un messaggio da visualizzare in /xlhelp"
                                      });
            PluginInterface.UiBuilder.Draw += DrawUI;
            PluginInterface.UiBuilder.OpenConfigUi += DrawConfigUI;
        }

        public void Dispose()
        {
            WindowSystem.RemoveAllWindows();
            CommandManager.RemoveHandler(CommandName);
        }

        private void OnCommand(string command, string args)
        {
            WindowSystem.GetWindow("La mia stupenda finestra").IsOpen = true;
        }

        private void DrawUI()
        {
            WindowSystem.Draw();
        }

        public void DrawConfigUI()
        {
            WindowSystem.GetWindow("Una stupenda finestra di configurazione").IsOpen = true;
        }
    }
}
