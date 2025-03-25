using System.Text;

namespace WadAnalyzer;

public class WadSector
{
    public short FloorHeight { get; set; }
    public short CeilingHeight { get; set; }
    public string FloorTexture { get; set; }
    public string CeilingTexture { get; set; }
    public short LightLevel { get; set; }
    public short SpecialType { get; set; }
    public short Tag { get; set; }

    public static WadSector[] FromWadLump(WadLump wadLump)
    {
        WadSector[] sectors = new WadSector[wadLump.Size / 26];
        
        for (int i = 0; i < sectors.Length; i++)
        {
            sectors[i] = new WadSector
            {
                FloorHeight = BitConverter.ToInt16(wadLump.Data, i * 26),
                CeilingHeight = BitConverter.ToInt16(wadLump.Data, i * 26 + 2),
                FloorTexture = Encoding.ASCII.GetString(wadLump.Data, i * 26 + 4, 8).TrimEnd('\0'),
                CeilingTexture = Encoding.ASCII.GetString(wadLump.Data, i * 26 + 12, 8).TrimEnd('\0'),
                LightLevel = BitConverter.ToInt16(wadLump.Data, i * 26 + 20),
                SpecialType = BitConverter.ToInt16(wadLump.Data, i * 26 + 22),
                Tag = BitConverter.ToInt16(wadLump.Data, i * 26 + 24)
            };
        }
        
        return sectors;
    }
}