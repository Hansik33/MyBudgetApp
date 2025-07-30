using System;
using System.Linq;
using System.Reflection;

namespace MyBudgetApp.Helpers
{
    public static class ViewLocator
    {
        public static Type ResolveViewType(Type viewModelType)
        {
            var viewModelName = viewModelType.FullName!;
            var viewName = viewModelName
                .Replace("ViewModels", "Views")
                .Replace("ViewModel", "View");

            var viewAssembly = Assembly.GetExecutingAssembly();

            var viewType = viewAssembly.GetTypes()
                .FirstOrDefault(viewType => viewType.FullName == viewName)
                ?? throw new InvalidOperationException($"No View found for ViewModel: {viewModelType.Name}");

            return viewType;
        }
    }
}