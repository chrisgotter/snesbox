using System.Text;
using Nall;

namespace Snes
{
    public static class LibSnes
    {
        public enum SNES_PORT { ONE = 0, TWO = 1 }
        public enum SNES_DEVICE { NONE = 0, JOYPAD = 1, MULTITAP = 2, MOUSE = 3, SUPER_SCOPE = 4, JUSTIFIER = 5, JUSTIFIERS = 6 }
        public enum SNES_DEVICE_ID_JOYPAD { B = 0, Y = 1, SELECT = 2, START = 3, UP = 4, DOWN = 5, LEFT = 6, RIGHT = 7, A = 8, X = 9, L = 10, R = 11 }
        public enum SNES_DEVICE_ID_MOUSE { X = 0, Y = 1, LEFT = 2, RIGHT = 3 }
        public enum SNES_DEVICE_ID_SUPER_SCOPE { X = 0, Y = 1, TRIGGER = 2, CURSOR = 3, TURBO = 4, PAUSE = 5 }
        public enum SNES_DEVICE_ID_JUSTIFIER { X = 0, Y = 1, TRIGGER = 2, START = 3 }
        public enum SNES_REGION { NTSC = 0, PAL = 1 }
        public enum SNES_MEMORY { CARTRIDGE_RAM = 0, CARTRIDGE_RTC = 1, BSX_RAM = 2, BSX_PRAM = 3, SUFAMI_TURBO_A_RAM = 4, SUFAMI_TURBO_B_RAM = 5, GAME_BOY_RAM = 6, GAME_BOY_RTC = 7 }

        public delegate void SnesVideoRefresh(ushort[] data, uint width, uint height);
        public delegate void SnesAudioSample(ushort left, ushort right);
        public delegate void SnesInputPoll();
        public delegate short SnesInputState(bool port, uint device, uint index, uint id);

        public static uint snes_library_revision_major() { return 1; }
        public static uint snes_library_revision_minor() { return 0; }

        public static event SnesVideoRefresh snes_video_refresh = null;
        public static event SnesAudioSample snes_audio_sample = null;
        public static event SnesInputPoll snes_input_poll = null;
        public static event SnesInputState snes_input_state = null;

        static LibSnes()
        {
            LibSnesInterface.inter.pvideo_refresh += new SnesVideoRefresh(Default_pvideo_refresh);
            LibSnesInterface.inter.paudio_sample += new SnesAudioSample(Default_paudio_sample);
            LibSnesInterface.inter.pinput_poll += new SnesInputPoll(Default_pinput_poll);
            LibSnesInterface.inter.pinput_state += new SnesInputState(Default_pinput_state);
        }

        static void Default_pvideo_refresh(ushort[] data, uint width, uint height)
        {
            if (!ReferenceEquals(snes_video_refresh, null))
            {
                snes_video_refresh(data, width, height);
            }
        }

        static void Default_paudio_sample(ushort left, ushort right)
        {
            if (!ReferenceEquals(snes_audio_sample, null))
            {
                snes_audio_sample(left, right);
            }
        }

        static void Default_pinput_poll()
        {
            if (!ReferenceEquals(snes_input_poll, null))
            {
                snes_input_poll();
            }
        }

        static short Default_pinput_state(bool port, uint device, uint index, uint id)
        {
            if (!ReferenceEquals(snes_input_state, null))
            {
                return snes_input_state(port, device, index, id);
            }
            return 0;
        }

        public static void snes_set_controller_port_device(bool port, uint device)
        {
            Input.input.port_set_device(port, (Input.Device)device);
        }

        public static void snes_init()
        {
            System.system.init(LibSnesInterface.inter);
            Input.input.port_set_device(false, Input.Device.Joypad);
            Input.input.port_set_device(true, Input.Device.Joypad);
        }

        public static void snes_term()
        {
            System.system.term();
        }

        public static void snes_power()
        {
            System.system.power();
        }

        public static void snes_reset()
        {
            System.system.reset();
        }

        public static void snes_run()
        {
            System.system.run();
        }

        public static void snes_cheat_reset()
        {
            Cheat.cheat.Clear();
            Cheat.cheat.synchronize();
        }

        public static void snes_cheat_set(uint index, bool enabled, string code)
        {
            Cheat.cheat[(int)index].Assign(code);
            Cheat.cheat[(int)index].enabled = enabled;
            Cheat.cheat.synchronize();
        }

