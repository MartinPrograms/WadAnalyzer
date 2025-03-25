namespace WadAnalyzer.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void LoadWad()
    {
        var wad = WadFile.FromFile("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Ultimate Doom\\base\\DOOM.WAD"); // Path to DOOM.WAD, adjust as needed
        
        Assert.That(wad, Is.Not.Null);
        
        AssertHeader(wad.Header);
        AssertLumps(wad.Lumps);
        AssertLevels(wad.Levels);
        AssertTextures(wad.Textures);
        
        AssertExport("W67_1", wad);
    }

    private void AssertExport(string name, WadFile file)
    {
        var success = file.Patches[name].Export("./test.png", file.Palettes.First().Value.ToSkiaSharpPalette());
        Assert.That(success, Is.True);
    }

    private void AssertTextures(Dictionary<string, WadTexture> wadTextures)
    {
        Assert.That(wadTextures.Count, Is.GreaterThan(0));
        for (int i = 0; i < wadTextures.Count; i++)
        {
            var texture = wadTextures.ElementAt(i);

            Assert.That(texture.Value.Name, Is.Not.Empty);
        }
    }

    private void AssertLevels(Dictionary<string, WadLevel> wadLevels)
    {
        Assert.That(wadLevels.Count, Is.EqualTo(36));

        for (int i = 0; i < wadLevels.Count; i++)
        {
            var level = wadLevels.ElementAt(i);
            
            AssertThings(level);
            AssertLineDefs(level);
            AssertSideDefs(level);
            AssertVertexes(level);
            AssertSectors(level);
            AssertSubSectors(level);
            AssertNodes(level);
            AssertSegs(level);
            AssertBlockMap(level);
        }
    }

    private void AssertBlockMap(KeyValuePair<string, WadLevel> level)
    {
        Assert.That(level.Value.GetBlockMap(), Is.Not.Null);
        Assert.That(level.Value.GetBlockMap().Blocks.Count, Is.GreaterThan(0));
    }

    private void AssertSegs(KeyValuePair<string, WadLevel> level)
    {
        Assert.That(level.Value.GetSegs(), Is.Not.Null);
        Assert.That(level.Value.GetSegs().Length, Is.GreaterThan(0));
    }

    private void AssertNodes(KeyValuePair<string, WadLevel> level)
    {
        Assert.That(level.Value.GetNodes(), Is.Not.Null);
        Assert.That(level.Value.GetNodes().Length, Is.GreaterThan(0));
    }

    private void AssertSubSectors(KeyValuePair<string, WadLevel> level)
    {
        Assert.That(level.Value.GetSubSectors(), Is.Not.Null);
        Assert.That(level.Value.GetSubSectors().Length, Is.GreaterThan(0));
    }

    private void AssertSectors(KeyValuePair<string, WadLevel> level)
    {
        Assert.That(level.Value.GetSectors(), Is.Not.Null);
        Assert.That(level.Value.GetSectors().Length, Is.GreaterThan(0));
    }

    private void AssertVertexes(KeyValuePair<string, WadLevel> level)
    {
        Assert.That(level.Value.GetVertices(), Is.Not.Null);
        Assert.That(level.Value.GetVertices().Length, Is.GreaterThan(0));
    }

    private void AssertSideDefs(KeyValuePair<string, WadLevel> level)
    {
        Assert.That(level.Value.GetSideDefs(), Is.Not.Null);
        Assert.That(level.Value.GetSideDefs().Length, Is.GreaterThan(0));
    }

    private void AssertLineDefs(KeyValuePair<string, WadLevel> level)
    {
        Assert.That(level.Value.GetLineDefs(), Is.Not.Null);
        Assert.That(level.Value.GetLineDefs().Length, Is.GreaterThan(0));
    }

    private static void AssertThings(KeyValuePair<string, WadLevel> level)
    {
        Assert.That(level.Value.GetThings(), Is.Not.Null);
        Assert.That(level.Value.GetThings().Length, Is.GreaterThan(0));
    }

    private void AssertLumps(List<WadLump> wadEntries)
    {
        Assert.That(wadEntries.Count, Is.EqualTo(2306));
    }

    private void AssertHeader(WadHeader wadHeader)
    {
        Assert.That(wadHeader.WadType, Is.EqualTo(WadType.IWAD));
        Assert.That(wadHeader.NumLumps, Is.EqualTo(2306));
        Assert.That(wadHeader.InfoTableOffset, Is.EqualTo(12371396));
    }
}