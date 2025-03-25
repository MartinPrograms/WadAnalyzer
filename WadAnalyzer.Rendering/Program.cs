

using System.Numerics;
using ImGuiNET;
using Silk.NET.Input;
using WadAnalyzer;
using WadAnalyzer.Rendering;

var renderer = new Renderer("WAD Analyzer");
var wad = WadFile.FromFile("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Ultimate Doom\\base\\DOOM.WAD"); // Path to DOOM.WAD, adjust as needed
CpuWadRenderer cpuRenderer = null!;

// We now have a simple wad file. To render it we must mess around a bit.

renderer.Load += () =>
{
    var texture = new Texture(renderer.GL, 512, 512);
    cpuRenderer = new CpuWadRenderer(wad, "E1M1", texture);
};

renderer.Update += (context, f) =>
{
    if (context.Keyboards[0].IsKeyPressed(Key.Escape))
    {
        renderer.Close();
    }

    // Do input stuff here
    Vector2 newMovement = Vector2.Zero;
    const float movementSpeed = 600;
    
    if (context.Keyboards[0].IsKeyPressed(Key.W))
    {
        newMovement += new Vector2(1, 0);
    }
    
    if (context.Keyboards[0].IsKeyPressed(Key.S))
    {
        newMovement += new Vector2(-1, 0);
    }
    
    if (context.Keyboards[0].IsKeyPressed(Key.A))
    {
        newMovement += new Vector2(0, -1);
    }
    
    if (context.Keyboards[0].IsKeyPressed(Key.D))
    {
        newMovement += new Vector2(0, 1);
    }
    
    // Rotate newMovement by the camera angle
    newMovement = Vector2.Transform(newMovement, Matrix3x2.CreateRotation(cpuRenderer.Camera.Angle));
    newMovement *= movementSpeed;
    newMovement *= (float)f;
    
    cpuRenderer.Camera.Position += newMovement;
    
    float newRotation = 0;
    
    if (context.Keyboards[0].IsKeyPressed(Key.Left))
    {
        newRotation -= 1;
    }
    
    if (context.Keyboards[0].IsKeyPressed(Key.Right))
    {
        newRotation += 1;
    }
    
    cpuRenderer.Camera.Angle += newRotation * (float)f;
    
    
};

renderer.Render += () =>
{    
    cpuRenderer.Render(); // Render once, because im stupid and its slow

    ImGui.Begin("Renderer");

    var size = ImGui.GetContentRegionAvail();
    
    ImGui.Image(new IntPtr(cpuRenderer.Renderable), size, new Vector2(0, 1), new Vector2(1, 0));
    
    ImGui.End();
};

renderer.Run();