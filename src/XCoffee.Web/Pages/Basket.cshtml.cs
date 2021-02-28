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
            , IBasketService basketService
        )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); ;
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));            
        }

        #endregion

        #region Methods
       

        public async Task<IActionResult> OnPostAddCoinAsync(int coinId)
        {
            var coin = Enumeration<int>.FromId<Coin>(coinId);
            
            if (coin is null)
            {
                ModelState.AddModelError("", "Moeda não aceita.");
                return Page();
            }

            if (!coin.Accept)
            {
                ModelState.AddModelError("", "Oops! Não estamos aceitando essa moeda temporáriamente.");
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
            return Page();
        }

        #endregion
    }
}
