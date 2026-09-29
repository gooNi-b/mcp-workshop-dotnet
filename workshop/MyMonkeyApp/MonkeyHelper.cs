namespace MyMonkeyApp;

/// <summary>원숭이 데이터를 조회하고 무작위로 선택합니다.</summary>
public static class MonkeyHelper
{
    private static Monkey[] monkeys = [];
    private static int randomPickCount;
    private static readonly object Sync = new();

    /// <summary>원숭이 목록을 교체하고 선택 횟수를 초기화합니다.</summary>
    /// <exception cref="ArgumentException">이름이 없는 항목이 포함된 경우</exception>
    public static void LoadMonkeys(IEnumerable<Monkey> source)
    {
        ArgumentNullException.ThrowIfNull(source);
        var items = source.ToArray();
        if (items.Any(monkey => monkey is null || string.IsNullOrWhiteSpace(monkey.Name)))
            throw new ArgumentException("모든 원숭이에 이름이 있어야 합니다.", nameof(source));
        lock (Sync)
        {
            monkeys = items;
            randomPickCount = 0;
        }
    }

    /// <summary>현재 목록의 읽기 전용 복사본을 반환합니다.</summary>
    public static IReadOnlyList<Monkey> GetMonkeys()
    {
        lock (Sync) return Array.AsReadOnly((Monkey[])monkeys.Clone());
    }

    /// <summary>공백과 대소문자를 무시하고 이름을 검색합니다.</summary>
    public static Monkey? GetMonkeyByName(string? name)
    {
        if (string.IsNullOrWhiteSpace(name)) return null;
        lock (Sync) return Array.Find(monkeys, monkey => string.Equals(monkey.Name, name.Trim(), StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>무작위 항목을 선택합니다. 목록이 비었으면 null을 반환합니다.</summary>
    public static Monkey? GetRandomMonkey()
    {
        lock (Sync)
        {
            if (monkeys.Length == 0) return null;
            randomPickCount++;
            return monkeys[Random.Shared.Next(monkeys.Length)];
        }
    }

    /// <summary>현재 목록에서 무작위로 선택한 횟수를 반환합니다.</summary>
    public static int GetRandomPickCount()
    {
        lock (Sync) return randomPickCount;
    }
}
