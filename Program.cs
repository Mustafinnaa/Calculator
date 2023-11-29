Console.Write("Введите выражение: ");
string ex = Console.ReadLine();

List<int> numb = new List<int>();
List<char> operat = new List<char>();

for (int i = 0; i < ex.Length; i++)
{
    if (char.IsDigit(ex[i]))
    {
        int number = 0;
        while (i < ex.Length && char.IsDigit(ex[i]))
        {
            number = number * 10 + (int)char.GetNumericValue(ex[i]);
            i++;
        }
        numb.Add(number);
        i--;
    }
    else if (ex[i] == '+' || ex[i] == '-' || ex[i] == '*' || ex[i] == '/')
    {
        while (operat.Count > 0 && Precedence(ex[i], operat[operat.Count - 1]))
        {
            int result = ApplyOperation(numb[numb.Count - 2], numb[numb.Count - 1], operat[operat.Count - 1]);
            numb.RemoveAt(numb.Count - 1);
            numb[numb.Count - 1] = result;
            operat.RemoveAt(operat.Count - 1);
        }
        operat.Add(ex[i]);
    }
}

while (operat.Count > 0)
{
    int result = ApplyOperation(numb[numb.Count - 2], numb[numb.Count - 1], operat[operat.Count - 1]);
    numb.RemoveAt(numb.Count - 1);
    numb[numb.Count - 1] = result;
    operat.RemoveAt(operat.Count - 1);
}

Console.Write("Результат: " + numb[0]);

static bool Precedence(char op1, char op2)
{
    if ((op1 == '*' || op1 == '/') && (op2 == '+' || op2 == '-'))
        return false;

    return true;
}

static int ApplyOperation(int a, int b, char op)
{
    switch (op)
    {
        case '+':
            return a + b;
        case '-':
            return a - b;
        case '*':
            return a * b;
        case '/':
            if (b == 0)
            {
                Console.WriteLine("Ошибка: деление на 0 запрещено!");
                Environment.Exit(0);
            }
            return a / b;
        default:
            return 0;
    }
}