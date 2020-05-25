using SportLeagueASPNetCoreTest.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportLeagueASPNetCoreTest.Models
{
    /// <summary>
    /// Инициализация пустой БД
    /// </summary>
    public class DummyData
    {
        public static void Initialize(ApplicationDbContext db)
        {
            if (!db.Movies.Any())
            {
                db.Movies.Add(new FilmInfo() { Title = "Терминатор", Description = "Фантастика", Director = "Джеймс Кэмерон", Year = 1984, ID = 1, User = "Ivan@fix.com"});
                db.Movies.Add(new FilmInfo() { Title = "Чужие", Description = "Фантастика", Director = "Джеймс Кэмерон", Year = 1986, ID = 2, User = "Ivan@fix.com" });
                db.Movies.Add(new FilmInfo() { Title = "Рэмбо: Первая кровь", Description = "Боевик", Director = "	Тед Котчефф", Year = 1982, ID = 3, User = "Ivan@fix.com" });
                db.Movies.Add(new FilmInfo() { Title = "Крепкий орешек", Description = "Боевик", Director = "Джон МакТирнан", Year = 1988, ID = 4, User = "Ivan@fix.com" });
                db.Movies.Add(new FilmInfo() { Title = "Миссия невыполнима", Description = "Боевик", Director = "Джеймс Кэмерон", Year = 1984, ID = 5, User = "Ivan@fix.com" });
                db.Movies.Add(new FilmInfo() { Title = "Горец", Description = "Боевик", Director = "Рассел Малкэй", Year = 1986, ID = 6, User = "Ivan@fix.com" });
                db.Movies.Add(new FilmInfo() { Title = "Рокки", Description = "Спорт", Director = "Джон Г. Эвилдсен", Year = 1976, ID = 7, User = "Ivan@fix.com" });
                db.Movies.Add(new FilmInfo() { Title = "Доспехи Бога", Description = "Приключения", Director = "Джеки Чан, Эрик Цан", Year = 1986, ID = 8, User = "Ivan@fix.com" });
                db.Movies.Add(new FilmInfo() { Title = "Индиана Джонс: В поисках утраченного ковчега", Description = "Приключения", Director = "Стивен Спилберг", Year = 1981, ID = 9, User = "Ivan@fix.com" });
                db.Movies.Add(new FilmInfo() { Title = "Звёздные войны: Эпизод 4 – Новая надежда", Description = "Фантастика", Director = "Джордж Лукас", Year = 1977, ID = 10, User = "Ivan@fix.com" });
                db.Movies.Add(new FilmInfo() { Title = "Двойной удар", Description = "Боевик", Director = "Шелдон Леттич", Year = 1991, ID = 11, User = "Ivan@fix.com" });
                db.Movies.Add(new FilmInfo() { Title = "Солдат", Description = "Фантастика", Director = "Пол У. С. Андерсон", Year = 1998, ID = 12, User = "Ivan@fix.com" });
                db.Movies.Add(new FilmInfo() { Title = "Гладиатор", Description = "История", Director = "Ридли Скотт", Year = 2000, ID = 13, User = "Ivan@fix.com" });
                db.Movies.Add(new FilmInfo() { Title = "Полицейская академия", Description = "Комедия", Director = "Хью Уилсон", Year = 1984, ID = 14, User = "Ivan@fix.com" });
                db.Movies.Add(new FilmInfo() { Title = "Маска", Description = "Комедия", Director = "Чак Рассел", Year = 1994, ID = 15, User = "Ivan@fix.com" });
                db.Movies.Add(new FilmInfo() { Title = "Побег из Шоушенка", Description = "Драма", Director = "Фрэнк Дарабонт", Year = 1994, ID = 16, User = "Ivan@fix.com" });
                db.Movies.Add(new FilmInfo() { Title = "Зеленая миля", Description = "Драма", Director = "Фрэнк Дарабонт", Year = 1999, ID = 17, User = "Ivan@fix.com" });
                db.Movies.Add(new FilmInfo() { Title = "Форрест Гамп", Description = "Драма", Director = "Роберт Земекис", Year = 1994, ID = 18, User = "Ivan@fix.com" });
                db.Movies.Add(new FilmInfo() { Title = "Леон", Description = "Боевик", Director = "Люк Бессон", Year = 1994, ID = 19, User = "Ivan@fix.com" });
                db.Movies.Add(new FilmInfo() { Title = "Матрица", Description = "Фантастика", Director = "Вачовски, Вачовски", Year = 1999, ID = 20, User = "Ivan@fix.com" });
                db.Movies.Add(new FilmInfo() { Title = "Пятый элемент", Description = "Фантастика", Director = "Люк Бессон", Year = 1997, ID = 21, User = "Ivan@fix.com" });
                db.Movies.Add(new FilmInfo() { Title = "Карты, деньги, два ствола", Description = "Криминал", Director = "Гай Ричин", Year = 1998, ID = 22, User = "Ivan@fix.com" });
                db.Movies.Add(new FilmInfo() { Title = "Криминальное чтиво", Description = "Криминал", Director = "Квентин Тарантино", Year = 1994, ID = 23, User = "Ivan@fix.com" });
                db.Movies.Add(new FilmInfo() { Title = "Назад в будущее", Description = "Фантастика", Director = "Роберт Земекис", Year = 1985, ID = 24, User = "Ivan@fix.com" });
                db.Movies.Add(new FilmInfo() { Title = "Человек дождя", Description = "Драма", Director = "Барри Левинсон", Year = 1988, ID = 25, User = "Ivan@fix.com" });
                db.Movies.Add(new FilmInfo() { Title = "Хищник", Description = "Фантастика", Director = "Джон МакТирнан", Year = 1987, ID = 26, User = "Ivan@fix.com" });
                db.Movies.Add(new FilmInfo() { Title = "Аватар", Description = "Фантастика", Director = "Джеймс Кэмерон", Year = 2009, ID = 27, User = "Ivan@fix.com" });
                db.Movies.Add(new FilmInfo() { Title = "Властелин колец: Возвращение Короля", Description = "Фэнтези", Director = "Питер Джексон", Year = 2003, ID = 28, User = "Ivan@fix.com" });
                db.Movies.Add(new FilmInfo() { Title = "Молчание ягнят", Description = "Триллер", Director = "Джонатан Демме", Year = 1990, ID = 29, User = "Ivan@fix.com" });
                db.Movies.Add(new FilmInfo() { Title = "День сурка", Description = "Комедия", Director = "Джеймс Кэмерон", Year = 1993, ID = 30, User = "Ivan@fix.com" });
                db.Movies.Add(new FilmInfo() { Title = "Охотники за привидениями", Description = "Комедия", Director = "Айвен Райтман", Year = 1984, ID = 31, User = "Ivan@fix.com" });
                db.Movies.Add(new FilmInfo() { Title = "Робокоп", Description = "Фантастика", Director = "Пол Верховен", Year = 1987, ID = 32, User = "Ivan@fix.com" });
                db.Movies.Add(new FilmInfo() { Title = "Храброе сердце", Description = "История", Director = "Мэл Гибсон", Year = 1995, ID = 33, User = "Ivan@fix.com" });
                db.SaveChanges();
            }
        }
    }
}
