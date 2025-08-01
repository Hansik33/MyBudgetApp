﻿using MyBudgetApp.Interfaces;
using MyBudgetApp.Interfaces.Dashboard;
using MyBudgetApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBudgetApp.Services.Dashboard
{
    public class CategoryService(IDatabaseService databaseService) : ICategoryService
    {
        public async Task<List<Category>> GetCategoriesAsync(int userId) => await databaseService.GetCategoriesAsync(userId);
        public async Task<Category> AddCategoryAsync(Category category) => await databaseService.AddCategoryAsync(category);
        public async Task DeleteCategoryAsync(int categoryId) => await databaseService.DeleteCategoryAsync(categoryId);
    }
}