        public static bool snes_load_cartridge_normal(byte[] rom_xml, byte[] rom_data, uint rom_size)
        {
            snes_cheat_reset();
            if (!ReferenceEquals(rom_data, null))
            {
                MappedRAM.cartrom.copy(rom_data, rom_size);
            }
            string xmlrom = (!ReferenceEquals(rom_xml, null)) ? ASCIIEncoding.ASCII.GetString(rom_xml) : new SnesInformation(rom_data, rom_size).xml_memory_map;
            Cartridge.cartridge.load(Cartridge.Mode.Normal, new string[] { xmlrom });
            System.system.power();
            return true;
        }

        public static bool snes_load_cartridge_bsx_slotted(byte[] rom_xml, byte[] rom_data, uint rom_size, byte[] bsx_xml, byte[] bsx_data, uint bsx_size)
        {
            snes_cheat_reset();
            if (!ReferenceEquals(rom_data, null))
            {
                MappedRAM.cartrom.copy(rom_data, rom_size);
            }
            string xmlrom = (!ReferenceEquals(rom_xml, null)) ? ASCIIEncoding.ASCII.GetString(rom_xml) : new SnesInformation(rom_data, rom_size).xml_memory_map;
            if (!ReferenceEquals(bsx_data, null))
            {
                MappedRAM.bsxflash.copy(bsx_data, bsx_size);
            }
            string xmlbsx = (!ReferenceEquals(bsx_xml, null)) ? ASCIIEncoding.ASCII.GetString(bsx_xml) : new SnesInformation(bsx_data, bsx_size).xml_memory_map;
            Cartridge.cartridge.load(Cartridge.Mode.BsxSlotted, new string[] { xmlrom, xmlbsx });
            System.system.power();
            return true;
        }

        public static bool snes_load_cartridge_bsx(byte[] rom_xml, byte[] rom_data, uint rom_size, byte[] bsx_xml, byte[] bsx_data, uint bsx_size)
        {
            snes_cheat_reset();
            if (!ReferenceEquals(rom_data, null))
            {
                MappedRAM.cartrom.copy(rom_data, rom_size);
            }
            string xmlrom = (!ReferenceEquals(rom_xml, null)) ? ASCIIEncoding.ASCII.GetString(rom_xml) : new SnesInformation(rom_data, rom_size).xml_memory_map;
            if (!ReferenceEquals(bsx_data, null))
            {
                MappedRAM.bsxflash.copy(bsx_data, bsx_size);
            }
            string xmlbsx = (!ReferenceEquals(bsx_xml, null)) ? ASCIIEncoding.ASCII.GetString(bsx_xml) : new SnesInformation(bsx_data, bsx_size).xml_memory_map;
            Cartridge.cartridge.load(Cartridge.Mode.Bsx, new string[] { xmlrom, xmlbsx });
            System.system.power();
            return true;
        }

        public static bool snes_load_cartridge_sufami_turbo(byte[] rom_xml, byte[] rom_data, uint rom_size, byte[] sta_xml, byte[] sta_data, uint sta_size, byte[] stb_xml, byte[] stb_data, uint stb_size)
        {
            snes_cheat_reset();
            if (!ReferenceEquals(rom_data, null))
            {
                MappedRAM.cartrom.copy(rom_data, rom_size);
            }
            string xmlrom = (!ReferenceEquals(rom_xml, null)) ? ASCIIEncoding.ASCII.GetString(rom_xml) : new SnesInformation(rom_data, rom_size).xml_memory_map;
            if (!ReferenceEquals(sta_data, null))
            {
                MappedRAM.stArom.copy(sta_data, sta_size);
            }
            string xmlsta = (!ReferenceEquals(sta_xml, null)) ? ASCIIEncoding.ASCII.GetString(sta_xml) : new SnesInformation(sta_data, sta_size).xml_memory_map;
            if (!ReferenceEquals(stb_data, null))
            {
                MappedRAM.stBrom.copy(stb_data, stb_size);
            }
            string xmlstb = (!ReferenceEquals(stb_xml, null)) ? ASCIIEncoding.ASCII.GetString(stb_xml) : new SnesInformation(stb_data, stb_size).xml_memory_map;
            Cartridge.cartridge.load(Cartridge.Mode.SufamiTurbo, new string[] { xmlrom, xmlsta, xmlstb });
            System.system.power();
            return true;
        }

        public static bool snes_load_cartridge_super_game_boy(byte[] rom_xml, byte[] rom_data, uint rom_size, byte[] dmg_xml, byte[] dmg_data, uint dmg_size)
        {
            snes_cheat_reset();
            if (!ReferenceEquals(rom_data, null))
            {
                MappedRAM.cartrom.copy(rom_data, rom_size);
            }
            string xmlrom = (!ReferenceEquals(rom_xml, null)) ? ASCIIEncoding.ASCII.GetString(rom_xml) : new SnesInformation(rom_data, rom_size).xml_memory_map;
            if (!ReferenceEquals(dmg_data, null))
            {
                MappedRAM.gbrom.copy(dmg_data, dmg_size);
            }
            string xmldmg = (!ReferenceEquals(dmg_xml, null)) ? ASCIIEncoding.ASCII.GetString(dmg_xml) : new SnesInformation(dmg_data, dmg_size).xml_memory_map;
            Cartridge.cartridge.load(Cartridge.Mode.SuperGameBoy, new string[] { xmlrom, xmldmg });
            System.system.power();
            return true;
        }

