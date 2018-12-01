namespace Krk.Bum.View.Street
{
    public class StoreBlocksMediator : BlocksMediator
    {
        protected override BlocksController GetBlocksController()
        {
            return viewContext.StoreBlocksController;
        }
    }
}
