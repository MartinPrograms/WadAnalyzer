using Silk.NET.OpenGL;

namespace WadAnalyzer.Rendering;

public class Texture
{
    public uint ID { get; set; }
    public uint Width { get; set; }
    public uint Height { get; set; }

    private GL gl;
    public unsafe Texture(GL gl, uint width, uint height)
    {
        this.gl = gl;
        Width = width;
        Height = height;
        ID = gl.GenTexture();
        gl.BindTexture(TextureTarget.Texture2D, ID);
        gl.TexImage2D(TextureTarget.Texture2D, 0, (int)InternalFormat.Rgba, width, height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, null);
        gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
        gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
        gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
    }

    public unsafe void SetData(byte[] data)
    {
        gl.BindTexture(TextureTarget.Texture2D, ID);
        fixed (byte* ptr = data)
        {
            gl.TexImage2D(TextureTarget.Texture2D, 0, (int)InternalFormat.Rgba, Width, Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, ptr);
        }
    }
    
    public unsafe void Resize(uint width, uint height)
    {
        Width = width;
        Height = height;
        gl.BindTexture(TextureTarget.Texture2D, ID);
        gl.TexImage2D(TextureTarget.Texture2D, 0, (int)InternalFormat.Rgba, width, height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, null);
    }

    public void Bind(int spot)
    {
        gl.ActiveTexture(TextureUnit.Texture0 + spot);
        gl.BindTexture(TextureTarget.Texture2D, ID);
    }
    
    public void Dispose()
    {
        gl.DeleteTexture(ID);
    }

    public unsafe void Clear()
    {
        gl.BindTexture(TextureTarget.Texture2D, ID);
        gl.TexImage2D(TextureTarget.Texture2D, 0, (int)InternalFormat.Rgba, Width, Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, null);
    }
}