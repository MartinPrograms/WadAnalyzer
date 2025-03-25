namespace WadAnalyzer;

public class WadSeg
{
    public ushort StartVertex;
    public ushort EndVertex;
    public short Angle;
    public ushort LineDef;
    public ushort Direction; // 0 = same as line, 1 = opposite
    public short Offset;
    
    public static WadSeg[] FromWadLump(WadLump wadLump)
    {
        var segs = new WadSeg[wadLump.Size / 12];
        for (int i = 0; i < segs.Length; i++)
        {
            segs[i] = new WadSeg
            {
                StartVertex = BitConverter.ToUInt16(wadLump.Data, i * 12),
                EndVertex = BitConverter.ToUInt16(wadLump.Data, i * 12 + 2),
                Angle = BitConverter.ToInt16(wadLump.Data, i * 12 + 4),
                LineDef = BitConverter.ToUInt16(wadLump.Data, i * 12 + 6),
                Direction = BitConverter.ToUInt16(wadLump.Data, i * 12 + 8),
                Offset = BitConverter.ToInt16(wadLump.Data, i * 12 + 10)
            };
        }

        return segs;
    }
}