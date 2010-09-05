using System;

namespace Snes.LibSnes
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

        public delegate void snes_video_refresh_t(ushort[] data, uint width, uint height);
        public delegate void snes_audio_sample_t(ushort left, ushort right);
        public delegate void snes_input_poll_t();
        public delegate short snes_input_state_t(bool port, uint device, uint index, uint id);

        public static uint snes_library_revision_major() { throw new NotImplementedException(); }
        public static uint snes_library_revision_minor() { throw new NotImplementedException(); }

        public static event snes_video_refresh_t snes_video_refresh;
        public static event snes_audio_sample_t snes_audio_sample;
        public static event snes_input_poll_t snes_input_poll;
        public static event snes_input_state_t snes_input_state;

        public static void snes_set_controller_port_device(bool port, uint device) { throw new NotImplementedException(); }
        public static void snes_init() { throw new NotImplementedException(); }
        public static void snes_term() { throw new NotImplementedException(); }
        public static void snes_power() { throw new NotImplementedException(); }
        public static void snes_reset() { throw new NotImplementedException(); }
        public static void snes_run() { throw new NotImplementedException(); }
        public static uint snes_serialize_size() { throw new NotImplementedException(); }
        public static bool snes_serialize(byte[] data, uint size) { throw new NotImplementedException(); }
        public static bool snes_unserialize(byte[] data, uint size) { throw new NotImplementedException(); }
        public static void snes_cheat_reset() { throw new NotImplementedException(); }
        public static void snes_cheat_set(uint index, bool enabled, byte[] code) { throw new NotImplementedException(); }
        public static bool snes_load_cartridge_normal(byte[] rom_xml, byte[] rom_data, uint rom_size) { throw new NotImplementedException(); }
        public static bool snes_load_cartridge_bsx_slotted(byte[] rom_xml, byte[] rom_data, uint rom_size, byte[] bsx_xml, byte[] bsx_data, uint bsx_size) { throw new NotImplementedException(); }
        public static bool snes_load_cartridge_bsx(byte[] rom_xml, byte[] rom_data, uint rom_size, byte[] bsx_xml, byte[] bsx_data, uint bsx_size) { throw new NotImplementedException(); }
        public static bool snes_load_cartridge_sufami_turbo(byte[] rom_xml, byte[] rom_data, uint rom_size, byte[] sta_xml, byte[] sta_data, uint sta_size, byte[] stb_xml, byte[] stb_data, uint stb_size) { throw new NotImplementedException(); }
        public static bool snes_load_cartridge_super_game_boy(byte[] rom_xml, byte[] rom_data, uint rom_size, byte[] dmg_xml, byte[] dmg_data, uint dmg_size) { throw new NotImplementedException(); }
        public static void snes_unload_cartridge() { throw new NotImplementedException(); }
        public static bool snes_get_region() { throw new NotImplementedException(); }
        public static byte[] snes_get_memory_data(uint id) { throw new NotImplementedException(); }
        public static uint snes_get_memory_size(uint id) { throw new NotImplementedException(); }
    }
}
