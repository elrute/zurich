/*
 * Alberto Blanco 2024 
 * hola@albertoblanco.dev
 * 
 */

using System.Text.Json;
using BusinessLogic.Entities;

namespace Zurich.DataAccess
{    

    public static class DatabaseInitializer
    {
        public static void Seed(ShapesContext dbContext)
        {
            if (dbContext.Points.Any())
            {
                // Assuming if there are any points, the database has already been seeded
                return;
            }
            
            var jsonData = File.ReadAllText("SeedData/objects.json");
            using var doc = JsonDocument.Parse(jsonData);

            foreach (var element in doc.RootElement.EnumerateArray())
            {
                Shape shape = DeserializeShape(element);
                switch (shape)
                {
                    case Point point:
                        dbContext.Points.Add(point);
                        break;
                    case Circle circle:
                        dbContext.Circles.Add(circle);
                        break;
                    case Rectangle rectangle:
                        dbContext.Rectangles.Add(rectangle);
                        break;
                    case Square square:
                        dbContext.Squares.Add(square);
                        break;
                }
            }

            dbContext.SaveChanges();
        }

        public static Shape DeserializeShape(JsonElement element)
        {
            var type = element.GetProperty("type").GetString();
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true, // Optionally make the deserializer case-insensitive
            };

            return type switch
            {
                "Point" => JsonSerializer.Deserialize<Point>(element.GetRawText(), options),
                "Circle" => JsonSerializer.Deserialize<Circle>(element.GetRawText(), options),
                "Rectangle" => JsonSerializer.Deserialize<Rectangle>(element.GetRawText(), options),
                "Square" => JsonSerializer.Deserialize<Square>(element.GetRawText(), options),
                _ => throw new InvalidOperationException($"Unknown type: {type}")
            };
        }
    }

}
