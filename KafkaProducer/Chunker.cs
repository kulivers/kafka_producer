namespace FastScratchConsole;

public static class Chunker
{
    public static IEnumerable<Chunk> Chunk(this byte[] bytes, int chuckSize = 999900)
    {
        var chunkMax = bytes.Length % chuckSize == 0 ? bytes.Length / chuckSize : bytes.Length / chuckSize + 1;
        var chunkIdx = 0;
        var chunksCount = 0;
        for (var i = 0; i < bytes.Length; i++)
        {
            chunkIdx++;
            if (chunkIdx >= chuckSize || bytes.Length - i < chuckSize)
            {
                chunkIdx = 0;
                yield return new Chunk()
                {
                    Guid = new Guid(), Value = bytes.Skip(chunksCount++ * chuckSize).Take(chuckSize).ToArray(),
                    ChunkPosition = (i + 1) / chuckSize, ChunkMax = chunkMax
                };
            }
        }
    }
}

public class Chunk
{
    public Guid Guid { get; set; }
    public byte[] Value { get; set; }
    public int ChunkPosition { get; set; }
    public int ChunkMax { get; set; }
}