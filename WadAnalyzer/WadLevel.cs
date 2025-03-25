namespace WadAnalyzer;

public class WadLevel
{
    public List<WadLump> Lumps { get; set; }
    
    public WadLevel()
    {
        Lumps = new List<WadLump>();
    }
    
    public WadThing[]? GetThings() => Lumps.FirstOrDefault(x => x.Name == "THINGS")?.GetThings();
    public WadLineDef[]? GetLineDefs() => Lumps.FirstOrDefault(x => x.Name == "LINEDEFS")?.GetLineDefs();
    public WadSideDef[]? GetSideDefs() => Lumps.FirstOrDefault(x => x.Name == "SIDEDEFS")?.GetSideDefs();
    public WadVertex[]? GetVertices() => Lumps.FirstOrDefault(x => x.Name == "VERTEXES")?.GetVertices();
    public WadSector[]? GetSectors() => Lumps.FirstOrDefault(x => x.Name == "SECTORS")?.GetSectors();
    public WadSeg[]? GetSegs() => Lumps.FirstOrDefault(x => x.Name == "SEGS")?.GetSegs();
    public WadSubSector[]? GetSubSectors() => Lumps.FirstOrDefault(x => x.Name == "SSECTORS")?.GetSubSectors();
    public WadNode[]? GetNodes() => Lumps.FirstOrDefault(x => x.Name == "NODES")?.GetNodes();
    public WadBlockMap? GetBlockMap() => Lumps.FirstOrDefault(x => x.Name == "BLOCKMAP")?.GetBlockMap();
}