using AlQuran.SDK.Enums;
using AlQuran.SDK.Models;

namespace AlQuran.SDK.Data;

/// <summary>
/// Contains complete metadata for all 114 Surahs of the Holy Quran.
/// </summary>
internal static class SurahMetadata
{
    // Format: number, arabicName, englishName, englishMeaning, ayahCount, revelationType, revelationOrder, rukuCount, juzStart, juzEnd, pageStart, hasBismillah

    internal static readonly Surah[] AllSurahs =
    {
        new(1,   "الفاتحة",     "Al-Fatiha",       "The Opening",                     7,   RevelationType.Meccan,  5,   1,  1,  1,  1,   true),
        new(2,   "البقرة",       "Al-Baqarah",      "The Cow",                         286, RevelationType.Medinan, 87,  40, 1,  3,  2,   true),
        new(3,   "آل عمران",     "Ali 'Imran",      "Family of Imran",                 200, RevelationType.Medinan, 89,  20, 3,  4,  50,  true),
        new(4,   "النساء",       "An-Nisa",         "The Women",                       176, RevelationType.Medinan, 92,  24, 4,  6,  77,  true),
        new(5,   "المائدة",      "Al-Ma'idah",      "The Table Spread",                120, RevelationType.Medinan, 112, 16, 6,  7,  106, true),
        new(6,   "الأنعام",      "Al-An'am",        "The Cattle",                      165, RevelationType.Meccan,  55,  20, 7,  8,  128, true),
        new(7,   "الأعراف",      "Al-A'raf",        "The Heights",                     206, RevelationType.Meccan,  39,  24, 8,  9,  151, true),
        new(8,   "الأنفال",      "Al-Anfal",        "The Spoils of War",               75,  RevelationType.Medinan, 88,  10, 9,  10, 177, true),
        new(9,   "التوبة",       "At-Tawbah",       "The Repentance",                  129, RevelationType.Medinan, 113, 16, 10, 11, 187, false),
        new(10,  "يونس",         "Yunus",           "Jonah",                           109, RevelationType.Meccan,  51,  11, 11, 11, 208, true),
        new(11,  "هود",          "Hud",             "Hud",                             123, RevelationType.Meccan,  52,  10, 11, 12, 221, true),
        new(12,  "يوسف",         "Yusuf",           "Joseph",                          111, RevelationType.Meccan,  53,  12, 12, 13, 235, true),
        new(13,  "الرعد",        "Ar-Ra'd",         "The Thunder",                     43,  RevelationType.Medinan, 96,  6,  13, 13, 249, true),
        new(14,  "إبراهيم",      "Ibrahim",         "Abraham",                         52,  RevelationType.Meccan,  72,  7,  13, 13, 255, true),
        new(15,  "الحجر",        "Al-Hijr",         "The Rocky Tract",                 99,  RevelationType.Meccan,  54,  6,  14, 14, 262, true),
        new(16,  "النحل",        "An-Nahl",         "The Bee",                         128, RevelationType.Meccan,  70,  16, 14, 14, 267, true),
        new(17,  "الإسراء",      "Al-Isra",         "The Night Journey",               111, RevelationType.Meccan,  50,  12, 15, 15, 282, true),
        new(18,  "الكهف",        "Al-Kahf",         "The Cave",                        110, RevelationType.Meccan,  69,  12, 15, 16, 293, true),
        new(19,  "مريم",         "Maryam",          "Mary",                            98,  RevelationType.Meccan,  44,  6,  16, 16, 305, true),
        new(20,  "طه",           "Taha",            "Ta-Ha",                           135, RevelationType.Meccan,  45,  8,  16, 16, 312, true),
        new(21,  "الأنبياء",     "Al-Anbiya",       "The Prophets",                    112, RevelationType.Meccan,  73,  7,  17, 17, 322, true),
        new(22,  "الحج",         "Al-Hajj",         "The Pilgrimage",                  78,  RevelationType.Medinan, 103, 10, 17, 17, 332, true),
        new(23,  "المؤمنون",     "Al-Mu'minun",     "The Believers",                   118, RevelationType.Meccan,  74,  6,  18, 18, 342, true),
        new(24,  "النور",        "An-Nur",          "The Light",                       64,  RevelationType.Medinan, 102, 9,  18, 18, 350, true),
        new(25,  "الفرقان",      "Al-Furqan",       "The Criterion",                   77,  RevelationType.Meccan,  42,  6,  18, 19, 359, true),
        new(26,  "الشعراء",      "Ash-Shu'ara",     "The Poets",                       227, RevelationType.Meccan,  47,  11, 19, 19, 367, true),
        new(27,  "النمل",        "An-Naml",         "The Ant",                         93,  RevelationType.Meccan,  48,  7,  19, 20, 377, true),
        new(28,  "القصص",        "Al-Qasas",        "The Stories",                     88,  RevelationType.Meccan,  49,  9,  20, 20, 385, true),
        new(29,  "العنكبوت",     "Al-'Ankabut",     "The Spider",                      69,  RevelationType.Meccan,  85,  7,  20, 21, 396, true),
        new(30,  "الروم",        "Ar-Rum",          "The Romans",                      60,  RevelationType.Meccan,  84,  6,  21, 21, 404, true),
        new(31,  "لقمان",        "Luqman",          "Luqman",                          34,  RevelationType.Meccan,  57,  4,  21, 21, 411, true),
        new(32,  "السجدة",       "As-Sajdah",       "The Prostration",                 30,  RevelationType.Meccan,  75,  3,  21, 21, 415, true),
        new(33,  "الأحزاب",      "Al-Ahzab",        "The Combined Forces",             73,  RevelationType.Medinan, 90,  9,  21, 22, 418, true),
        new(34,  "سبأ",          "Saba",            "Sheba",                           54,  RevelationType.Meccan,  58,  6,  22, 22, 428, true),
        new(35,  "فاطر",         "Fatir",           "Originator",                      45,  RevelationType.Meccan,  43,  5,  22, 22, 434, true),
        new(36,  "يس",           "Ya-Sin",          "Ya-Sin",                          83,  RevelationType.Meccan,  41,  5,  22, 23, 440, true),
        new(37,  "الصافات",      "As-Saffat",       "Those Who Set the Ranks",         182, RevelationType.Meccan,  56,  5,  23, 23, 446, true),
        new(38,  "ص",            "Sad",             "The Letter Sad",                  88,  RevelationType.Meccan,  38,  5,  23, 23, 453, true),
        new(39,  "الزمر",        "Az-Zumar",        "The Troops",                      75,  RevelationType.Meccan,  59,  8,  23, 24, 458, true),
        new(40,  "غافر",         "Ghafir",          "The Forgiver",                    85,  RevelationType.Meccan,  60,  9,  24, 24, 467, true),
        new(41,  "فصلت",         "Fussilat",        "Explained in Detail",             54,  RevelationType.Meccan,  61,  6,  24, 25, 477, true),
        new(42,  "الشورى",       "Ash-Shura",       "The Consultation",                53,  RevelationType.Meccan,  62,  5,  25, 25, 483, true),
        new(43,  "الزخرف",       "Az-Zukhruf",      "The Ornaments of Gold",           89,  RevelationType.Meccan,  63,  7,  25, 25, 489, true),
        new(44,  "الدخان",       "Ad-Dukhan",       "The Smoke",                       59,  RevelationType.Meccan,  64,  3,  25, 25, 496, true),
        new(45,  "الجاثية",      "Al-Jathiyah",     "The Crouching",                   37,  RevelationType.Meccan,  65,  4,  25, 25, 499, true),
        new(46,  "الأحقاف",      "Al-Ahqaf",        "The Wind-Curved Sandhills",       35,  RevelationType.Meccan,  66,  4,  26, 26, 502, true),
        new(47,  "محمد",          "Muhammad",        "Muhammad",                        38,  RevelationType.Medinan, 95,  4,  26, 26, 507, true),
        new(48,  "الفتح",        "Al-Fath",         "The Victory",                     29,  RevelationType.Medinan, 111, 4,  26, 26, 511, true),
        new(49,  "الحجرات",      "Al-Hujurat",      "The Rooms",                       18,  RevelationType.Medinan, 106, 2,  26, 26, 515, true),
        new(50,  "ق",            "Qaf",             "The Letter Qaf",                  45,  RevelationType.Meccan,  34,  3,  26, 26, 518, true),
        new(51,  "الذاريات",     "Adh-Dhariyat",    "The Winnowing Winds",             60,  RevelationType.Meccan,  67,  3,  26, 27, 520, true),
        new(52,  "الطور",        "At-Tur",          "The Mount",                       49,  RevelationType.Meccan,  76,  2,  27, 27, 523, true),
        new(53,  "النجم",        "An-Najm",         "The Star",                        62,  RevelationType.Meccan,  23,  3,  27, 27, 526, true),
        new(54,  "القمر",        "Al-Qamar",        "The Moon",                        55,  RevelationType.Meccan,  37,  3,  27, 27, 528, true),
        new(55,  "الرحمن",       "Ar-Rahman",       "The Beneficent",                  78,  RevelationType.Medinan, 97,  3,  27, 27, 531, true),
        new(56,  "الواقعة",      "Al-Waqi'ah",      "The Inevitable",                  96,  RevelationType.Meccan,  46,  3,  27, 27, 534, true),
        new(57,  "الحديد",       "Al-Hadid",        "The Iron",                        29,  RevelationType.Medinan, 94,  4,  27, 27, 537, true),
        new(58,  "المجادلة",     "Al-Mujadila",     "The Pleading Woman",              22,  RevelationType.Medinan, 105, 3,  28, 28, 542, true),
        new(59,  "الحشر",        "Al-Hashr",        "The Exile",                       24,  RevelationType.Medinan, 101, 3,  28, 28, 545, true),
        new(60,  "الممتحنة",     "Al-Mumtahanah",   "She That Is to Be Examined",      13,  RevelationType.Medinan, 91,  2,  28, 28, 549, true),
        new(61,  "الصف",         "As-Saff",         "The Ranks",                       14,  RevelationType.Medinan, 109, 2,  28, 28, 551, true),
        new(62,  "الجمعة",       "Al-Jumu'ah",      "The Congregation, Friday",        11,  RevelationType.Medinan, 110, 2,  28, 28, 553, true),
        new(63,  "المنافقون",    "Al-Munafiqun",    "The Hypocrites",                  11,  RevelationType.Medinan, 104, 2,  28, 28, 554, true),
        new(64,  "التغابن",      "At-Taghabun",     "The Mutual Disillusion",          18,  RevelationType.Medinan, 108, 2,  28, 28, 556, true),
        new(65,  "الطلاق",       "At-Talaq",        "The Divorce",                     12,  RevelationType.Medinan, 99,  2,  28, 28, 558, true),
        new(66,  "التحريم",      "At-Tahrim",       "The Prohibition",                 12,  RevelationType.Medinan, 107, 2,  28, 28, 560, true),
        new(67,  "الملك",        "Al-Mulk",         "The Sovereignty",                 30,  RevelationType.Meccan,  77,  2,  29, 29, 562, true),
        new(68,  "القلم",        "Al-Qalam",        "The Pen",                         52,  RevelationType.Meccan,  2,   2,  29, 29, 564, true),
        new(69,  "الحاقة",       "Al-Haqqah",       "The Reality",                     52,  RevelationType.Meccan,  78,  2,  29, 29, 566, true),
        new(70,  "المعارج",      "Al-Ma'arij",      "The Ascending Stairways",         44,  RevelationType.Meccan,  79,  2,  29, 29, 568, true),
        new(71,  "نوح",          "Nuh",             "Noah",                            28,  RevelationType.Meccan,  71,  2,  29, 29, 570, true),
        new(72,  "الجن",         "Al-Jinn",         "The Jinn",                        28,  RevelationType.Meccan,  40,  2,  29, 29, 572, true),
        new(73,  "المزمل",       "Al-Muzzammil",    "The Enshrouded One",              20,  RevelationType.Meccan,  3,   2,  29, 29, 574, true),
        new(74,  "المدثر",       "Al-Muddaththir",  "The Cloaked One",                 56,  RevelationType.Meccan,  4,   2,  29, 29, 575, true),
        new(75,  "القيامة",      "Al-Qiyamah",      "The Resurrection",                40,  RevelationType.Meccan,  31,  2,  29, 29, 577, true),
        new(76,  "الإنسان",      "Al-Insan",        "The Human",                       31,  RevelationType.Medinan, 98,  2,  29, 29, 578, true),
        new(77,  "المرسلات",     "Al-Mursalat",     "The Emissaries",                  50,  RevelationType.Meccan,  33,  2,  29, 29, 580, true),
        new(78,  "النبأ",        "An-Naba",         "The Tidings",                     40,  RevelationType.Meccan,  80,  2,  30, 30, 582, true),
        new(79,  "النازعات",     "An-Nazi'at",      "Those Who Drag Forth",            46,  RevelationType.Meccan,  81,  2,  30, 30, 583, true),
        new(80,  "عبس",          "Abasa",           "He Frowned",                      42,  RevelationType.Meccan,  24,  1,  30, 30, 585, true),
        new(81,  "التكوير",      "At-Takwir",       "The Overthrowing",                29,  RevelationType.Meccan,  7,   1,  30, 30, 586, true),
        new(82,  "الانفطار",     "Al-Infitar",      "The Cleaving",                    19,  RevelationType.Meccan,  82,  1,  30, 30, 587, true),
        new(83,  "المطففين",     "Al-Mutaffifin",   "The Defrauding",                  36,  RevelationType.Meccan,  86,  1,  30, 30, 587, true),
        new(84,  "الانشقاق",     "Al-Inshiqaq",     "The Sundering",                   25,  RevelationType.Meccan,  83,  1,  30, 30, 589, true),
        new(85,  "البروج",       "Al-Buruj",        "The Mansions of the Stars",       22,  RevelationType.Meccan,  27,  1,  30, 30, 590, true),
        new(86,  "الطارق",       "At-Tariq",        "The Morning Star",                17,  RevelationType.Meccan,  36,  1,  30, 30, 591, true),
        new(87,  "الأعلى",       "Al-A'la",         "The Most High",                   19,  RevelationType.Meccan,  8,   1,  30, 30, 591, true),
        new(88,  "الغاشية",      "Al-Ghashiyah",    "The Overwhelming",                26,  RevelationType.Meccan,  68,  1,  30, 30, 592, true),
        new(89,  "الفجر",        "Al-Fajr",         "The Dawn",                        30,  RevelationType.Meccan,  10,  1,  30, 30, 593, true),
        new(90,  "البلد",        "Al-Balad",        "The City",                        20,  RevelationType.Meccan,  35,  1,  30, 30, 594, true),
        new(91,  "الشمس",        "Ash-Shams",       "The Sun",                         15,  RevelationType.Meccan,  26,  1,  30, 30, 595, true),
        new(92,  "الليل",        "Al-Layl",         "The Night",                       21,  RevelationType.Meccan,  9,   1,  30, 30, 595, true),
        new(93,  "الضحى",        "Ad-Duha",         "The Morning Hours",               11,  RevelationType.Meccan,  11,  1,  30, 30, 596, true),
        new(94,  "الشرح",        "Ash-Sharh",       "The Relief",                      8,   RevelationType.Meccan,  12,  1,  30, 30, 596, true),
        new(95,  "التين",        "At-Tin",          "The Fig",                         8,   RevelationType.Meccan,  28,  1,  30, 30, 597, true),
        new(96,  "العلق",        "Al-'Alaq",        "The Clot",                        19,  RevelationType.Meccan,  1,   1,  30, 30, 597, true),
        new(97,  "القدر",        "Al-Qadr",         "The Power",                       5,   RevelationType.Meccan,  25,  1,  30, 30, 598, true),
        new(98,  "البينة",       "Al-Bayyinah",     "The Clear Proof",                 8,   RevelationType.Medinan, 100, 1,  30, 30, 598, true),
        new(99,  "الزلزلة",      "Az-Zalzalah",     "The Earthquake",                  8,   RevelationType.Medinan, 93,  1,  30, 30, 599, true),
        new(100, "العاديات",     "Al-'Adiyat",      "The Coursers",                    11,  RevelationType.Meccan,  14,  1,  30, 30, 599, true),
        new(101, "القارعة",      "Al-Qari'ah",      "The Calamity",                    11,  RevelationType.Meccan,  30,  1,  30, 30, 600, true),
        new(102, "التكاثر",      "At-Takathur",     "The Rivalry in World Increase",   8,   RevelationType.Meccan,  16,  1,  30, 30, 600, true),
        new(103, "العصر",        "Al-'Asr",         "The Declining Day",               3,   RevelationType.Meccan,  13,  1,  30, 30, 601, true),
        new(104, "الهمزة",       "Al-Humazah",      "The Traducer",                    9,   RevelationType.Meccan,  32,  1,  30, 30, 601, true),
        new(105, "الفيل",        "Al-Fil",          "The Elephant",                    5,   RevelationType.Meccan,  19,  1,  30, 30, 601, true),
        new(106, "قريش",         "Quraysh",         "Quraysh",                         4,   RevelationType.Meccan,  29,  1,  30, 30, 602, true),
        new(107, "الماعون",      "Al-Ma'un",        "The Small Kindnesses",            7,   RevelationType.Meccan,  17,  1,  30, 30, 602, true),
        new(108, "الكوثر",       "Al-Kawthar",      "The Abundance",                   3,   RevelationType.Meccan,  15,  1,  30, 30, 602, true),
        new(109, "الكافرون",     "Al-Kafirun",      "The Disbelievers",                6,   RevelationType.Meccan,  18,  1,  30, 30, 603, true),
        new(110, "النصر",        "An-Nasr",         "The Divine Support",              3,   RevelationType.Medinan, 114, 1,  30, 30, 603, true),
        new(111, "المسد",        "Al-Masad",        "The Palm Fiber",                  5,   RevelationType.Meccan,  6,   1,  30, 30, 603, true),
        new(112, "الإخلاص",      "Al-Ikhlas",       "The Sincerity",                   4,   RevelationType.Meccan,  22,  1,  30, 30, 604, true),
        new(113, "الفلق",        "Al-Falaq",        "The Daybreak",                    5,   RevelationType.Meccan,  20,  1,  30, 30, 604, true),
        new(114, "الناس",        "An-Nas",          "Mankind",                         6,   RevelationType.Meccan,  21,  1,  30, 30, 604, true),
    };

    /// <summary>
    /// Total number of Surahs in the Quran.
    /// </summary>
    internal const int TotalSurahs = 114;

    /// <summary>
    /// Total number of Ayahs in the Quran.
    /// </summary>
    internal const int TotalAyahs = 6236;

    /// <summary>
    /// Bismillah text in Uthmani script.
    /// </summary>
    internal const string BismillahUthmani = "بِسْمِ ٱللَّهِ ٱلرَّحْمَٰنِ ٱلرَّحِيمِ";

    /// <summary>
    /// Bismillah text in simple script.
    /// </summary>
    internal const string BismillahSimple = "بسم الله الرحمن الرحيم";
}
