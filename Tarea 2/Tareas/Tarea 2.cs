Console.WriteLine("--- Even or Odd Checker ---");
Console.Write("Enter a whole number (integer): ");

string userInput = Console.ReadLine();
int numberToCheck = int.Parse(userInput);
int remainder = numberToCheck % 2;

if (remainder == 0)
{
    Console.WriteLine("\nResult: The number " + numberToCheck + " is EVEN.");
}
else
{
    Console.WriteLine("\nResult: The number " + numberToCheck + " is ODD.");
}
Console.ReadKey();