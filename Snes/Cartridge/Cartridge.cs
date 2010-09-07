﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using Nall;
using Snes.Chip.SuperFX;
using Snes.Memory;

namespace Snes.Cartridge
{
    partial class Cartridge
    {
        public static Cartridge cartridge = new Cartridge();

        public enum Mode : uint { Normal, BsxSlotted, Bsx, SufamiTurbo, SuperGameBoy }
        public enum Region : uint { NTSC, PAL }
        public enum SuperGameBoyVersion : uint { Version1, Version2 }

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

        public Collection<Mapping> mapping = new Collection<Mapping>();

        public void load(Mode cartridge_mode, string[] xml_list)
        {
            mode = cartridge_mode;
            region = Region.NTSC;
            ram_size = 0;
            spc7110_data_rom_offset = 0x100000;
            supergameboy_version = SuperGameBoyVersion.Version1;
            supergameboy_ram_size = 0;
            supergameboy_rtc_size = 0;
            serial_baud_rate = 57600;

            has_bsx_slot = false;
            has_superfx = false;
            has_sa1 = false;
            has_srtc = false;
            has_sdd1 = false;
            has_spc7110 = false;
            has_spc7110rtc = false;
            has_cx4 = false;
            has_dsp1 = false;
            has_dsp2 = false;
            has_dsp3 = false;
            has_dsp4 = false;
            has_obc1 = false;
            has_st0010 = false;
            has_st0011 = false;
            has_st0018 = false;
            has_msu1 = false;
            has_serial = false;

            parse_xml(xml_list);

            if (ram_size > 0)
            {
                MappedRAM.cartram.map(Enumerable.Repeat<byte>(0xff, (int)ram_size).ToArray(), ram_size);
            }

            if (has_srtc || has_spc7110rtc)
            {
                MappedRAM.cartram.map(Enumerable.Repeat<byte>(0xff, 20).ToArray(), 20);
            }

            if (mode == Mode.Bsx)
            {
                MappedRAM.bsxram.map(Enumerable.Repeat<byte>(0xff, 32 * 1024).ToArray(), 32 * 1024);
                MappedRAM.bsxram.map(Enumerable.Repeat<byte>(0xff, 512 * 1024).ToArray(), 512 * 1024);
            }

            if (mode == Mode.SufamiTurbo)
            {
                if (!ReferenceEquals(MappedRAM.stArom.data(), null)) MappedRAM.stAram.map(Enumerable.Repeat<byte>(0xff, 128 * 1024).ToArray(), 128 * 1024);
                if (!ReferenceEquals(MappedRAM.stBrom.data(), null)) MappedRAM.stBram.map(Enumerable.Repeat<byte>(0xff, 128 * 1024).ToArray(), 128 * 1024);
            }

            if (mode == Mode.SuperGameBoy)
            {
                if (!ReferenceEquals(MappedRAM.gbrom.data(), null))
                {
                    if (supergameboy_ram_size != 0) MappedRAM.gbram.map(Enumerable.Repeat<byte>(0xff, (int)supergameboy_ram_size).ToArray(), supergameboy_ram_size);
                    if (supergameboy_rtc_size != 0) MappedRAM.gbrtc.map(Enumerable.Repeat<byte>(0x00, (int)supergameboy_rtc_size).ToArray(), supergameboy_rtc_size);
                }
            }

            MappedRAM.cartrom.write_protect(true);
            MappedRAM.cartram.write_protect(false);
            MappedRAM.cartrtc.write_protect(false);
            MappedRAM.bsxflash.write_protect(true);
            MappedRAM.bsxram.write_protect(false);
            MappedRAM.bsxpram.write_protect(false);
            MappedRAM.stArom.write_protect(true);
            MappedRAM.stAram.write_protect(false);
            MappedRAM.stBrom.write_protect(true);
            MappedRAM.stBram.write_protect(false);
            MappedRAM.gbrom.write_protect(true);
            MappedRAM.gbram.write_protect(false);
            MappedRAM.gbrtc.write_protect(false);

            unchecked
            {
                uint checksum = (uint)~0;
                foreach (var n in MappedRAM.cartram.data())
                {
                    checksum = CRC32.adjust(checksum, n);
                }
                if (MappedRAM.bsxflash.size() != 0 && MappedRAM.bsxflash.size() != (uint)~0)
                {
                    foreach (var n in MappedRAM.bsxflash.data())
                    {
                        checksum = CRC32.adjust(checksum, n);
                    }
                }
                if (MappedRAM.stArom.size() != 0 && MappedRAM.stArom.size() != (uint)~0)
                {
                    foreach (var n in MappedRAM.stArom.data())
                    {
                        checksum = CRC32.adjust(checksum, n);
                    }
                }
                if (MappedRAM.stBrom.size() != 0 && MappedRAM.stBrom.size() != (uint)~0)
                {
                    foreach (var n in MappedRAM.stBrom.data())
                    {
                        checksum = CRC32.adjust(checksum, n);
                    }
                }
                if (MappedRAM.gbrom.size() != 0 && MappedRAM.gbrom.size() != (uint)~0)
                {
                    foreach (var n in MappedRAM.gbrom.data())
                    {
                        checksum = CRC32.adjust(checksum, n);
                    }
                }
                crc32 = ~checksum;
            }

            SHA256 sha = new SHA256();
            byte[] shahash = new byte[32];
            SHA256.init(sha);
            SHA256.chunk(sha, MappedRAM.cartrom.data(), MappedRAM.cartrom.size());
            SHA256.final(sha);
            SHA256.hash(sha, shahash);

            string hash = string.Empty;
            foreach (var n in shahash)
            {
                hash += n.ToString("X2");
            }
            sha256 = hash;

            Bus.bus.load_cart();
            loaded = true;
        }

