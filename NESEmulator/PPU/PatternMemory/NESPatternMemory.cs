namespace NESEmulator.PPU.PatternMemory
{
    public class NESPatternMemory
    {
        private readonly Tile[,] _tiles; //a 8x16 grid of tiles, each one is a 8x8 bitMap (4 possible values, 2 bits), a tile is 16 Bytes

        public NESPatternMemory()
        {
            _tiles = new Tile[8, 16];
        }
    }
}
