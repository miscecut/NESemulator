using NESEmulator.Cartridge.Mapper;
using NESEmulator.Cartridge.Mapper.NESMappers;
using System.IO;

namespace NESEmulator.Cartridge
{
    public class NESCartridge : ICartridge
    {
        private readonly byte[] _programMemory;
        private readonly byte[] _characterMemory;
        private readonly byte[] _trainer; //This is usually not present (AKA 0 Bytes)
        private readonly CartridgeHeader _header; //the first 16 Bytes of the .nes file
        private readonly byte _mapperId; //Information contained in the header
        private readonly IMapper _mapper;

        public NESCartridge(string filePath)
        {
            var allFileBytes = File.ReadAllBytes(filePath);
            var fileBytePointer = 0;

            //Header infos creation
            var headerBytes = BytesUtils.GetByteRange(allFileBytes, fileBytePointer, 16); //The first 16 Bytes are the header bytes
            _header = new CartridgeHeader(headerBytes);
            fileBytePointer = 16;

            //Trainer is populated with the next 512 Bytes of the file, if present
            _trainer = _header.IsTrainerPresent() ? BytesUtils.GetByteRange(allFileBytes, 16, 16 + 512) : new byte[0];
            fileBytePointer = _header.IsTrainerPresent() ? 528 : 16;

            //Mapper id is discovered
            _mapperId = _header.GetMapperId();

            //CHR & PRG memory size is discovered
            _programMemory = BytesUtils.GetByteRange(allFileBytes, fileBytePointer, fileBytePointer + (16 * 1024 * _header.SizeOfProgramROM)); //the size in the header is in 16 KB units
            fileBytePointer += (16 * 1024 * _header.SizeOfProgramROM);
            _characterMemory = BytesUtils.GetByteRange(allFileBytes, fileBytePointer, fileBytePointer + (8 * 1024 * _header.SizeOfCharacterROM)); //the size in the header is in 8 KB units
            fileBytePointer += (8 * 1024 * _header.SizeOfCharacterROM);

            _mapper = new Mapper000(_header.SizeOfProgramROM, _header.SizeOfCharacterROM); //TODO: SWITCH mappers
        }

        //Only for testing
        public NESCartridge(byte banksOfProgramRom, byte banksOfCharacterRom, IMapper mapper)
        {
            _mapper = mapper;
            _programMemory = new byte[16 * 1024 * banksOfProgramRom];
            _characterMemory = new byte[16 * 1024 * banksOfCharacterRom];
        }

        public bool CPURead(ushort address, ref byte data)
        {
            var mapperResponse = _mapper.CPUMapRead(address);
            if (mapperResponse.Consistent)
            {
                var actualAddress = mapperResponse.MappedAddress;
                data = _programMemory[actualAddress];
                return true;
            }
            return false;
        }

        public bool CPUWrite(ushort address, byte data)
        {
            var mapperResponse = _mapper.CPUMapRead(address);
            if (mapperResponse.Consistent)
            {
                var actualAddress = mapperResponse.MappedAddress;
                _programMemory[actualAddress] = data;
                return true;
            }
            return false;
        }

        public bool PPURead(ushort address, ref byte data)
        {
            var mapperResponse = _mapper.PPUMapRead(address);
            if (mapperResponse.Consistent)
            {
                var actualAddress = mapperResponse.MappedAddress;
                data = _characterMemory[actualAddress];
                return true;
            }
            return false;
        }

        public bool PPUWrite(ushort address, byte data)
        {
            var mapperResponse = _mapper.CPUMapRead(address);
            if (mapperResponse.Consistent)
            {
                var actualAddress = mapperResponse.MappedAddress;
                _characterMemory[actualAddress] = data;
                return true;
            }
            return false;
        }
    }
}
