﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Model;
using NLayer.Core.Services;

namespace NLayer.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, ICategoryService categoryService, IMapper mapper)
        {
            _productService = productService;
            _categoryService = categoryService;
            _mapper = mapper;

        }

        public async Task<IActionResult> Index()
        {
            return View(await _productService.GetProductsWithCategory());
        }

        public async Task<IActionResult> Save()
        {
            var categories = await _categoryService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {

            if (ModelState.IsValid)
            {
                await _productService.AddAsync(_mapper.Map<Product>(productDto));
                return Redirect("/Admin/Products/Index");
            }

            var categories = await _categoryService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");
            return View();
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productService.GetByIdAsync(id);


            var categories = await _categoryService.GetAllAsync();

            var categoryiesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

            ViewBag.categories = new SelectList(categoryiesDto, "Id", "Name", product.CategoryId);

            return View(_mapper.Map<ProductDto>(product));

        }
        [HttpPost]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {

                await _productService.UpdateAsync(_mapper.Map<Product>(productDto));
                return RedirectToAction(nameof(Index));

            }

            var categories = await _categoryService.GetAllAsync();

            var categoryiesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

            ViewBag.categories = new SelectList(categoryiesDto, "Id", "Name", productDto.CategoryId);

            return View(productDto);

        }

        public async Task<IActionResult> Remove(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            await _productService.RemoveAsync(product);

            return RedirectToAction(nameof(Index));
        }

    }
}
