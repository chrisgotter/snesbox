
namespace Snes
{
    public delegate void SMPCoreOp(SMPCoreOpArguments args);

    public class SMPCoreOperation
    {
        private SMPCoreOp op { get; set; }
        private SMPCoreOpArguments args { get; set; }

        public SMPCoreOperation(SMPCoreOp op, SMPCoreOpArguments args)
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
