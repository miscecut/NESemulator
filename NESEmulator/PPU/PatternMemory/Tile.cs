namespace NESEmulator.PPU.PatternMemory
{
    //A tile is the unit stored int he Pattern Memory, it's an 8x8 bitMap square
    public class Tile //AKA Sprite
    {
        //A bitmap in the NES is a couple of two bits (4 possibile values)
        //private readonly bool[,] _msb; //an 8x8 bit grid, MSB of the bitmaps
        //private readonly bool[,] _lsb; //an 8x8 bit grid, LSB of the bitmaps
        private readonly byte[] _tileContent; //it's 16 x 8 bits, so 16 bytes (2 grids)

        public Tile()
        {
            //_msb = new bool[8,8];
            //_lsb = new bool[8,8];
            _tileContent = new byte[16];
        }
    }
}
