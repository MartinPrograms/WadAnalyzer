using System.Text;

namespace WadAnalyzer;

public class WadSideDef
{
    public short XOffset { get; set; }
    public short YOffset { get; set; }
    public string UpperTexture { get; set; }
    public string LowerTexture { get; set; }
    public string MiddleTexture { get; set; }
    public short Sector { get; set; }


    public static WadSideDef[] FromWadLump(WadLump wadLump)
    {
        var sideDefs = new WadSideDef[wadLump.Size / 30];
        for (int i = 0; i < sideDefs.Length; i++)
        {
            sideDefs[i] = new WadSideDef
            {
                XOffset = BitConverter.ToInt16(wadLump.Data, i * 30),
                YOffset = BitConverter.ToInt16(wadLump.Data, i * 30 + 2),
                UpperTexture = Encoding.ASCII.GetString(wadLump.Data, i * 30 + 4, 8).TrimEnd('\0'),
                LowerTexture = Encoding.ASCII.GetString(wadLump.Data, i * 30 + 12, 8).TrimEnd('\0'),
                MiddleTexture = Encoding.ASCII.GetString(wadLump.Data, i * 30 + 20, 8).TrimEnd('\0'),
                Sector = BitConverter.ToInt16(wadLump.Data, i * 30 + 28)
            };
        }

        return sideDefs;
    }
}