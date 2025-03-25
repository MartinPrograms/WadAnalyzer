namespace WadAnalyzer;

/// <summary>
/// WAD file header.
/// </summary>
public class WadHeader
{
    public static int Offset => 12;
    public WadType WadType { get; set; } // 0-3 
    public int NumLumps { get; set; } // 4-7
    public int InfoTableOffset { get; set; } // 8-11

    public static WadHeader FromBytes(byte[] bytes)
    {
        return new WadHeader()
        {
            WadType = new string(bytes.Take(4).Select(x => (char)x).ToArray()).ToWadType(),
            NumLumps = BitConverter.ToInt32(bytes, 4),
            InfoTableOffset = BitConverter.ToInt32(bytes, 8),
        };
    }
}

public enum WadType
{
    IWAD,
    PWAD
}

public static class WadTypeExtensions
{
    public static string ToWadString(this WadType wadType)
    {
        return wadType switch
        {
            WadType.IWAD => "IWAD",
            WadType.PWAD => "PWAD",
            _ => throw new ArgumentOutOfRangeException(nameof(wadType), wadType, null)
        };
    }

    public static WadType ToWadType(this string wadString)
    {
        if (wadString == "IWAD")
        {
            return WadType.IWAD;
        }
        
        if (wadString == "PWAD")
        {
            return WadType.PWAD;
        }
        
        throw new ArgumentOutOfRangeException(nameof(wadString), wadString, null);
    }
}