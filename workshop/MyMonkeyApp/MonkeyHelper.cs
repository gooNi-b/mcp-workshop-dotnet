using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMonkeyApp;

/// <summary>
/// 원숭이 데이터 관리를 위한 헬퍼 클래스
/// </summary>
public static class MonkeyHelper
{
    private static List<Monkey> monkeys = new();
    private static int randomPickCount = 0;
    private static readonly object lockObj = new();

    /// <summary>
    /// MCP 서버에서 원숭이 데이터를 비동기로 로드합니다.
    /// </summary>
    public static async Task LoadMonkeysAsync(IEnumerable<Monkey> source)
    {
        lock (lockObj)
        {
            monkeys = source.ToList();
        }
    }

    /// <summary>
    /// 모든 원숭이 목록을 반환합니다.
    /// </summary>
    public static IReadOnlyList<Monkey> GetMonkeys()
    {
        lock (lockObj)
        {
            return monkeys.AsReadOnly();
        }
    }

    /// <summary>
    /// 이름으로 특정 원숭이 정보를 반환합니다.
    /// </summary>
    public static Monkey? GetMonkeyByName(string name)
    {
        lock (lockObj)
        {
            return monkeys.FirstOrDefault(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }

    /// <summary>
    /// 무작위 원숭이를 반환하고, 선택 횟수를 추적합니다.
    /// </summary>
    public static Monkey? GetRandomMonkey()
    {
        lock (lockObj)
        {
            if (monkeys.Count == 0) return null;
            var random = new Random();
            var selected = monkeys[random.Next(monkeys.Count)];
            randomPickCount++;
            return selected;
        }
    }

    /// <summary>
    /// 무작위 선택된 횟수를 반환합니다.
    /// </summary>
    public static int GetRandomPickCount()
    {
        lock (lockObj)
        {
            return randomPickCount;
        }
    }
}
