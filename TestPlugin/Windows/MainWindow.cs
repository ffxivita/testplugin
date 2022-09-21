using System;
using System.Numerics;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using ImGuiScene;

namespace TestPlugin.Windows
{
    public class MainWindow : Window, IDisposable
    {
        private TextureWrap FfxivitaImage;
        private Plugin Plugin;

        public MainWindow(Plugin plugin, TextureWrap ffxivitaImage) : base(
            "La mia favolosa finestra",
            ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse)
        {
            SizeConstraints = new WindowSizeConstraints
            {
                MinimumSize = new Vector2(375, 330),
                MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
            };

            FfxivitaImage = ffxivitaImage;
            Plugin = plugin;
        }

        public void Dispose()
        {
            FfxivitaImage.Dispose();
        }

        public override void Draw()
        {
            ImGui.Text($"The random config bool is {Plugin.Configuration.SomePropertyToBeSavedAndWithADefault}");
            
            if (ImGui.Button("Mostra Impostazioni"))
            {
                this.Plugin.DrawConfigUI();
            }
            
            ImGui.Spacing();
            
            ImGui.Text("Powered by FFXIVITA");
            ImGui.Indent(55);
            ImGui.Image(FfxivitaImage.ImGuiHandle, new Vector2(FfxivitaImage.Width, FfxivitaImage.Height));
            ImGui.Unindent(55);
        }
    }
}
