using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.RazorPages;

using XCoffee.Core.Domain;
using XCoffee.Ordering.Domain.Interfaces;
using XCoffee.Ordering.Domain.AggregatesModel.OrderAggregate;

namespace XCoffee.Web.Pages
{
    public sealed class BasketModel : PageModel
    {
        #region Read-Only Fields

        private readonly ILogger<BasketModel> _logger;

        private readonly IOrderService _orderService;
        private readonly IBasketService _basketService;

        #endregion

        #region Properties

        public decimal Amount => _basketService.Basket.Amount;
        public decimal AmountPaid => _basketService.Basket.AmountPaid;
        public decimal AmountPayBack => _basketService.Basket.AmountPayBack;
        public IEnumerable<BasketItem> Items => _basketService.ListItems();
        public IEnumerable<BasketCoin> Coins => _basketService.ListCoins();
        public IEnumerable<Coin> CoinOptions => Enumeration<int>.GetAll<Coin>().OrderBy(x => x.Value);
        public IEnumerable<BasketCoin> PayBackCoins => _basketService.Basket.PayBackCoins.OrderBy(x => x.Coin.Value);

        #endregion

        #region Constructors

        public BasketModel(ILogger<BasketModel> logger
            , IOrderService orderService
            , IBasketService basketService
        )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); ;
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));            
        }

        #endregion

        #region Methods       

        public async Task<IActionResult> OnPostAddCoinAsync(int coinId)
        {
            var coin = Enumeration<int>.FromId<Coin>(coinId);
            
            if (coin is null)
            {
                ModelState.AddModelError("", "Moeda n�o aceita.");
                return Page();
            }

            if (!coin.Accept)
            {
                ModelState.AddModelError("", "Oops! N�o estamos aceitando essa moeda tempor�riamente.");
                return Page();
            }

            _basketService.AddCoin(coin, 1);

            return Page();
        }

        public async Task<IActionResult> OnPostCancelAsync()
        {
            _basketService.Cancel();
            return RedirectToPage("Index");
        }


        public async Task<IActionResult> OnPostClearAsync()
        {
            _basketService.ClearCoins();
            return Page();            
        }

        public async Task<IActionResult> OnPostFinalizeAsync()
        {
            await _orderService.Add(_basketService.Basket);
            return RedirectToPage("Index");
        }

        #endregion
    }
}
