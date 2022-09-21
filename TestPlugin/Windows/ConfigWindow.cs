using System;
using System.Numerics;
using Dalamud.Interface.Windowing;
using ImGuiNET;

namespace TestPlugin.Windows
{
    public class ConfigWindow : Window, IDisposable
    {
        private Configuration Configuration;

        public ConfigWindow(Plugin plugin) : base(
            "Una bellissima finestra di configurazione",
            ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoScrollbar |
            ImGuiWindowFlags.NoScrollWithMouse)
        {
            Size = new Vector2(232, 75);
            SizeCondition = ImGuiCond.Always;
            Configuration = plugin.Configuration;
        }
        
        public void Dispose() { }

        public override void Draw()
        {
            var configValue = Configuration.SomePropertyToBeSavedAndWithADefault;
            if (ImGui.Checkbox("Bool di config Random", ref configValue))
            {
                Configuration.SomePropertyToBeSavedAndWithADefault = configValue;
                Configuration.Save();
            }
        }
    }
}
