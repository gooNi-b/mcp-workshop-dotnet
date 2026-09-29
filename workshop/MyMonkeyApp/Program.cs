using MyMonkeyApp;

var monkeys = new List<Monkey>
{
    new() { Name = "Baboon", Location = "Africa & Asia", Population = 10000, Details = "Baboons are African and Arabian Old World monkeys belonging to the genus Papio.", Latitude = -8.783195, Longitude = 34.508523 },
    new() { Name = "Capuchin Monkey", Location = "Central & South America", Population = 23000, Details = "The capuchin monkeys are New World monkeys of the subfamily Cebinae.", Latitude = 12.769013, Longitude = -85.602364 },
    new() { Name = "Blue Monkey", Location = "Central and East Africa", Population = 12000, Details = "The blue monkey or diademed monkey is a species of Old World monkey native to Africa.", Latitude = 1.957709, Longitude = 37.297204 }
};

try { MonkeyHelper.LoadMonkeys(monkeys); }
catch (ArgumentException ex)
{
    Console.Error.WriteLine($"원숭이 데이터를 불러올 수 없습니다: {ex.Message}");
    return;
}

const string art = "  .-\"\"-.\n (  o o  )\n  |  ^  |\n  \\  -  /\n   '---'";
while (true)
{
    if (!Console.IsOutputRedirected) Console.Clear();
    Console.WriteLine(art);
    Console.WriteLine("\n==== Monkey App Menu ====");
    Console.WriteLine("1. 모든 원숭이 목록 보기");
    Console.WriteLine("2. 이름으로 원숭이 상세 정보 보기");
    Console.WriteLine("3. 무작위 원숭이 보기");
    Console.WriteLine("4. 종료");
    Console.Write("메뉴를 선택하세요: ");
    var input = Console.ReadLine();
    if (input is null) break;

    switch (input.Trim())
    {
        case "1":
            Console.WriteLine("\n[모든 원숭이 목록]");
            var all = MonkeyHelper.GetMonkeys();
            if (all.Count == 0) Console.WriteLine("원숭이 데이터가 없습니다.");
            foreach (var monkey in all)
                Console.WriteLine($"- {monkey.Name} | {monkey.Location} | 개체수: {monkey.Population:N0}");
            break;
        case "2":
            Console.Write("\n원숭이 이름을 입력하세요: ");
            var name = Console.ReadLine();
            if (name is null) return;
            if (string.IsNullOrWhiteSpace(name)) Console.WriteLine("이름을 입력하세요.");
            else if (MonkeyHelper.GetMonkeyByName(name) is { } found) ShowDetails(found);
            else Console.WriteLine("해당 이름의 원숭이를 찾을 수 없습니다.");
            break;
        case "3":
            if (MonkeyHelper.GetRandomMonkey() is { } selected)
            {
                Console.WriteLine("\n[무작위 원숭이]");
                ShowDetails(selected);
                Console.WriteLine($"무작위 선택 횟수: {MonkeyHelper.GetRandomPickCount()}");
            }
            else Console.WriteLine("원숭이 데이터가 없습니다.");
            break;
        case "4":
            Console.WriteLine("앱을 종료합니다.");
            return;
        default:
            Console.WriteLine("잘못된 입력입니다. 1~4 중에서 선택하세요.");
            break;
    }

    if (!Console.IsInputRedirected)
    {
        Console.WriteLine("\n아무 키나 누르면 계속합니다...");
        Console.ReadKey(intercept: true);
    }
}
Console.WriteLine("앱을 종료합니다.");

/// <summary>선택된 원숭이의 상세 정보를 표시합니다.</summary>
static void ShowDetails(Monkey monkey) => Console.WriteLine(
    $"이름: {monkey.Name}\n서식지: {monkey.Location}\n개체수: {monkey.Population:N0}\n설명: {monkey.Details}\n위치: {monkey.Latitude}, {monkey.Longitude}");
