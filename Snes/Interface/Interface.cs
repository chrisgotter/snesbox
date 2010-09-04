
namespace Snes.Interface
{
    interface Interface
    {
        void video_refresh(ushort[] data, uint width, uint height);

        void audio_sample(ushort l_sample, ushort r_sample);

        void input_poll();

        short input_poll(bool port, Input.Input.Device device, uint index, uint id);
    }
}
