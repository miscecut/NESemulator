using System.Collections.Generic;

namespace NESEmulator.PPU.PaletteMemory
{
    //0x3F00 to 0x3FFF
    public class NESPaletteMemory : IPaletteMemory
    {
        private readonly byte[] _colors;
        private readonly IList<ushort> _backgroundColorMirrorBytes;

        public NESPaletteMemory()
        {
            _colors = new byte[32];
            _backgroundColorMirrorBytes = new ushort[] { 0x0000, 0x0004, 0x0008, 0x000C, 0x0010, 0x0014, 0x0018, 0x001C }; //these bytes reflect the background color, so when something is written in one of these addresses, it is written in the others too
        }

        public byte Read(ushort address)
        {
            return _colors[address & 0x001F];
        }

        public void Write(ushort address, byte data)
        {
            ushort effectiveAddress = (ushort)(address % 0x001F); //only 32 Bytes
            _colors[effectiveAddress] = data;

            if (_backgroundColorMirrorBytes.Contains(effectiveAddress)) //if an addressing mirroring the background is written, the other mirrors are written
                FillBackgroundColor(data);
        }

        private void FillBackgroundColor(byte data)
        {
            foreach(ushort address in _backgroundColorMirrorBytes)
                _colors[address] = data;
        }
    }
}
