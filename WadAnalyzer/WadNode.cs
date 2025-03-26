namespace WadAnalyzer;

public class WadNodeBounds
{
    public short XMin { get; set; }
    public short YMin { get; set; }
    public short XMax { get; set; }
    public short YMax { get; set; }
}

public class WadNode
{
    public short PartitionLineX { get; set; }
    public short PartitionLineY { get; set; }
    public short DeltaX { get; set; }
    public short DeltaY { get; set; }
    
    public WadNodeBounds RightChildBounds { get; set; }
    public WadNodeBounds LeftChildBounds { get; set; }
    
    public ushort RightChild { get; set; }
    public ushort LeftChild { get; set; }

    public static WadNode[] FromWadLump(WadLump wadLump)
    {
        var nodes = new WadNode[wadLump.Size / 28];
        for (var i = 0; i < nodes.Length; i++)
        {
            nodes[i] = new WadNode
            {
                PartitionLineX = BitConverter.ToInt16(wadLump.Data, i * 28),
                PartitionLineY = BitConverter.ToInt16(wadLump.Data, i * 28 + 2),
                DeltaX = BitConverter.ToInt16(wadLump.Data, i * 28 + 4),
                DeltaY = BitConverter.ToInt16(wadLump.Data, i * 28 + 6),
                RightChildBounds = new WadNodeBounds
                {
                    XMin = BitConverter.ToInt16(wadLump.Data, i * 28 + 8),
                    YMin = BitConverter.ToInt16(wadLump.Data, i * 28 + 10),
                    XMax = BitConverter.ToInt16(wadLump.Data, i * 28 + 12),
                    YMax = BitConverter.ToInt16(wadLump.Data, i * 28 + 14)
                },
                LeftChildBounds = new WadNodeBounds
                {
                    XMin = BitConverter.ToInt16(wadLump.Data, i * 28 + 16),
                    YMin = BitConverter.ToInt16(wadLump.Data, i * 28 + 18),
                    XMax = BitConverter.ToInt16(wadLump.Data, i * 28 + 20),
                    YMax = BitConverter.ToInt16(wadLump.Data, i * 28 + 22)
                },
                RightChild = BitConverter.ToUInt16(wadLump.Data, i * 28 + 24),
                LeftChild = BitConverter.ToUInt16(wadLump.Data, i * 28 + 26)
            };
        }
        
        return nodes;
    }
}