namespace WadAnalyzer;

public class WadVertex
{
    public short X { get; set; }
    public short Y { get; set; }
    
    public static WadVertex[] FromWadLump(WadLump wadLump)
    {
        var vertices = new WadVertex[wadLump.Size / 4];
        for (var i = 0; i < vertices.Length; i++)
        {
            vertices[i] = new WadVertex
            {
                X = BitConverter.ToInt16(wadLump.Data, i * 4),
                Y = BitConverter.ToInt16(wadLump.Data, i * 4 + 2)
            };
        }

        return vertices;
    }
}