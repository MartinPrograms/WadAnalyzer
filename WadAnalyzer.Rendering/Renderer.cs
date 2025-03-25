using System.ComponentModel;
using ImGuiNET;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using Silk.NET.OpenGL.Extensions.ImGui;

namespace WadAnalyzer.Rendering;

public class Renderer
{
    public string Name { get; }
    
    private IWindow _window;
    private ImGuiController _controller;
    private IInputContext _input;

    public bool IsInitialized => _window.IsInitialized;
    public GL GL { get; private set; }
    
    public Action<IInputContext,float> Update { get; set; }
    public Action Render { get; set; }
    public Action Load { get; set; }

    public Renderer(string name)
    {
        Name = name;
        
        _window = Window.Create(WindowOptions.Default with
        {
            API = GraphicsAPI.Default with { API = ContextAPI.OpenGL, Profile = ContextProfile.Core, Flags = ContextFlags.Debug, Version = new(4, 6) },
            Title = Name,
            Size = new(1280, 720),
            WindowState = WindowState.Maximized
        });
        
        _window.Closing += WindowOnClosing;
        _window.Load += WindowOnLoad;
        _window.Render += WindowOnRender;
        _window.Update += WindowOnUpdate;
        _window.Resize += WindowOnResize;
    }
    
    private void WindowOnResize(Vector2D<int> obj)
    {
        GL.Viewport(0, 0, (uint)obj.X, (uint)obj.Y);
    }

    private void WindowOnClosing()
    {
        
    }

    private void WindowOnUpdate(double obj)
    {
        Update?.Invoke(_input, (float) obj);
        _controller.Update((float) obj);
    }

    private void WindowOnRender(double obj)
    {        
        GL.Viewport(0, 0, (uint)_window.Size.X, (uint)_window.Size.Y);

        GL.Clear((uint) ClearBufferMask.ColorBufferBit);
        
        Render?.Invoke();
        
        _controller.Render();
    }

    private void WindowOnLoad()
    {
        GL = GL.GetApi(_window);
        _input = _window.CreateInput();
        _controller = new ImGuiController(GL, _window, _input);
        
        Load?.Invoke();
    }

    public void Run()
    {
        _window.Run();
    }

    public void Close()
    {
        _window.Close();
    }
}