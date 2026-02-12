using AlQuran.SDK.Enums;

namespace AlQuran.SDK.Tests;

/// <summary>
/// Tests for thread safety and concurrent access.
/// Since Quran class uses Lazy&lt;T&gt; with isThreadSafe=true,
/// concurrent access should be safe.
/// </summary>
public class ConcurrencyTests
{
    [Fact]
    public async Task ConcurrentSurahAccess_Should_BeThreadSafe()
    {
        var exceptions = new List<Exception>();
        var tasks = new Task[100];

        for (int i = 0; i < 100; i++)
        {
            var surahNumber = (i % 114) + 1;
            tasks[i] = Task.Run(() =>
            {
                try
                {
                    var surah = Quran.GetSurah(surahNumber);
                    Assert.NotNull(surah);
                    Assert.Equal(surahNumber, surah.Number);
                }
                catch (Exception ex)
                {
                    lock (exceptions) { exceptions.Add(ex); }
                }
            });
        }

        await Task.WhenAll(tasks);
        Assert.Empty(exceptions);
    }

    [Fact]
    public async Task ConcurrentAyahAccess_Should_BeThreadSafe()
    {
        var exceptions = new List<Exception>();
        var tasks = new Task[50];

        for (int i = 0; i < 50; i++)
        {
            var surahNumber = (i % 10) + 1;
            tasks[i] = Task.Run(() =>
            {
                try
                {
                    var ayahs = Quran.GetAyahs(surahNumber);
                    Assert.NotEmpty(ayahs);
                    Assert.All(ayahs, a => Assert.Equal(surahNumber, a.SurahNumber));
                }
                catch (Exception ex)
                {
                    lock (exceptions) { exceptions.Add(ex); }
                }
            });
        }

        await Task.WhenAll(tasks);
        Assert.Empty(exceptions);
    }

    [Fact]
    public async Task ConcurrentSearch_Should_BeThreadSafe()
    {
        var exceptions = new List<Exception>();
        var tasks = new Task[20];

        for (int i = 0; i < 20; i++)
        {
            var surahNumber = (i % 5) + 1;
            tasks[i] = Task.Run(() =>
            {
                try
                {
                    var results = Quran.Search("الله", surahNumber, ScriptType.Simple);
                    Assert.NotNull(results);
                    Assert.All(results, r => Assert.Equal(surahNumber, r.SurahNumber));
                }
                catch (Exception ex)
                {
                    lock (exceptions) { exceptions.Add(ex); }
                }
            });
        }

        await Task.WhenAll(tasks);
        Assert.Empty(exceptions);
    }

    [Fact]
    public async Task ConcurrentJuzAccess_Should_BeThreadSafe()
    {
        var exceptions = new List<Exception>();
        var tasks = new Task[60];

        for (int i = 0; i < 60; i++)
        {
            var juzNumber = (i % 30) + 1;
            tasks[i] = Task.Run(() =>
            {
                try
                {
                    var juz = Quran.GetJuz(juzNumber);
                    Assert.NotNull(juz);
                    Assert.Equal(juzNumber, juz.Number);
                }
                catch (Exception ex)
                {
                    lock (exceptions) { exceptions.Add(ex); }
                }
            });
        }

        await Task.WhenAll(tasks);
        Assert.Empty(exceptions);
    }

    [Fact]
    public async Task ConcurrentMixedAccess_Should_BeThreadSafe()
    {
        var exceptions = new List<Exception>();
        var tasks = new List<Task>();

        // Concurrent surah lookups
        for (int i = 0; i < 20; i++)
        {
            var n = (i % 114) + 1;
            tasks.Add(Task.Run(() =>
            {
                try { var s = Quran.GetSurah(n); Assert.NotNull(s); }
                catch (Exception ex) { lock (exceptions) { exceptions.Add(ex); } }
            }));
        }

        // Concurrent ayah lookups
        for (int i = 0; i < 20; i++)
        {
            var n = (i % 10) + 1;
            tasks.Add(Task.Run(() =>
            {
                try { var a = Quran.GetAyahs(n); Assert.NotEmpty(a); }
                catch (Exception ex) { lock (exceptions) { exceptions.Add(ex); } }
            }));
        }

        // Concurrent juz lookups
        for (int i = 0; i < 20; i++)
        {
            var n = (i % 30) + 1;
            tasks.Add(Task.Run(() =>
            {
                try { var j = Quran.GetJuz(n); Assert.NotNull(j); }
                catch (Exception ex) { lock (exceptions) { exceptions.Add(ex); } }
            }));
        }

        // Concurrent sajda lookups
        for (int i = 0; i < 10; i++)
        {
            tasks.Add(Task.Run(() =>
            {
                try { var s = Quran.GetAllSajdas(); Assert.Equal(15, s.Count); }
                catch (Exception ex) { lock (exceptions) { exceptions.Add(ex); } }
            }));
        }

        await Task.WhenAll(tasks);
        Assert.Empty(exceptions);
    }

    [Fact]
    public async Task ConcurrentGetAllSurahs_Should_ReturnIndependentLists()
    {
        var lists = new List<List<AlQuran.SDK.Models.Surah>>();
        var tasks = new Task[10];

        for (int i = 0; i < 10; i++)
        {
            tasks[i] = Task.Run(() =>
            {
                var list = Quran.GetAllSurahs();
                lock (lists) { lists.Add(list); }
            });
        }

        await Task.WhenAll(tasks);

        // Each call should return a new list instance
        for (int i = 0; i < lists.Count; i++)
        {
            for (int j = i + 1; j < lists.Count; j++)
            {
                Assert.NotSame(lists[i], lists[j]);
            }
        }

        // But all lists should have the same content
        Assert.All(lists, l => Assert.Equal(114, l.Count));
    }
}
