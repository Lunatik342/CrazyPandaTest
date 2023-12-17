using RedPanda.Project.Interfaces;

namespace RedPanda.Project.Services.Interfaces
{
    public interface IPromoPurchaseService
    {
        void BuyPromo(IPromoModel promo);
    }
}