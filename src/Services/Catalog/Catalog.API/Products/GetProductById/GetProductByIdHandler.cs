namespace Catalog.API.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
public record GetProductByIdResult(Product Product);

internal class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByIdQueryHandler.Handle called with {@Request}", request);
        var product = await session.LoadAsync<Product>(request.Id, cancellationToken);

        if (product is null)
        {
            logger.LogWarning("Product with Id {ProductId} not found.", request.Id);
            throw new ProductNotFoundException();
        }

        return new GetProductByIdResult(product);
    }
}