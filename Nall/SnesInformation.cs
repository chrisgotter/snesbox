using System;

namespace Nall
{
    public class SnesInformation
    {
        public string xml_memory_map;

        public SnesInformation(byte[] data, uint size) { throw new NotImplementedException(); }

        private void read_header(byte[] data, uint size) { throw new NotImplementedException(); }
        private uint find_header(byte[] data, uint size) { throw new NotImplementedException(); }
        private uint score_header(byte[] data, uint size, uint addr) { throw new NotImplementedException(); }
        private uint gameboy_ram_size(byte[] data, uint size) { throw new NotImplementedException(); }
        private bool gameboy_has_rtc(byte[] data, uint size) { throw new NotImplementedException(); }

        private enum HeaderField { CartName = 0x00, Mapper = 0x15, RomType = 0x16, RomSize = 0x17, RamSize = 0x18, CartRegion = 0x19, Company = 0x1a, Version = 0x1b, Complement = 0x1c, Checksum = 0x1e, ResetVector = 0x3c }
        private enum Mode { ModeNormal, ModeBsxSlotted, ModeBsx, ModeSufamiTurbo, ModeSuperGameBoy }
        private enum Type { TypeNormal, TypeBsxSlotted, TypeBsxBios, TypeBsx, TypeSufamiTurboBios, TypeSufamiTurbo, TypeSuperGameBoy1Bios, TypeSuperGameBoy2Bios, TypeGameBoy, TypeUnknown }
        private enum Region { NTSC, PAL }
        private enum MemoryMapper { LoROM, HiROM, ExLoROM, ExHiROM, SuperFXROM, SA1ROM, SPC7110ROM, BSCLoROM, BSCHiROM, BSXROM, STROM }
        private enum DSP1MemoryMapper { DSP1Unmapped, DSP1LoROM1MB, DSP1LoROM2MB, DSP1HiROM }

        private bool loaded;        //is a base cartridge inserted?
        private uint crc32;     //crc32 of all cartridges (base+slot(s))
        private uint rom_size;
        private uint ram_size;

        private Mode mode;
        private Type type;
        private Region region;
        private MemoryMapper mapper;
        private DSP1MemoryMapper dsp1_mapper;

        private bool has_bsx_slot;
        private bool has_superfx;
        private bool has_sa1;
        private bool has_srtc;
        private bool has_sdd1;
        private bool has_spc7110;
        private bool has_spc7110rtc;
        private bool has_cx4;
        private bool has_dsp1;
        private bool has_dsp2;
        private bool has_dsp3;
        private bool has_dsp4;
        private bool has_obc1;
        private bool has_st010;
        private bool has_st011;
        private bool has_st018;
    }
}