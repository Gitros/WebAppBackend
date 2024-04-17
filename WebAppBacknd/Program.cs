using WebAppBacknd.Dtos;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

List<GameDto> games = [
    new (
        1,
        "Cyberpunk 2077",
        "Fighting",
        19.99M,
        new DateOnly(2019, 7, 15)),
    new (
        2,
        "The Binding of Isaac",
        "Roguelike",
        10.99M,
        new DateOnly(2015, 5, 21)),
    new(
        3,
        "League of Legends",
        "Battle arena",
        105.20M,
        new DateOnly(2009, 2, 12)),
];

// GET /games
app.MapGet("games", () => games);

// GET /games/1
app.MapGet("games/{id}", (int id) => games.Find(game => game.Id == id));

app.Run();
