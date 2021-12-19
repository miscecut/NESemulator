namespace NESEmulator.PPU.PaletteMemory
{

    public class NESPaletteMemory
    {
        private byte _backGroundColor; //address 0x3F00, 256 colors
        private byte[,] _patterns; //8 patterns composed by 4 bytes (3 colors and a blank one pointing to the backgroundcolor

        public NESPaletteMemory()
        {
            _backGroundColor = 0x00;
            _patterns = new byte[8, 3]; //there is a 4th byte, but it's always equal to the background color
        }
    }
}
