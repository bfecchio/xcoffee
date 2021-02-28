using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.RazorPages;

using XCoffee.Ordering.Domain.Interfaces;
using XCoffee.Ordering.Domain.AggregatesModel.ProductAggregate;

namespace XCoffee.Web.Pages
{
    public sealed class IndexModel : PageModel
    {
        #region Read-Only Fields

        private readonly ILogger<IndexModel> _logger;
        private readonly IBasketService _basketService;
        private readonly IProductService _productService;

        #endregion

        #region Properties

        public IEnumerable<Product> Catalog { get; set; }

        #endregion

        #region Constructors

        public IndexModel(ILogger<IndexModel> logger
            , IBasketService basketService
            , IProductService productService
        )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); ;
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        #endregion

        #region Methods        

        public async Task OnGetAsync()
        {
            Catalog = await _productService.GetAll();
        }

        public async Task<IActionResult> OnPostAsync(Guid productId)
        {
            var product = await _productService.Get(productId);
            if (product is null)
            {
                ModelState.AddModelError("", "Produto não localizado!");
                return Page();
            }
            
            _basketService.New();
            _basketService.AddItem(product, 1);

            return RedirectToPage("Basket");
        }

        #endregion
    }
}
