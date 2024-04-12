using System;

namespace PROG6221_POE_Part1
{
    // ingredient class
    class Ingredient
    {
        // getters and setters
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string UnitOfMeasurement { get; set; } // Corrected property name

        // constructor
        public Ingredient(string name, double quantity, string unitOfMeasurement)
        {
            Name = name;
            Quantity = quantity;
            UnitOfMeasurement = unitOfMeasurement;
        }
    }

    // steps class
    class Step
    {
        // getters and setter
        public string Descriptions { get; set; }

        // constructor
        public Step(string description)
        {
            Descriptions = description;
        }
    }

    // recipe class
    class Recipe
    {
        // One dimentional arrays to store ingredient and steps
        private Ingredient[] ingredients;
        private Step[] steps;

        // Constructor
        public Recipe(int numIngredients, int numSteps)
        {
            ingredients = new Ingredient[numIngredients];
            steps = new Step[numSteps];
        }

        // add ingredient method
        public void AddIngredient(int index, string name, double quantity, string unit)
        {
            ingredients[index] = new Ingredient(name, quantity, unit);
        }

        // add step method
        public void AddStep(int index, string description)
        {
            steps[index] = new Step(description);
        }

        // clear recipe method
        public void ClearRecipe()
        {
            Array.Clear(ingredients, 0, ingredients.Length);
            Array.Clear(steps, 0, steps.Length);
        }

        // show recipe method
        public void ShowRecipe()
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine("\nFull Recipe:");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Ingredients:");
            if (ingredients != null && ingredients.Length > 0)
            {

                foreach (Ingredient ingredient in ingredients)
                {
                    if (ingredient != null)
                    {
                        Console.WriteLine($"{ingredient.Quantity} {ingredient.UnitOfMeasurement} of {ingredient.Name}");
                    }

                }
            }
            else
            {
                Console.WriteLine("No ingredients");
            }
            Console.WriteLine("\nSteps:");
            if (steps != null && steps.Length > 0)
            {
                for (int i = 0; i < steps.Length; i++)
                {
                    if (steps[i] != null)
                    {
                        Console.WriteLine($"{i + 1}. {steps[i].Descriptions}");
                    }
                }
            }
            else
            {
                Console.WriteLine("No steps");
            }
        }

        // scale recipe method
        public void ScaleRecipe(double factor)
        {
            foreach (Ingredient ingredient in ingredients)
            {
                ingredient.Quantity *= factor;
            }
        }

        // reset quantities method
        public void ResetQuantities(double[] OriginalValue)
        {
            for (int i = 0; i < ingredients.Length; i++)
            {
                ingredients[i].Quantity = OriginalValue[i];
            }
        }



        class Program
        {
            // main method
            static void Main(string[] args)
            {
                Console.WriteLine("Welcome!");

                Recipe recipe = null;
                while (true)
                {
                    int NumIngredients, NumSteps;
                    do
                    {
                        Console.WriteLine("Enter the number of ingredients:");
                    }
                    while (!int.TryParse(Console.ReadLine(), out NumIngredients) || NumIngredients <= 0);

                    do
                    {
                        Console.WriteLine("Enter the number of steps: ");
                    }
                    while (!int.TryParse(Console.ReadLine(), out NumSteps) || NumSteps <= 0);

                    // create new recipe
                    recipe = new Recipe(NumIngredients, NumSteps);

                    double[] OriginalValue = new double[NumIngredients];

                    // ingredients input
                    for (int i = 0; i < NumIngredients; i++)
                    {
                        Console.WriteLine($"\nIngredient {i + 1};");
                        Console.WriteLine("Name: ");
                        string name = Console.ReadLine();

                        int quantity;
                        do
                        {
                            Console.WriteLine("Quantity: ");
                        } while (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0);

                        OriginalValue[i] = quantity;

                        Console.WriteLine("Unit of measurement: ");
                        string unit = Console.ReadLine();

                        recipe.AddIngredient(i, name, quantity, unit);
                    }

                    // steps input
                    for (int i = 0; i < NumSteps; i++)
                    {
                        Console.WriteLine($"\nStep {i + 1}:");
                        Console.WriteLine("Description: ");
                        string description = Console.ReadLine();

                        recipe.AddStep(i, description);
                    }

                    while (true)
                    {
                        // displaying menu
                        Console.WriteLine("-----------------------------------");
                        Console.WriteLine("\nSelect from the following menu: ");
                        Console.WriteLine("-----------------------------------");
                        Console.WriteLine("1.Display full recipe" +
                                          "\n2.Scale recipe" +
                                          "\n3.Reset values" +
                                          "\n4.Clear recipe" +
                                          "\n5.Exit application");
                        int selection;
                        if (!int.TryParse(Console.ReadLine(), out selection))
                        {
                            Console.WriteLine("Invalid input.");
                            continue;
                        }

                        // switch statement
                        switch (selection)
                        {
                            case 1:
                                recipe.ShowRecipe();
                                break;
                            case 2:
                                double scalingFactor;
                                do
                                {
                                    Console.WriteLine("\nScale the recipe by a factor of " +
                                        "\n1)0.5" +
                                        "\n2)2" +
                                        "\n3)3:");
                                }
                                while (!double.TryParse(Console.ReadLine(), out scalingFactor) || (scalingFactor != 0.5 && scalingFactor != 2 && scalingFactor != 3));
                                recipe.ScaleRecipe(scalingFactor);
                                recipe.ShowRecipe();
                                break;
                            case 3:
                                string ValueReset;
                                do
                                {
                                    Console.WriteLine("\nReset ingredient quantities to original? (yes/no)");
                                    ValueReset = Console.ReadLine();
                                }
                                while (ValueReset.ToLower() != "yes" && ValueReset.ToLower() != "no");

                                if (ValueReset.ToLower() == "yes")
                                {
                                    recipe.ResetQuantities(OriginalValue);
                                    recipe.ShowRecipe(); // Display updated recipe after reset
                                }
                                break;
                            case 4:
                                recipe.ClearRecipe();
                                recipe.ShowRecipe();
                                break;
                            case 5:
                                System.Environment.Exit(0);
                                Console.WriteLine("Goodbye.");
                                break;
                            default:
                                Console.WriteLine("Invalid input");
                                break;
                        }

                        if (selection == 4)
                        {
                            break;
                        }
                    }

                    string ContinueInput;
                    do
                    {
                        Console.WriteLine("\nDo you want to ener a new recipe? (yes/no)");
                        ContinueInput = Console.ReadLine();
                    }
                    while (ContinueInput.ToLower() != "yes" && ContinueInput.ToLower() != "no");

                    if (ContinueInput.ToLower() != "yes")
                    {
                        System.Environment.Exit(0);
                        Console.WriteLine("Goodbye.");
                    }
                }
            }
        }
    }
}
        
    