        public void unload()
        {
            MappedRAM.cartrom.reset();
            MappedRAM.cartram.reset();
            MappedRAM.cartrtc.reset();
            MappedRAM.bsxflash.reset();
            MappedRAM.bsxram.reset();
            MappedRAM.bsxpram.reset();
            MappedRAM.stArom.reset();
            MappedRAM.stAram.reset();
            MappedRAM.stBrom.reset();
            MappedRAM.stBram.reset();
            MappedRAM.gbrom.reset();
            MappedRAM.gbram.reset();
            MappedRAM.gbrtc.reset();

            if (loaded == false)
            {
                return;
            }
            Bus.bus.unload_cart();
            loaded = false;
        }

        public Cartridge()
        {
            loaded = false;
            unload();
        }

        private void parse_xml(string[] list)
        {
            mapping.Clear();
            parse_xml_cartridge(list[0]);

            if (mode == Mode.BsxSlotted)
            {
                parse_xml_bsx(list[1]);
            }
            else if (mode == Mode.Bsx)
            {
                parse_xml_bsx(list[1]);
            }
            else if (mode == Mode.SufamiTurbo)
            {
                parse_xml_sufami_turbo(list[1], false);
                parse_xml_sufami_turbo(list[2], true);
            }
            else if (mode == Mode.SuperGameBoy)
            {
                parse_xml_gameboy(list[1]);
            }
        }

        private void parse_xml_cartridge(string data)
        {
            XDocument document = XDocument.Parse(data);
            if (document.Root.IsEmpty)
            {
                return;
            }

            region = (document.Element("cartridge").Attribute("region").Value == "NTSC") ? Region.NTSC : Region.PAL;

            foreach (var node in document.Elements("rom"))
            {
                xml_parse_rom(node);
            }
            foreach (var node in document.Elements("ram"))
            {
                xml_parse_ram(node);
            }
            foreach (var node in document.Elements("superfx"))
            {
                xml_parse_superfx(node);
            }
            foreach (var node in document.Elements("sa1"))
            {
                xml_parse_sa1(node);
            }
            foreach (var node in document.Elements("bsx"))
            {
                xml_parse_bsx(node);
            }
            foreach (var node in document.Elements("sufamiturbo"))
            {
                xml_parse_sufamiturbo(node);
            }
            foreach (var node in document.Elements("supergameboy"))
            {
                xml_parse_supergameboy(node);
            }
            foreach (var node in document.Elements("srtc"))
            {
                xml_parse_srtc(node);
            }
            foreach (var node in document.Elements("sdd1"))
            {
                xml_parse_sdd1(node);
            }
            foreach (var node in document.Elements("spc7110"))
            {
                xml_parse_spc7110(node);
            }
            foreach (var node in document.Elements("cx4"))
            {
                xml_parse_cx4(node);
            }
            foreach (var node in document.Elements("necdsp"))
            {
                xml_parse_necdsp(node);
            }
            foreach (var node in document.Elements("obc1"))
            {
                xml_parse_obc1(node);
            }
            foreach (var node in document.Elements("setadsp"))
            {
                xml_parse_setadsp(node);
            }
            foreach (var node in document.Elements("setarisc"))
            {
                xml_parse_setarisc(node);
            }
            foreach (var node in document.Elements("msu1"))
            {
                xml_parse_msu1(node);
            }
            foreach (var node in document.Elements("serial"))
            {
                xml_parse_serial(node);
            }
        }

        private void parse_xml_bsx(string data)
        {
        }

        private void parse_xml_sufami_turbo(string data, bool slot)
        {
        }

        private void parse_xml_gameboy(string data)
        {
            XDocument document = XDocument.Parse(data);
            if (document.Elements().Count() == 0)
            {
                return;
            }

            supergameboy_rtc_size = (document.Element("cartridge").Attribute("rtc").Value == "true") ? 4U : 0U;
            supergameboy_ram_size = uint.Parse(document.Element("cartridge").Element("ram").Attribute("size").Value);
        }

