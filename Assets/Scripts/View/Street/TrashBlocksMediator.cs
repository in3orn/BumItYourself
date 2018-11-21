namespace Krk.Bum.View.Street
{
    public class TrashBlocksMediator : BlocksMediator
    {
        protected override BlocksController GetBlocksController()
        {
            return viewContext.TrashBlocksController;
        }
    }
}
