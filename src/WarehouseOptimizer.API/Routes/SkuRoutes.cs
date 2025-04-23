using WarehouseOptimizer.Infrastructure;

namespace WarehouseOptimizer.API.Routes;

public static class SkuRoutes
{
    public static IEndpointRouteBuilder MapSkuRoutes(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/sku", async (IRepository<SkuRecord> repository) =>
        {
            var result = await repository.GetAllAsync();
            return Results.Ok(result);
        });

        app.MapGet("/api/sku/{code}", async (string code, IRepository<SkuRecord> repository) =>
        {
            var sku = await repository.FindOneAsync(x => x.SKU == code);
            return sku is not null ? Results.Ok(sku) : Results.NotFound();
        });

        return app;
    }
}