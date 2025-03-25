namespace WadAnalyzer;

public class WadSubSector
{
    public ushort SegCount;
    public ushort FirstSeg;

    public static WadSubSector[] FromWadLump(WadLump wadLump)
    {
        var segs = new WadSubSector[wadLump.Size / 4];
        for (var i = 0; i < segs.Length; i++)
        {
            segs[i] = new WadSubSector
            {
                SegCount = BitConverter.ToUInt16(wadLump.Data, i * 4),
                FirstSeg = BitConverter.ToUInt16(wadLump.Data, i * 4 + 2)
            };
        }

        return segs;
    }
}