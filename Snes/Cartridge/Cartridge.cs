using System;
using Snes.Memory;

namespace Snes.Cartridge
{
    partial class Cartridge
    {
        public enum Mode : uint
        {
            Normal,
            BsxSlotted,
            Bsx,
            SufamiTurbo,
            SuperGameBoy,
        }

        public enum Region : uint
        {
            NTSC,
            PAL,
        }

        public enum SuperGameBoyVersion : uint
        {
            Version1,
            Version2,
        }

        //assigned externally to point to file-system datafiles (msu1 and serial)
        //example: "/path/to/filename.sfc" would set this to "/path/to/filename"
        public string basename { get; set; }

        public bool loaded { get; private set; }
        public uint crc32 { get; private set; }
        public string sha256 { get; private set; }

        public Mode mode { get; private set; }
        public Region region { get; private set; }
        public uint ram_size { get; private set; }
        public uint spc7110_data_rom_offset { get; private set; }
        public SuperGameBoyVersion supergameboy_version { get; private set; }
        public uint supergameboy_ram_size { get; private set; }
        public uint supergameboy_rtc_size { get; private set; }
        public uint serial_baud_rate { get; private set; }

        public bool has_bsx_slot { get; private set; }
        public bool has_superfx { get; private set; }
        public bool has_sa1 { get; private set; }
        public bool has_srtc { get; private set; }
        public bool has_sdd1 { get; private set; }
        public bool has_spc7110 { get; private set; }
        public bool has_spc7110rtc { get; private set; }
        public bool has_cx4 { get; private set; }
        public bool has_dsp1 { get; private set; }
        public bool has_dsp2 { get; private set; }
        public bool has_dsp3 { get; private set; }
        public bool has_dsp4 { get; private set; }
        public bool has_obc1 { get; private set; }
        public bool has_st0010 { get; private set; }
        public bool has_st0011 { get; private set; }
        public bool has_st0018 { get; private set; }
        public bool has_msu1 { get; private set; }
        public bool has_serial { get; private set; }

        public Mapping[] mapping;

        public void load(Mode cartridge_mode, string xml_list) { throw new NotImplementedException(); }
        public void unload() { throw new NotImplementedException(); }

        public Cartridge() { throw new NotImplementedException(); }

        //TODO: Serialize
        //private void parse_xml(const lstring&);
        //private void parse_xml_cartridge(const char*);
        //private void parse_xml_bsx(const char*);
        //private void parse_xml_sufami_turbo(const char*, bool);
        //private void parse_xml_gameboy(const char*);

        //private void xml_parse_rom(xml_element&);
        //private void xml_parse_ram(xml_element&);
        //private void xml_parse_superfx(xml_element&);
        //private void xml_parse_sa1(xml_element&);
        //private void xml_parse_bsx(xml_element&);
        //private void xml_parse_sufamiturbo(xml_element&);
        //private void xml_parse_supergameboy(xml_element&);
        //private void xml_parse_srtc(xml_element&);
        //private void xml_parse_sdd1(xml_element&);
        //private void xml_parse_spc7110(xml_element&);
        //private void xml_parse_cx4(xml_element&);
        //private void xml_parse_necdsp(xml_element&);
        //private void xml_parse_obc1(xml_element&);
        //private void xml_parse_setadsp(xml_element&);
        //private void xml_parse_setarisc(xml_element&);
        //private void xml_parse_msu1(xml_element&);
        //private void xml_parse_serial(xml_element&);

        //void xml_parse_address(Mapping&, const string&);
        //void xml_parse_mode(Mapping&, const string&);
    }
}