        private void xml_parse_rom(XElement root)
        {
            foreach (var leaf in root.Elements("map"))
            {
                Mapping m = new Mapping(MappedRAM.cartrom);
                if (leaf.Attributes("address").Any())
                {
                    xml_parse_address(m, leaf.Attribute("address").Value);
                }
                if (leaf.Attributes("mode").Any())
                {
                    xml_parse_mode(m, leaf.Attribute("mode").Value);
                }
                if (leaf.Attributes("offset").Any())
                {
                    m.offset = uint.Parse(leaf.Attribute("offset").Value);
                }
                if (leaf.Attributes("size").Any())
                {
                    m.size = uint.Parse(leaf.Attribute("size").Value);
                }
                mapping.Add(m);
            }
        }

        private void xml_parse_ram(XElement root)
        {
            if (root.Attributes("size").Any())
            {
                ram_size = uint.Parse(root.Attribute("size").Value);
            }

            foreach (var leaf in root.Elements("map"))
            {
                Mapping m = new Mapping(MappedRAM.cartram);

                if (leaf.Attributes("address").Any())
                {
                    xml_parse_address(m, leaf.Attribute("address").Value);
                }
                if (leaf.Attributes("mode").Any())
                {
                    xml_parse_mode(m, leaf.Attribute("mode").Value);
                }
                if (leaf.Attributes("offset").Any())
                {
                    m.offset = uint.Parse(leaf.Attribute("offset").Value);
                }
                if (leaf.Attributes("size").Any())
                {
                    m.size = uint.Parse(leaf.Attribute("size").Value);
                }

                mapping.Add(m);
            }
        }

        private void xml_parse_superfx(XElement root)
        {
            has_superfx = true;

            foreach (var node in root.Elements("rom"))
            {
                foreach (var leaf in root.Elements("map"))
                {
                    Mapping m = new Mapping(SuperFXCPUROM.fxrom);

                    if (leaf.Attributes("address").Any())
                    {
                        xml_parse_address(m, leaf.Attribute("address").Value);
                    }
                    if (leaf.Attributes("mode").Any())
                    {
                        xml_parse_mode(m, leaf.Attribute("mode").Value);
                    }
                    if (leaf.Attributes("offset").Any())
                    {
                        m.offset = uint.Parse(leaf.Attribute("offset").Value);
                    }
                    if (leaf.Attributes("size").Any())
                    {
                        m.size = uint.Parse(leaf.Attribute("size").Value);
                    }

                    mapping.Add(m);
                }
            }

            foreach (var node in root.Elements("ram"))
            {
                foreach (var leaf in root.Elements("map"))
                {
                    if (leaf.Attributes("size").Any())
                    {
                        ram_size = uint.Parse(leaf.Attribute("size").Value);
                    }

                    Mapping m = new Mapping(SuperFXCPURAM.fxram);

                    if (leaf.Attributes("address").Any())
                    {
                        xml_parse_address(m, leaf.Attribute("address").Value);
                    }
                    if (leaf.Attributes("mode").Any())
                    {
                        xml_parse_mode(m, leaf.Attribute("mode").Value);
                    }
                    if (leaf.Attributes("offset").Any())
                    {
                        m.offset = uint.Parse(leaf.Attribute("offset").Value);
                    }
                    if (leaf.Attributes("size").Any())
                    {
                        m.size = uint.Parse(leaf.Attribute("size").Value);
                    }

                    mapping.Add(m);
                }
            }

            foreach (var node in root.Elements("mmio"))
            {
                foreach (var leaf in root.Elements("map"))
                {
                    Mapping m = new Mapping(SuperFX.superfx);

                    if (leaf.Attributes("address").Any())
                    {
                        xml_parse_address(m, leaf.Attribute("address").Value);
                    }

                    mapping.Add(m);
                }
            }
        }

        private void xml_parse_sa1(XElement root)
        {
            throw new NotImplementedException();
        }

        private void xml_parse_bsx(XElement root)
        {
            throw new NotImplementedException();
        }

        private void xml_parse_sufamiturbo(XElement root)
        {
            throw new NotImplementedException();
        }

        private void xml_parse_supergameboy(XElement root)
        {
            throw new NotImplementedException();
        }

        private void xml_parse_srtc(XElement root)
        {
            throw new NotImplementedException();
        }

        private void xml_parse_sdd1(XElement root)
        {
            throw new NotImplementedException();
        }

        private void xml_parse_spc7110(XElement root)
        {
            throw new NotImplementedException();
        }

        private void xml_parse_cx4(XElement root)
        {
            throw new NotImplementedException();
        }

        private void xml_parse_necdsp(XElement root)
        {
            throw new NotImplementedException();
        }

        private void xml_parse_obc1(XElement root)
        {
            throw new NotImplementedException();
        }

        private void xml_parse_setadsp(XElement root)
        {
            throw new NotImplementedException();
        }

        private void xml_parse_setarisc(XElement root)
        {
            throw new NotImplementedException();
        }

        private void xml_parse_msu1(XElement root)
        {
            throw new NotImplementedException();
        }

        private void xml_parse_serial(XElement root)
        {
            throw new NotImplementedException();
        }

        private void xml_parse_address(Mapping m, string data)
        {
            throw new NotImplementedException();
        }

        private void xml_parse_mode(Mapping m, string data)
        {
            throw new NotImplementedException();
        }
    }
}
