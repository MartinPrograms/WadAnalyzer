namespace WadAnalyzer;

public class WadLevel
{
    public List<WadLump> Lumps { get; set; }
    
    public WadLevel()
    {
        Lumps = new List<WadLump>();
    }
    
    private WadThing[]? _cachedThings;
    public WadThing[]? GetThings() => _cachedThings ??= Lumps.FirstOrDefault(x => x.Name == "THINGS")?.GetThings();
    
    private WadLineDef[]? _cachedLineDefs;
    public WadLineDef[]? GetLineDefs() => _cachedLineDefs ??= Lumps.FirstOrDefault(x => x.Name == "LINEDEFS")?.GetLineDefs();
    
    private WadSideDef[]? _cachedSideDefs;
    public WadSideDef[]? GetSideDefs() => _cachedSideDefs ??= Lumps.FirstOrDefault(x => x.Name == "SIDEDEFS")?.GetSideDefs();
    
    private WadVertex[]? _cachedVertices;
    public WadVertex[]? GetVertices() => _cachedVertices ??= Lumps.FirstOrDefault(x => x.Name == "VERTEXES")?.GetVertices();
    
    private WadSector[]? _cachedSectors;
    public WadSector[]? GetSectors() => _cachedSectors ??= Lumps.FirstOrDefault(x => x.Name == "SECTORS")?.GetSectors();
    
    private WadSeg[]? _cachedSegs;
    public WadSeg[]? GetSegs() => _cachedSegs ??= Lumps.FirstOrDefault(x => x.Name == "SEGS")?.GetSegs();
    
    private WadSubSector[]? _cachedSubSectors;
    public WadSubSector[]? GetSubSectors() => _cachedSubSectors ??= Lumps.FirstOrDefault(x => x.Name == "SSECTORS")?.GetSubSectors();
    
    private WadNode[]? _cachedNodes;
    public WadNode[]? GetNodes() => _cachedNodes ??= Lumps.FirstOrDefault(x => x.Name == "NODES")?.GetNodes();
    
    private WadBlockMap? _cachedBlockMap;
    public WadBlockMap? GetBlockMap() => _cachedBlockMap ??= Lumps.FirstOrDefault(x => x.Name == "BLOCKMAP")?.GetBlockMap();

    public WadSubSector? GetSubSector(int subsectorIndex)
    {
        return _cachedSubSectors.ElementAtOrDefault(subsectorIndex);
    }
}