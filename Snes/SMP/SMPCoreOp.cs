
namespace Snes
{
    public delegate void SMPCoreOperation(SMPCoreOpArguments args);

    public class SMPCoreOp
    {
        private SMPCoreOperation op { get; set; }
        private SMPCoreOpArguments args { get; set; }

        public SMPCoreOp(SMPCoreOperation op, SMPCoreOpArguments args)
        {
            this.op = op;
            this.args = args;
        }

        public void Invoke()
        {
            op(args);
        }
    }
}