        public static void snes_unload_cartridge()
        {
            Cartridge.cartridge.unload();
        }

        public static bool snes_get_region()
        {
            return System.system.region == System.Region.NTSC ? false : true;
        }

        public static byte[] snes_get_memory_data(uint id)
        {
            if (Cartridge.cartridge.loaded == false) return null;

            switch ((SNES_MEMORY)id)
            {
                case SNES_MEMORY.CARTRIDGE_RAM:
                    return MappedRAM.cartram.data();
                case SNES_MEMORY.CARTRIDGE_RTC:
                    return MappedRAM.cartrtc.data();
                case SNES_MEMORY.BSX_RAM:
                    if (Cartridge.cartridge.mode != Cartridge.Mode.Bsx)
                    {
                        break;
                    }
                    return MappedRAM.bsxram.data();
                case SNES_MEMORY.BSX_PRAM:
                    if (Cartridge.cartridge.mode != Cartridge.Mode.Bsx)
                    {
                        break;
                    }
                    return MappedRAM.bsxpram.data();
                case SNES_MEMORY.SUFAMI_TURBO_A_RAM:
                    if (Cartridge.cartridge.mode != Cartridge.Mode.SufamiTurbo)
                    {
                        break;
                    }
                    return MappedRAM.stAram.data();
                case SNES_MEMORY.SUFAMI_TURBO_B_RAM:
                    if (Cartridge.cartridge.mode != Cartridge.Mode.SufamiTurbo)
                    {
                        break;
                    }
                    return MappedRAM.stBram.data();
                case SNES_MEMORY.GAME_BOY_RAM:
                    if (Cartridge.cartridge.mode != Cartridge.Mode.SuperGameBoy)
                    {
                        break;
                    }
                    SuperGameBoy.supergameboy.save();
                    return MappedRAM.gbram.data();
                case SNES_MEMORY.GAME_BOY_RTC:
                    if (Cartridge.cartridge.mode != Cartridge.Mode.SuperGameBoy)
                    {
                        break;
                    }
                    SuperGameBoy.supergameboy.save();
                    return MappedRAM.gbrtc.data();
            }

            return null;
        }

        public static uint snes_get_memory_size(uint id)
        {
            if (Cartridge.cartridge.loaded == false)
            {
                return 0;
            }
            uint size = 0;

            switch ((SNES_MEMORY)id)
            {
                case SNES_MEMORY.CARTRIDGE_RAM:
                    size = MappedRAM.cartram.size();
                    break;
                case SNES_MEMORY.CARTRIDGE_RTC:
                    size = MappedRAM.cartrtc.size();
                    break;
                case SNES_MEMORY.BSX_RAM:
                    if (Cartridge.cartridge.mode != Cartridge.Mode.Bsx)
                    {
                        break;
                    }
                    size = MappedRAM.bsxram.size();
                    break;
                case SNES_MEMORY.BSX_PRAM:
                    if (Cartridge.cartridge.mode != Cartridge.Mode.Bsx)
                    {
                        break;
                    }
                    size = MappedRAM.bsxpram.size();
                    break;
                case SNES_MEMORY.SUFAMI_TURBO_A_RAM:
                    if (Cartridge.cartridge.mode != Cartridge.Mode.SufamiTurbo)
                    {
                        break;
                    }
                    size = MappedRAM.stAram.size();
                    break;
                case SNES_MEMORY.SUFAMI_TURBO_B_RAM:
                    if (Cartridge.cartridge.mode != Cartridge.Mode.SufamiTurbo)
                    {
                        break;
                    }
                    size = MappedRAM.stBram.size();
                    break;
                case SNES_MEMORY.GAME_BOY_RAM:
                    if (Cartridge.cartridge.mode != Cartridge.Mode.SuperGameBoy)
                    {
                        break;
                    }
                    size = MappedRAM.gbram.size();
                    break;
                case SNES_MEMORY.GAME_BOY_RTC:
                    if (Cartridge.cartridge.mode != Cartridge.Mode.SuperGameBoy)
                    {
                        break;
                    }
                    size = MappedRAM.gbrtc.size();
                    break;
            }

            unchecked
            {
                if (size == (uint)-1U)
                {
                    size = 0;
                }
            }
            return size;
        }
    }
}
