using System.Numerics;

namespace WadAnalyzer.Rendering;

public class CpuWadRenderer
{
    public const int TEXTURE_SIZE = 64; // 64x64 texture
    public const float TOLERANCE = 0.1f;
    public const int MAX_PORTAL_RECURSION = 16;
    public const int MAX_DRAW_DISTANCE = 1024;

    private readonly Texture _renderTexture;
    public uint Renderable => _renderTexture.ID;

    private readonly WadFile _wad;
    private readonly WadLevel _level;

    private readonly WadNode[] _nodes;
    private readonly WadLineDef[] _lineDefs;
    private readonly WadSideDef[] _sideDefs;
    private readonly WadSector[] _sectors;
    private readonly WadVertex[] _vertices;
    private readonly WadThing[] _things;

    public (Vector2 Position, float Angle, float Fov) Camera = (Vector2.Zero, 0f, 90f);
    private float _wallHeight = 512 / 2f;


    public CpuWadRenderer(WadFile wad, string level, Texture renderTexture)
    {
        _wad = wad;
        _level = wad.Levels[level];
        _renderTexture = renderTexture;

        _nodes = _level.GetNodes()!;
        _lineDefs = _level.GetLineDefs()!;
        _sideDefs = _level.GetSideDefs()!;
        _sectors = _level.GetSectors()!;
        _vertices = _level.GetVertices()!;
        _things = _level.GetThings()!;
    }

    public void Render()
    {
        int width = (int)_renderTexture.Width;
        int height = (int)_renderTexture.Height;
        byte[] pixels = new byte[width * height * 4];
        
        
        
        _renderTexture.SetData(pixels);
    }
    
    private Color GetTexturePixel(WadTexture texture, int x, int y)
    {
        if (texture.ResolvedPatches.Count == 0)
        {
            return new Color(0,0,0) { A = 0 };
        }
        var patch = texture.ResolvedPatches[0];
        var palette = _wad.Palettes.First().Value;
        if (x >= patch.Width || y >= patch.Height)
        {
            return new Color(0,0,0) { A = 0 };
        }
        int pixel = patch.Pixels[x][y];
        return palette.GetDefaultPalette()[pixel];
    }
}