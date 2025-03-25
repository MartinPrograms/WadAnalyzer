using System.Text.RegularExpressions;
using SkiaSharp;

namespace WadAnalyzer;

public static class WadExtensions
{
    /// <summary>
    /// Checks if a name matches a level marker.
    /// This does not guarantee that the lump is a level marker, but it is a good indicator.
    /// </summary>
    /// <param name="lump">A lump to check.</param>
    /// <returns>True if the lump is a level marker, false otherwise.</returns>
    public static bool IsLevelMarker(this WadLump lump)
    {
        string name = lump.Name;
        bool isDoom1Marker = Regex.IsMatch(name, @"E\dM\d", RegexOptions.IgnoreCase);
        bool isDoom2Marker = Regex.IsMatch(name, @"MAP\d\d", RegexOptions.IgnoreCase);
        
        if (isDoom1Marker && lump.Name.StartsWith("D_E", StringComparison.OrdinalIgnoreCase))
            return false; // This is a music lump, not a level marker.
        
        return isDoom1Marker || isDoom2Marker;
    }

    /// <summary>
    /// Checks if a lump can be a property of a level, for example a THINGS lump.
    /// </summary>
    /// <param name="lump></param>
    /// <returns></returns>
    public static bool IsLevelProperty(this WadLump lump)
    {
        string name = lump.Name;
        bool isLevelMarker = lump.IsLevelMarker();
        bool isThings = name.Equals("THINGS", StringComparison.OrdinalIgnoreCase);
        bool isLineDefs = name.Equals("LINEDEFS", StringComparison.OrdinalIgnoreCase);
        bool isSideDefs = name.Equals("SIDEDEFS", StringComparison.OrdinalIgnoreCase);
        bool isVertexes = name.Equals("VERTEXES", StringComparison.OrdinalIgnoreCase);
        bool isSectors = name.Equals("SECTORS", StringComparison.OrdinalIgnoreCase);
        bool isSegs = name.Equals("SEGS", StringComparison.OrdinalIgnoreCase);
        bool isSsectors = name.Equals("SSECTORS", StringComparison.OrdinalIgnoreCase);
        bool isNodes = name.Equals("NODES", StringComparison.OrdinalIgnoreCase);
        bool isReject = name.Equals("REJECT", StringComparison.OrdinalIgnoreCase);
        bool isBlockMap = name.Equals("BLOCKMAP", StringComparison.OrdinalIgnoreCase);
        return isLevelMarker || isThings || isLineDefs || isSideDefs || isVertexes || isSectors || isSegs || isSsectors || isNodes || isReject || isBlockMap;
    }
    
    public static bool IsTexture(this WadLump lump)
    {
        string name = lump.Name;
        return name.StartsWith("TEXTURE", StringComparison.OrdinalIgnoreCase);
    }

    public static bool IsPalette(this WadLump lump)
    {
        string name = lump.Name;
        return name.StartsWith("PLAYPAL", StringComparison.OrdinalIgnoreCase);
    }
    
    public static SKColor[] ToSkiaSharpPalette(this WadPalette palette)
    {
        return palette.GetSkiaPalette();
    }
}