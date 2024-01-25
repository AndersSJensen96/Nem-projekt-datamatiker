using Nem.Models;
using Nem.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Nem.Controllers.Commands
{
    public class SortRecipeCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;


        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            FilterList(parameter);
            FilterByMealtime(parameter);
            FilterByCategory(parameter);
            SearchBy(parameter);
            SortList(parameter);
        }

        public void SearchBy(object parameter)
        {
            DisplayRecipeListViewModel displayRecipeListViewModel = (DisplayRecipeListViewModel)parameter;
            if (displayRecipeListViewModel.SearchBoxInput.Length >= 1)
            {
                ObservableCollection<Recipe> temp = new ObservableCollection<Recipe>();
                foreach (Recipe recipe in displayRecipeListViewModel.FilteredRecipies)
                {
                    if (recipe.Name.ToUpper().Contains(displayRecipeListViewModel.SearchBoxInput.ToUpper()))
                    {
                        temp.Add(recipe);
                    }
                }
                displayRecipeListViewModel.FilteredRecipies = temp;
            }
        }
        public void FilterList(object parameter)
        {
            DisplayRecipeListViewModel displayRecipeListViewModel = (DisplayRecipeListViewModel)parameter;
            List<Recipe> filterList = new List<Recipe>();
            if(displayRecipeListViewModel.Meat == true || 
                displayRecipeListViewModel.Organic == true || 
                displayRecipeListViewModel.Animal == true  ||
                displayRecipeListViewModel.Other == true)
            {
                foreach (Recipe recipe in displayRecipeListViewModel.Recipes.ToList())
                {
                    if (displayRecipeListViewModel.Animal == true && recipe.Animal != false)
                    {
                        if (displayRecipeListViewModel.Meat == false && recipe.Meat == false)
                        {
                            filterList.Add(recipe);
                        } else if (displayRecipeListViewModel.Meat == true)
                        {
                            filterList.Add(recipe);
                        }
                    }
                    else if (displayRecipeListViewModel.Meat == true && recipe.Meat != false)
                    {
                        filterList.Add(recipe);
                    }
                    else if (displayRecipeListViewModel.Organic == true && recipe.Organic != false)
                    { 
                        if (displayRecipeListViewModel.Meat == false && recipe.Meat == false)
                        {
                            if (displayRecipeListViewModel.Animal == false && recipe.Animal == false)
                            {
                                filterList.Add(recipe);
                            }
                        }
                        else if (displayRecipeListViewModel.Meat == true && displayRecipeListViewModel.Animal == true)
                        {
                            filterList.Add(recipe);
                        }
                    }
                    else if (recipe.Meat == false &&
                            recipe.Organic == false &&
                            recipe.Animal == false && 
                            displayRecipeListViewModel.Other == true)
                    {
                        filterList.Add(recipe);
                    }
                }
                displayRecipeListViewModel.FilteredRecipies = new ObservableCollection<Recipe>(filterList);
            }
            else
            {
                displayRecipeListViewModel.FilteredRecipies = displayRecipeListViewModel.Recipes;
            }
           
        }
        public void FilterByCategory(object parameter)
        {
            DisplayRecipeListViewModel displayRecipeListViewModel = (DisplayRecipeListViewModel)parameter;
            List<Recipe> tempList = new List<Recipe>();
            if(displayRecipeListViewModel.SelectedFoodCategory != null &&
                displayRecipeListViewModel.SelectedFoodCategory.Name != "Alle madkategorier")
            {
                foreach (Recipe recipe in displayRecipeListViewModel.FilteredRecipies.ToList())
                {
                    if (recipe.FoodCategories.Any(x => (x.Name == displayRecipeListViewModel.SelectedFoodCategory.Name)))
                    {
                        tempList.Add(recipe);
                    }
                }
                displayRecipeListViewModel.FilteredRecipies = new ObservableCollection<Recipe>(tempList);
            }
        }
        public void FilterByMealtime(object parameter)
        {
            DisplayRecipeListViewModel displayRecipeListViewModel = (DisplayRecipeListViewModel)parameter;
            List<Recipe> tempList = new List<Recipe>();
            if(displayRecipeListViewModel.SelectedMealtime != null && 
                displayRecipeListViewModel.SelectedMealtime.Name != "Alle måltidsgrupper")
            {
                foreach (Recipe recipe in displayRecipeListViewModel.FilteredRecipies.ToList())
                {
                    if (recipe.Mealtimes.Any(x => (x.Name == displayRecipeListViewModel.SelectedMealtime.Name)))
                    {
                        tempList.Add(recipe);
                    }
                }
                displayRecipeListViewModel.FilteredRecipies = new ObservableCollection<Recipe>(tempList);
            }
        }
        public void SortList(object parameter)
        {
            DisplayRecipeListViewModel displayRecipeListViewModel = (DisplayRecipeListViewModel)parameter;
            //displayRecipeListViewModel.FilteredRecipies = displayRecipeListViewModel.Recipes;

            switch (displayRecipeListViewModel.SelectedSearchValue)
            {
                case "Nyeste":
                    {

                        //TypeCasting displayRecipeListViewModel as an ObservableCollection<Recipe> to use IEnumerable.OrderBy and sort the list by CreationTime 
                        //then casts it back to an ObservableCollection
                        displayRecipeListViewModel.FilteredRecipies = new ObservableCollection<Recipe>(displayRecipeListViewModel.FilteredRecipies.OrderByDescending(n => n.CreationTime).ToList());

                        break;
                    }

                case "Ældst":
                    {
                        displayRecipeListViewModel.FilteredRecipies = new ObservableCollection<Recipe>(displayRecipeListViewModel.FilteredRecipies.OrderBy(n => n.CreationTime).ToList());
                        break;
                    }

                case "Tid":
                    {
                        displayRecipeListViewModel.FilteredRecipies = new ObservableCollection<Recipe>(displayRecipeListViewModel.FilteredRecipies.OrderBy(n => n.CookingTime).ToList());
                        break;
                    }

                case "Flest Hjerter":
                    {
                        displayRecipeListViewModel.FilteredRecipies = displayRecipeListViewModel.FilteredRecipies;
                        break;
                    }

                case "Sværeste":
                    {
                        displayRecipeListViewModel.FilteredRecipies = new ObservableCollection<Recipe>(displayRecipeListViewModel.FilteredRecipies.OrderByDescending(n => n.Difficulty).ToList());
                        break;
                    }

                case "Nemmeste":
                    {
                        displayRecipeListViewModel.FilteredRecipies = new ObservableCollection<Recipe>(displayRecipeListViewModel.FilteredRecipies.OrderBy(n => n.Difficulty).ToList());
                        break;
                    }

                default:
                    {
                        displayRecipeListViewModel.FilteredRecipies = displayRecipeListViewModel.FilteredRecipies;
                        break;
                    }

            }
        }

    }
}

