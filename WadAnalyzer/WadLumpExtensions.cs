namespace WadAnalyzer;

public static class WadLumpExtensions
{
    public static WadThing[] GetThings(this WadLump wadLump)
    {
        return WadThing.FromWadLump(wadLump);
    }

    public static WadLineDef[] GetLineDefs(this WadLump wadLump)
    {
        return WadLineDef.FromWadLump(wadLump);
    }
    
    public static WadSideDef[] GetSideDefs(this WadLump wadLump)
    {
        return WadSideDef.FromWadLump(wadLump);
    }
    
    public static WadVertex[] GetVertices(this WadLump wadLump)
    {
        return WadVertex.FromWadLump(wadLump);
    }
    
    public static WadSector[] GetSectors(this WadLump wadLump)
    {
        return WadSector.FromWadLump(wadLump);
    }
    
    public static WadNode[] GetNodes(this WadLump wadLump)
    {
        return WadNode.FromWadLump(wadLump);
    }
    
    public static WadSubSector[] GetSubSectors(this WadLump wadLump)
    {
        return WadSubSector.FromWadLump(wadLump);
    }
    
    public static WadSeg[] GetSegs(this WadLump wadLump)
    {
        return WadSeg.FromWadLump(wadLump);
    }
    
    public static WadBlockMap GetBlockMap(this WadLump wadLump)
    {
        return WadBlockMap.FromWadLump(wadLump);
    }
    
    public static WadTexture[] GetTextures(this WadLump wadLump)
    {
        return WadTexture.FromWadLump(wadLump);
    }
}