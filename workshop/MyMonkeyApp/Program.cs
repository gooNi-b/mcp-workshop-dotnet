
using MyMonkeyApp;
using System.Threading.Tasks;

class Program
{
	private static readonly string[] asciiArts = new[]
	{
		@"  (\__/)
  (•ㅅ•)
  / 　 づ",
		@"  ／￣￣￣＼
 /　●　●　\
 (　　●　　)
 \＿＿＿／",
		@"   w  c( .. )o   (")
	  \\__(-)    /
		 /   \\  /
		(     )"
	};

	static async Task Main(string[] args)
	{
		// MCP 서버에서 원숭이 데이터 받아오기 (여기서는 샘플 데이터 사용)
		var monkeys = new List<Monkey>
		{
			new Monkey { Name = "Baboon", Location = "Africa & Asia", Population = 10000, Details = "Baboons are African and Arabian Old World monkeys belonging to the genus Papio.", Image = "", Latitude = -8.783195, Longitude = 34.508523 },
			new Monkey { Name = "Capuchin Monkey", Location = "Central & South America", Population = 23000, Details = "The capuchin monkeys are New World monkeys of the subfamily Cebinae.", Image = "", Latitude = 12.769013, Longitude = -85.602364 },
			new Monkey { Name = "Blue Monkey", Location = "Central and East Africa", Population = 12000, Details = "The blue monkey or diademed monkey is a species of Old World monkey native to Africa.", Image = "", Latitude = 1.957709, Longitude = 37.297204 }
			// ... 필요시 더 추가
		};
		await MonkeyHelper.LoadMonkeysAsync(monkeys);

		var random = new Random();
		while (true)
		{
			Console.Clear();
			// 랜덤 ASCII 아트 출력
			Console.WriteLine(asciiArts[random.Next(asciiArts.Length)]);
			Console.WriteLine("\n==== Monkey App Menu ====");
			Console.WriteLine("1. 모든 원숭이 목록 보기");
			Console.WriteLine("2. 이름으로 원숭이 상세 정보 보기");
			Console.WriteLine("3. 무작위 원숭이 보기");
			Console.WriteLine("4. 종료");
			Console.Write("메뉴를 선택하세요: ");
			var input = Console.ReadLine();

			switch (input)
			{
				case "1":
					Console.WriteLine("\n[모든 원숭이 목록]");
					foreach (var m in MonkeyHelper.GetMonkeys())
					{
						Console.WriteLine($"- {m.Name} | {m.Location} | 개체수: {m.Population}");
					}
					Pause();
					break;
				case "2":
					Console.Write("\n원숭이 이름을 입력하세요: ");
					var name = Console.ReadLine();
					var monkey = MonkeyHelper.GetMonkeyByName(name ?? "");
					if (monkey != null)
					{
						Console.WriteLine($"\n이름: {monkey.Name}\n서식지: {monkey.Location}\n개체수: {monkey.Population}\n설명: {monkey.Details}");
					}
					else
					{
						Console.WriteLine("해당 이름의 원숭이를 찾을 수 없습니다.");
					}
					Pause();
					break;
				case "3":
					var randMonkey = MonkeyHelper.GetRandomMonkey();
					if (randMonkey != null)
					{
						Console.WriteLine($"\n[무작위 원숭이]\n이름: {randMonkey.Name}\n서식지: {randMonkey.Location}\n개체수: {randMonkey.Population}\n설명: {randMonkey.Details}");
						Console.WriteLine($"(무작위 선택 횟수: {MonkeyHelper.GetRandomPickCount()})");
					}
					else
					{
						Console.WriteLine("원숭이 데이터가 없습니다.");
					}
					Pause();
					break;
				case "4":
					Console.WriteLine("앱을 종료합니다.");
					return;
				default:
					Console.WriteLine("잘못된 입력입니다.");
					Pause();
					break;
			}
		}
	}

	private static void Pause()
	{
		Console.WriteLine("\n아무 키나 누르면 계속합니다...");
		Console.ReadKey();
	}
}
