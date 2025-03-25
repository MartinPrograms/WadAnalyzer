namespace WadAnalyzer;

public class WadBlockMap
{
    public short XOrigin { get; set; }
    public short YOrigin { get; set; }
    public short Width { get; set; }
    public short Height { get; set; }
    public List<ushort> Blocks;

    public static WadBlockMap FromWadLump(WadLump wadLump)
    {
        var blockMap = new WadBlockMap();
        blockMap.XOrigin = BitConverter.ToInt16(wadLump.Data, 0);
        blockMap.YOrigin = BitConverter.ToInt16(wadLump.Data, 2);
        blockMap.Width = BitConverter.ToInt16(wadLump.Data, 4);
        blockMap.Height = BitConverter.ToInt16(wadLump.Data, 6);
        blockMap.Blocks = new List<ushort>();
        for (int i = 8; i < wadLump.Data.Length; i += 2)
        {
            var data = (BitConverter.ToUInt16(wadLump.Data, i));
            if (data != 0xFFFF)
            {
                blockMap.Blocks.Add(data);
            }
        }
        return blockMap;
    }
}