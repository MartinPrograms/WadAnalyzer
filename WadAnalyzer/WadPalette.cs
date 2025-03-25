using SkiaSharp;

namespace WadAnalyzer;

public class Color
{
    public byte R;
    public byte G;
    public byte B;
    public byte A = 255;
    
    public Color(byte r, byte g, byte b)
    {
        R = r;
        G = g;
        B = b;
    }
}

/// <summary>
/// Think a playpal lump in a WAD file.
/// </summary>
public class WadPalette
{
    public string Name { get; set; } = "PLAYPAL";
    public List<Color[]> Palettes  { get; set; } = new List<Color[]>();

    public const int ColorsPerPalette = 256;
    public const int BytesPerColor = 3;
    public const int PaletteSize = ColorsPerPalette * BytesPerColor;
    public const int TotalPalettes = 14;
    
    public static WadPalette FromWadLump(WadLump lump)
    {
        var palette = new WadPalette();
        byte[] data = lump.Data;
        
        // PLAYPAL contains 14 palettes (0-13)
        for (int paletteNum = 0; paletteNum < TotalPalettes; paletteNum++)
        {
            int paletteOffset = paletteNum * PaletteSize;
            Color[] colors = new Color[ColorsPerPalette];
            
            for (int i = 0; i < ColorsPerPalette; i++)
            {
                int offset = paletteOffset + (i * BytesPerColor);
                colors[i] = new Color(
                    data[offset],     // R
                    data[offset + 1], // G
                    data[offset + 2]  // B
                );
            }
            
            palette.Palettes.Add(colors);
        }
        
        return palette;
    }
    
    public Color[] GetDefaultPalette()
    {
        return Palettes[0];
    }
    
    public SKColor[] GetSkiaPalette()
    {
        var skiaPalette = new SKColor[ColorsPerPalette];
        Color[] colors = GetDefaultPalette();
        
        for (int i = 0; i < ColorsPerPalette; i++)
        {
            Color color = colors[i];
            skiaPalette[i] = new SKColor(color.R, color.G, color.B);
        }
        
        return skiaPalette;
    }
}