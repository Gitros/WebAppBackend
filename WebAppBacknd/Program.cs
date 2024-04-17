using WebAppBacknd.Dtos;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var app = builder.Build();

        const string GetGameEndpointName = "GetGame";

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
        app.MapGet("games/{id}", (int id) =>
        {
            GameDto? game = games.Find(game => game.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);
        })
        .WithName(GetGameEndpointName);

        // POST /games
        app.MapPost("games", (CreateGameDto newGame) =>
        {
            GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate);

            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });

        // PUT /games
        app.MapPut("games/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex(game => game.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );

            return Results.NoContent();
        });

        // Delete /games/1

        app.MapDelete("games/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });

        app.Run();
    }
}