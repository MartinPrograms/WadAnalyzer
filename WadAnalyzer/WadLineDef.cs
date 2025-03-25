namespace WadAnalyzer;

public class WadLineDef
{
    public ushort StartVertex { get; set; }
    public ushort EndVertex { get; set; }
    public ushort Flags { get; set; }
    public ushort Action { get; set; }
    public ushort Tag { get; set; }
    public ushort RightSidedef { get; set; }
    public ushort LeftSidedef { get; set; }

    public static WadLineDef[] FromWadLump(WadLump wadLump)
    {
        // 14 bytes per linedef
        var linedefs = new WadLineDef[wadLump.Size / 14];
        
        for (var i = 0; i < linedefs.Length; i++)
        {
            var offset = i * 14;
            linedefs[i] = new WadLineDef
            {
                StartVertex = BitConverter.ToUInt16(wadLump.Data, offset),
                EndVertex = BitConverter.ToUInt16(wadLump.Data, offset + 2),
                Flags = BitConverter.ToUInt16(wadLump.Data, offset + 4),
                Action = BitConverter.ToUInt16(wadLump.Data, offset + 6),
                Tag = BitConverter.ToUInt16(wadLump.Data, offset + 8),
                RightSidedef = BitConverter.ToUInt16(wadLump.Data, offset + 10),
                LeftSidedef = BitConverter.ToUInt16(wadLump.Data, offset + 12)
            };
        }
        
        return linedefs;
    }
}